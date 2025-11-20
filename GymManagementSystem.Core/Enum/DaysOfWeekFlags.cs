namespace GymManagementSystem.Core.Enum;

[Flags]
public enum DaysOfWeekFlags
{
    None = 0,
    Monday = 1 << 0, //1
    Tuesday = 1 << 1, //2
    Wednesday = 1 << 2, //4
    Thursday = 1 << 3,
    Friday = 1 << 4,
    Saturday = 1 << 5,
    Sunday = 1 << 6,
}
