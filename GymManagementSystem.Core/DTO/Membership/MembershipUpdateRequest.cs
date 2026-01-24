namespace GymManagementSystem.Core.DTO.Membership;

public class MembershipUpdateRequest
{
    public string Name { get; set; } = string.Empty;
    public int ClassBookingDaysInAdvanceCount { get; set; }
    public int FreeFriendEntryCountPerMonth { get; set; }
    public int FreePersonalTrainingSessions { get; set; }
}   