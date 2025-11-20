namespace GymManagementSystem.Core.Domain.Entities;

public class Termination
{
    public Guid Id { get; set; }
    public Guid ContractId { get; set; }
    public Contract? Contract { get; set; }
    public Guid ClientId { get; set; }
    public Client? Client { get; set; }
    public string? Reason { get; set; }
    public DateTime RequestedAt { get; set; }
    public string? IsSigned { get; set; }
}
