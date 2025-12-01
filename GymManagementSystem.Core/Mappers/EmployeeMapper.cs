using GymManagementSystem.Core.Domain.Entities;
using GymManagementSystem.Core.DTO.Employee;

namespace GymManagementSystem.Core.Mappers;
public static class EmployeeMapper
{
    public static Employee ToEmployee(this EmployeeAddRequest request)
    {
        return new Employee()
        {
            Email = request.Email,
            FirstName = request.FirstName,
            LastName = request.LastName,
            EmployeeRole = request.EmployeeRole,
            PhoneNumber = request.PhoneNumber,
        };
    }

    public static EmployeeInfoResponse ToEmployeeInfoResponse(this Employee employee)
    {
        return new EmployeeInfoResponse()
        {
            EmployeeId = employee.Id
        };
    }

    public static EmployeeResponse ToEmployeeResponse(this Employee employee)
    {
        return new EmployeeResponse()
        {
            Id = employee.Id,
            FirstName = employee.FirstName,
            LastName = employee.LastName,
            EmployeeRole = employee.EmployeeRole,
            PhoneNumber = employee.PhoneNumber,
            Email = employee.Email,
        };
    }
}