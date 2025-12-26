using GymManagementSystem.Core.DTO.Membership;
using GymManagementSystem.Core.DTO.MembershipPrice;
using GymManagementSystem.Core.Result;
using GymManagementSystem.WPF.Core;
using GymManagementSystem.WPF.HttpServices;
using GymManagementSystem.WPF.ServiceContracts;
using GymManagementSystem.WPF.ViewModels.Membership;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace GymManagementSystem.WPF.ViewModels.MembershipPrice;
public class MembershipPriceAddViewModel : ViewModel, IParameterReceiver
{
    public MembershipPriceAddViewModel(SidebarViewModel sidebarView, INavigationService navigationService, MembershipPriceHttpClient membershipPriceHttpClient, MembershipHttpClient membershipHttpClient)
    {
        SidebarView = sidebarView;
        Navigation = navigationService;
        MembershipPriceAdd = new MembershipPriceAddRequest();
        _membershipHttpClient = membershipHttpClient;
        _membershipPriceHttpClient = membershipPriceHttpClient;
        AddMembershipPriceCommand = new AsyncRelayCommand(item => AddMembershipPrice(), item => true);
        ReturnToMembershipDetailsCommand = new RelayCommand(item => Navigation.NavigateTo<MembershipDetailsViewModel>(MembershipPriceAdd.MembershipId), item => true);
    }

    private async Task AddMembershipPrice()
    {
        MessageBoxResult messageBoxResult = MessageBox.Show($"Are you sure to set new membership price to {MembershipPriceAdd.Price}?", "Confirm", MessageBoxButton.YesNo, MessageBoxImage.Question);
        if (messageBoxResult == MessageBoxResult.Yes)
        {
            Result<Unit> result = await _membershipPriceHttpClient.PostMembershipPriceAsync(MembershipPriceAdd);
            if (result.IsSuccess)
            {
                Navigation.NavigateTo<MembershipPriceViewModel>(MembershipPriceAdd.MembershipId);
            }
            else
            {
                MessageBox.Show($"{result.ErrorMessage}");
            }
        }



    }
    private MembershipInfoResponse _membership;

    public MembershipInfoResponse Membership
    {
        get { return _membership; }
        set { _membership = value; OnPropertyChanged(); }
    }

    private readonly MembershipPriceHttpClient _membershipPriceHttpClient;
    private readonly MembershipHttpClient _membershipHttpClient;
    public MembershipPriceAddRequest MembershipPriceAdd { get; set; }

    public SidebarViewModel SidebarView { get; set; }

    public INavigationService Navigation { get; set; }
    public ICommand AddMembershipPriceCommand { get; }
    public ICommand ReturnToMembershipDetailsCommand { get; }



    public void ReceiveParameter(object parameter)
    {
        if (parameter is Guid membershipId)
        {
            MembershipPriceAdd.MembershipId = membershipId;
            _ = LoadMembershipNameAsync(membershipId);
        }
    }

    private async Task LoadMembershipNameAsync(Guid membershipId)
    {
        Membership = await _membershipHttpClient.GetMembershipNameAsync(membershipId);       
    }
}
