using GymManagementSystem.WPF.Core;
using System.ComponentModel;
using System.Text.RegularExpressions;

namespace GymManagementSystem.WPF.ViewModels.Client.Models;
public class ClientAddFormModel : ObservableObject, IDataErrorInfo
{
    private string _firstName = string.Empty;
    public string FirstName
    {
        get => _firstName;
        set
        {
            _firstName = value;
            IsTouched = true;
            OnPropertyChanged();
            OnPropertyChanged(nameof(ShouldValidate));
        }
    }

    private string _lastName = string.Empty;
    public string LastName
    {
        get => _lastName;
        set
        {
            _lastName = value;
            IsTouched = true;
            OnPropertyChanged();
            OnPropertyChanged(nameof(ShouldValidate));
        }
    }

    private string _email = string.Empty;
    public string Email
    {
        get => _email;
        set
        {
            _email = value;
            IsTouched = true;
            OnPropertyChanged();
            OnPropertyChanged(nameof(ShouldValidate));
        }
    }

    private string _phoneNumber = string.Empty;
    public string PhoneNumber
    {
        get => _phoneNumber;
        set
        {
            _phoneNumber = value;
            IsTouched = true;
            OnPropertyChanged();
            OnPropertyChanged(nameof(ShouldValidate));
        }
    }

    private DateTime _dateOfBirth = DateTime.UtcNow;
    public DateTime DateOfBirth
    {
        get => _dateOfBirth;
        set
        {
            _dateOfBirth = value;
            IsTouched = true;
            OnPropertyChanged();
            OnPropertyChanged(nameof(ShouldValidate));
        }
    }

    private string _street = string.Empty;
    public string Street
    {
        get => _street;
        set
        {
            _street = value;
            IsTouched = true;
            OnPropertyChanged();
            OnPropertyChanged(nameof(ShouldValidate));
        }
    }

    private string _city = string.Empty;
    public string City
    {
        get => _city;
        set
        {
            _city = value;
            IsTouched = true;
            OnPropertyChanged();
            OnPropertyChanged(nameof(ShouldValidate));
        }
    }

    public bool IsTouched { get; set; }
    public bool ShouldValidate => IsTouched;

    public string this[string columnName]
    {
        get
        {
            switch (columnName)
            {
                case nameof(FirstName):
                    if (ShouldValidate)
                    {
                        if (string.IsNullOrWhiteSpace(FirstName))
                            return "First name is required.";
                        if (FirstName.Length > 50)
                            return "First name cannot exceed 50 characters.";
                    }
                    break;

                case nameof(LastName):
                    if (ShouldValidate)
                    {
                        if (string.IsNullOrWhiteSpace(LastName))
                            return "Last name is required.";
                        if (LastName.Length > 50)
                            return "Last name cannot exceed 50 characters.";
                    }
                    break;

                case nameof(Email):
                    if (ShouldValidate)
                    {
                        if (string.IsNullOrWhiteSpace(Email))
                            return "Email is required.";
                        if (Email.Length > 60)
                            return "Email cannot exceed 100 characters.";
                        if (!Regex.IsMatch(
                                Email,
                                @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
                            return "A valid email is required.";
                    }
                    break;

                case nameof(PhoneNumber):
                    if (ShouldValidate)
                    {
                        if (string.IsNullOrWhiteSpace(PhoneNumber))
                            return "Phone number is required.";
                        if (!Regex.IsMatch(
                                PhoneNumber,
                                @"^\+?[1-9]\d{1,14}$"))
                            return "A valid phone number is required.";
                    }
                    break;

                case nameof(DateOfBirth):
                    if (ShouldValidate)
                    {
                        if (DateOfBirth > DateTime.UtcNow.AddYears(-13))
                            return "Client must be above 13.";
                    }
                    break;

                case nameof(Street):
                    if (ShouldValidate)
                    {
                        if (string.IsNullOrWhiteSpace(Street))
                            return "Street is required.";
                        if (Street.Length > 60)
                            return "Street cannot exceed 60 characters.";
                    }
                    break;

                case nameof(City):
                    if (ShouldValidate)
                    {
                        if (string.IsNullOrWhiteSpace(City))
                            return "City is required.";
                        if (City.Length > 50)
                            return "City cannot exceed 50 characters.";
                    }
                    break;
            }

            return string.Empty;
        }
    }

    public string Error => null!;
}
