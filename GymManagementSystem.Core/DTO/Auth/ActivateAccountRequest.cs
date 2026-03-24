namespace GymManagementSystem.Core.DTO.Auth;
public record ActivateAccountRequest(
    string UserId,
    string Token,
    string NewPassword
);
