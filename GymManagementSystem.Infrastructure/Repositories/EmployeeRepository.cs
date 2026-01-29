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

    public async Task<IEnumerable<Employee>> GetAllEmployeesAsync(string? searchText = null)
    {
        IQueryable<Employee> query = _dbContext.Employees.AsNoTracking();


        if (searchText != null)
        {
            string searchLower = searchText.ToLower();
            string[] terms = searchLower.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            foreach (string term in terms)
            {
                query = query.Where(item =>
                    item.Person!.FirstName.ToLower().Contains(term) ||
                    item.Person.LastName.ToLower().Contains(term) ||
                    item.Person.Email.ToLower().Contains(term) ||
                    item.Person.PhoneNumber.ToLower().Contains(term));
            }
        }



        return await query.Include(item => item.Person).ToListAsync();
    }

    public async Task<Employee?> GetEmployeeByIdAsync(Guid employeeId)
    {
       return await _dbContext.Employees.Include(item => item.Person).ThenInclude(item => item!.EmploymentTerminations).FirstOrDefaultAsync(item => item.Id == employeeId);    
    }
}

