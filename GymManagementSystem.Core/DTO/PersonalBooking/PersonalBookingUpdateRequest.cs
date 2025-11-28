namespace GymManagementSystem.Core.DTO.PersonalBooking
{
    public class PersonalBookingUpdateRequest
    {
        public Guid Id { get; set; }
        public DateTime Start { get;  set; }
        public DateTime End { get;  set; }
    }
}