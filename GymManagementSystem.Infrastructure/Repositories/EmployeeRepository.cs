using GymManagementSystem.Core.Domain.Entities;
using GymManagementSystem.Core.Domain.RepositoryContracts;
using GymManagementSystem.Infrastructure.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace GymManagementSystem.Infrastructure.Repositories;

public class EmployeeRepository : IEmployeeRepository
{
    private readonly ApplicationDbContext _dbContext;
    public EmployeeRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void CreateEmployee(Employee employee)
    {
        _dbContext.Employees.Add(employee);
    }

    public void DeleteEmployee(Employee employee)
    {
        _dbContext.Remove(employee);
    }

    public async Task<Employee?> GetEmployeeByIdAsync(Guid employeeId)
    {
       return await _dbContext.Employees.Include(item => item.Person).ThenInclude(item => item!.EmploymentTerminations).FirstOrDefaultAsync(item => item.Id == employeeId);    
    }
}

