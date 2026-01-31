using GymManagementSystem.Core.DTO.MembershipPrice;
using GymManagementSystem.WPF.Result;
using GymManagementSystem.WPF.Core;
using GymManagementSystem.WPF.HttpServices;
using GymManagementSystem.WPF.ServiceContracts;
using GymManagementSystem.WPF.ViewModels.Membership;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Windows;

namespace GymManagementSystem.WPF.ViewModels.MembershipPrice;

public class MembershipPriceViewModel : ViewModel, IParameterReceiver
{
    private readonly MembershipPriceHttpClient _membershipPriceHttpClient;
    public SidebarViewModel SidebarView { get; }

    private ObservableCollection<MembershipPriceResponse> _membershipPrices = new();

    public MembershipPriceViewModel(MembershipPriceHttpClient membershipPriceHttpClient, SidebarViewModel sidebarView, INavigationService navigation)
    {
        _membershipPriceHttpClient = membershipPriceHttpClient;
        SidebarView = sidebarView;
        Navigation = navigation;
        MembershipPrices = new ObservableCollection<MembershipPriceResponse>();
        ReturnCommand = new RelayCommand(item => Navigation.NavigateTo<MembershipDetailsViewModel>(MembershipId), item => true);
        OpenAddMembershipPriceViewCommand = new RelayCommand(item => Navigation.NavigateTo<MembershipPriceAddViewModel>(MembershipId), item => true);
        LoadMembershipPricesCommand = new AsyncRelayCommand(item => LoadMembershipPrices(), item => true);
    }

    public ObservableCollection<MembershipPriceResponse> MembershipPrices
    {
        get { return _membershipPrices; }
        set { _membershipPrices = value; OnPropertyChanged(); }
    }
    public Guid MembershipId { get; set; }

    public ICommand ReturnCommand { get; }
    public ICommand OpenAddMembershipPriceViewCommand { get; }
    public ICommand LoadMembershipPricesCommand { get; }
    public INavigationService Navigation { get; set; }
    

    public void ReceiveParameter(object parameter)
    {
        if(parameter is Guid membershipId)
        {
            MembershipId = membershipId;
        }
    }

    private async Task LoadMembershipPrices()
    {

       Result<ObservableCollection<MembershipPriceResponse>> result = await _membershipPriceHttpClient.GetAllMembershipsPricesAsync(MembershipId);
        if (!result.IsSuccess)
        {
            MessageBox.Show($"{result.GetUserMessage()}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
        MembershipPrices = result.Value!;
    }
}
