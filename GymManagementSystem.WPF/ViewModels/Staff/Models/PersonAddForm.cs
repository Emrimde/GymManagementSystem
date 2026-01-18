using GymManagementSystem.WPF.Core;
using System.Collections;
using System.ComponentModel;
using System.IO;
using System.Text.RegularExpressions;

namespace GymManagementSystem.WPF.ViewModels.Staff.Models;

public class PersonAddForm : ObservableObject, INotifyDataErrorInfo
{
    private readonly Dictionary<string, List<string>> _errors = new();
    public bool HasErrors => _errors.Any();

    public event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged;

    public IEnumerable GetErrors(string? propertyName)
    {
        if (propertyName != null && _errors.ContainsKey(propertyName))
            return _errors[propertyName];

        return Enumerable.Empty<string>();
    }

    protected void ValidateProperty(string propertyName)
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


    public bool IsFormComplete =>
      !string.IsNullOrWhiteSpace(FirstName) &&
      !string.IsNullOrWhiteSpace(LastName) &&
      !string.IsNullOrWhiteSpace(Email) &&
      !string.IsNullOrWhiteSpace(PhoneNumber) &&
      !string.IsNullOrWhiteSpace(Street) &&
      !string.IsNullOrWhiteSpace(City);
      


    private string _firstName;
    public string FirstName
    {
        get { return _firstName; }
        set
        {
            _firstName = value;
            OnPropertyChanged();
            ValidateProperty(nameof(FirstName));
        }
    }

    private string _lastName;
    public string LastName
    {
        get { return _lastName; }
        set
        {
            _lastName = value;
            OnPropertyChanged();
            ValidateProperty(nameof(LastName));
        }
    }

    private string _email;
    public string Email
    {
        get { return _email; }
        set
        {
            _email = value;
            OnPropertyChanged();
            ValidateProperty(nameof(Email));
        }
    }

    private string _phoneNumber;
    public string PhoneNumber
    {
        get { return _phoneNumber; }
        set
        {
            _phoneNumber = value;
            OnPropertyChanged();
            ValidateProperty(nameof(PhoneNumber));
        }
    }

    private string _street;
    public string Street
    {
        get { return _street; }
        set
        {
            _street = value;
            OnPropertyChanged();
            ValidateProperty(nameof(Street));
        }
    }

    private string _city;
    public string City
    {
        get { return _city; }
        set
        {
            _city = value;
            OnPropertyChanged();
            ValidateProperty(nameof(City));
        }
    }
}
