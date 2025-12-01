using GymManagementSystem.Core.Enum;

namespace GymManagementSystem.Core.DTO.Employee;

public class EmployeeResponse
{
    public Guid Id { get; set; }
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string PhoneNumber { get; set; } = default!;
    public EmployeeRole EmployeeRole { get; set; }
}
