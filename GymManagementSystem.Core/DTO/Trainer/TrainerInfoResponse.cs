namespace GymManagementSystem.Core.DTO.Trainer;

public class TrainerInfoResponse
{
    public Guid Id { get; set; }
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public string? FullName { get; set; } 
}
