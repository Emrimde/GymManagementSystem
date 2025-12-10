using GymManagementSystem.Core.Domain.Entities;
using GymManagementSystem.Core.Domain.RepositoryContracts;
using GymManagementSystem.Core.DTO.Client;
using GymManagementSystem.Core.DTO.Contract;
using GymManagementSystem.Core.Enum;
using GymManagementSystem.Core.Mappers;
using GymManagementSystem.Core.Mappers.ClientMapper;
using GymManagementSystem.Core.Result;
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

    public async Task<Contract> CreateAsync(Contract entity)
    {
        _dbContext.Contracts.Add(entity);
        await _dbContext.SaveChangesAsync();
        return entity;
    }

    public async Task<PageResult<ContractResponse>> GetAllAsync(int pageSize = 50, int page = 1, string? searchText = null)
    {
        IQueryable<Contract> query = _dbContext.Contracts;

        if (searchText != null)
        {
            string searchTextlower = searchText.ToLower();
            string[] terms = searchTextlower.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            foreach (string term in terms)
            {
                string pattern = $"%{term}%";
                query = query.Where(item => item.ClientMembership!.Client!.FirstName.ToLower().Contains(term) ||
                                               item.ClientMembership!.Client!.LastName.ToLower().Contains(term));
                                                    
            }
        }



        int totalCount = await query.CountAsync();
        int totalPages = (int)Math.Ceiling((double)totalCount / pageSize);
        List<ContractResponse> list = await query.OrderBy(item => item.ClientMembership!.Client!.FirstName)
                                                    .Skip((page - 1) * pageSize)
                                                        .Take(pageSize)
                                                            .Select(item => item.ToContractResponse())
                                                                .ToListAsync();



        return new PageResult<ContractResponse>()
        {
            Items = list,
            TotalCount = totalCount,
            PageSize = pageSize,
            TotalPages = totalPages,
            CurrentPage = page
        };
    }

    public async Task<Contract?> GetByIdAsync(Guid id)
    {
       return await _dbContext.Contracts.Include(item => item.ClientMembership).ThenInclude(item=> item.Membership).Include(item => item.ClientMembership!.Client).FirstOrDefaultAsync(item => item.Id == id);
    }

    public async Task<Contract?> UpdateAsync(Guid id, Contract entity)
    {
        Contract contract = new Contract { Id = id, ContractStatus = entity.ContractStatus };
        _dbContext.Attach(contract);
        _dbContext.Entry(contract).Property(item => item.ContractStatus).IsModified = true;

        await _dbContext.SaveChangesAsync();
        return contract;
    }
}
