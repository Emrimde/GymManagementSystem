namespace GymManagementSystem.Core.DTO.TrainerContract;

public class TrainerContractDetailsResponse
{
    public Guid Id { get; set; }
    public string ContractType { get; set; } = default!;
    public string TrainerType { get; set; } = default!;
    public string ClubCommissionPercent { get; set; } = default!;
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Email { get; set; }
    public bool CanShowBooking { get; set; }
    public bool CanTerminate { get; set; }
    public string Valid { get; set; } = default!;
    public Guid PersonId { get; set; }
}
