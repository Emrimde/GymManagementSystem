
using GymManagementSystem.Core.DTO.Membership;
using GymManagementSystem.Core.Result;
using GymManagementSystem.WPF.Core;
using GymManagementSystem.WPF.HttpServices;
using GymManagementSystem.WPF.ServiceContracts;
using System.Windows;
using System.Windows.Input;

namespace GymManagementSystem.WPF.ViewModels.Membership;

public class MembershipEditViewModel : ViewModel,IParameterReceiver
{
    private INavigationService _navigation;
    private MembershipUpdateRequest _membershipUpdateRequest;

    private readonly MembershipHttpClient _httpClient;
    public ICommand UpdateMembershipCommand { get; }

    public MembershipUpdateRequest MembershipUpdateRequest
    {
        get { return _membershipUpdateRequest; }
        set
        {
            if (_membershipUpdateRequest != value)
            {
                _membershipUpdateRequest = value;
                OnPropertyChanged();
            }
        }
    }
    public INavigationService Navigation
    {
        get { return _navigation; }
        set { _navigation = value; OnPropertyChanged(); }
    }
    public Guid MembershipId { get; private set; }
    public SidebarViewModel SidebarView { get; set; }
    public MembershipEditViewModel(SidebarViewModel sidebarViewModel, INavigationService navigation, MembershipHttpClient httpClient)
    {
        Navigation = navigation;
        SidebarView = sidebarViewModel;
        MembershipUpdateRequest = new MembershipUpdateRequest();
        _httpClient = httpClient;
        UpdateMembershipCommand = new AsyncRelayCommand(UpdateMembershipAsync, item => true);
    }

    private async Task UpdateMembershipAsync(object arg)
    {
        Result<MembershipResponse> result = await _httpClient.PutMembershipAsync(MembershipUpdateRequest, MembershipId);
        if (result.IsSuccess)
        {
            MessageBox.Show($"Membership {result.Value!.Name} is already edited!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            Navigation.NavigateTo<MembershipViewModel>();
        }
        else
        {
            MessageBox.Show($"Error: {result.ErrorMessage}", "Error during edition", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    public void ReceiveParameter(object parameter)
    {
        if (parameter is MembershipResponse membership)
        {
            MembershipId = membership.Id;
            MembershipUpdateRequest.Name = membership.Name;
            MembershipUpdateRequest.Price = membership.Price;
        }
    }
}
