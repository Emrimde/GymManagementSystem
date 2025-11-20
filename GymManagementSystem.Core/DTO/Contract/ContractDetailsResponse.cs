using GymManagementSystem.Core.DTO.Client;
using GymManagementSystem.Core.Enum;

namespace GymManagementSystem.Core.DTO.Contract;

public class ContractDetailsResponse
{
    public string StartDate { get; set; } = default!;
    public string? EndDate { get; set; }
    public ClientDetailsResponse? Client { get; set; }
    public string Name { get; set; } = string.Empty;
    public MembershipTypeEnum MembershipType { get; set; }
    public decimal Price { get; set; }
    public ContractStatus ContractStatus { get; set; }
}
