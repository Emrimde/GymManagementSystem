namespace GymManagementSystem.Core.Domain.Entities;

public class Trainer
{
    public Guid Id { get; set; }
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string PhoneNumber { get; set; } = default!;
    public bool IsActive { get; set; } = true;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? DeletedAt { get; set; }
    public TrainerAvailabilityTemplate? AvailabilityTemplate { get; set; }

   // public Guid? CreatedById { get; set; }
    //public Guid? UpdatedById { get; set; }

    // Nawigacje
    //public ICollection<Classes> Classes{ get; set; }
}
