using GymManagementSystem.Core.DTO.Person;
using GymManagementSystem.Core.Result;
using GymManagementSystem.WPF.Core;
using GymManagementSystem.WPF.HttpServices;
using GymManagementSystem.WPF.ServiceContracts;
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

    public StaffViewModel(SidebarViewModel sidebarView, INavigationService navigation, StaffHttpClient staffHttpClient)
    {
        SidebarView = sidebarView;
        Navigation = navigation;
        _staffHttpClient = staffHttpClient;
        OpenStaffAddView = new RelayCommand(item => Navigation.NavigateTo<StaffAddViewModel>(), item => true);
        People = new ObservableCollection<PersonResponse>();
        _ = LoadPeopleAsync();
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
