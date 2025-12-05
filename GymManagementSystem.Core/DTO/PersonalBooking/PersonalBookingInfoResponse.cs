using GymManagementSystem.Core.Domain.Entities;

namespace GymManagementSystem.Core.DTO.PersonalBooking;

public class PersonalBookingInfoResponse
{
    public Guid Id { get; set; }
    public BookingStatus Status { get; set; }
    public string Price { get; set; } = default!;
}
