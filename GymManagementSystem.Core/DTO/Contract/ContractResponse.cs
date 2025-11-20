using GymManagementSystem.Core.DTO.ClientMembership;
using GymManagementSystem.Core.Enum;


namespace GymManagementSystem.Core.DTO.Contract;

public class ContractResponse
{
    public Guid Id { get; set; } 
    public ClientMembershipResponse? ClientMembership { get; set; }
    public string CreatedAt { get; set; } = default!;
    public ContractStatus ContractStatus { get; set; }
}
