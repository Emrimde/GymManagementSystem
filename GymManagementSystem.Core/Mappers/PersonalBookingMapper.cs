using GymManagementSystem.Core.Domain.Entities;
using GymManagementSystem.Core.DTO.PersonalBooking;

namespace GymManagementSystem.Core.Mappers;
public static class PersonalBookingMapper
{
    public static PersonalBookingInfoResponse ToPersonalBookingInfoResponse(this PersonalBooking personalBooking)
    {
        return new PersonalBookingInfoResponse() { Id = personalBooking.Id };

    }
    public static PersonalBooking ToPersonalBooking(this PersonalBookingAddRequest personalBooking)
    {
        return new PersonalBooking()
        {
            TrainerId = personalBooking.TrainerId,
            ClientId = personalBooking.ClientId,
            Start = personalBooking.Start,
            End = personalBooking.End
        };
    }
}
