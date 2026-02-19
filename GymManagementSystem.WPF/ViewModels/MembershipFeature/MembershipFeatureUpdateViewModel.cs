using GymManagementSystem.Core.DTO.MembershipFeature;
using GymManagementSystem.WPF.Result;
using GymManagementSystem.WPF.Core;
using GymManagementSystem.WPF.HttpServices;
using GymManagementSystem.WPF.ServiceContracts;
using GymManagementSystem.WPF.ViewModels.Membership;
using System.Collections;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace GymManagementSystem.WPF.ViewModels.MembershipFeature;
public class MembershipFeatureUpdateViewModel : ViewModel, IParameterReceiver, INotifyDataErrorInfo
{
    private Guid _membershipFeatureId;
    public SidebarViewModel SidebarView { get; }
    private readonly MembershipHttpClient _membershipHttpClient;
    private readonly INavigationService _navigation;
    public ICommand UpdateMembershipFeatureCommand { get; }
    public ICommand LoadMembershipFeatureCommand { get; }
    public Dictionary<string, List<string>> _errors = new();
    public bool HasErrors => _errors.Any();

    private string _featureDescription = string.Empty;


    public string FeatureDescription
    {
        get { return _featureDescription; }
        set
        {
            _featureDescription = value;
            OnPropertyChanged();
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

    public event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged;

    public IEnumerable GetErrors(string? propertyName)
    {
        if (propertyName != null && _errors.ContainsKey(propertyName))
        {
            return _errors[propertyName];
        }
        return Enumerable.Empty<string>();
    }

    public void ReceiveParameter(object parameter)
    {
        if(parameter is Guid membershipFeatureId)
        {
            _membershipFeatureId = membershipFeatureId;
        }
    }
    public MembershipFeatureUpdateViewModel(SidebarViewModel sidebarView, MembershipHttpClient membershipHttpClient, INavigationService navigation)
    {
        SidebarView = sidebarView;
        _membershipHttpClient = membershipHttpClient;
        _navigation = navigation;
        UpdateMembershipFeatureCommand = new AsyncRelayCommand(item => UpdateMembershipFeatureAsync(), item => FeatureDescription.Length > 10);
        LoadMembershipFeatureCommand = new AsyncRelayCommand(item => LoadMembershipFeatureAsync(), item => true);
    }

    private async Task LoadMembershipFeatureAsync()
    {
        Result<MembershipFeatureForEditResponse> result = await _membershipHttpClient.GetMembershipFeatureForEdit(_membershipFeatureId);
        if (!result.IsSuccess)
        {
            MessageBox.Show($"{result.GetUserMessage()}");
        }
        FeatureDescription = result.Value!.FeatureDescription;
    }

    private async Task UpdateMembershipFeatureAsync()
    {
        MembershipFeatureUpdateRequest request = new MembershipFeatureUpdateRequest()
        {
            MembershipFeatureId = _membershipFeatureId,
            FeatureDescription = FeatureDescription,
        };

        Result<Unit> result = await _membershipHttpClient.UpdateMembershipFeature(request);
        if (!result.IsSuccess)
        {
            MessageBox.Show($"{result.GetUserMessage()}");
        }
        _navigation.NavigateTo<MembershipViewModel>();
    }
}
