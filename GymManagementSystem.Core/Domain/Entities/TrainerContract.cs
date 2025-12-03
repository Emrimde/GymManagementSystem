using GymManagementSystem.Core.Enum;

namespace GymManagementSystem.Core.Domain.Entities;

public class TrainerContract
{
    public Guid Id { get; set; }
    public Guid PersonId { get; set; }
    public ContractTypeEnum ContractType { get; set; } // B2B / Zlecenie
    public TrainerTypeEnum TrainerType { get; set; }
    // Rozliczenia
    public decimal ClubCommissionPercent { get; set; }
    public DateTime ValidFrom { get; set; }
    public DateTime? ValidTo { get; set; }

    // --- Dane firmy do B2B ---
    public string? CompanyName { get; set; }     // Nazwa działalności
    public string? TaxId { get; set; }           // NIP
    public string? CompanyAddress { get; set; }  // Adres firmy

    // Podpisy i dokumenty
    public bool IsSigned { get; set; }
    public string? ContractDocumentPath { get; set; }
    public DateTime? SignedAt { get; set; }

    // Nawigacje
    public Person Person { get; set; } = default!;
    public ICollection<TrainerRate> Rates { get; set; } = new List<TrainerRate>();
    public TrainerProfile? TrainerProfile { get; set; }
    public ICollection<PersonalBooking> PersonalBookings { get; set; } = new List<PersonalBooking>();
    public ICollection<GymClass> GymClasses { get; set; } = new List<GymClass>();
}
