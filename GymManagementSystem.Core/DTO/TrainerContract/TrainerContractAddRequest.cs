using GymManagementSystem.Core.Enum;

namespace GymManagementSystem.Core.DTO.TrainerContract;
public class TrainerContractAddRequest
{
    public ContractTypeEnum ContractType { get; set; } // B2B / Zlecenie
    public TrainerTypeEnum TrainerType { get; set; } // B2B / Zlecenie

    //public string FirstName { get; set; } = default!;
    //public string LastName { get; set; } = default!;
    //public string Email { get; set; } = default!;
    //public string PhoneNumber { get; set; } = default!;
    public Guid PersonId { get; set; }

    // Rozliczenia
    public decimal ClubCommissionPercent { get; set; }
    public DateTime? ValidFrom { get; set; }
    public DateTime? ValidTo { get; set; }

    // --- Dane firmy do B2B ---
    public string? CompanyName { get; set; }     // Nazwa działalności
    public string? TaxId { get; set; }           // NIP
    public string? CompanyAddress { get; set; }  // Adres firmy

   
   
}
