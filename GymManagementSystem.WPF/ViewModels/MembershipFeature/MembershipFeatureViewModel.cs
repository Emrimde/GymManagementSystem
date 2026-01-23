using GymManagementSystem.Core.DTO.MembershipFeature;
using GymManagementSystem.WPF.Core;
using GymManagementSystem.WPF.HttpServices;
using GymManagementSystem.WPF.ServiceContracts;
using GymManagementSystem.WPF.ViewModels.Membership;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace GymManagementSystem.WPF.ViewModels.MembershipFeature;
public class MembershipFeatureViewModel : ViewModel, IParameterReceiver
{
    public Guid MembershipId { get; set; }
    public SidebarViewModel SidebarView { get; set; }
    public INavigationService Navigation { get; set; }
    private readonly MembershipHttpClient _membershipHttpCLient;
    private ObservableCollection<MembershipFeatureResponse> _membershipFeatures = new();

    public ObservableCollection<MembershipFeatureResponse>  MembershipFeatures
    {
        get { return _membershipFeatures; }
        set { _membershipFeatures  = value;  OnPropertyChanged(); }
    }
    public ICommand OpenAddMembershipFeatureCommand { get; }
    public ICommand ReturnCommand { get; }

    public MembershipFeatureViewModel(SidebarViewModel sidebarView, INavigationService navigation, MembershipHttpClient membershipHttpCLient)
    {
        SidebarView = sidebarView;
        Navigation = navigation;
        _membershipHttpCLient = membershipHttpCLient;
        OpenAddMembershipFeatureCommand = new RelayCommand(item => Navigation.NavigateTo<MembershipFeatureAddViewModel>(MembershipId), item => true);
        ReturnCommand = new RelayCommand(item => Navigation.NavigateTo<MembershipDetailsViewModel>(MembershipId), item => true);
    }

    public void ReceiveParameter(object parameter)
    {
        if(parameter is Guid id)
        {
            _ = LoadMembershipFeatures(id);
            MembershipId = id;
        }
    }

    private async Task LoadMembershipFeatures(Guid id)
    {
       MembershipFeatures = await _membershipHttpCLient.GetAllMembershipFeaturesByMembershipIdAsync(id);
    }
}
