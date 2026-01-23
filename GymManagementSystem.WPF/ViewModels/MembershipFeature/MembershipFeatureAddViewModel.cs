using GymManagementSystem.Core.DTO.MembershipFeature;
using GymManagementSystem.Core.Result;
using GymManagementSystem.WPF.Core;
using GymManagementSystem.WPF.HttpServices;
using GymManagementSystem.WPF.ServiceContracts;
using GymManagementSystem.WPF.ViewModels.Membership;
using System.Collections;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace GymManagementSystem.WPF.ViewModels.MembershipFeature;
public class MembershipFeatureAddViewModel : ViewModel, IParameterReceiver, INotifyDataErrorInfo
{
    private Guid _membershipId { get; set; }
    public ICommand AddMembershipFeatureCommand { get;  }
    public ICommand CancelCommand { get;  }

    private string _featureDescription = string.Empty;

    public string FeatureDescription
    {
        get { return _featureDescription; }
        set { _featureDescription = value; OnPropertyChanged();
            ValidateProperty(nameof(FeatureDescription));
        }
    }

    private void ValidateProperty(string propertyName)
    {
        _errors.Remove(propertyName);

        var errors = new List<string>();

        switch (propertyName)
        {
            case nameof(FeatureDescription):
                if (string.IsNullOrWhiteSpace(FeatureDescription))
                    errors.Add("Feature description is required.");
                else if (FeatureDescription.Length < 10)
                    errors.Add("Feature description name must have more than 10 letters");
                break;
        }

        if (errors.Any())
            _errors[propertyName] = errors;

        ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
    }

    public INavigationService Navigation { get; set; }
    public SidebarViewModel SidebarView { get; set; }

    public Dictionary<string, List<string>> _errors = new();
    public bool HasErrors => _errors.Any();

    private readonly MembershipHttpClient _membershipHttpClient;

    public event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged;

    public MembershipFeatureAddViewModel(INavigationService navigation, SidebarViewModel sidebarView, MembershipHttpClient membershipHttpClient)
    {
        Navigation = navigation;
        SidebarView = sidebarView;
        _membershipHttpClient = membershipHttpClient;
        AddMembershipFeatureCommand = new AsyncRelayCommand(item => AddMembershipFeatureAsync(), item => FeatureDescription.Length > 10);
        CancelCommand = new RelayCommand(item => Navigation.NavigateTo<MembershipDetailsViewModel>(_membershipId), item => true);

    }
    private async Task AddMembershipFeatureAsync()
    {
        MembershipFeatureAddRequest request = new MembershipFeatureAddRequest()
        {
            FeatureDescription = FeatureDescription,
            MembershipId = _membershipId
        };
        Result<Unit> result = await _membershipHttpClient.PostMembershipFeatureAsync(request);
        if (!result.IsSuccess)
        {
            MessageBox.Show($"{result.ErrorMessage}");
        }
        Navigation.NavigateTo<MembershipFeatureViewModel>(_membershipId);
    }

    public void ReceiveParameter(object parameter)
    {
        if (parameter is Guid id) 
        {
            _membershipId = id; 
        }
    }

    public IEnumerable GetErrors(string? propertyName)
    {
        if(propertyName != null && _errors.ContainsKey(propertyName))
        {
            return _errors[propertyName];
        }
        return Enumerable.Empty<string>();
    }
}
