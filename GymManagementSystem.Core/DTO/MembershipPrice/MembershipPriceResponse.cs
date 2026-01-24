namespace GymManagementSystem.Core.DTO.MembershipPrice;

public class MembershipPriceResponse
{
    public decimal Price { get; set; }
    public string ValidFromLabel { get; set; } = default!; 
    public string ValidToLabel { get; set; } = default!;
    public string LabelPrice { get; set; } = default!;
}
