namespace GymManagementSystem.Core.WebDTO.Trainer;
using GymManagementSystem.Core.WebDTO.ScheduledClassDto;
public class GroupInstructorPanelResponse
{
    public string TrainerName { get; set; } = default!;
    public string PhoneNumber { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string Location { get; set; } = default!;
    public IEnumerable<ScheduledClassDto> ScheduledClasses { get; set; } = default!;
}
