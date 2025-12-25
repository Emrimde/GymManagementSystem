using GymManagementSystem.Core.Domain.Entities;
using GymManagementSystem.Core.Domain.RepositoryContracts;
using GymManagementSystem.Core.DTO.Person.ReadModel;
using GymManagementSystem.Infrastructure.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace GymManagementSystem.Infrastructure.Repositories;
public class PersonRepository : IPersonRepository
{
    private readonly ApplicationDbContext _dbContext;
    public PersonRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Guid AddPersonToStaff(Person person)
    {
        _dbContext.People.Add(person);
        return person.Id;
    }

    public void UpdatePerson(Person person)
    {
        _dbContext.People.Update(person);
    }

    public async Task<IEnumerable<PersonReadModel>> GetAllStaffAsync()
    {
        return await _dbContext.People.Select(item => new PersonReadModel()
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
}
