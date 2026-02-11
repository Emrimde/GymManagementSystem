namespace GymManagementSystem.Core.DTO.TrainerRate;
public class TrainerRateResponse
{
    public Guid Id { get; set; }
    public int DurationInMinutes { get; set; }
    public string RatePerSessions { get; set; } = default!;
    public string ValidFrom { get; set; } = default!;
    public string? ValidTo { get; set; }
}
