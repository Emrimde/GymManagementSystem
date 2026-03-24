using GymManagementSystem.Core.Domain.Identity;
using GymManagementSystem.Core.DTO.Auth;

namespace GymManagementSystem.Core.ServiceContracts;
public interface IJwtService
{
    Task<AuthenticationResponse> CreateJwtToken(User user);
}
