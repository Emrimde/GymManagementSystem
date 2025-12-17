using GymManagementSystem.Core.Enum;

namespace GymManagementSystem.Core.DTO;
public class VisitResponse
{
    public VisitSourceEnum VisitSource { get; set; }
    public string VisitDate { get; set; } = default!;

}