namespace GymManagementSystem.Core.DTO.Person;

public class PersonUpdateRequest
{
    public Guid PersonId { get; set; }
    public string City { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string Street { get; set; } = string.Empty;
}