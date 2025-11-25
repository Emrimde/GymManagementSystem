namespace GymManagementSystem.Core.DTO.ClassBooking;

public class ClassBookingResponse
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Date { get; set; }
    public string CreatedAt { get; set; } = default!;
    public string UpdatedAt { get; set; } = default!;
    public string? CancelledAt { get; set; } 
}
