using GymManagementSystem.Core.Domain.Entities;
using GymManagementSystem.Core.Domain.RepositoryContracts;
using GymManagementSystem.Core.DTO.EmploymentTermination;
using GymManagementSystem.Core.Mappers;
using GymManagementSystem.Infrastructure.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace GymManagementSystem.Infrastructure.Repositories;
public class EmploymentTerminationRepository : IEmploymentTerminationRepository
{
    private readonly ApplicationDbContext _dbContext;
    public EmploymentTerminationRepository(ApplicationDbContext dbContext)
    {
           _dbContext = dbContext;
    }

    public async Task<EmploymentTerminationInfoResponse> AddEmploymentTermination(EmploymentTermination employmentTermination)
    {
        _dbContext.EmploymentTerminations.Add(employmentTermination);
        await _dbContext.SaveChangesAsync();
        return employmentTermination.ToEmploymentTerminationInfoResponse();
    }

    public async Task<IEnumerable<EmploymentTerminationResponse>> GetEmploymentTerminationsAsync()
    {
        return await _dbContext.EmploymentTerminations.Include(item => item.Person).Select(item => new EmploymentTerminationResponse()
        {
            Id = item.Id,
            FirstName =  item.Person!.FirstName,
            LastName =  item.Person!.LastName,
            PhoneNumber = item.Person!.PhoneNumber,
            RequestedDate = item.RequestedDate,
            EffectiveDate = item.EffectiveDate
        }).ToListAsync();
    }
}
