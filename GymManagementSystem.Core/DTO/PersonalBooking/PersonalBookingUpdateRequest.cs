namespace GymManagementSystem.Core.DTO.PersonalBooking
{
    public class PersonalBookingUpdateRequest
    {
        public Guid Id { get; set; }
        public DateTime Start { get;  set; }
        public DateTime End { get;  set; }
        public Guid TrainerId { get; set; }
        public Guid TrainerRateId { get; set; }
        public Guid ClientId { get; set; }
        public DateTime StartDay { get; set; }
        public TimeSpan StartHour { get; set; }
    }
}