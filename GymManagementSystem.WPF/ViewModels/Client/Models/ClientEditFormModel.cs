using GymManagementSystem.WPF.Core;
using System.Collections;
using System.ComponentModel;
using System.Text.RegularExpressions;

namespace GymManagementSystem.WPF.ViewModels.Client.Models;

public class ClientEditFormModel : ObservableObject , INotifyDataErrorInfo
{
    Dictionary<string, List<string>> _errors = new();
    public IEnumerable GetErrors(string? propertyName)
    {
        if(propertyName != null && _errors.ContainsKey(propertyName))
        {
            return _errors[propertyName];
        }
        return Enumerable.Empty<string>();
    }

    public bool HasErrors => _errors.Any();
    private void TouchAndValidate(string propertyName)
    {
        ValidateProperty(propertyName);
        OnPropertyChanged(propertyName);
        OnPropertyChanged(nameof(HasErrors));
    }

    private void ValidateProperty(string propertyName)
    {
       _errors.Remove(propertyName);
        List<string> errors = new();

        switch (propertyName)
        {
            case nameof(LastName):
                if (string.IsNullOrWhiteSpace(LastName))
                    errors.Add("Last name is required.");
                if (LastName.Length > 50)
                    errors.Add("Last name cannot exceed 50 characters.");
                break;

            case nameof(PhoneNumber):
                if (string.IsNullOrWhiteSpace(PhoneNumber))
                    errors.Add("Phone number is required.");
                if (!Regex.IsMatch(PhoneNumber, @"^\+?[1-9]\d{1,14}$"))
                    errors.Add("A valid phone number is required.");
                break;

            case nameof(Street):
                if (string.IsNullOrWhiteSpace(Street))
                    errors.Add("Street is required.");
                break;

            case nameof(City):
                if (string.IsNullOrWhiteSpace(City))
                    errors.Add( "City is required.");
                break;
        }

        if(errors.Any())
        {
            _errors.Add(propertyName, errors);
        }

        ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
    }
    
    public bool IsFormComplete =>
       !string.IsNullOrWhiteSpace(LastName) &&
       !string.IsNullOrWhiteSpace(PhoneNumber) &&
       !string.IsNullOrWhiteSpace(Street) &&
       !string.IsNullOrWhiteSpace(City);



    private string _lastName = string.Empty;
    public string LastName
    {
        get => _lastName;
        set
        {
            if (_lastName != value)
            {
                _lastName = value;
                TouchAndValidate(nameof(LastName));
            }
        }
    }
    private string _street = string.Empty;
    public string Street
    {
        get => _street;
        set
        {
            _street = value;
            TouchAndValidate(nameof(Street));
        }
    }
    private string _city = string.Empty;
    public string City
    {
        get => _city;
        set
        {
            _city = value;
            TouchAndValidate(nameof(City));
        }
    }
    private string _phoneNumber = string.Empty;

    public event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged;

    public string PhoneNumber
    {
        get => _phoneNumber;
        set
        {
            _phoneNumber = value;
            TouchAndValidate(nameof(PhoneNumber));
        }
    }
}


