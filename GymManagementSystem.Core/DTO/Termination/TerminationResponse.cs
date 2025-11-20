namespace GymManagementSystem.Core.DTO.Termination;

public class TerminationResponse
{
    public Guid Id { get; set; }
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public string? Reason { get; set; }
    public string RequestedAt { get; set; } = default!;
}
