namespace GymManagementSystem.Core.DTO.MembershipPrice;

public class MembershipPriceResponse
{
    public decimal Price { get; set; }
    public DateTime ValidFrom { get; set; }
    public DateTime? ValidTo { get; set; }
    public string? LabelPrice { get; set; }
}
