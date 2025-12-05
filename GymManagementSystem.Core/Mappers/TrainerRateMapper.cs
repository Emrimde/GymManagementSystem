using GymManagementSystem.Core.Domain.Entities;
using GymManagementSystem.Core.DTO.TrainerRate;

namespace GymManagementSystem.Core.Mappers;
public static class TrainerRateMapper
{
    public static TrainerRateResponse ToTrainerRateResponse(this TrainerRate trainerRate)
    {
        return new TrainerRateResponse()
        {
            DurationInMinutes = trainerRate.DurationInMinutes,
            Id = trainerRate.Id,
            RatePerSessions = trainerRate.RatePerSessions,
            ValidFrom = trainerRate.ValidFrom,
            ValidTo = trainerRate.ValidTo
        };
    }
    public static TrainerRateSelectResponse ToTrainerRateSelectResponse(this TrainerRate trainerRate)
    {
        return new TrainerRateSelectResponse()
        {
            DisplayPriceDuration = trainerRate.DurationInMinutes.ToString() + trainerRate.RatePerSessions.ToString()
        };
    }
}
