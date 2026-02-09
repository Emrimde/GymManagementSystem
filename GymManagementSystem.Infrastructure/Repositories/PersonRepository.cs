using GymManagementSystem.Core.Domain.Entities;
using GymManagementSystem.Core.Domain.RepositoryContracts;
using GymManagementSystem.Core.DTO.Person.ReadModel;
using GymManagementSystem.Core.Enum;
using GymManagementSystem.Core.WebDTO.Trainer;
using GymManagementSystem.Infrastructure.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace GymManagementSystem.Infrastructure.Repositories;
public class PersonRepository : IPersonRepository
{
    private readonly ApplicationDbContext _dbContext;
    public PersonRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void AddPersonToStaff(Person person)
    {
        _dbContext.People.Add(person);
    }

    public void UpdatePerson(Person person)
    {
        _dbContext.People.Update(person);
    }

    public async Task<IEnumerable<PersonReadModel>> GetAllStaffAsync(string? searchText, bool? isTrainer, EmployeeRole? employeeRole, TrainerTypeEnum? trainerTypeEnum, bool? isActive)
    {
        IQueryable<Person> query = _dbContext.People.AsQueryable();

        if (!string.IsNullOrEmpty(searchText))
        {
            string lowerCaseSearchText = searchText.ToLower();
            string[] terms = lowerCaseSearchText.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            foreach (string term in terms)
            {
                query = query.Where(item => item.FirstName.ToLower().Contains(term) || item.LastName.ToLower().Contains(term) || item.PhoneNumber.Contains(term) || item.Email.Contains(term));
            }
        }

        if (isTrainer.HasValue)
        {
            query = query.Where(item => (item.TrainerContract != null) == isTrainer.Value);
            if (isTrainer == false)
            {
                query = query.Where(item => (item.Employee != null) == true);
            }
        }
        if (isActive.HasValue)
        {
            query = query.Where(item => item.IsActive == isActive);
        }
        if (employeeRole.HasValue)
        {
            query = query.Where(item => item.Employee != null && item.Employee.Role == employeeRole.Value);
        }
        if (trainerTypeEnum.HasValue)
        {
            query = query.Where(item => item.TrainerContract != null && item.TrainerContract.TrainerType == trainerTypeEnum);
        }

        return await query.Select(item => new PersonReadModel()
        {
            EmployeeRole = item.Employee != null ? item.Employee.Role : null,
            TrainerTypeEnum = item.TrainerContract != null ? item.TrainerContract.TrainerType : null,
            HasEmployee = item.Employee == null ? false : true,
            HasTrainer = item.TrainerContract == null ? false : true,
            CreatedAt = item.CreatedAt,
            Email = item.Email,
            FirstName = item.FirstName,
            LastName = item.LastName,
            Id = item.Id,
            IsActive = item.IsActive,
            PhoneNumber = item.PhoneNumber,
            UpdatedAt = item.UpdatedAt,
            TrainerContractId = item.TrainerContract != null ? item.TrainerContract.Id : null,
            EmployeeId = item.Employee != null ? item.Employee.Id : null,
        }).ToListAsync();
    }

    public async Task<Person?> GetPersonByIdAsync(Guid personId)
    {
        return await _dbContext.People.Include(item => item.TrainerContract).Include(item => item.Employee).Include(item => item.EmploymentTerminations).FirstOrDefaultAsync(item => item.Id == personId);
    }
    public IQueryable<Person> GetPersonById(Guid personId)
    {
        return _dbContext.People.Where(item => item.IsActive);
    }

    public async Task<IEnumerable<Person>> GetAllActivePeopleWithTerminationAsync()
    {
        DateTime now = DateTime.UtcNow;

        IEnumerable<Person> people = await _dbContext.People
            .Where(item => item.IsActive).Where(item =>
            item.EmploymentTerminations
                .Where(item => item.IsActive)
                .Max(item => item.EffectiveDate.Date) <= now.Date
            ).Include(item => item.TrainerContract).Include(item => item.Employee)
            .ToListAsync();
        return people;
    }

    public async Task<GroupInstructorPanelResponse?> GetGroupInstructorPanelResponseAsync(Guid personId)
    {
       return await _dbContext.People.Where(item => item.Id == personId).Select(item => new GroupInstructorPanelResponse()
        {
            TrainerName = $"{item.FirstName} {item.LastName}",
            PhoneNumber = item.PhoneNumber,
            Email = item.Email,
            Location = item.TrainerContract.Person.City,
        }).FirstOrDefaultAsync();
    }
}
