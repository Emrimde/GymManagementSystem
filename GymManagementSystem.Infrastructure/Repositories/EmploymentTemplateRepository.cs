using GymManagementSystem.Core.Domain.Entities;
using GymManagementSystem.Core.Domain.RepositoryContracts;
using GymManagementSystem.Core.DTO.EmploymentTemplate;
using GymManagementSystem.Core.Mappers;
using GymManagementSystem.Infrastructure.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace GymManagementSystem.Infrastructure.Repositories;

public class EmploymentTemplateRepository : IEmploymentTemplateRepository
{
    private readonly ApplicationDbContext _dbContext;
    public EmploymentTemplateRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<EmploymentTemplateInfoResponse> CreateEmploymentTemplateAsync(EmploymentTemplate entity)
    {
       _dbContext.EmploymentTemplates.Add(entity);
       await _dbContext.SaveChangesAsync();
       return entity.ToEmploymentTemplateInfoResponse();
    }

    public async Task<IEnumerable<EmploymentTemplate>> GetAllEmploymentTemplateAsync(CancellationToken cancellationToken)
    {
       return await _dbContext.EmploymentTemplates.ToListAsync(cancellationToken); 
    }
}
