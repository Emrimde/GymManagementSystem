namespace GymManagementSystem.Core.DTO.Employee;
public class EmployeeInfoResponse
{
    public Guid EmployeeId { get; set; }
    public string TemporaryPassword { get; set; } = default!;  
}
