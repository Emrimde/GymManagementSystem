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
            RatePerSessions = trainerRate.RatePerSessions.ToString() + " $",
            ValidFrom = trainerRate.ValidFrom.ToLocalTime().ToString("dd.MM.yyyy HH:mm"),
            ValidTo = trainerRate.ValidTo.HasValue ? trainerRate.ValidTo.Value.ToLocalTime().ToString("dd.MM.yyyy HH:mm") : null
        };
    }
    public static TrainerRateSelectResponse ToTrainerRateSelectResponse(this TrainerRate trainerRate)
    {
        return new TrainerRateSelectResponse()
        {
            DisplayPriceDuration = trainerRate.DurationInMinutes.ToString() + trainerRate.RatePerSessions.ToString()
        };
    }
    public static TrainerRateInfoResponse ToTrainerRateInfoResponse(this TrainerRate trainerRate)
    {
        return new TrainerRateInfoResponse()
        {
           Id = trainerRate.Id,
        };
    }
    public static TrainerRate ToTrainerRate(this TrainerRateAddRequest trainerRate)
    {
        return new TrainerRate()
        {
            DurationInMinutes = trainerRate.DurationInMinutes,
            RatePerSessions = trainerRate.RatePerSessions,
            ValidFrom = trainerRate.ValidFrom,
            TrainerContractId = trainerRate.TrainerContractId,
        };
    }
}
