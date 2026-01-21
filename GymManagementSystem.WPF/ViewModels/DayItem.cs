using GymManagementSystem.Core.Enum;
using GymManagementSystem.WPF.Core;

namespace GymManagementSystem.WPF.ViewModels;
public class DayItem : ObservableObject
{
    public DaysOfWeekFlags Day { get; set; }
    private bool _isSelected;

    public bool IsSelected
    {
        get => _isSelected;
        set
        {
            if (_isSelected == value) return;
            _isSelected = value;
            OnPropertyChanged();
        }
    }
}
