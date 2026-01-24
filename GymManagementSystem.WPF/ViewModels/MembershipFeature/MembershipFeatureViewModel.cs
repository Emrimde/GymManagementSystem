using GymManagementSystem.Core.DTO.MembershipFeature;
using GymManagementSystem.Core.Result;
using GymManagementSystem.WPF.Core;
using GymManagementSystem.WPF.HttpServices;
using GymManagementSystem.WPF.ServiceContracts;
using GymManagementSystem.WPF.ViewModels.Membership;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace GymManagementSystem.WPF.ViewModels.MembershipFeature;
public class MembershipFeatureViewModel : ViewModel, IParameterReceiver
{
    public Guid MembershipId { get; set; }
    public SidebarViewModel SidebarView { get; set; }
    public INavigationService Navigation { get; set; }
    private readonly MembershipHttpClient _membershipHttpCLient;
    private ObservableCollection<MembershipFeatureResponse> _membershipFeatures = new();

    public ObservableCollection<MembershipFeatureResponse> MembershipFeatures
    {
        get { return _membershipFeatures; }
        set { _membershipFeatures = value; OnPropertyChanged(); }
    }
    public ICommand OpenAddMembershipFeatureCommand { get; }
    public ICommand ReturnCommand { get; }
    public ICommand DeleteMembershipFeatureCommand { get; }
    public ICommand OpenEditMembershipFeatureCommand { get; }

    public MembershipFeatureViewModel(SidebarViewModel sidebarView, INavigationService navigation, MembershipHttpClient membershipHttpCLient)
    {
        SidebarView = sidebarView;
        Navigation = navigation;
        _membershipHttpCLient = membershipHttpCLient;
        OpenAddMembershipFeatureCommand = new RelayCommand(item => Navigation.NavigateTo<MembershipFeatureAddViewModel>(MembershipId), item => true);
        OpenEditMembershipFeatureCommand = new RelayCommand(item => Navigation.NavigateTo<MembershipFeatureUpdateViewModel>(item!), item => true);
        ReturnCommand = new RelayCommand(item => Navigation.NavigateTo<MembershipDetailsViewModel>(MembershipId), item => true);
        DeleteMembershipFeatureCommand = new AsyncRelayCommand(item => DeleteMembershipFeatureAsync(item), item => true);
    }

    private async Task DeleteMembershipFeatureAsync(object item)
    {
        MessageBoxResult mbResult = MessageBox.Show("Are you sure you want to permanently delete this feature? The membership description on the website will be updated.", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Warning);
        if (mbResult == MessageBoxResult.Yes)
        {
            if (item is Guid membershipFeatureId)
            {
                Result<Unit> result = await _membershipHttpCLient.DeleteMembershipFeatureAsync(membershipFeatureId);
                if (!result.IsSuccess)
                {
                    MessageBox.Show($"{result.ErrorMessage}", "Error during deleting", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                Navigation.NavigateTo<MembershipFeatureViewModel>(MembershipId);
            }
        }
    }

    public void ReceiveParameter(object parameter)
    {
        if (parameter is Guid id)
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
