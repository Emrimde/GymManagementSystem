using GymManagementSystem.Core.Enum;

namespace GymManagementSystem.Core.Domain.Entities;

public class TrainerRate
{
    public Guid Id { get; set; }
    public Guid TrainerContractId { get; set; }
    public TrainerContract? TrainerContract { get; set; }
   
    public int? DurationInMinutes { get; set; }
    public decimal RatePerSessions { get; set; }
    public DateTime ValidFrom { get; set; }
    public DateTime? ValidTo { get; set; }
}