using GymManagementSystem.Core.Domain.Entities;
using GymManagementSystem.Core.Domain.RepositoryContracts;
using GymManagementSystem.Core.DTO.TrainerRate;
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
      await _dbContext.SaveChangesAsync();
    }

    public async Task<TrainerRateResponse?> GetTrainerRateByIdAsync(Guid id)
    {
      return await _dbContext.TrainerRates.Where(item => item.Id == id).Select(item => new TrainerRateResponse()
      {
          Id = item.Id,
          DurationInMinutes = item.DurationInMinutes,
          RatePerSessions = item.RatePerSessions,
          ValidFrom = item.ValidFrom,
          ValidTo = item.ValidTo
      }).FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<TrainerRate>> GetTrainerRates(Guid id)
    {
      return await _dbContext.TrainerRates.Where(item => item.TrainerContractId == id).ToListAsync();
    }

    public async Task<IEnumerable<TrainerRateSelectResponse>> GetTrainerRatesSelect(Guid id)
    {
       IEnumerable<TrainerRateSelectResponse> response = await _dbContext.TrainerRates.Where(item => item.TrainerContractId == id).Select(item => new TrainerRateSelectResponse()
        {
           TrainerRateId = item.Id,
           DisplayPriceDuration = item.DurationInMinutes.ToString() + "min / " +  item.RatePerSessions.ToString() + "$",
           DurationInMinutes = item.DurationInMinutes,
           Price = item.RatePerSessions

       }).ToListAsync();
        return response;
    }
}
