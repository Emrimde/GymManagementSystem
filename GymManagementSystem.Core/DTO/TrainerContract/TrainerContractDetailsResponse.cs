using GymManagementSystem.Core.Enum;

namespace GymManagementSystem.Core.DTO.TrainerContract;

public class TrainerContractDetailsResponse
{
    public Guid Id { get; set; }
    public ContractTypeEnum ContractType { get; set; } 
    public TrainerTypeEnum TrainerType { get; set; }
    // Rozliczenia
    public string ClubCommissionPercent { get; set; } = default!;

    // --- Dane firmy do B2B ---
    public string? CompanyName { get; set; }     // Nazwa działalności
    public string? TaxId { get; set; }           // NIP
    public string? CompanyAddress { get; set; }  // Adres firmy
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Email { get; set; }
    public bool IsPersonalTrainer { get; set; }
    public bool IsB2B { get; set; }
    public string Valid { get; set; } = default!;
    // Podpisy i dokumenty
    public string IsSigned { get; set; } = default!;
}
