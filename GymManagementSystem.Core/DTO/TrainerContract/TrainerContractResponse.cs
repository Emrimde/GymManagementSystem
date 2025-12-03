using GymManagementSystem.Core.Enum;

namespace GymManagementSystem.Core.DTO.TrainerContract;

public class TrainerContractResponse
{
    public Guid Id { get; set; }
    public string? FirstName { get; set; }   
    public string? LastName { get; set; }
    public string? PhoneNumber { get; set; }
    public ContractTypeEnum ContractType { get; set; }
    public TrainerTypeEnum TrainerType { get; set; }
}
