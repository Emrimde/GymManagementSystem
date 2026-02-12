using GymManagementSystem.Core.DTO.Membership;
using GymManagementSystem.WPF.Core;
using GymManagementSystem.WPF.HttpServices;
using GymManagementSystem.WPF.Result;
using GymManagementSystem.WPF.ServiceContracts;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace GymManagementSystem.WPF.ViewModels.Membership;

public class MembershipViewModel : ViewModel
{
    public SidebarViewModel SidebarView { get; }
   
    public ICommand OpenMembershipDetailsCommand { get; set; }
    public ICommand OpenEditMembershipCommand { get; }
    private readonly MembershipHttpClient _membershipHttpClient;
    public INavigationService Navigation { get; set; }

    private ObservableCollection<MembershipResponse> _memberships = new();

    public ObservableCollection<MembershipResponse> Memberships
    {
        get { return _memberships; }
        set
        {
            if (_memberships != value)
            {
                _memberships = value;
                OnPropertyChanged();
            }
        }
    }


    public MembershipViewModel(INavigationService navigationService, SidebarViewModel sidebarView, MembershipHttpClient membershipHttpClient)
    {
        Navigation = navigationService;
        SidebarView = sidebarView;
        _membershipHttpClient = membershipHttpClient;
        Memberships = new ObservableCollection<MembershipResponse>();
        OpenMembershipDetailsCommand = new RelayCommand(item => Navigation.NavigateTo<MembershipDetailsViewModel>(item!), item => true);
        _ = LoadMembershipsAsync();
        OpenEditMembershipCommand = new RelayCommand(item => Navigation.NavigateTo<MembershipEditViewModel>(item!), item => true);
    }

    private async Task LoadMembershipsAsync()
    {
        Result<ObservableCollection<MembershipResponse>> result = await _membershipHttpClient.GetAllMembershipsAsync();
        Memberships = result.Value!;
    }
}
