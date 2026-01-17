using GymManagementSystem.WPF.Core;
using System.Collections;
using System.ComponentModel;
using System.Text.RegularExpressions;

namespace GymManagementSystem.WPF.ViewModels.Client.Models;

public class ClientAddFormModel : ObservableObject, INotifyDataErrorInfo
{

    private readonly Dictionary<string, List<string>> _errors = new();
    public bool HasErrors => _errors.Any();

    #region formProperties

    private string _firstName = string.Empty;
    public string FirstName
    {
        get => _firstName;
        set
        {
            _firstName = value;
            TouchAndValidate(nameof(FirstName));
        }
    }

    private string _lastName = string.Empty;
    public string LastName
    {
        get => _lastName;
        set
        {
            _lastName = value;
            TouchAndValidate(nameof(LastName));
        }
    }

    private string _email = string.Empty;
    public string Email
    {
        get => _email;
        set
        {
            _email = value;
            TouchAndValidate(nameof(Email));
        }
    }

    private string _phoneNumber = string.Empty;
    public string PhoneNumber
    {
        get => _phoneNumber;
        set
        {
            _phoneNumber = value;
            TouchAndValidate(nameof(PhoneNumber));
        }
    }

    private DateTime _dateOfBirth = DateTime.UtcNow;
    public DateTime DateOfBirth
    {
        get => _dateOfBirth;
        set
        {
            _dateOfBirth = value;
            TouchAndValidate(nameof(DateOfBirth));
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
    #endregion


    public event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged;

    public IEnumerable GetErrors(string? propertyName)
    {
        if (propertyName != null && _errors.ContainsKey(propertyName))
            return _errors[propertyName];

        return Enumerable.Empty<string>();
    }
    public bool IsFormComplete =>
      !string.IsNullOrWhiteSpace(FirstName) &&
      !string.IsNullOrWhiteSpace(LastName) &&
      !string.IsNullOrWhiteSpace(Email) &&
      !string.IsNullOrWhiteSpace(PhoneNumber) &&
      !string.IsNullOrWhiteSpace(Street) &&
      !string.IsNullOrWhiteSpace(City) &&
      DateOfBirth != DateTime.UtcNow;

    private void TouchAndValidate(string propertyName)
    { 
        ValidateProperty(propertyName);
        OnPropertyChanged(propertyName);
        OnPropertyChanged(nameof(HasErrors));
    }

    private void ValidateProperty(string propertyName)
    {
        _errors.Remove(propertyName);

        var errors = new List<string>();

        switch (propertyName)
        {
            case nameof(FirstName):
                if (string.IsNullOrWhiteSpace(FirstName))
                    errors.Add("First name is required.");
                else if (FirstName.Length > 50)
                    errors.Add("First name cannot exceed 50 characters.");
                break;

            case nameof(LastName):
                if (string.IsNullOrWhiteSpace(LastName))
                    errors.Add("Last name is required.");
                else if (LastName.Length > 50)
                    errors.Add("Last name cannot exceed 50 characters.");
                break;

            case nameof(Email):
                if (string.IsNullOrWhiteSpace(Email))
                    errors.Add("Email is required.");
                else if (Email.Length > 60)
                    errors.Add("Email cannot exceed 60 characters.");
                else if (!Regex.IsMatch(Email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
                    errors.Add("A valid email is required.");
                break;

            case nameof(PhoneNumber):
                if (string.IsNullOrWhiteSpace(PhoneNumber))
                    errors.Add("Phone number is required.");
                else if (!Regex.IsMatch(PhoneNumber, @"^\+?[1-9]\d{1,14}$"))
                    errors.Add("A valid phone number is required.");
                break;

            case nameof(DateOfBirth):
                if (DateOfBirth > DateTime.UtcNow.AddYears(-13))
                    errors.Add("Client must be above 13.");
                break;

            case nameof(Street):
                if (string.IsNullOrWhiteSpace(Street))
                    errors.Add("Street is required.");
                else if (Street.Length > 60)
                    errors.Add("Street cannot exceed 60 characters.");
                break;

            case nameof(City):
                if (string.IsNullOrWhiteSpace(City))
                    errors.Add("City is required.");
                else if (City.Length > 50)
                    errors.Add("City cannot exceed 50 characters.");
                break;
        }

        if (errors.Any())
            _errors[propertyName] = errors;

        ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
    }

    
}
