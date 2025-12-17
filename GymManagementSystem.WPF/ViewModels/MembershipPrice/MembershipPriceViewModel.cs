using GymManagementSystem.Core.DTO.MembershipPrice;
using GymManagementSystem.Core.Result;
using GymManagementSystem.WPF.Core;
using GymManagementSystem.WPF.HttpServices;
using GymManagementSystem.WPF.ServiceContracts;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace GymManagementSystem.WPF.ViewModels.MembershipPrice;

public class MembershipPriceViewModel : ViewModel, IParameterReceiver
{
    private readonly MembershipPriceHttpClient _membershipPriceHttpClient;
    public SidebarViewModel SidebarView { get; }
    private INavigationService _navigation;

    private ObservableCollection<MembershipPriceResponse> _membershipPrices;

    public MembershipPriceViewModel(MembershipPriceHttpClient membershipPriceHttpClient, SidebarViewModel sidebarView, INavigationService navigation)
    {
        _membershipPriceHttpClient = membershipPriceHttpClient;
        SidebarView = sidebarView;
        Navigation = navigation;
        MembershipPrices = new ObservableCollection<MembershipPriceResponse>();
    }

    public ObservableCollection<MembershipPriceResponse> MembershipPrices
    {
        get { return _membershipPrices; }
        set { _membershipPrices = value; OnPropertyChanged(); }
    }


    public INavigationService Navigation
    {
        get { return _navigation; }
        set { _navigation = value; OnPropertyChanged(); }
    }

    public void ReceiveParameter(object parameter)
    {
        if(parameter is Guid id)
        {
            _ = LoadMembershipPrices(id);
        }
    }

    private async Task LoadMembershipPrices(Guid id)
    {
        MembershipPrices = await _membershipPriceHttpClient.GetAllMembershipsAsync(id);
    }
}
