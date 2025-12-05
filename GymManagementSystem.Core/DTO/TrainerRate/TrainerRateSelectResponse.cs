namespace GymManagementSystem.Core.DTO.TrainerRate;
public class TrainerRateSelectResponse
{
    public Guid TrainerRateId { get; set; }
    public string DisplayPriceDuration { get; set; } = default!;
    public int? DurationInMinutes { get; set; }
    public decimal Price { get; set; }
}
