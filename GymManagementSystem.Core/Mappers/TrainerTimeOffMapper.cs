using GymManagementSystem.Core.Domain.Entities;
using GymManagementSystem.Core.DTO.TrainerTimeOff;

namespace GymManagementSystem.Core.Mappers;
public static class TrainerTimeOffMapper
{
    public static TrainerTimeOff ToTrainerTimeOff(this TrainerTimeOffAddRequest request)
    {
        return new TrainerTimeOff()
        {
            End = request.End,
            Reason = request.Reason,
            Start = request.Start,
        };
    }
    public static TrainerTimeOff ToTrainerTimeOff(this TrainerTimeOffUpdateRequest request)
    {
        return new TrainerTimeOff()
        {
            End = request.End,
            Reason = request.Reason,
            Start = request.Start,
            TrainerId = request.TrainerId,
        };
    }

    public static TrainerTimeOffInfoResponse ToTrainerTimeOffInfoResponse(this TrainerTimeOff request)
    {
        return new TrainerTimeOffInfoResponse()
        {
           Id = request.Id,
        };
    }
}
