namespace GymManagementSystem.Core.DTO.Trainer;

public class TrainerDetailsResponse
{
    public Guid Id { get; set; }
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string PhoneNumber { get; set; } = default!;
    public bool IsActive { get; set; } = true;
    public string CreatedAt { get; set; } 
    public string UpdatedAt { get; set; } 
    public string? DeletedAt { get; set; }
}
