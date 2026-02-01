using GymManagementSystem.Core.Domain.Entities;

namespace GymManagementSystem.Core.DTO.PersonalBooking;
public class PersonalBookingResponse
{
    public Guid PersonalBookingId { get; set; }
    public string Date { get; set; } = default!;
    public string StartEndTime { get; set; } = default!;
    public string BookingStatus { get; set; } = default!;
    public string Price { get; set; } = default!;
    public string TrainerFullName { get; set; } = default!;
}