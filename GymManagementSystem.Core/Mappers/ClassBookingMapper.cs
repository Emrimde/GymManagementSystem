using GymManagementSystem.Core.Domain.Entities;
using GymManagementSystem.Core.DTO.ClassBooking;

namespace GymManagementSystem.Core.Mappers;

public static class ClassBookingMapper
{
    public static ClassBooking ToClassBooking(this ClassBookingAddRequest request)
    {
        return new ClassBooking()
        {
            ScheduledClassId = request.ScheduledClassId,
        };
    }

    public static ClassBookingInfoResponse ToClassBookingInfo(this ClassBooking classBooking)
    {
        return new ClassBookingInfoResponse()
        {
            FirstName = classBooking.Client?.FirstName ?? string.Empty,
            LastName = classBooking.Client?.FirstName ?? string.Empty,
            Name = classBooking.ScheduledClass?.GymClass?.Name ?? string.Empty,
        };
    }
}
