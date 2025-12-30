using GymManagementSystem.Core.Domain.Identity;
using GymManagementSystem.Core.DTO.Auth;
using GymManagementSystem.Core.ServiceContracts;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementSystem.Core.Services;

public class JwtService : IJwtService
{
    private readonly IConfiguration _configuration;
    private readonly UserManager<User> _userManager;
    public JwtService(IConfiguration configuration, UserManager<User> userManager)
    {
        _configuration = configuration;
        _userManager = userManager;
    }
    public async Task<AuthenticationResponse> CreateJwtToken(User user)
    {
        var roles = await _userManager.GetRolesAsync(user);
        DateTime expirationDate = DateTime.UtcNow.AddMinutes(Convert.ToDouble(_configuration["Jwt:Expiration_Minutes"]));
        List<Claim> claims = new List<Claim> {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString() ),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
           new Claim(
    JwtRegisteredClaimNames.Iat,
    DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString(),
    ClaimValueTypes.Integer64),
       
    };
        foreach (var role in roles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        }



        SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
        SigningCredentials signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);


        JwtSecurityToken tokenGenerator = new JwtSecurityToken(_configuration["Jwt:Issuer"], _configuration["Jwt:Audience"],claims, expires: expirationDate, signingCredentials: signingCredentials);

        JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
        string token = tokenHandler.WriteToken(tokenGenerator);

        return new AuthenticationResponse()
        {
            Token = token,
            ExpirationTime = expirationDate,
        };
    }
}
