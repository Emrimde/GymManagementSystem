using GymManagementSystem.Core.Domain.Entities;
using GymManagementSystem.Core.DTO.PersonalBooking;

namespace GymManagementSystem.Core.Mappers;
public static class PersonalBookingMapper
{
    public static PersonalBookingInfoResponse ToPersonalBookingInfoResponse(this PersonalBooking personalBooking)
    {
        return new PersonalBookingInfoResponse() { Id = personalBooking.Id, Status = personalBooking.Status, Price = personalBooking.Price.ToString()};

    }
    public static PersonalBooking ToPersonalBooking(this PersonalBookingAddRequest personalBooking)
    {
        return new PersonalBooking()
        {
            TrainerContractId = personalBooking.TrainerId,
            
            Start = personalBooking.StartDay,
           
        };
    }
}
