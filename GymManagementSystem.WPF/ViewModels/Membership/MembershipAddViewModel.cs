using GymManagementSystem.Core.DTO.Client;
using GymManagementSystem.Core.DTO.Membership;
using GymManagementSystem.Core.Enum;
using GymManagementSystem.Core.Result;
using GymManagementSystem.WPF.Core;
using GymManagementSystem.WPF.HttpServices;
using GymManagementSystem.WPF.ServiceContracts;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace GymManagementSystem.WPF.ViewModels.Membership;

public class MembershipAddViewModel : ViewModel
{
    public ObservableCollection<MembershipTypeEnum> MembershipTypes { get; set; }
    private MembershipTypeEnum _selectedmembershipType ;

    public MembershipTypeEnum SelectedMembershipType 
    {
        get { return _selectedmembershipType; }
        set { _selectedmembershipType = value; OnPropertyChanged(); }
    }



    private readonly MembershipHttpClient _httpClient;
    public SidebarViewModel SidebarView { get; set; }
    public ICommand AddMembershipCommand { get; }
    private MembershipAddRequest _membershipAddRequest;
    private INavigationService _navigation;
    public INavigationService Navigation
    {
        get { return _navigation; }
        set
        {
            _navigation = value; OnPropertyChanged();
        }
    }
    public MembershipAddRequest MembershipAddRequest
    {
        get { return _membershipAddRequest; }

        set
        {
            if (_membershipAddRequest != value)
            {
                _membershipAddRequest = value;
                OnPropertyChanged();
            }
        }
    }

    public MembershipAddViewModel(SidebarViewModel sidebarView, MembershipHttpClient httpClient, INavigationService navigation)
    {
        _httpClient = httpClient;
        SidebarView = sidebarView;
        Navigation = navigation;
        MembershipAddRequest = new MembershipAddRequest();
        // Zmień inicjalizację MembershipTypes, aby uniknąć możliwego przekazania null
        MembershipTypes = new ObservableCollection<MembershipTypeEnum>(
            Enum.GetValues(typeof(MembershipTypeEnum)).Cast<MembershipTypeEnum>()
        );
        MembershipTypes = new ObservableCollection<MembershipTypeEnum>(Enum.GetValues(typeof(MembershipTypeEnum)) as MembershipTypeEnum[]);
        AddMembershipCommand = new AsyncRelayCommand(AddMembershipAsync, item => true);
    }

    private async Task AddMembershipAsync(object arg)
    {
        MembershipAddRequest.MembershipType = SelectedMembershipType;
        Result<MembershipResponse> result = await _httpClient.PostMembershipAsync(MembershipAddRequest);
        if (result.IsSuccess)
        {
            string name = result.Value!.Name;

            MessageBox.Show($"Success, membership {name} is already created", null, MessageBoxButton.OK);
            Navigation.NavigateTo<MembershipViewModel>();
        }
        else
        {
            MessageBox.Show($"{result.ErrorMessage}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
