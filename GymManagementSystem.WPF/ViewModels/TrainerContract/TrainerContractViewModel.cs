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
        Result<ObservableCollection<TrainerContractResponse>> result = await _trainerHttpClient.GetTrainerContracts();
        if (!result.IsSuccess) 
        {
            MessageBox.Show($"{result.ErrorMessage}");
        }
        else
        {
            foreach(TrainerContractResponse item in result.Value!)
            {
                TrainerContracts.Add(item);
            }
        }
    }

    public SidebarViewModel SidebarView { get; set; }

	public ObservableCollection<TrainerContractResponse> TrainerContracts { get; set; }

	public ICommand OpenAddTrainerViewCommand { get;  }
	public ICommand OpenTrainerDetailsCommand { get;  }
}
