using GymManagementSystem.Core.Domain.Entities;
using GymManagementSystem.Core.Domain.RepositoryContracts;
using GymManagementSystem.Core.DTO.TrainerContract;
using GymManagementSystem.Core.Enum;
using GymManagementSystem.Core.Mappers;
using GymManagementSystem.Infrastructure.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace GymManagementSystem.Infrastructure.Repositories;

public class TrainerRepository : ITrainerRepository
{
    private readonly ApplicationDbContext _dbContext;
    public TrainerRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<TrainerTimeOff> CreateTrainerTimeOffAsync(TrainerTimeOff trainerTimeOff)
    {
        _dbContext.TrainerTimeOff.Add(trainerTimeOff);
        await _dbContext.SaveChangesAsync();
        return trainerTimeOff;
    }

    public Task<bool> AnyTrainerOffOverlapAsync(Guid trainerId, Guid? trainerTimeOffId, DateTime start, DateTime end)
    {
        if (trainerTimeOffId != null)
        {
            return _dbContext.TrainerTimeOff
                .AnyAsync(item =>
                    item.TrainerId == trainerId &&
                    item.Id != trainerTimeOffId &&
                    item.Start < end &&
                    item.End > start
                );
        }
        else
        {
            return _dbContext.TrainerTimeOff
               .AnyAsync(item =>
                   item.TrainerId == trainerId &&
                   item.Start < end &&
                   item.End > start
               );
        }
    }

    public Task<bool> AnyPersonalBookingOverlapAsync(Guid trainerId, DateTime start, DateTime end)
    {
        return _dbContext.PersonalBookings
            .AnyAsync(item =>
                item.TrainerContractId == trainerId &&
                item.Start < end &&
                item.End > start
            );
    }
    public async Task<IEnumerable<TrainerTimeOff>> GetTrainerTimeOffs(CancellationToken cancellationToken)
    {
        IEnumerable<TrainerTimeOff> list = await _dbContext.TrainerTimeOff.ToListAsync();
        return list;
    }
    public async Task<TrainerContractInfoResponse> CreateTrainerContractAsync(TrainerContract trainerContract)
    {
        _dbContext.TrainerContracts.Add(trainerContract);
        await _dbContext.SaveChangesAsync();
        return trainerContract.ToTrainerContractInfoResponse();

    }
    public async Task<IEnumerable<TrainerContract>> GetAllTrainerContractsAsync(CancellationToken cancellationToken)
    {
        return await _dbContext.TrainerContracts.Include(item => item.Person).ToListAsync(cancellationToken);
    }
    public async Task<TrainerContract?> GetTrainerContractAsync(Guid id, bool includeDetails)
    {
        if (!includeDetails)
        {
            return await _dbContext.TrainerContracts.FirstOrDefaultAsync(item => item.Id == id);
        }
        else
        {
            return await _dbContext.TrainerContracts
                                        .Include(item => item.Person)
                                            .ThenInclude(item => item.EmploymentTerminations)
                                                .FirstOrDefaultAsync(item => item.Id == id);
        }
    }

    public async Task<IEnumerable<TrainerContract>> GetAllGroupInstructorsAsync(CancellationToken cancellationToken)
    {
        return await _dbContext.TrainerContracts.Where(item => item.TrainerType == TrainerTypeEnum.GroupInstructor).Include(item => item.Person).ToListAsync(cancellationToken);
    }
}
