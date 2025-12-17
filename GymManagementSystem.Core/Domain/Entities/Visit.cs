using GymManagementSystem.Core.Enum;

namespace GymManagementSystem.Core.Domain.Entities;

public class Visit
{
    public Guid Id { get; set; }
    public Guid ClientId { get; set; }
    public Client? Client { get; set; }
    public Guid? ClientMembershipId { get; set; }
    public VisitSourceEnum VisitSource { get; set; } 
    public DateTime VisitDate { get; set; }
}
