using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace GymManagementSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Clients",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FirstName = table.Column<string>(type: "text", nullable: false),
                    LastName = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    PhoneNumber = table.Column<string>(type: "text", nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    StreetAddress = table.Column<string>(type: "text", nullable: false),
                    City = table.Column<string>(type: "text", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    HasParentalConsent = table.Column<bool>(type: "boolean", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GeneralGymDetails",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    GymName = table.Column<string>(type: "text", nullable: false),
                    Address = table.Column<string>(type: "text", nullable: false),
                    ContactNumber = table.Column<string>(type: "text", nullable: false),
                    Nip = table.Column<string>(type: "text", nullable: false),
                    PrimaryColor = table.Column<string>(type: "text", nullable: false),
                    BackgroundColor = table.Column<string>(type: "text", nullable: false),
                    SecondColor = table.Column<string>(type: "text", nullable: false),
                    OpenTime = table.Column<TimeSpan>(type: "interval", nullable: false),
                    CloseTime = table.Column<TimeSpan>(type: "interval", nullable: false),
                    DefaultRate60 = table.Column<decimal>(type: "numeric", nullable: false),
                    DefaultRate90 = table.Column<decimal>(type: "numeric", nullable: false),
                    DefaultRate120 = table.Column<decimal>(type: "numeric", nullable: false),
                    DefaultGroupClassRate = table.Column<decimal>(type: "numeric", nullable: false),
                    LogoUrl = table.Column<string>(type: "text", nullable: false),
                    AboutUs = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GeneralGymDetails", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Memberships",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    ClassBookingDaysInAdvanceCount = table.Column<int>(type: "integer", nullable: false),
                    FreeFriendEntryCountPerMonth = table.Column<int>(type: "integer", nullable: false),
                    FreePersonalTrainingSessions = table.Column<int>(type: "integer", nullable: false),
                    MembershipType = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Memberships", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "People",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FirstName = table.Column<string>(type: "text", nullable: false),
                    LastName = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    PhoneNumber = table.Column<string>(type: "text", nullable: false),
                    Street = table.Column<string>(type: "text", nullable: false),
                    City = table.Column<string>(type: "text", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    IdentityUserId = table.Column<Guid>(type: "uuid", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_People", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TrainerProfiles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    EmployeeId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrainerProfiles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RoleId = table.Column<Guid>(type: "uuid", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ClientId = table.Column<Guid>(type: "uuid", nullable: true),
                    UserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: true),
                    SecurityStamp = table.Column<string>(type: "text", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true),
                    PhoneNumber = table.Column<string>(type: "text", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Visits",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ClientId = table.Column<Guid>(type: "uuid", nullable: false),
                    ClientMembershipId = table.Column<Guid>(type: "uuid", nullable: true),
                    VisitSource = table.Column<int>(type: "integer", nullable: false),
                    VisitDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Visits", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Visits_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClientMemberships",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ClientId = table.Column<Guid>(type: "uuid", nullable: false),
                    MembershipId = table.Column<Guid>(type: "uuid", nullable: false),
                    StartDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    EndDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    MembershipStatus = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientMemberships", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClientMemberships_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClientMemberships_Memberships_MembershipId",
                        column: x => x.MembershipId,
                        principalTable: "Memberships",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MembershipFeatures",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    MembershipId = table.Column<Guid>(type: "uuid", nullable: false),
                    FeatureDescription = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MembershipFeatures", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MembershipFeatures_Memberships_MembershipId",
                        column: x => x.MembershipId,
                        principalTable: "Memberships",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MembershipPrices",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    MembershipId = table.Column<Guid>(type: "uuid", nullable: false),
                    Price = table.Column<decimal>(type: "numeric", nullable: false),
                    ValidFrom = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ValidTo = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    LabelPrice = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MembershipPrices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MembershipPrices_Memberships_MembershipId",
                        column: x => x.MembershipId,
                        principalTable: "Memberships",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    PersonId = table.Column<Guid>(type: "uuid", nullable: false),
                    MonthlySalaryBrutto = table.Column<decimal>(type: "numeric", nullable: false),
                    ValidFrom = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ValidTo = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Role = table.Column<int>(type: "integer", nullable: false),
                    EmploymentType = table.Column<int>(type: "integer", nullable: false),
                    ContractTypeEnum = table.Column<int>(type: "integer", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Employees_People_PersonId",
                        column: x => x.PersonId,
                        principalTable: "People",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EmploymentTerminations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    PersonId = table.Column<Guid>(type: "uuid", nullable: false),
                    EffectiveDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    RequestedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmploymentTerminations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmploymentTerminations_People_PersonId",
                        column: x => x.PersonId,
                        principalTable: "People",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TrainerContracts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    PersonId = table.Column<Guid>(type: "uuid", nullable: false),
                    ContractType = table.Column<int>(type: "integer", nullable: false),
                    TrainerType = table.Column<int>(type: "integer", nullable: false),
                    ClubCommissionPercent = table.Column<decimal>(type: "numeric", nullable: false),
                    ValidFrom = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ValidTo = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsSigned = table.Column<bool>(type: "boolean", nullable: false),
                    TrainerProfileId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrainerContracts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TrainerContracts_People_PersonId",
                        column: x => x.PersonId,
                        principalTable: "People",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TrainerContracts_TrainerProfiles_TrainerProfileId",
                        column: x => x.TrainerProfileId,
                        principalTable: "TrainerProfiles",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "text", nullable: false),
                    ProviderKey = table.Column<string>(type: "text", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "text", nullable: true),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    RoleId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    LoginProvider = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Value = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Contracts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ClientMembershipId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsSigned = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    ContractStatus = table.Column<int>(type: "integer", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contracts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Contracts_ClientMemberships_ClientMembershipId",
                        column: x => x.ClientMembershipId,
                        principalTable: "ClientMemberships",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Terminations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ClientMembershipId = table.Column<Guid>(type: "uuid", nullable: false),
                    Reason = table.Column<string>(type: "text", nullable: true),
                    RequestedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Terminations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Terminations_ClientMemberships_ClientMembershipId",
                        column: x => x.ClientMembershipId,
                        principalTable: "ClientMemberships",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GymClasses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    TrainerContractId = table.Column<Guid>(type: "uuid", nullable: false),
                    DaysOfWeek = table.Column<int>(type: "integer", nullable: false),
                    StartHour = table.Column<TimeSpan>(type: "interval", nullable: false),
                    Duration = table.Column<TimeSpan>(type: "interval", nullable: false),
                    MaxPeople = table.Column<int>(type: "integer", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GymClasses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GymClasses_TrainerContracts_TrainerContractId",
                        column: x => x.TrainerContractId,
                        principalTable: "TrainerContracts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PersonalBookings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TrainerContractId = table.Column<Guid>(type: "uuid", nullable: false),
                    ClientId = table.Column<Guid>(type: "uuid", nullable: false),
                    Start = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    End = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    Price = table.Column<decimal>(type: "numeric", nullable: false),
                    TrainerProfileId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonalBookings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PersonalBookings_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PersonalBookings_TrainerContracts_TrainerContractId",
                        column: x => x.TrainerContractId,
                        principalTable: "TrainerContracts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PersonalBookings_TrainerProfiles_TrainerProfileId",
                        column: x => x.TrainerProfileId,
                        principalTable: "TrainerProfiles",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TrainerRates",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TrainerContractId = table.Column<Guid>(type: "uuid", nullable: false),
                    DurationInMinutes = table.Column<int>(type: "integer", nullable: false),
                    RatePerSessions = table.Column<decimal>(type: "numeric", nullable: false),
                    ValidFrom = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ValidTo = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrainerRates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TrainerRates_TrainerContracts_TrainerContractId",
                        column: x => x.TrainerContractId,
                        principalTable: "TrainerContracts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TrainerTimeOff",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TrainerId = table.Column<Guid>(type: "uuid", nullable: false),
                    Start = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    End = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Reason = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrainerTimeOff", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TrainerTimeOff_TrainerContracts_TrainerId",
                        column: x => x.TrainerId,
                        principalTable: "TrainerContracts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ScheduledClasses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    GymClassId = table.Column<Guid>(type: "uuid", nullable: false),
                    Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    StartFrom = table.Column<TimeSpan>(type: "interval", nullable: false),
                    StartTo = table.Column<TimeSpan>(type: "interval", nullable: false),
                    MaxPeople = table.Column<int>(type: "integer", nullable: false),
                    IsCancelled = table.Column<bool>(type: "boolean", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScheduledClasses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ScheduledClasses_GymClasses_GymClassId",
                        column: x => x.GymClassId,
                        principalTable: "GymClasses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClassBookings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ScheduledClassId = table.Column<Guid>(type: "uuid", nullable: false),
                    ClientId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CancelledAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClassBookings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClassBookings_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClassBookings_ScheduledClasses_ScheduledClassId",
                        column: x => x.ScheduledClassId,
                        principalTable: "ScheduledClasses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "GeneralGymDetails",
                columns: new[] { "Id", "AboutUs", "Address", "BackgroundColor", "CloseTime", "ContactNumber", "DefaultGroupClassRate", "DefaultRate120", "DefaultRate60", "DefaultRate90", "GymName", "LogoUrl", "Nip", "OpenTime", "PrimaryColor", "SecondColor" },
                values: new object[] { new Guid("65893784-da50-4c63-ae01-9704c5d5eda3"), "We are a place created for people who want to truly improve their health, physique, and well-being — not just “tick off” a workout. Our goal is to build a strong, capable, and mindful community where everyone, regardless of their level, feels welcome. We combine modern equipment with expert coaching to make training not only hard, but smart. We focus on quality of movement, steady progress, and safety, because long-term results matter more than quick fixes. We help our members set clear goals and achieve them step by step.\r\n\r\nWe don’t believe in shortcuts — we believe in building lasting habits and real lifestyle change. We create an environment where training becomes part of everyday life, not a burden. We believe that a strong body builds a strong mind. That’s why we support, motivate, and educate — not just count reps. Our gym is more than equipment; it’s people, atmosphere, and a shared drive to be better than yesterday.", "123 Fitness St, Muscle City", "#363740", new TimeSpan(0, 22, 0, 0, 0), "123456789", 60m, 150m, 100m, 120m, "NextLevelGym", "http://localhost:5105/uploads/logos/logo_d8faf809-8917-4ddd-b78e-618df23cf5c8.png", "123456789", new TimeSpan(0, 7, 0, 0, 0), "#EEEEEE", "#9AAD00" });

            migrationBuilder.InsertData(
                table: "Memberships",
                columns: new[] { "Id", "ClassBookingDaysInAdvanceCount", "FreeFriendEntryCountPerMonth", "FreePersonalTrainingSessions", "MembershipType", "Name" },
                values: new object[,]
                {
                    { new Guid("18ec8725-c23b-4ea4-90d4-2952e3b110a0"), 7, 3, 1, 0, "Silver Membership" },
                    { new Guid("62dd1607-fd54-4186-b282-8ef9d82cddcf"), 7, 3, 1, 1, "Silver Membership" },
                    { new Guid("bedd6962-6fa4-435d-8505-b7c6092b9875"), 14, 6, 2, 0, "Gold Membership" },
                    { new Guid("db4a0dc9-6d66-445f-8ae1-e5b941e873cf"), 14, 6, 2, 1, "Gold Membership" }
                });

            migrationBuilder.InsertData(
                table: "MembershipPrices",
                columns: new[] { "Id", "LabelPrice", "MembershipId", "Price", "ValidFrom", "ValidTo" },
                values: new object[,]
                {
                    { new Guid("0d29e7b7-a940-4187-8a74-76ba99af8b1f"), null, new Guid("18ec8725-c23b-4ea4-90d4-2952e3b110a0"), 100m, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null },
                    { new Guid("1260207e-c0e3-4503-bb31-a5ef13e5cee8"), null, new Guid("bedd6962-6fa4-435d-8505-b7c6092b9875"), 150m, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null },
                    { new Guid("6707ecbd-f2c1-49ee-ae24-8ce497a4f501"), null, new Guid("db4a0dc9-6d66-445f-8ae1-e5b941e873cf"), 1500m, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null },
                    { new Guid("f23a5317-8f53-4fc2-bfba-661728098038"), null, new Guid("62dd1607-fd54-4186-b282-8ef9d82cddcf"), 1000m, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_ClientId",
                table: "AspNetUsers",
                column: "ClientId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ClassBookings_ClientId",
                table: "ClassBookings",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_ClassBookings_ScheduledClassId",
                table: "ClassBookings",
                column: "ScheduledClassId");

            migrationBuilder.CreateIndex(
                name: "IX_ClientMemberships_ClientId",
                table: "ClientMemberships",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_ClientMemberships_MembershipId",
                table: "ClientMemberships",
                column: "MembershipId");

            migrationBuilder.CreateIndex(
                name: "IX_Contracts_ClientMembershipId",
                table: "Contracts",
                column: "ClientMembershipId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Employees_PersonId",
                table: "Employees",
                column: "PersonId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EmploymentTerminations_PersonId",
                table: "EmploymentTerminations",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_GymClasses_TrainerContractId",
                table: "GymClasses",
                column: "TrainerContractId");

            migrationBuilder.CreateIndex(
                name: "IX_MembershipFeatures_MembershipId",
                table: "MembershipFeatures",
                column: "MembershipId");

            migrationBuilder.CreateIndex(
                name: "IX_MembershipPrices_MembershipId",
                table: "MembershipPrices",
                column: "MembershipId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonalBookings_ClientId",
                table: "PersonalBookings",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonalBookings_TrainerContractId",
                table: "PersonalBookings",
                column: "TrainerContractId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonalBookings_TrainerProfileId",
                table: "PersonalBookings",
                column: "TrainerProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_ScheduledClasses_GymClassId",
                table: "ScheduledClasses",
                column: "GymClassId");

            migrationBuilder.CreateIndex(
                name: "IX_Terminations_ClientMembershipId",
                table: "Terminations",
                column: "ClientMembershipId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TrainerContracts_PersonId",
                table: "TrainerContracts",
                column: "PersonId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TrainerContracts_TrainerProfileId",
                table: "TrainerContracts",
                column: "TrainerProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_TrainerRates_TrainerContractId",
                table: "TrainerRates",
                column: "TrainerContractId");

            migrationBuilder.CreateIndex(
                name: "IX_TrainerTimeOff_TrainerId",
                table: "TrainerTimeOff",
                column: "TrainerId");

            migrationBuilder.CreateIndex(
                name: "IX_Visits_ClientId",
                table: "Visits",
                column: "ClientId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "ClassBookings");

            migrationBuilder.DropTable(
                name: "Contracts");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "EmploymentTerminations");

            migrationBuilder.DropTable(
                name: "GeneralGymDetails");

            migrationBuilder.DropTable(
                name: "MembershipFeatures");

            migrationBuilder.DropTable(
                name: "MembershipPrices");

            migrationBuilder.DropTable(
                name: "PersonalBookings");

            migrationBuilder.DropTable(
                name: "Terminations");

            migrationBuilder.DropTable(
                name: "TrainerRates");

            migrationBuilder.DropTable(
                name: "TrainerTimeOff");

            migrationBuilder.DropTable(
                name: "Visits");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "ScheduledClasses");

            migrationBuilder.DropTable(
                name: "ClientMemberships");

            migrationBuilder.DropTable(
                name: "GymClasses");

            migrationBuilder.DropTable(
                name: "Clients");

            migrationBuilder.DropTable(
                name: "Memberships");

            migrationBuilder.DropTable(
                name: "TrainerContracts");

            migrationBuilder.DropTable(
                name: "People");

            migrationBuilder.DropTable(
                name: "TrainerProfiles");
        }
    }
}
