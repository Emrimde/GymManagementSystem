using GymManagementSystem.Core.Domain.Entities;
using GymManagementSystem.Core.DTO.GymClass;

namespace GymManagementSystem.Core.Mappers;
public static class GymClassMapper
{
    public static GymClassResponse ToGymResponse(this GymClass gymClass)
    {
        return new GymClassResponse
        {
            Id = gymClass.Id,
            Name = gymClass.Name,
            StartHour = gymClass.StartHour.ToString(@"hh\:mm"), // TimeSpan – bez ToString
            Duration = TimeSpan.FromMinutes(60).ToString(@"hh\:mm"),
            EndTime = (gymClass.StartHour + TimeSpan.FromMinutes(60)).ToString(@"hh\:mm"),
            MaxPeople = gymClass.MaxPeople,
            Days = gymClass.DaysOfWeek.ToString(),
            CreatedAt = gymClass.CreatedAt.ToString("dd.MM.yyyy"),
            UpdatedAt = gymClass.UpdatedAt.ToString("dd.MM.yyyy"),
            IsActive = gymClass.IsActive,
            CanActivate = !gymClass.IsActive
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
    public static void ModfiyGymClass(this GymClass gymClass, GymClassUpdateRequest gymClassUpdate)
    {
        gymClass.Name = gymClassUpdate.Name;
        gymClass.DaysOfWeek = gymClassUpdate.DaysOfWeek;
        gymClass.StartHour = gymClassUpdate.StartHour;
        gymClass.TrainerContractId = gymClassUpdate.TrainerId;
        gymClass.MaxPeople = gymClassUpdate.MaxPeople;
    }
}
