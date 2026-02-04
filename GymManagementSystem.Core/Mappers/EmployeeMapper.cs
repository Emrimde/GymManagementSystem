using GymManagementSystem.Core.Domain.Entities;
using GymManagementSystem.Core.DTO.Employee;

namespace GymManagementSystem.Core.Mappers;
public static class EmployeeMapper
{
    public static Employee ToEmployee(this EmployeeAddRequest request)
    {
        return new Employee()
        {
            PersonId = request.PersonId,
            EmploymentType = request.EmploymentType,
            Role = request.Role,
            MonthlySalaryBrutto = request.MonthlySalaryBrutto,
        };
    }

    public static EmployeeInfoResponse ToEmployeeInfoResponse(this Employee employee)
    {
        return new EmployeeInfoResponse()
        {
            EmployeeId = employee.Id,

        };
    }

    public static EmployeeResponse ToEmployeeResponse(this Employee employee)
    {
        return new EmployeeResponse()
        {
            Id = employee.Id,
            FirstName = employee.Person.FirstName,
            LastName = employee.Person.LastName,
            PhoneNumber = employee.Person.PhoneNumber,
            Email = employee.Person.Email,
            EmployeeRole = employee.Role,
            EmploymentType = employee.EmploymentType,
        };
    }
    public static EmployeeDetailsResponse ToEmployeeDetailsResponse(this Employee employee)
    {
        return new EmployeeDetailsResponse()
        {
            PersonId = employee.PersonId,
            Id = employee.Id,
            FirstName = employee.Person!.FirstName,
            LastName = employee.Person.LastName,
            PhoneNumber = employee.Person.PhoneNumber,
            Email = employee.Person.Email,
            Role = employee.Role.ToString(),
            Valid = employee.ValidFrom.ToString("dd.MM.yyyy") + "-" + (employee.ValidTo?.ToString("dd.MM:yyyy") ?? "Permanent"),
            City = employee.Person.City,
            Street = employee.Person.Street,
            CanTerminate = !employee.Person.EmploymentTerminations.Any(item => item.IsActive)
        };
    }
}