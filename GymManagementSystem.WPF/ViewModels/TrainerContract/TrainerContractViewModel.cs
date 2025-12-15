using GymManagementSystem.Core.DTO.TrainerContract;
using GymManagementSystem.Core.Result;
using GymManagementSystem.WPF.Core;
using GymManagementSystem.WPF.HttpServices;
using GymManagementSystem.WPF.ServiceContracts;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace GymManagementSystem.WPF.ViewModels.TrainerContract;
public class TrainerContractViewModel : ViewModel
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

    public ICommand SearchTrainerContractsCommand { get; }

    public List<int> VisiblePages => Enumerable.Range(start, count).ToList();

    private int _selectedPage;

    public int SelectedPage
    {
        get { return _selectedPage; }
        set
        {
            if (_selectedPage == value) return;
            _selectedPage = value;
            CurrentPage = value;
            if (string.IsNullOrEmpty(SearchText))
            {
                _ = LoadTrainerContracts();
            }
            else
            {
                _ = SearchTrainerContracts();
            }
            OnPropertyChanged();

        }
    }

    private string _searchText;

    public string SearchText
    {
        get { return _searchText; }
        set { _searchText = value; OnPropertyChanged(); }
    }



    private async Task SearchTrainerContracts()
    {
        PageResult<TrainerContractResponse> pageResult = await _trainerHttpClient.GetTrainerContracts(SearchText, CurrentPage);
        TrainerContracts = new ObservableCollection<TrainerContractResponse>(pageResult.Items);
        CurrentPage = pageResult.CurrentPage;
        TotalPages = pageResult.TotalPages;
    }

    private INavigationService _navigation;

    public INavigationService Navigation
    {
        get { return _navigation; }
        set { _navigation = value; OnPropertyChanged(); }
    }

    private readonly TrainerHttpClient _trainerHttpClient;

    public TrainerContractViewModel(INavigationService navigation, TrainerHttpClient trainerHttpClient, SidebarViewModel sidebarView)
    {
        Navigation = navigation;
        CurrentPage = 1;
        TotalPages = 1;
        _trainerHttpClient = trainerHttpClient;
        SidebarView = sidebarView;
        SearchTrainerContractsCommand = new AsyncRelayCommand(item => SearchTrainerContracts(), item => true);
        TrainerContracts = new ObservableCollection<TrainerContractResponse>();
        OpenAddTrainerViewCommand = new RelayCommand(item => Navigation.NavigateTo<TrainerContractAddViewModel>(), item => true);
        OpenTrainerDetailsCommand = new RelayCommand(item => Navigation.NavigateTo<TrainerContractDetailsViewModel>(item), item => true);
        _ = LoadTrainerContracts();
    }

    private async Task LoadTrainerContracts()
    {
        PageResult<TrainerContractResponse> result = await _trainerHttpClient.GetTrainerContracts(null, CurrentPage);
        TrainerContracts = new ObservableCollection<TrainerContractResponse>(result.Items);
        CurrentPage = result.CurrentPage;
        TotalPages = result.TotalPages;

    }

    public SidebarViewModel SidebarView { get; set; }

    private ObservableCollection<TrainerContractResponse> _trainerContracts;
    public ObservableCollection<TrainerContractResponse> TrainerContracts
    {
        get
        { return _trainerContracts; }
        set
        {
            if (_trainerContracts != value)
            {
                _trainerContracts = value;
                OnPropertyChanged();
            }
        }
    }

    public ICommand OpenAddTrainerViewCommand { get; }
    public ICommand OpenTrainerDetailsCommand { get; }
}
