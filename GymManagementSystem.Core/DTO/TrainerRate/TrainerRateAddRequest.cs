namespace GymManagementSystem.Core.DTO.TrainerRate;

public class TrainerRateAddRequest
{
    public Guid TrainerContractId { get; set; }
    public int DurationInMinutes { get; set; }
    public decimal RatePerSessions { get; set; }
    public DateTime ValidFrom { get; set; }
}
