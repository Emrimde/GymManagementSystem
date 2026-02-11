using GymManagementSystem.Core.Domain.Entities;
using GymManagementSystem.Core.Domain.RepositoryContracts;
using GymManagementSystem.Core.DTO.TrainerRate;
using GymManagementSystem.Core.Mappers;
using GymManagementSystem.Infrastructure.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace GymManagementSystem.Infrastructure.Repositories;

public class TrainerRateRepository : ITrainerRateRepository
{
    private readonly ApplicationDbContext _dbContext;
    public TrainerRateRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddRangeAsync(IEnumerable<TrainerRate> trainerRates)
    {
        await _dbContext.TrainerRates.AddRangeAsync(trainerRates);
    }

    public async Task<TrainerRateInfoResponse> AddTrainerRateAsync(TrainerRate trainerRate)
    {
        _dbContext.TrainerRates.Add(trainerRate);
        await _dbContext.SaveChangesAsync();
        return trainerRate.ToTrainerRateInfoResponse();
    }

    public async Task<TrainerRateResponse?> GetTrainerRateByIdAsync(Guid id)
    {
        return await _dbContext.TrainerRates.Where(item => item.Id == id).Select(item => new TrainerRateResponse()
        {
            Id = item.Id,
            DurationInMinutes = item.DurationInMinutes,
            RatePerSessions = item.RatePerSessions.ToString() + " $",
            ValidFrom = item.ValidFrom.ToLocalTime().ToString("dd.MM.yyyy HH:mm"),
            ValidTo = item.ValidTo.HasValue ? item.ValidTo.Value.ToLocalTime().ToString("dd.MM.yyyy HH:mm") : null
        }).FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<TrainerRate>> GetTrainerRates(Guid trainerId, bool? showActive)
    {
        IQueryable<TrainerRate> query = _dbContext.TrainerRates.OrderByDescending(item => item.ValidFrom);
        if (showActive.HasValue && showActive.Value == true)
        {
            query = query.Where(item => item.ValidTo == null);
        }
        return await query.ToListAsync();
        //return await _dbContext.TrainerRates.Where(item => item.TrainerContractId == trainerId).OrderByDescending(item => item.ValidFrom).ToListAsync();
    }


    public async Task<TrainerRateForPersonalBookingAddResponse?> GetTrainerRateForPersonalBookingAddResponseAsync(Guid trainerRateId)
    {
        return await _dbContext.TrainerRates.Where(item => item.Id == trainerRateId).Select(item => new TrainerRateForPersonalBookingAddResponse()
        {
            TrainerRateId = item.Id,
            DurationInMinutes = item.DurationInMinutes,
            RatePerSessions = item.RatePerSessions
        }).FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<TrainerRateSelectResponse>> GetTrainerRatesSelect(Guid trainerId)
    {
        IEnumerable<TrainerRateSelectResponse> response = await _dbContext.TrainerRates.Where(item => item.TrainerContractId == trainerId && item.ValidTo == null).Select(item => new TrainerRateSelectResponse()
        {
            TrainerRateId = item.Id,
            DisplayPriceDuration = item.DurationInMinutes.ToString() + "min / " + item.RatePerSessions.ToString() + "$",
            DurationInMinutes = item.DurationInMinutes,
            Price = item.RatePerSessions

        }).ToListAsync();
        return response;
    }
}
