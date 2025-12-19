using GymManagementSystem.Core.Domain.RepositoryContracts;
using GymManagementSystem.Core.DTO.Feature;
using GymManagementSystem.Infrastructure.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace GymManagementSystem.Infrastructure.Repositories;
public class FeatureRepository : IFeatureRepository
{
    private readonly ApplicationDbContext _dbContext;
    public FeatureRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<FeatureResponse>> GetAllFeatures()
    {
        return await _dbContext.Features.Select(item => new FeatureResponse()
        {
            BenefitDescription = item.BenefitDescription,
            FeatureId = item.Id
        }).ToListAsync();    
    }
}
