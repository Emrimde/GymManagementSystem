using GymManagementSystem.Core.DTO.Person;
using GymManagementSystem.WPF.Result;
using GymManagementSystem.WPF.Core;
using GymManagementSystem.WPF.HttpServices;
using GymManagementSystem.WPF.ServiceContracts;
using GymManagementSystem.WPF.ViewModels.Employee;
using GymManagementSystem.WPF.ViewModels.TrainerContract;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
namespace GymManagementSystem.WPF.ViewModels.Staff;

public class StaffViewModel : ViewModel
{
	private ObservableCollection<PersonResponse> _people;

	public ObservableCollection<PersonResponse> People
    {
		get { return _people; }
		set { _people = value; OnPropertyChanged(); }
	}
	public SidebarViewModel SidebarView { get; set; }
	public INavigationService Navigation {  get; set; }
	private readonly StaffHttpClient _staffHttpClient;
    public ICommand OpenStaffAddView { get;  }
    public ICommand OpenEditPersonCommand { get;  }
    public ICommand OpenPersonDetailsCommand { get; set; }

    public StaffViewModel(SidebarViewModel sidebarView, INavigationService navigation, StaffHttpClient staffHttpClient)
    {
        SidebarView = sidebarView;
        Navigation = navigation;
        _staffHttpClient = staffHttpClient;
        OpenStaffAddView = new RelayCommand(item => Navigation.NavigateTo<StaffAddViewModel>(), item => true);
        OpenPersonDetailsCommand = new RelayCommand(item => OpenDetailsAsync(item), item => true);
        OpenEditPersonCommand = new RelayCommand(item => Navigation.NavigateTo<StaffUpdateViewModel>(item!), item=> true);

        People = new ObservableCollection<PersonResponse>();
        _ = LoadPeopleAsync();
    }

    private void OpenDetailsAsync(object parameter)
    {
        if (parameter is PersonResponse response)
        {
            if (response.TrainerContractId != null)
            {
                Navigation.NavigateTo<TrainerContractDetailsViewModel>(response.TrainerContractId);
            }
            else if(response.EmployeeId != null) 
            {
                Navigation.NavigateTo<EmployeeDetailsViewModel>(response.EmployeeId);
            }
            else
            {
                Navigation.NavigateTo<StaffDetailsViewModel>(response.Id);
            }
        }
    }

    private async Task LoadPeopleAsync()
    {
        Result<ObservableCollection<PersonResponse>> result = await _staffHttpClient.GetAllStaffAsync();
        if (!result.IsSuccess)
        {
            MessageBox.Show("Loading staff failed");
            return;
        }
        People = result.Value!;
    }
}
