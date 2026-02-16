using GymManagementSystem.Core.Domain.Identity;
using GymManagementSystem.Core.DTO.Auth;
using System.Security.Claims;

namespace GymManagementSystem.Core.ServiceContracts;
public interface IJwtService
{
    Task<AuthenticationResponse> CreateJwtToken(User user);
}
