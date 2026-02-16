namespace GymManagementSystem.Core.DTO.GeneralGymDetail;

public class GeneralGymUpdateRequest
{
    public Guid Id { get; set; }
    public string GymName { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string ContactNumber { get; set; } = string.Empty;
    public string AboutUs { get; set; } = string.Empty;
    public string LogoUrl { get; set; } = string.Empty;
    public decimal DefaultRate60 { get; set; }
    public decimal DefaultRate90 { get; set; }
    public decimal DefaultRate120 { get; set; }
}
