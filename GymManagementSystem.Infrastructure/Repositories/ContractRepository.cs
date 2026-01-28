using GymManagementSystem.Core.Domain.Entities;
using GymManagementSystem.Core.Domain.RepositoryContracts;
using GymManagementSystem.Infrastructure.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace GymManagementSystem.Infrastructure.Repositories;

public class ContractRepository : IContractRepository
{
    private readonly ApplicationDbContext _dbContext;
    public ContractRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public void CreateAsync(Contract entity)
    {
        _dbContext.Contracts.Add(entity);
    }
    public async Task<Contract?> GetByIdAsync(Guid id)
    {
       return await _dbContext.Contracts.Include(item => item.ClientMembership).ThenInclude(item=> item.Membership).Include(item => item.ClientMembership!.Client).FirstOrDefaultAsync(item => item.Id == id);
    }

    public async Task<Contract?> UpdateAsync(Guid id, Contract entity)
    {
        _dbContext.Contracts.Update(entity);

        await _dbContext.SaveChangesAsync();
   
        return entity;
    }
}
