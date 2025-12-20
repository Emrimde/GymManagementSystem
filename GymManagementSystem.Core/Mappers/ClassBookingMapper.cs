using GymManagementSystem.Core.Domain.Entities;
using GymManagementSystem.Core.DTO.ClassBooking;
using GymManagementSystem.Core.DTO.ClassBooking.ReadModel;

namespace GymManagementSystem.Core.Mappers;

public static class ClassBookingMapper
{
    //public static ClassBookingResponse ToClassBookingResponse(this ClassBooking classBooking)
    //{
    //    return new ClassBookingResponse()
    //    {
    //        Id = classBooking.Id,
    //        Name = classBooking.ScheduledClass?.GymClass?.Name ?? string.Empty,
    //        StartFrom = classBooking.ScheduledClass?.StartFrom.ToString() ?? string.Empty,
    //        StartTo = classBooking.ScheduledClass?.StartTo.ToString() ?? string.Empty,
    //        Date = classBooking.ScheduledClass?.Date.ToString("dd.MM.yyyy") ?? string.Empty,
    //        CreatedAt = classBooking.CreatedAt.ToString("dd.MM.yyyy"),
    //    };
    //}
    public static ClassBookingResponse ToClassBookingResponse(this ClassBookingReadModel classBooking)
    {
        return new ClassBookingResponse()
        {
            Id = classBooking.Id,
            Name = classBooking.Name,
            StartFrom = classBooking.StartFrom.ToString(),
            StartTo = classBooking.StartTo.ToString(),
            Date = classBooking.Date.ToString("dd.MM.yyyy"),
            CreatedAt = classBooking.CreatedAt.ToString("dd.MM.yyyy"),
        };
    }

    public static ClassBooking ToClassBooking(this ClassBookingAddRequest request)
    {
        return new ClassBooking()
        {
            ClientId = request.ClientId,
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
