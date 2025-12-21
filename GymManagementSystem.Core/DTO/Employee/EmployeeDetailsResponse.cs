namespace GymManagementSystem.Core.DTO.Employee;
public class EmployeeDetailsResponse
{
    public Guid Id { get; set; }
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string PhoneNumber { get; set; } = default!;
    public string? Role { get; set; }
    public string ValidFrom { get; set; } = default!;
    public string ValidTo { get; set; } = default!;
}
