namespace GymManagementSystem.Core.DTO;

public class VisitAddRequest
{
    public Guid? ClientMembershipId { get; set; }
    public bool IsActive { get; set; }
}