namespace GymManagementSystem.Core.WebDTO.Membership;
public class MembershipWebDetailsResponse
{
    public Guid Id { get; set; }
    public string MembershipName { get; set; } = default!;
    public List<string> MembershipFeatures { get; set; } = new List<string>();
    public decimal Price { get; set; }
    public bool IsMonthly { get; set; } 
}
