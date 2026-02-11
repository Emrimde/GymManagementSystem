namespace GymManagementSystem.Core.DTO.TrainerRate;
public class TrainerRateForPersonalBookingAddResponse
{
    public Guid TrainerRateId { get; set; }
    public decimal RatePerSessions { get; set; }
    public int DurationInMinutes { get; set; }
}
