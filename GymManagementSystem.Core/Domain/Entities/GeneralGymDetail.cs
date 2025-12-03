namespace GymManagementSystem.Core.Domain.Entities;

public class GeneralGymDetail
{
    public Guid Id { get; set; }
    public string GymName { get; set; } = default!;
    public string Address { get; set; } = default!;
    public string ContactNumber { get; set; } = default!;
    public string PrimaryColor { get; set; } = default!;
    public string BackgroundColor { get; set; } = default!;
    public string SecondColor { get; set; } = default!;
    public TimeSpan OpenTime { get; set; }
    public TimeSpan CloseTime { get; set; }

    // Global cennik
    public decimal DefaultRate60 { get; set; }
    public decimal DefaultRate90 { get; set; }
    public decimal DefaultRate120 { get; set; }
    public decimal DefaultGroupClassRate { get; set; }
}
