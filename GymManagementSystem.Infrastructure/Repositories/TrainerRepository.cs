using GymManagementSystem.Core.Domain.Entities;
using GymManagementSystem.Core.Domain.RepositoryContracts;
using GymManagementSystem.Core.DTO.Trainer;
using GymManagementSystem.Core.DTO.TrainerContract;
using GymManagementSystem.Core.Enum;
using GymManagementSystem.Core.Mappers;
using GymManagementSystem.Core.Resulttttt;
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
    public TrainerContract CreateTrainerContractAsync(TrainerContract trainerContract)
    {
        _dbContext.TrainerContracts.Add(trainerContract);
        return trainerContract;
    }
    public async Task<PageResult<TrainerContractResponse>> GetAllTrainerContractsAsync(int page = 1, int pageSize = 50, string? searchText = null)
    {

        IQueryable<TrainerContract> query = _dbContext.TrainerContracts;
        if (searchText != null)
        {
            string searchLower = searchText.ToLower();
            string[] terms = searchLower.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            foreach (string term in terms)
            {
                query = query.Where(item => item.Person.FirstName.ToLower().Contains(term) || item.Person.LastName.ToLower().Contains(term));
            }
        }

        int totalCount = query.Count();
        int totalpages = totalCount / pageSize;

        List<TrainerContractResponse> list = await query.OrderBy(item => item.Person.FirstName).Skip((page - 1) * pageSize).Take(pageSize).Include(item => item.Person).Select(item => item.ToTrainerContractResponse()).ToListAsync();
        return new PageResult<TrainerContractResponse>
        {
            CurrentPage = page,
            Items = list,
            PageSize = pageSize,
            TotalCount = totalCount,
            TotalPages = totalpages
        };
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

    public async Task<IEnumerable<TrainerContractInfoResponse>> GetAllGroupInstructorsAsync(CancellationToken cancellationToken)
    {
        return await _dbContext.TrainerContracts.AsNoTracking().Where(item => item.TrainerType == TrainerTypeEnum.GroupInstructor).Select(item => new TrainerContractInfoResponse()
        {
           Id = item.Id,
           FullName = item.Person.FirstName + " " + item.Person.LastName
        }).ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<TrainerInfoResponse>> GetAllPersonalTrainersAsync()
    {
        return await _dbContext.TrainerContracts.AsNoTracking().Where(item => item.TrainerType == TrainerTypeEnum.PersonalTrainer && item.Person.IsActive).Select(item => new TrainerInfoResponse()
        {
            FirstName = item.Person.FirstName,
            LastName = item.Person.LastName,
            FullName = item.Person.FirstName + " " + item.Person.LastName ,
            Id  = item.Id
        }).ToListAsync();
    }
}
