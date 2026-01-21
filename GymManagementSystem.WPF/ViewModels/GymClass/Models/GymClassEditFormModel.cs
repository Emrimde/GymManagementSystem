using GymManagementSystem.Core.Enum;
using GymManagementSystem.WPF.Core;
using System.Collections;
using System.ComponentModel;

namespace GymManagementSystem.WPF.ViewModels.GymClass.Models;
public class GymClassEditFormModel : ObservableObject, INotifyDataErrorInfo
{
    private Dictionary<string, List<string>> _errors = new();
    public bool HasErrors => _errors.Any();

    public event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged;

    public IEnumerable GetErrors(string? propertyName)
    {
        if (propertyName != null && _errors.ContainsKey(propertyName))
        {
            return _errors[propertyName];
        }
        return Enumerable.Empty<string>();
    }

    private string _name;

    public string Name
    {
        get { return _name; }
        set
        {
            _name = value;
            OnPropertyChanged();
            ValidateProperty(nameof(Name));

        }
    }

    private void ValidateProperty(string propertyName)
    {
        _errors.Remove(propertyName);

        List<string> errors = new();

        switch (propertyName)
        {

            case nameof(Name):
                if (string.IsNullOrWhiteSpace(Name))
                    errors.Add("Name is required.");
                break;

            case nameof(TrainerContractId):
                if (TrainerContractId == Guid.Empty)
                    errors.Add("Trainer must be selected.");
                break;

            case nameof(DaysOfWeek):
                if (DaysOfWeek == DaysOfWeekFlags.None)
                    errors.Add("Select at least one day.");
                break;
            case nameof(StartHour):
                if (StartHour == default)
                    errors.Add("Start hour must be set.");
                else if (StartHour < TimeSpan.FromHours(7) ||
                         StartHour > TimeSpan.FromHours(22))
                    errors.Add("Start hour must be between 7:00 and 22:00.");
                break;

            case nameof(MaxPeople):
                if (MaxPeople <= 0)
                    errors.Add("Max people must be greater than 0.");
                break;
        }

        if (errors.Any())
            _errors[propertyName] = errors;

        ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
        OnPropertyChanged(nameof(HasErrors));
    }


    private Guid _trainerContractId;

    public Guid TrainerContractId
    {
        get { return _trainerContractId; }
        set
        {
            _trainerContractId = value;
            OnPropertyChanged();
            ValidateProperty(nameof(TrainerContractId));
        }
    }

    private TimeSpan _startHour;

    public TimeSpan StartHour
    {
        get { return _startHour; }
        set
        {
            _startHour = value;
            OnPropertyChanged();
            ValidateProperty(nameof(StartHour));

        }
    }

    private DaysOfWeekFlags _daysOfWeek;

    public DaysOfWeekFlags DaysOfWeek
    {
        get { return _daysOfWeek; }
        set
        {
            _daysOfWeek = value;
            OnPropertyChanged();
            ValidateProperty(nameof(DaysOfWeek));
        }
    }


    private int _maxPeople;
    public int MaxPeople
    {
        get => _maxPeople;
        set
        {
            _maxPeople = value;
            OnPropertyChanged();
            ValidateProperty(nameof(MaxPeople));
        }
    }


    public bool IsFormComplete =>
       !string.IsNullOrWhiteSpace(Name) &&
       TrainerContractId != Guid.Empty &&
       MaxPeople > 0 && StartHour != default;

}
