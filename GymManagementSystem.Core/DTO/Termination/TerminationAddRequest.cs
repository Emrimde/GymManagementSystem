namespace GymManagementSystem.Core.DTO.Termination;

public class TerminationAddRequest
{
    public Guid ClientId { get; set; }
    public string? Reason { get; set; }
}
