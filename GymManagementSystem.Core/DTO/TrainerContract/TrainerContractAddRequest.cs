using GymManagementSystem.Core.Enum;

namespace GymManagementSystem.Core.DTO.TrainerContract;
public class TrainerContractAddRequest
{
    public ContractTypeEnum ContractType { get; set; } // B2B / Zlecenie
    public TrainerTypeEnum TrainerType { get; set; } // B2B / Zlecenie
    public Guid PersonId { get; set; }
    public decimal ClubCommissionPercent { get; set; }
    public string? CompanyName { get; set; }     // Nazwa działalności
    public string? TaxId { get; set; }           // NIP
    public string? CompanyAddress { get; set; }  // Adres firmy
}
