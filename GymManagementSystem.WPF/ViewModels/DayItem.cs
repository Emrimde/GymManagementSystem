using GymManagementSystem.Core.Enum;
using GymManagementSystem.WPF.Core;

namespace GymManagementSystem.WPF.ViewModels;
public class DayItem : ObservableObject
{
    public DaysOfWeekFlags Day { get; set; }
    public bool IsSelected { get; set; }
}
