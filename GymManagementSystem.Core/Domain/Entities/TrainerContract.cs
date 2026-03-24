using GymManagementSystem.Core.Enum;

namespace GymManagementSystem.Core.Domain.Entities;

public class TrainerContract
{
    public Guid Id { get; set; }
    public Guid PersonId { get; set; }
    public ContractTypeEnum ContractType { get; set; } 
    public TrainerTypeEnum TrainerType { get; set; }
    public decimal ClubCommissionPercent { get; set; }
    public DateTime ValidFrom { get; set; }
    public DateTime? ValidTo { get; set; }
    public bool IsSigned { get; set; } = true;
    public Person Person { get; set; } = default!;
    public ICollection<TrainerRate> Rates { get; set; } = new List<TrainerRate>();
    public ICollection<PersonalBooking> PersonalBookings { get; set; } = new List<PersonalBooking>();
    public ICollection<GymClass> GymClasses { get; set; } = new List<GymClass>();
}
