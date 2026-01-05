using GymManagementSystem.Core.DTO.Client;
using GymManagementSystem.Core.DTO.Membership;

namespace GymManagementSystem.Core.DTO.ClientMembership;

public class ClientMembershipDetailsResponse
{
    public Guid Id { get; set; }
    public ClientResponse? Client { get; set; }
    public string? ClientFullName { get; set; } = default!;
    public string? MembershipName { get; set; } = default!;
    public string IsActive { get; set; } = default!;
    public string StartDate { get; set; } = default!;
    public string EndDate { get; set; } = default!;
    public string SignedContractDate { get; set; } = default!;
    public bool WasTerminated { get; set; } //flaga do visibility
    public string? TerminationReason { get; set; } = default!;
    public string? TerminationDate {  get; set; } = default!;
    
}
