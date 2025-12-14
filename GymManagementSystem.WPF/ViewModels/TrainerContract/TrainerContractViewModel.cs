using GymManagementSystem.Core.DTO.TrainerContract;
using GymManagementSystem.Core.Result;
using GymManagementSystem.WPF.Core;
using GymManagementSystem.WPF.HttpServices;
using GymManagementSystem.WPF.ServiceContracts;
using System.Collections.ObjectModel;
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

    public List<int> VisiblePages => Enumerable.Range(start, count).ToList();


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
        _trainerHttpClient = trainerHttpClient;
        SidebarView = sidebarView;
		TrainerContracts = new ObservableCollection<TrainerContractResponse>();
        OpenAddTrainerViewCommand = new RelayCommand(item => Navigation.NavigateTo<TrainerContractAddViewModel>(), item => true);
        OpenTrainerDetailsCommand = new RelayCommand(item => Navigation.NavigateTo<TrainerContractDetailsViewModel>(item), item => true);
        _ = LoadTrainerContracts();
    }

    private async Task LoadTrainerContracts()
    {
        PageResult<TrainerContractResponse> result = await _trainerHttpClient.GetTrainerContracts(null);
            foreach(TrainerContractResponse item in result.Items)
            {
                TrainerContracts.Add(item);
            }
    }

    public SidebarViewModel SidebarView { get; set; }

	public ObservableCollection<TrainerContractResponse> TrainerContracts { get; set; }

	public ICommand OpenAddTrainerViewCommand { get;  }
	public ICommand OpenTrainerDetailsCommand { get;  }
}
