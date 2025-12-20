namespace GymManagementSystem.Core.DTO.ClassBooking.ReadModel;
public class ClassBookingReadModel
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public DateTime Date {  get; set; }
    public TimeSpan StartFrom { get; set; }
    public TimeSpan StartTo { get; set; }
    public DateTime CreatedAt { get; set; } = default!;
}
