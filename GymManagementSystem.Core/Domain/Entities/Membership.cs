using GymManagementSystem.Core.Enum;

namespace GymManagementSystem.Core.Domain.Entities;
public class Membership
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public bool IsTrainerOnly { get; set; }
    public MembershipTypeEnum MembershipType { get; set; }
}
