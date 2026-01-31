namespace GymManagementSystem.Core.DTO.MembershipPrice;

public class MembershipPriceResponse
{
    public string Price { get; set; } = default!;
    public string ValidFromLabel { get; set; } = default!; 
    public string ValidToLabel { get; set; } = default!;
    public string LabelPrice { get; set; } = default!;
}
