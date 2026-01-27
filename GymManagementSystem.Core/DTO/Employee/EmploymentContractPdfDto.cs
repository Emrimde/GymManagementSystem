using GymManagementSystem.Core.Enum;

namespace GymManagementSystem.Core.DTO.Employee;

public class EmploymentContractPdfDto
{
    // Gym
    public string GymName { get; set; } = string.Empty;
    public string GymAddress { get; set; } = string.Empty;
    public string ContactNumber { get; set; } = string.Empty;
    public string Nip { get; set; } = string.Empty;

    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string PhoneNumber { get; set; } = default!;
    public string Address { get; set; } = default!;




    // Contract
    public ContractTypeEnum ContractType { get; set; }
    public string ValidFrom { get; set; } = string.Empty;
    public string ValidTo { get; set; } = string.Empty;

    public string Salary { get; set; } = string.Empty;
    public EmploymentType EmploymentType { get; set; }
    public EmployeeRole Role { get; set; }


}

