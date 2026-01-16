using GymManagementSystem.WPF.Core;
using System.ComponentModel;
using System.Text.RegularExpressions;

namespace GymManagementSystem.WPF.ViewModels.Client.Models;

public class ClientEditFormModel : ObservableObject , IDataErrorInfo
{
    private string _lastName = string.Empty;
    public string LastName
    {
        get => _lastName;
        set
        {
            if (_lastName != value)
            {
                _lastName = value;
                OnPropertyChanged();
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
            OnPropertyChanged();
        }
    }
    private string _city = string.Empty;
    public string City
    {
        get => _city;
        set
        {
            _city = value;
            OnPropertyChanged();
        }
    }
    private string _phoneNUmber = string.Empty;
    public string PhoneNumber
    {
        get => _phoneNUmber;
        set
        {
            _phoneNUmber = value;
            OnPropertyChanged();
        }
    }

    public string Error => string.Empty;

    public string this[string columnName]
    {
        get
        {
            switch (columnName)
            {
                case nameof(LastName):
                    if (string.IsNullOrWhiteSpace(LastName))
                        return "Last name is required.";
                    if (LastName.Length > 50)
                        return "Last name cannot exceed 50 characters.";
                    break;

                case nameof(PhoneNumber):
                    if (string.IsNullOrWhiteSpace(PhoneNumber))
                        return "Phone number is required.";
                    if (!Regex.IsMatch(PhoneNumber, @"^\+?[1-9]\d{1,14}$"))
                        return "A valid phone number is required.";
                    break;

                case nameof(Street):
                    if (string.IsNullOrWhiteSpace(Street))
                        return "Street is required.";
                    break;

                case nameof(City):
                    if (string.IsNullOrWhiteSpace(City))
                        return "City is required.";
                    break;
            }

            return string.Empty;
        }
    }
}


