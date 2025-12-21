using GymManagementSystem.Core.Result;
using GymManagementSystem.WPF.Core;
using GymManagementSystem.WPF.HttpServices;
using GymManagementSystem.WPF.ServiceContracts;
using System.Threading.Tasks;

namespace GymManagementSystem.WPF.ViewModels.Employee;
public class EmployeeDetailsViewModel : ViewModel, IParameterReceiver
{
    private readonly EmployeeHttpClient _employeeHttpClient;
    private EmployeeDetailsViewModel _employee;

    public EmployeeDetailsViewModel Employee
    {
        get { return _employee; }
        set { _employee = value; OnPropertyChanged(); }
    }

    public EmployeeDetailsViewModel(EmployeeHttpClient employeeHttpClient, SidebarViewModel sidebarView, INavigationService navigation)
    {
        _employeeHttpClient = employeeHttpClient;
        SidebarView = sidebarView;
        Navigation = navigation;
    }

    public SidebarViewModel SidebarView { get; set; }
    public INavigationService Navigation {  get; set; }

    public void ReceiveParameter(object parameter)
    {
        if(parameter is Guid employeeId)
        {
            _ = LoadEmployeeAsync(employeeId);
        }
    }

    private async Task<object> LoadEmployeeAsync(Guid employeeId)
    {
        Result<EmployeeDetailsResponse> result = await _employeeHttpClient.GetEmployeeByIdAsync(employeeId);
    }
}
