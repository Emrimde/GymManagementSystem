using GymManagementSystem.Core.Domain.Entities;
using GymManagementSystem.Core.DTO.GymClass;

namespace GymManagementSystem.Core.Mappers;
public static class GymClassMapper
{
    public static GymClassResponse ToGymResponse(this GymClass gymClass)
    {
        return new GymClassResponse()
        {
            Id = gymClass.Id,
            CreatedAt = gymClass.CreatedAt.ToString("dd.MM.yyyy"),
            UpdatedAt = gymClass.UpdatedAt.ToString("dd.MM.yyyy"),
            Duration = gymClass.Duration,
            MaxPeople = gymClass.MaxPeople,
            Name = gymClass.Name,
            StartHour = gymClass.StartHour,
            Days = gymClass.DaysOfWeek.ToString()
        };
    }
    public static GymClassInfoResponse ToGymInfoResponse(this GymClass gymClass)
    {
        return new GymClassInfoResponse()
        {
            Id = gymClass.Id,
            Name = gymClass.Name,
        };
    }
    public static GymClass ToGymClass(this GymClassAddRequest gymClass)
    {
        return new GymClass()
        {
            Name = gymClass.Name,
    
            DaysOfWeek = gymClass.DaysOfWeek,
            StartHour = gymClass.StartHour,
            TrainerContractId = gymClass.TrainerContractId,
            MaxPeople = gymClass.MaxPeople,
        };
    }
}
