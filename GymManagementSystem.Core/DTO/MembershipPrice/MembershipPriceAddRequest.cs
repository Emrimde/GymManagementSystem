namespace GymManagementSystem.Core.DTO.MembershipPrice;
public class MembershipPriceAddRequest
{
    public Guid MembershipId { get; set; }
    public string? LabelPrice { get; set; }
    public decimal Price { get; set; }
}
