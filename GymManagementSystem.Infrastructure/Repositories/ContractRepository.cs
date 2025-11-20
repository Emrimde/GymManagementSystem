using GymManagementSystem.Core.Domain.Entities;
using GymManagementSystem.Core.Domain.RepositoryContracts;
using GymManagementSystem.Core.Enum;
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

    public async Task<Contract?> GetActiveContractAsync(Guid clientId)
    {
        return await _dbContext.Contracts.FirstOrDefaultAsync(item => item.ContractStatus == ContractStatus.Draft);
    }

    public async Task<Contract> CreateAsync(Contract entity, CancellationToken cancellationToken)
    {
        _dbContext.Contracts.Add(entity);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return entity;
    }

    public async Task<IEnumerable<Contract>> GetAllAsync(CancellationToken cancellationToken)
    {
        IEnumerable<Contract> contracts = await _dbContext.Contracts.Include(item => item.ClientMembership).ThenInclude(item => item.Client).ToListAsync(cancellationToken);
        return contracts;
    }

    public async Task<Contract?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
       return await _dbContext.Contracts.Include(item => item.ClientMembership).ThenInclude(item=> item.Membership).Include(item => item.ClientMembership!.Client).FirstOrDefaultAsync(item => item.Id == id);
    }

    public async Task<Contract?> UpdateAsync(Guid id, Contract entity, CancellationToken cancellationToken)
    {

       
        await _dbContext.SaveChangesAsync(cancellationToken);
        return entity;

    }
}
