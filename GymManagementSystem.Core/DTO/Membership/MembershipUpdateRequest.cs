using GymManagementSystem.Core.Enum;

namespace GymManagementSystem.Core.DTO.Membership;

public class MembershipUpdateRequest
{
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public bool IsTrainerOnly { get; set; }
    public MembershipTypeEnum MembershipType { get; set; }
    public bool CanBeTerminated { get; set; }
}   