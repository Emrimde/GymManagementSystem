using GymManagementSystem.Core.DTO.Client;
using GymManagementSystem.Core.Result;
using GymManagementSystem.WPF.Core;
using GymManagementSystem.WPF.HttpServices;
using GymManagementSystem.WPF.ServiceContracts;
using GymManagementSystem.WPF.ViewModels.Client;
using GymManagementSystem.WPF.ViewModels.ClientMembership;
using System.Collections.ObjectModel;
using System.Windows.Documents;
using System.Windows.Input;

namespace GymManagementSystem.WPF.ViewModels;

public class ClientViewModel : ViewModel
{
    private int _currentPage;

    public int CurrentPage
    {
        get { return _currentPage; }
        set
        {
            _currentPage = value; OnPropertyChanged();

            OnPropertyChanged(nameof(CanGoPrevious));
            OnPropertyChanged(nameof(CanGoNext));
            OnPropertyChanged(nameof(VisiblePages));
            OnPropertyChanged(nameof(start));
            OnPropertyChanged(nameof(end));
        }
    }

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

    public bool CanGoNext => CurrentPage < TotalPages;
    public bool CanGoPrevious => CurrentPage > 1;

    private int start => Math.Max(1, CurrentPage - 2);
    private int end => Math.Min(TotalPages, CurrentPage + 2);
    private int count => end - start + 1;

    public List<int> VisiblePages => Enumerable.Range(start, count).ToList();


    public SidebarViewModel SidebarView { get; }
    private INavigationService _navigation;
    public ICommand OpenAddClientView { get; }
    public ICommand OpenEditClientCommand { get; }
    public ICommand OpenClientDetailsCommand { get; }
    public ICommand OpenAddClientMembershipViewCommand { get; }

    private readonly ClientHttpClient _clientHttpClient;
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
            CurrentPage = value;
            if(string.IsNullOrEmpty(SearchText))
            {
                _ = LoadClientsAsync();
            }
            else
            {
                _ = SearchClients();
            }
            OnPropertyChanged();

        }
    }



    private string _searchText;

    public string SearchText
    {
        get { return _searchText; }
        set
        {
            _searchText = value;
            OnPropertyChanged();
        }
    }

    public ICommand SearchClientsCommand { get; }

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


    public ClientViewModel(INavigationService navigationService, SidebarViewModel sidebarView, ClientHttpClient clientHttpClient)
    {
        CurrentPage = 1;
        TotalPages = 1;
        _navigation = navigationService;
        SidebarView = sidebarView;
        _clientHttpClient = clientHttpClient;
        Clients = new ObservableCollection<ClientResponse>();
        _ = LoadClientsAsync();
        OpenAddClientView = new RelayCommand((item) => Navigation.NavigateTo<ClientAddViewModel>(), item => true);
        OpenEditClientCommand = new RelayCommand(
    item =>
    {
        if (item is ClientResponse client)
            _navigation.NavigateTo<ClientUpdateViewModel>(client);
    }, item => true);

        SearchClientsCommand = new AsyncRelayCommand(item => SearchClients(), item => true);

        OpenClientDetailsCommand = new RelayCommand(item =>
        {
            if (item is Guid id)
                Navigation.NavigateTo<ClientDetailsViewModel>(id);
        }, item => true);

      
        OpenAddClientMembershipViewCommand = new RelayCommand(item =>
        


            Navigation.NavigateTo<ClientMembershipAddViewModel>(item), item => true);
    }

    private async Task SearchClients()
    {
        PageResult<ClientResponse> pageResult = await _clientHttpClient.GetAllClientsAsync(SearchText, CurrentPage);
        Clients = new ObservableCollection<ClientResponse>(pageResult.Items);
        CurrentPage = pageResult.CurrentPage;
        TotalPages = pageResult.TotalPages;
    }

    private async Task LoadClientsAsync()
    {
        PageResult<ClientResponse> pageResult = await _clientHttpClient.GetAllClientsAsync(null, CurrentPage);
        Clients = new ObservableCollection<ClientResponse>(pageResult.Items);
        CurrentPage = pageResult.CurrentPage;
        TotalPages = pageResult.TotalPages;
    }
}
