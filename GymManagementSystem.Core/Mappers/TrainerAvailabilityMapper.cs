using GymManagementSystem.Core.Domain.Entities;
using GymManagementSystem.Core.DTO.TrainerAvailabilityTemplate;

namespace GymManagementSystem.Core.Mappers;
public static class TrainerAvailabilityMapper
{
    public static TrainerAvailabilityInfoResponse ToTrainerAvailabilityInfoResponse(this TrainerAvailabilityTemplate trainerAvailbality)
    {
        return new TrainerAvailabilityInfoResponse()
        {
            Id = trainerAvailbality.Id,
        };
    }
    public static TrainerAvailabilityTemplate ToTrainerAvailabilityTemplate(this TrainerAvailabilityAddRequest trainerAvailbality)
    {
        return new TrainerAvailabilityTemplate()
        {
            TrainerId = trainerAvailbality.TrainerId,
            StartTime = trainerAvailbality.StartTime,
            EndTime = trainerAvailbality.EndTime,
        };
    }
}
