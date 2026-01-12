namespace GymManagementSystem.Core.DTO.GeneralGymDetail;

public class GeneralGymUpdateRequest
{
    public Guid Id { get; set; }
    public string GymName { get; set; } = default!;
    public string Address { get; set; } = default!;
    public string ContactNumber { get; set; } = default!;
    public string PrimaryColor { get; set; } = default!;
    public string BackgroundColor { get; set; } = default!;
    public string SecondColor { get; set; } = default!;
    public decimal DefaultRate60 { get; set; }
    public decimal DefaultRate90 { get; set; }
    public decimal DefaultRate120 { get; set; }
    public string AboutUs { get; set; } = default!;
    public string? LogoUrl { get; set; }
}
