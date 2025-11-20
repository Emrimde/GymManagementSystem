using GymManagementSystem.Core.Enum;

namespace GymManagementSystem.Core.Domain.Entities;

public class Contract
{
    public Guid Id { get; set; }
    public Guid ClientMembershipId { get; set; }
    public ClientMembership? ClientMembership { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? IsSigned { get; set; }
    public ContractStatus ContractStatus { get; set; }
    public bool IsActive { get; set; }
}
