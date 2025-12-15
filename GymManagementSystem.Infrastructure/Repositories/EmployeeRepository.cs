using GymManagementSystem.Core.Domain.Entities;
using GymManagementSystem.Core.Domain.RepositoryContracts;
using GymManagementSystem.Core.DTO.Employee;
using GymManagementSystem.Core.Mappers;
using GymManagementSystem.Infrastructure.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace GymManagementSystem.Infrastructure.Repositories;

public class EmployeeRepository : IEmployeeRepository
{
    private readonly ApplicationDbContext _dbContext;
    public EmployeeRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<EmployeeInfoResponse> CreateEmployeeAsync(Employee employee)
    {
        _dbContext.Employees.Add(employee);
        await _dbContext.SaveChangesAsync();
        return employee.ToEmployeeInfoResponse();
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
}
