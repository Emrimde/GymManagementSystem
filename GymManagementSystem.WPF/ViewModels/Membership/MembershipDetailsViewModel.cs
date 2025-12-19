using GymManagementSystem.Core.DTO.ClientMembership;
using GymManagementSystem.Core.DTO.Membership;
using GymManagementSystem.Core.Result;
using GymManagementSystem.WPF.Core;
using GymManagementSystem.WPF.HttpServices;
using GymManagementSystem.WPF.ServiceContracts;
using GymManagementSystem.WPF.ViewModels.MembershipFeature;
using GymManagementSystem.WPF.ViewModels.MembershipPrice;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace GymManagementSystem.WPF.ViewModels.Membership;

public class MembershipDetailsViewModel : ViewModel, IParameterReceiver
{
    private readonly MembershipHttpClient _membershipHttpClient;
    private INavigationService _navigation;
    private MembershipResponse _membership;
    public ICommand OpenAddMembershipFeatureViewCommand { get; }
    public ICommand OpenMembershipPricesHistory { get; }
    public ICommand OpenAddMembershipPriceCommand { get; }
    public ICommand OpenMembershipFeaturesViewCommand { get; }

    public MembershipResponse Membership
    {
        get { return _membership; }
        set { _membership = value; OnPropertyChanged(); }
    }

    public MembershipDetailsViewModel(MembershipHttpClient membershipHttpClient, INavigationService navigation, SidebarViewModel sidebarView)
    {
        _membershipHttpClient = membershipHttpClient;
        Navigation = navigation;
        SidebarView = sidebarView;
        OpenMembershipPricesHistory = new RelayCommand(item => Navigation.NavigateTo<MembershipPriceViewModel>(Membership.Id), item => true);
        OpenAddMembershipPriceCommand = new RelayCommand(item => Navigation.NavigateTo<MembershipPriceAddViewModel>(Membership.Id), item => true);
        OpenAddMembershipFeatureViewCommand = new RelayCommand(item => Navigation.NavigateTo<MembershipFeatureAddViewModel>(Membership.Id), item => true);
        OpenMembershipFeaturesViewCommand = new RelayCommand(item => Navigation.NavigateTo<MembershipFeatureViewModel>(Membership.Id), item => true);
    }

    public INavigationService Navigation
    {
        get { return _navigation; }
        set { _navigation = value; OnPropertyChanged(); }
    }

    public SidebarViewModel SidebarView { get; }

    

    public void ReceiveParameter(object parameter)
    {
        if (parameter is Guid membershipId)
        {
            _ = LoadMembershipDetailsAsync(membershipId);
        }
    }

    private async Task LoadMembershipDetailsAsync(Guid membershipId)
    {
        Result<MembershipResponse> result = await _membershipHttpClient.GetMembershipByIdAsync(membershipId);
        if(result.IsSuccess && result.Value is not null)
        {
            Membership = result.Value;
        }
        else
        {
            MessageBox.Show($"Error loading membership details: {result.ErrorMessage}");
        }
    }


}
