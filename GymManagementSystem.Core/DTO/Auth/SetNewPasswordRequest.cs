namespace GymManagementSystem.Core.DTO.Auth;
public class SetNewPasswordRequest
{
    public Guid PersonId { get; set; }
    public string NewPassword { get; set; } = default!;
}
