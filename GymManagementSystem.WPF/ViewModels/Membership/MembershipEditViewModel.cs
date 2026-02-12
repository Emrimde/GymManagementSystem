using GymManagementSystem.Core.DTO.Membership;
using GymManagementSystem.WPF.Result;
using GymManagementSystem.WPF.Core;
using GymManagementSystem.WPF.HttpServices;
using GymManagementSystem.WPF.ServiceContracts;
using System.Collections;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace GymManagementSystem.WPF.ViewModels.Membership;

public class MembershipEditViewModel : ViewModel, IParameterReceiver, INotifyDataErrorInfo
{

    private readonly MembershipHttpClient _httpClient;
    public ICommand UpdateMembershipCommand { get; }
    public ICommand LoadMembershipCommand { get; }
    public ICommand CancelCommand { get; }

    private string _name = string.Empty;
    private Dictionary<string, List<string>> _errors = new();

    public event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged;

    public string Name
    {
        get { return _name; }
        set { _name = value; OnPropertyChanged(); ValidateProperty(nameof(Name)); }
    }

    private int _classBookingDaysInAdvanceCount;
    public int ClassBookingDaysInAdvanceCount
    {
        get => _classBookingDaysInAdvanceCount;
        set
        {
            if (_classBookingDaysInAdvanceCount == value) return;
            _classBookingDaysInAdvanceCount = value;
            OnPropertyChanged();
            ValidateProperty(nameof(ClassBookingDaysInAdvanceCount));
        }
    }

    private int _freeFriendEntryCountPerMonth;
    public int FreeFriendEntryCountPerMonth
    {
        get => _freeFriendEntryCountPerMonth;
        set
        {
            if (_freeFriendEntryCountPerMonth == value) return;
            _freeFriendEntryCountPerMonth = value;
            OnPropertyChanged();
            ValidateProperty(nameof(FreeFriendEntryCountPerMonth));
        }
    }

    private void ValidateProperty(string propertyName)
    {
        _errors.Remove(propertyName);

        var errors = new List<string>();

        switch (propertyName)
        {
            case nameof(Name):
                if (string.IsNullOrWhiteSpace(Name))
                    errors.Add("Name is required.");
                else if (Name.Length > 50)
                    errors.Add("Name cannot exceed 50 characters.");
                else if(Name.Length < 10)
                    errors.Add("Name cannot must have at least 10 characters.");
                break;
            case nameof(ClassBookingDaysInAdvanceCount):
                if (ClassBookingDaysInAdvanceCount < 0)
                    errors.Add("Value cannot be negative.");
                break;

            case nameof(FreeFriendEntryCountPerMonth):
                if (FreeFriendEntryCountPerMonth < 0)
                    errors.Add("Value cannot be negative.");
                break;

        }

        if (errors.Any())
            _errors[propertyName] = errors;

        ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
    }

   
    public INavigationService Navigation { get; set; }
    public Guid MembershipId { get; private set; }
    public SidebarViewModel SidebarView { get; set; }

    public bool HasErrors => _errors.Any();

    public MembershipEditViewModel(SidebarViewModel sidebarViewModel, INavigationService navigation, MembershipHttpClient httpClient)
    {
        Navigation = navigation;
        SidebarView = sidebarViewModel;
        CancelCommand = new RelayCommand(item => Navigation.NavigateTo<MembershipViewModel>(), item => true);
        LoadMembershipCommand = new AsyncRelayCommand(item => LoadMembershipForEditByIdAsync(), item => true);
        _httpClient = httpClient;
        UpdateMembershipCommand = new AsyncRelayCommand(UpdateMembershipAsync, item => Name.Length < 50 &&  Name != string.Empty && Name.Length > 10 && ClassBookingDaysInAdvanceCount > 0 && FreeFriendEntryCountPerMonth > 0);
    }

    private async Task LoadMembershipForEditByIdAsync()
    {
        Result<MembershipResponse> result = await _httpClient.GetMembershipByIdAsync(MembershipId);
        Name = result.Value!.Name;
        ClassBookingDaysInAdvanceCount = result.Value!.ClassBookingDaysInAdvanceCount;
        FreeFriendEntryCountPerMonth = result.Value!.FreeFriendEntryCountPerMonth;
    }

    private async Task UpdateMembershipAsync(object arg)
    {
        MembershipUpdateRequest request = new MembershipUpdateRequest()
        {
            Name = Name,
            ClassBookingDaysInAdvanceCount = ClassBookingDaysInAdvanceCount,
            FreeFriendEntryCountPerMonth = FreeFriendEntryCountPerMonth,
        };

        Result<Unit> result = await _httpClient.PutMembershipAsync(request, MembershipId);
        if (result.IsSuccess)
        {
            MessageBox.Show($"Membership is already edited!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            Navigation.NavigateTo<MembershipViewModel>();
        }
        else
        {
            MessageBox.Show($"Error: {result.GetUserMessage()}", "Error during edition", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    public void ReceiveParameter(object parameter)
    {
        if (parameter is Guid membershipId)
        {
            MembershipId = membershipId;
        }
    }

    public IEnumerable GetErrors(string? propertyName)
    {
        if (propertyName != null && _errors.ContainsKey(propertyName))
        {
            return _errors[propertyName];
        }
        return Enumerable.Empty<string>();
    }
}
