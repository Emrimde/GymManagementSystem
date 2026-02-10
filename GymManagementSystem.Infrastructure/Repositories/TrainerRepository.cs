using GymManagementSystem.Core.Domain.Entities;
using GymManagementSystem.Core.Domain.RepositoryContracts;
using GymManagementSystem.Core.DTO.Trainer;
using GymManagementSystem.Core.DTO.TrainerContract;
using GymManagementSystem.Core.Enum;
using GymManagementSystem.Core.Mappers;
using GymManagementSystem.Core.Resulttttt;
using GymManagementSystem.Core.WebDTO.Trainer;
using GymManagementSystem.Core.WebDTO.TrainerTimeOff;
using GymManagementSystem.Infrastructure.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace GymManagementSystem.Infrastructure.Repositories;

public class TrainerRepository : ITrainerRepository
{
    private readonly ApplicationDbContext _dbContext;
    public TrainerRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void CreateTrainerTimeOffAsync(TrainerTimeOff trainerTimeOff)
    {
        _dbContext.TrainerTimeOff.Add(trainerTimeOff);
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

    public Task<bool> AnyPersonalBookingOverlapAsync(Guid trainerId, Guid? personalBookingId, DateTime start, DateTime end)
    {
        if (personalBookingId.HasValue)
        {
            return _dbContext.PersonalBookings
                        .AnyAsync(item => item.Id != personalBookingId &&
                            item.TrainerContractId == trainerId &&
                            item.Start < end &&
                            item.End > start
                        );
        }
        else
        {
            return _dbContext.PersonalBookings
                .AnyAsync(item =>
                    item.TrainerContractId == trainerId &&
                    item.Start < end &&
                    item.End > start
                );
        }
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
            FullName = item.Person.FirstName + " " + item.Person.LastName,
            Id = item.Id
        }).ToListAsync();
    }

    public void DeleteTrainer(TrainerContract trainerContract)
    {
        _dbContext.Remove(trainerContract);
    }

    public async Task<string?> GetTrainerTimeOffReasonAsync(Guid trainerTimeOffId)
    {
        return await _dbContext.TrainerTimeOff.Where(item => item.Id == trainerTimeOffId).Select(item => item.Reason).FirstOrDefaultAsync();
    }

    public async Task<bool> DeleteTrainerTimeOffAsync(Guid trainerTimeOffId)
    {
        TrainerTimeOff? trainerTimeOff = await _dbContext.TrainerTimeOff.FindAsync(trainerTimeOffId);
        if (trainerTimeOff == null)
        {
            return false;
        }
        _dbContext.Remove(trainerTimeOff);
        return true;
    }

    public async Task<TrainerPanelInfoResponse?> GetTrainerPanelInfoResponse(Guid personId)
    {
        DateTime start = new DateTime(
DateTime.UtcNow.Year,
    DateTime.UtcNow.Month,
    1,
    0, 0, 0,
    DateTimeKind.Utc);
        DateTime end = start.AddMonths(1);

        return await _dbContext.TrainerContracts.AsNoTracking().Where(item => item.TrainerType == TrainerTypeEnum.PersonalTrainer && item.PersonId == personId).Select(item => new TrainerPanelInfoResponse()
        {
            Email = item.Person.Email,
            Location = item.Person.City,
            MonthlyPersonalBookingCount = item.PersonalBookings.Count(item => item.Start >= start && item.Start < end),
            PhoneNumber = item.Person.PhoneNumber,
            TrainerName = item.Person.FirstName + " " + item.Person.LastName,
        }).FirstOrDefaultAsync();
    }


    public async Task<IEnumerable<TrainerTimeOffWebResponse>>  GetTrainerTimeOffsForTrainerPanelAsync(Guid personId)
    {
        return await _dbContext.TrainerTimeOff.Where(item => item.Trainer.PersonId == personId).Select(item => new TrainerTimeOffWebResponse()
        {
            TimeOffStart = item.Start.ToLocalTime().ToString("HH:mm"),
            TimeOffEnd = item.End.ToLocalTime().ToString("HH:mm"),
            TimeOffStartDate = item.Start.ToLocalTime().ToString("dd:MM:yyyy"),
            TimeOffEndDate = item.End.ToLocalTime().ToString("dd:MM:yyyy"),
            Reason = item.Reason
        }).ToListAsync();
    }
}
