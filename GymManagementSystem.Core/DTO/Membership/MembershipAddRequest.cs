using GymManagementSystem.Core.Enum;

namespace GymManagementSystem.Core.DTO.Membership;

public class MembershipAddRequest
{
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public MembershipTypeEnum MembershipType { get; set; }
}