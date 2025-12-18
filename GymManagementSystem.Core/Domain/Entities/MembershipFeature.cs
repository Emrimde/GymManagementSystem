using GymManagementSystem.Core.Enum;

namespace GymManagementSystem.Core.Domain.Entities;
public class MembershipFeature
{
    public Guid Id { get; set; }
    public Guid MembershipId { get; set; }
    public Membership? Membership { get; set; }
    public Guid FeatureId { get; set; }
    public Feature? Feature { get; set; }
    public int? BenefitFrequency { get; set; } // ile razy 
    public PeriodEnum? Period { get; set; } // co miesiąc, co trzy miesiące, co 6 miesięcy 
}
