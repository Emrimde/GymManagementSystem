namespace GymManagementSystem.Core.DTO.Employee;

public class EmploymentContractPdfDto
{
    // Gym
    public string GymName { get; set; } = string.Empty;
    public string GymAddress { get; set; } = string.Empty;
    public string ContactNumber { get; set; } = string.Empty;
    public string Nip { get; set; } = string.Empty;

    // Contract
    public string ContractType { get; set; } = string.Empty;
    public string ValidFrom { get; set; } = string.Empty;
    public string ValidTo { get; set; } = string.Empty;

    public string Salary { get; set; } = string.Empty;
    public string EmploymentType { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;


}

