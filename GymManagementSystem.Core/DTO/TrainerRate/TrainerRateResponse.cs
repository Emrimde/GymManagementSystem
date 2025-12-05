namespace GymManagementSystem.Core.DTO.TrainerRate;
public class TrainerRateResponse
{
    public Guid Id { get; set; }
    public int DurationInMinutes { get; set; }
    public decimal RatePerSessions { get; set; }
    public DateTime ValidFrom { get; set; }
    public DateTime? ValidTo { get; set; }
}
