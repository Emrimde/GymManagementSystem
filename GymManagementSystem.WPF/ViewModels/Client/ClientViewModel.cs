using GymManagementSystem.Core.DTO.Client;
using GymManagementSystem.WPF.Result;
using GymManagementSystem.Core.Resulttttt;
using GymManagementSystem.WPF.Core;
using GymManagementSystem.WPF.HttpServices;
using GymManagementSystem.WPF.ServiceContracts;
using GymManagementSystem.WPF.ViewModels.ClientMembership;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace GymManagementSystem.WPF.ViewModels.Client;

public class ClientViewModel : ViewModel
{
    private readonly ClientHttpClient _clientHttpClient;
    private INavigationService _navigation;
    private int _totalPages;

    public int TotalPages
    {
        get { return _totalPages; }
        set
        {
            _totalPages = value; OnPropertyChanged();

            OnPropertyChanged(nameof(CanGoNext));
            OnPropertyChanged(nameof(VisiblePages));
        }
    }

    public bool CanGoNext => SelectedPage < TotalPages;
    public bool CanGoPrevious => SelectedPage > 1;

    private int start => Math.Max(1, SelectedPage - 2);
    private int end => Math.Min(TotalPages, SelectedPage + 2);
    private int count => end - start + 1;
    public List<int> VisiblePages => Enumerable.Range(start, count).ToList();

    public SidebarViewModel SidebarView { get; }
    public ICommand OpenAddClientView { get; }
    public ICommand OpenEditClientCommand { get; }
    public ICommand OpenClientDetailsCommand { get; }
    public ICommand OpenAddClientMembershipViewCommand { get; }

    public INavigationService Navigation
    {
        get { return _navigation; }
        set
        {
            _navigation = value; OnPropertyChanged();
        }
    }

    private int _selectedPage;

    public int SelectedPage
    {
        get { return _selectedPage; }
        set {
            if (_selectedPage == value) return;
            _selectedPage = value;
            OnPropertyChanged();
            OnPropertyChanged(nameof(CanGoNext));
            OnPropertyChanged(nameof(CanGoPrevious));
            OnPropertyChanged(nameof(VisiblePages));
        }
    }


    private string _searchText;

    public string SearchText
    {
        get => _searchText;
        set
        {
            if (_searchText == value) return;
            _searchText = value;
            OnPropertyChanged();
            SelectedPage = 1;  
        }
    }

    public ICommand LoadClientsCommand { get; }

    private ObservableCollection<ClientResponse> _clients;

    public ObservableCollection<ClientResponse> Clients
    {
        get { return _clients; }
        set
        {
            if (_clients != value)
            {
                _clients = value;
                OnPropertyChanged();
            }
        }
    }

    private async Task SearchClientsAsync()
    {
        Result<PageResult<ClientResponse>> pageResult = await _clientHttpClient.GetAllClientsAsync(SearchText, SelectedPage);
        Clients = new ObservableCollection<ClientResponse>(pageResult.Value!.Items);
        TotalPages = pageResult.Value!.TotalPages;
    }

    private async Task GetAllClientsAsync()
    {
        Result<PageResult<ClientResponse>> pageResult = await _clientHttpClient.GetAllClientsAsync(null, SelectedPage);
        Clients = new ObservableCollection<ClientResponse>(pageResult.Value!.Items);
        TotalPages = pageResult.Value!.TotalPages;
    }
    public ClientViewModel(INavigationService navigationService, SidebarViewModel sidebarView, ClientHttpClient clientHttpClient)
    {
        _searchText = string.Empty;
        _selectedPage = 1;
        _totalPages = 1;
        _navigation = navigationService;
        SidebarView = sidebarView;
        _clientHttpClient = clientHttpClient;
        Clients = new ObservableCollection<ClientResponse>();

        OpenAddClientView = new RelayCommand((item) => Navigation.NavigateTo<ClientAddViewModel>(), item => true);
        OpenEditClientCommand = new RelayCommand(item => _navigation.NavigateTo<ClientUpdateViewModel>(item!), item => true);
        LoadClientsCommand = new AsyncRelayCommand(item => LoadClientsAsync(item), item => true);
        OpenClientDetailsCommand = new RelayCommand(item => Navigation.NavigateTo<ClientDetailsViewModel>(item!), item => true);
        OpenAddClientMembershipViewCommand = new RelayCommand(item => Navigation.NavigateTo<ClientMembershipAddViewModel>(item!), item => true);
    }

    private async Task LoadClientsAsync(object item)
    {
        if(item is int selectedPage)
        {
            SelectedPage = selectedPage;
        }
        if(string.IsNullOrEmpty(SearchText))
        {
           await GetAllClientsAsync();
        }
        else
        {
            await SearchClientsAsync();
        }
    }
}
