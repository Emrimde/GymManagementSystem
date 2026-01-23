using GymManagementSystem.Core.Enum;

namespace GymManagementSystem.Core.DTO.Membership;

public class MembershipResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int ClassBookingDaysInAdvanceCount { get; set; }
    public int FreeFriendEntryCountPerMonth { get; set; }
    public int FreePersonalTrainingSessions { get; set; }
    public MembershipTypeEnum MembershipType { get; set; }
}