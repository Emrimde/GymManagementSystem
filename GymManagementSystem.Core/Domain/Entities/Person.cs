namespace GymManagementSystem.Core.Domain.Entities;

public class Person
{
    public Guid Id { get; set; }
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string PhoneNumber { get; set; } = default!;
    public bool IsActive { get; set; } = true;
    
    public DateTime CreatedAt = DateTime.UtcNow;

    public DateTime UpdatedAt = DateTime.UtcNow;
    public DateTime? DeletedAt { get; set; }
    public Employee? Employee { get; set; }
    public TrainerContract? TrainerContract{ get; set; }
    public ICollection<EmploymentTermination> EmploymentTerminations { get; set; }
    = new List<EmploymentTermination>();

}
