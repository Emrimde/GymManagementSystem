using GymManagementSystem.Core.Enum;

namespace GymManagementSystem.Core.Domain.Entities;
public class Membership
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int ClassBookingDaysInAdvanceCount { get; set; }
    public int FreeFriendEntryCountPerMonth { get; set; }
    public int FreePersonalTrainingSessions { get; set; }
    public MembershipTypeEnum MembershipType { get; set; }
    public ICollection<MembershipPrice> MembershipPrices { get; set; } = new List<MembershipPrice>();
    public ICollection<MembershipFeature> MembershipFeatures { get; set; } = new List<MembershipFeature>();
}
