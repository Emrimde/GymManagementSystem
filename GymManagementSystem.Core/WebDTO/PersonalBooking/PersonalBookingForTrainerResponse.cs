namespace GymManagementSystem.Core.WebDTO.PersonalBooking;
public class PersonalBookingForTrainerResponse
{
    public Guid PersonalBookingId { get; set; }
    public string ClientName { get; set; } = default!;
    public string Date { get; set; } = default!;
    public string Duration { get; set; } = default!;
    public string EndTime { get; set; } = default!;
    public string Price { get; set; } = default!;

}
