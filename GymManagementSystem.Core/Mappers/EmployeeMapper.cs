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
            ContractTypeEnum = request.ContractTypeEnum,
            MonthlySalaryBrutto = request.MonthlySalaryBrutto,
            ValidFrom = request.ValidFrom ?? DateTime.UtcNow,
            ValidTo = request.ValidTo,
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
            Id = employee.Id,
            FirstName = employee.Person!.FirstName,
            LastName = employee.Person.LastName,
            PhoneNumber = employee.Person.PhoneNumber,
            Email = employee.Person.Email,
            Role = employee.Role.ToString(),
            ValidFrom = employee.ValidFrom.ToString("dd.MM.yyyy"),
            ValidTo = employee.ValidTo?.ToString("dd.MM.yyyy") ?? "permanent",
            City = employee.Person.City,
            Street = employee.Person.Street
        };
    }
}