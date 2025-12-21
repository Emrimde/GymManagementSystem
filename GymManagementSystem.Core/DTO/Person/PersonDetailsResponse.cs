namespace GymManagementSystem.Core.DTO.Person;
public class PersonDetailsResponse
{
    public Guid Id { get; set; }
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string PhoneNumber { get; set; } = default!;
    public string? Role { get; set; }
    public string ValidFrom { get; set; } = default!;
    public string ValidTo { get; set; } = default!;
    public string Status { get; set; } = default!;
    public bool IsActive { get; set; } = default!;
}
