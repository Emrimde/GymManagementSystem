using GymManagementSystem.Core.Enum;

namespace GymManagementSystem.Core.DTO.TrainerContract;
public class TrainerContractAddRequest
{
    public ContractTypeEnum ContractType { get; set; }
    public TrainerTypeEnum TrainerType { get; set; } 
    public Guid PersonId { get; set; }
    public decimal ClubCommissionPercent { get; set; }
}
