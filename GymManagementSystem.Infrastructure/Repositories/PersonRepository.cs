using GymManagementSystem.Core.Domain.Entities;
using GymManagementSystem.Core.Domain.RepositoryContracts;
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
    public async Task<Person?> GetPersonByIdAsync(Guid personId)
    {
        return await _dbContext.People.Include(item => item.TrainerContract).Include(item => item.Employee).FirstOrDefaultAsync(item => item.Id == personId);
    }
}
