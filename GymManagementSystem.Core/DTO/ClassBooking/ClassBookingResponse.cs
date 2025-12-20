namespace GymManagementSystem.Core.DTO.ClassBooking;

public class ClassBookingResponse
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? Date { get; set; }
    public string? StartFrom { get; set; }
    public string? StartTo { get; set; }
    public string CreatedAt { get; set; } = default!;
}
