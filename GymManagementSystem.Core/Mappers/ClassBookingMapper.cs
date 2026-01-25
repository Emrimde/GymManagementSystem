using GymManagementSystem.Core.Domain.Entities;
using GymManagementSystem.Core.DTO.ClassBooking;
using GymManagementSystem.Core.DTO.ClassBooking.ReadModel;

namespace GymManagementSystem.Core.Mappers;

public static class ClassBookingMapper
{
    public static ClassBookingResponse ToClassBookingResponse(this ClassBookingReadModel classBooking)
    {
        return new ClassBookingResponse()
        {
            Id = classBooking.Id,
            Name = classBooking.Name,
            StartFrom = classBooking.StartFrom.ToString(@"hh\:mm"),
            StartTo = (classBooking.StartFrom + TimeSpan.FromMinutes(60)).ToString(@"hh\:mm"),
            Date = classBooking.Date.ToString("dd.MM.yyyy"),
            CreatedAt = classBooking.CreatedAt.ToString("dd.MM.yyyy"),
        };
    }

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
