using GymManagementSystem.Core.DTO.Employee;
using GymManagementSystem.Core.Result;
using GymManagementSystem.WPF.Core;
using GymManagementSystem.WPF.HttpServices;
using GymManagementSystem.WPF.ServiceContracts;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace GymManagementSystem.WPF.ViewModels.Employee;
public class EmployeeViewModel : ViewModel
{
    private readonly EmployeeHttpClient _employeeHttpClient;
	private INavigationService _navigation;
    public INavigationService Navigation
	{
		get { return _navigation; }
		set { _navigation = value; }
	}
	public SidebarViewModel SidebarView { get; set; }
	public ObservableCollection<EmployeeResponse> Employees { get; set; }

    public ICommand OpenAddEmployeeViewCommand { get; }

    public EmployeeViewModel(EmployeeHttpClient employeeHttpClient, INavigationService navigation, SidebarViewModel sidebar)
    {
        _employeeHttpClient = employeeHttpClient;
        Navigation = navigation;
        SidebarView = sidebar;
        OpenAddEmployeeViewCommand = new RelayCommand(item => OpenAddEmployeeView(), item => true);
        Employees = new ObservableCollection<EmployeeResponse>();
        _ = LoadEmployees();
    }

    private void OpenAddEmployeeView()
    {
        Navigation.NavigateTo<EmployeeAddViewModel>();
    }

    private async Task LoadEmployees()
    {
      Result<ObservableCollection<EmployeeResponse>> result = await _employeeHttpClient.GetAllEmployeesAsync();
        if (result.IsSuccess) 
        {
            foreach (EmployeeResponse employee in result.Value!) 
            {
                Employees.Add(employee);
            }
        }
        else
        {
            MessageBox.Show($"{result.ErrorMessage}");
        }
    }
}
