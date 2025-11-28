namespace GymManagementSystem.Core.DTO.Trainer
{
    public class TrainerScheduleResponse
    {
        public Guid TrainerId { get; set; }
        public List<TrainerScheduleDay> Days { get; set; } = new();
    }

    public class TrainerScheduleDay
    {
        public DateOnly Date { get; set; }
        public List<TrainerScheduleItem> Items { get; set; } = new();
    }

    public class TrainerScheduleItem
    {
        public Guid? TimeOffId { get; set; }
        public Guid? BookingId { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }

        public TrainerScheduleItemType Type { get; set; }
        public string? ClientName { get; set; } // tylko gdy booked
    }

    public enum TrainerScheduleItemType
    {
        Available,
        Booked,
        TimeOff
    }

}
