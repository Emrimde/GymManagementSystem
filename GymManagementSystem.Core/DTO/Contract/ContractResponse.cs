using GymManagementSystem.Core.DTO.ClientMembership;
using GymManagementSystem.Core.Enum;

namespace GymManagementSystem.Core.DTO.Contract;

public class ContractResponse
{
    public Guid Id { get; set; } 
    public ClientMembershipResponse? ClientMembership { get; set; }
    public string? FirstName { get; set; } = default!;
    public string? LastName { get; set; } = default!;
    public string CreatedAt { get; set; } = default!;
    public ContractStatus ContractStatus { get; set; }
}
