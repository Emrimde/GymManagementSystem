using GymManagementSystem.Core.Domain.Entities;
using GymManagementSystem.Core.DTO.GymClass;

namespace GymManagementSystem.Core.Mappers;
public static class GymClassMapper
{
    public static GymClassInfoResponse ToGymInfoResponse(this GymClass gymClass)
    {
        return new GymClassInfoResponse()
        {
            Id = gymClass.Id,
            Name = gymClass.Name,
        };
    }
    public static GymClass ToGymClass(this GymClassAddRequest request)
    {
        return new GymClass(
            request.Name,
            request.TrainerContractId,
            request.DaysOfWeek,
            request.StartHour,
            request.MaxPeople
        );
    }
}
