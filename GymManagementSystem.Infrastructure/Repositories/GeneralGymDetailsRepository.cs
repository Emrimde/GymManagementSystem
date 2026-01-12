using GymManagementSystem.Core.Domain.Entities;
using GymManagementSystem.Core.Domain.RepositoryContracts;
using GymManagementSystem.Infrastructure.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace GymManagementSystem.Infrastructure.Repositories;

public class GeneralGymDetailsRepository : IGeneralGymRepository
{
    private readonly ApplicationDbContext _dbContext;
    public GeneralGymDetailsRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<GeneralGymDetail?> GetGeneralGymDetailsAsync()
    {
        return await _dbContext.GeneralGymDetails.FirstOrDefaultAsync();
    }
}
