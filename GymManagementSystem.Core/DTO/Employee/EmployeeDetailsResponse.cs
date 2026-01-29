namespace GymManagementSystem.Core.DTO.Employee;
public class EmployeeDetailsResponse
{
    public Guid Id { get; set; }
    public Guid PersonId { get; set; }
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string PhoneNumber { get; set; } = default!;
    public string? Role { get; set; }
    public string Street { get; set; } = default!;
    public string City { get; set; } = default!;
    public string Valid { get; set; } = default!;
    public bool CanTerminate { get; set; } = default!;
}
