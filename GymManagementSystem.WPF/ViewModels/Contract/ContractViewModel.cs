using GymManagementSystem.Core.DTO.Contract;
using GymManagementSystem.WPF.Core;
using GymManagementSystem.WPF.HttpServices;
using GymManagementSystem.WPF.ServiceContracts;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace GymManagementSystem.WPF.ViewModels.Contract;

public class ContractViewModel : ViewModel
{
    public SidebarViewModel SidebarView { get; }
    private INavigationService _navigation;
    public ContractUpdateRequest UpdateRequest { get; }
    public ICommand OpenEditMembershipCommand { get; }
    public ICommand OpenContractDetailsCommand { get; }
    public ICommand TerminateContractCommand { get; }
    private readonly ContractHttpClient _contractHttpCLient;
    public INavigationService Navigation
    {
        get { return _navigation; }
        set
        {
            _navigation = value; OnPropertyChanged();
        }
    }

    private ObservableCollection<ContractResponse> _contracts;

    public ObservableCollection<ContractResponse> Contracts
    {
        get { return _contracts; }
        set
        {
            if (_contracts != value)
            {
                _contracts = value;
                OnPropertyChanged();
            }
        }
    }


    public ContractViewModel(INavigationService navigationService, SidebarViewModel sidebarView, ContractHttpClient contractHttpCLient)
    {
        _navigation = navigationService;
        SidebarView = sidebarView;
        _contractHttpCLient = contractHttpCLient;
        Contracts = new ObservableCollection<ContractResponse>();
        _ = LoadContractsAsync();
        OpenContractDetailsCommand = new RelayCommand(item => {

            if (item is Guid id)
                Navigation.NavigateTo<ContractDetailsViewModel>(id);
            else
                MessageBox.Show("Fail to open contract details");
            
            }, item => true);
    }

    private async Task LoadContractsAsync()
    {
        Contracts = await _contractHttpCLient.GetContractsAsync();
    }
}
