using FluentValidation.AspNetCore;
using GymManagementSystem.API.Jobs;
using GymManagementSystem.Core;
using GymManagementSystem.Core.Domain.Identity;
using GymManagementSystem.Infrastructure;
using GymManagementSystem.Infrastructure.DatabaseContext;
using Hangfire;
using Hangfire.PostgreSql;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHealthChecks();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

builder.Services.AddHangfire(config =>
{
    config
        .SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
        .UseSimpleAssemblyNameTypeSerializer()
        .UseRecommendedSerializerSettings()
        .UsePostgreSqlStorage(
            options =>
            {
                options.UseNpgsqlConnection(
                    builder.Configuration.GetConnectionString("ConnectionString"));
            },
            new PostgreSqlStorageOptions
            {
                SchemaName = "hangfire"
            });
});

builder.Services.AddHangfireServer();


builder.Services.AddFluentValidationAutoValidation();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration
           .GetConnectionString("ConnectionString"));
});
builder.Services.AddIdentity<User, Role>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredLength = 3;
    options.User.AllowedUserNameCharacters = null!;

})
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();


var jwtKey = builder.Configuration["Jwt:Key"];

if (string.IsNullOrWhiteSpace(jwtKey))
    throw new InvalidOperationException("JWT Key is not configured.");

builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,

            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(jwtKey)
            ),

            RoleClaimType = ClaimTypes.Role
        };
    });


builder.Services.AddAuthorization();
builder.Services.AddInfrastructureServices();
builder.Services.AddCoreServices();


var app = builder.Build();
// apply job
using (var scope = app.Services.CreateScope())
{
    var recurringJobs = scope.ServiceProvider
        .GetRequiredService<IRecurringJobManager>();

    recurringJobs.AddOrUpdate<DeactivateExpiredPersonsJob>(
        "deactivate-expired-persons",
        job => job.Run(),
        Cron.Daily()
    );
    recurringJobs.AddOrUpdate<GenerateNewScheduledClassesJob>(
        "generate-new-scheduled-classes",
        job => job.Run(),
        Cron.Daily()
    );
    recurringJobs.AddOrUpdate<DeactivateTerminatedClientMembershipsJob>(
        "deactivate-terminated-client-memberships",
        job => job.Run(),
        Cron.Daily()
    );
}


// Create roles and main starter account
using (var scope = app.Services.CreateScope())
{
    await IdentitySeeder.SeedAsync(scope.ServiceProvider);
}

app.UseHangfireDashboard();
app.UseRouting();
app.UseStaticFiles();
app.UseCors("AllowAll");

app.UseAuthentication();

app.UseAuthorization();

app.Use(async (ctx, next) =>
{
    await next();

    if (ctx.Response.StatusCode == 403)
    {
        ctx.Response.ContentType = "application/problem+json";
        await ctx.Response.WriteAsJsonAsync(new ProblemDetails
        {
            Status = 403,
            Title = "Forbidden",
            Detail = "RequiredRole:Manager,Owner"
        });
    }
});


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.MapControllers();
app.MapHealthChecks("/health");

app.Run();
