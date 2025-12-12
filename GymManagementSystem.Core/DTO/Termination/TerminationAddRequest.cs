namespace GymManagementSystem.Core.DTO.Termination;

public class TerminationAddRequest
{
    //public Guid ContractId { get; set; }
    //public Guid ClientId { get; set; }
    public Guid ClientMembershipId { get; set; }
    public string? Reason { get; set; }
    //public DateTime? RequestedAt { get; set; }
}
