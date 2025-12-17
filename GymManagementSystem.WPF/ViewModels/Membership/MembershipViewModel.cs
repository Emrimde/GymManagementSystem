using GymManagementSystem.Core.DTO.Membership;
using GymManagementSystem.WPF.Core;
using GymManagementSystem.WPF.HttpServices;
using GymManagementSystem.WPF.ServiceContracts;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace GymManagementSystem.WPF.ViewModels.Membership;

public class MembershipViewModel : ViewModel
{
    public SidebarViewModel SidebarView { get; }
    private INavigationService _navigation;
    public ICommand OpenMembershipDetailsCommand { get; set; }
    public ICommand OpenAddMembershipView { get; }
    public ICommand OpenEditMembershipCommand { get; }
    private readonly MembershipHttpClient _membershipHttpClient;
    public INavigationService Navigation
    {
        get { return _navigation; }
        set
        {
            _navigation = value; OnPropertyChanged();
        }
    }

    private ObservableCollection<MembershipResponse> _memberships;

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
        _navigation = navigationService;
        SidebarView = sidebarView;
        _membershipHttpClient = membershipHttpClient;
        Memberships = new ObservableCollection<MembershipResponse>();
        OpenMembershipDetailsCommand = new RelayCommand(item => Navigation.NavigateTo<MembershipDetailsViewModel>(item), item => true);
        _ = LoadMembershipsAsync();
        OpenAddMembershipView = new RelayCommand((item) => Navigation.NavigateTo<MembershipAddViewModel>(), item => true);
        OpenEditMembershipCommand = new RelayCommand(
    item =>
    {
        if (item is MembershipResponse membership)
            _navigation.NavigateTo<MembershipEditViewModel>(membership);
    },
    item => true);
    }

    private async Task LoadMembershipsAsync()
    {
        Memberships = (ObservableCollection<MembershipResponse>)await _membershipHttpClient.GetAllMembershipsAsync();
    }
}
