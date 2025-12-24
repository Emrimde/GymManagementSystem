namespace GymManagementSystem.Core.DTO.ClientMembership;
public class ClientMembershipContractPreviewResponse
{
    public string FullName { get; set; } = default!;
    public string MembershipName { get; set; } = default!;
    public string StartDate { get; set; } = default!;
    public string? EndDate { get; set; } = default!;
    public string Price { get; set; } = default!;
}
