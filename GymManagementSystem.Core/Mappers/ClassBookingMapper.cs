using GymManagementSystem.Core.Domain.Entities;
using GymManagementSystem.Core.DTO.ClassBooking;

namespace GymManagementSystem.Core.Mappers;

public static class ClassBookingMapper
{
    public static ClassBookingResponse ToGymBookingResponse(this ClassBooking classBooking)
    {
        return new ClassBookingResponse()
        {
            Id = classBooking.Id,
            Name = classBooking.ScheduledClass?.GymClass?.Name ?? string.Empty,
            FirstName = classBooking.Client?.FirstName ?? string.Empty,
            LastName = classBooking.Client?.FirstName ?? string.Empty,
            CreatedAt = classBooking.CreatedAt.ToString("dd.MM.yyyy"),
            UpdatedAt = classBooking.CreatedAt.ToString("dd.MM.yyyy"),
            CancelledAt = classBooking.CreatedAt.ToString("dd.MM.yyyy"),
        };
    }

    public static ClassBooking ToGymClassBooking(this ClassBookingAddRequest request) 
    {
        return new ClassBooking()
        {
            ClientId = request.ClientId,
            ScheduledClassId = request.ScheduledClassId,
        };
    }

    public static ClassBookingInfoResponse ToGymClassBooking(this ClassBooking classBooking)
    {
        return new ClassBookingInfoResponse()
        {
            FirstName = classBooking.Client?.FirstName ?? string.Empty,
            LastName = classBooking.Client?.FirstName ?? string.Empty,
            Name = classBooking.ScheduledClass?.GymClass?.Name ?? string.Empty,
        };
    }
}
