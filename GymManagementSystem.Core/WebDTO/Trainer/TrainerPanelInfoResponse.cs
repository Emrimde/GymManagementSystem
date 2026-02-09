using GymManagementSystem.Core.WebDTO.PersonalBooking;
using GymManagementSystem.Core.WebDTO.TrainerTimeOff;

namespace GymManagementSystem.Core.WebDTO.Trainer;

public class TrainerPanelInfoResponse
{
    public int MonthlyPersonalBookingCount { get; set; } = default!;
    public string TrainerName { get; set; } = default!;
    public string PhoneNumber { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string Location { get; set; } = default!;
    public IEnumerable<PersonalBookingForTrainerResponse> PersonalBookings { get; set; } = default!;
    public IEnumerable<TrainerTimeOffWebResponse> TrainerTimeOffs { get; set; } = default!;
}
