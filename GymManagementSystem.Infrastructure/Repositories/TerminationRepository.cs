using GymManagementSystem.Core.Domain.Entities;
using GymManagementSystem.Core.Domain.RepositoryContracts;
using GymManagementSystem.Core.DTO.Client;
using GymManagementSystem.Core.DTO.Termination;
using GymManagementSystem.Core.Mappers;
using GymManagementSystem.Core.Mappers.ClientMapper;
using GymManagementSystem.Core.Result;
using GymManagementSystem.Core.Resulttttt;
using GymManagementSystem.Infrastructure.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace GymManagementSystem.Infrastructure.Repositories;

public class TerminationRepository : IRepository<TerminationResponse,Termination>
{
    private readonly ApplicationDbContext _dbContext;

    public TerminationRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void CreateAsync(Termination entity)
    {
       _dbContext.Terminations.Add(entity);
    }

    public Task<Termination?> GetByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<Termination?> UpdateAsync(Guid id, Termination entity)
    {
        throw new NotImplementedException();
    }
}
