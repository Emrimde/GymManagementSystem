using GymManagementSystem.Core.DTO.Auth;
using GymManagementSystem.Core.DTO.Email;

namespace GymManagementSystem.Core.ServiceContracts;
public interface IEmailService
{
    Task SendLink(EmailRequest request);
}
