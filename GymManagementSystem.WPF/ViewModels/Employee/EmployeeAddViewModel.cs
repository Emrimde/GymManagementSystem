using GymManagementSystem.Core.DTO.Employee;
using GymManagementSystem.Core.Enum;
using GymManagementSystem.Core.Result;
using GymManagementSystem.WPF.Core;
using GymManagementSystem.WPF.HttpServices;
using GymManagementSystem.WPF.ServiceContracts;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace GymManagementSystem.WPF.ViewModels.Employee;
public class EmployeeAddViewModel : ViewModel
{
    private readonly EmployeeHttpClient _employeeHttpClient;
    private EmployeeAddRequest _employee;

    public EmployeeAddRequest Employee
    {
        get { return _employee; }
        set { _employee = value; OnPropertyChanged(); }
    }

    private bool _isTrainer ;

    public bool IsTrainer 
    {
        get { return _isTrainer; }
        set { _isTrainer = value; OnPropertyChanged(); }
    }


    public ObservableCollection<EmployeeRole> EmployeeRoles { get; set; }
    private EmployeeRole _selectedEmployeeRole;

    public EmployeeRole SelectedEmployeeRole
    {
        get { return _selectedEmployeeRole; }
        set { 
            if(_selectedEmployeeRole != value)
            {
                _selectedEmployeeRole = value;
                Employee.EmployeeRole = _selectedEmployeeRole;
                if(_selectedEmployeeRole == EmployeeRole.Trainer)
                {
                    IsTrainer = true;
                }
                else
                {
                    IsTrainer = false;
                }
                OnPropertyChanged();
            }
        }
    }


    public ObservableCollection<EmploymentType> EmploymentTypes { get; set; }
    private EmploymentType _selectedEmploymentType;


    public EmploymentType SelectedEmploymentType
    {
        get { return _selectedEmploymentType; }
        set
        {
            if (_selectedEmploymentType != value)
            {
                _selectedEmploymentType = value; 
                Employee.EmploymentType = _selectedEmploymentType; 
                OnPropertyChanged();
            }
        }
    }


    private INavigationService _navigation;

    public INavigationService Navigation
    {
        get { return _navigation; }
        set { _navigation = value; OnPropertyChanged(); }
    }


    public SidebarViewModel SidebarView { get; set; }


    public ICommand AddEmployeeCommand { get; }
    public EmployeeAddViewModel(EmployeeHttpClient employeeHttpClient, SidebarViewModel sidebarView, INavigationService navigation)
    {
        _employeeHttpClient = employeeHttpClient;
        SidebarView = sidebarView;
        _navigation = navigation;
        Employee = new EmployeeAddRequest();
        EmploymentTypes = new ObservableCollection<EmploymentType>(Enum.GetValues<EmploymentType>().Cast<EmploymentType>());
        EmployeeRoles = new ObservableCollection<EmployeeRole>(Enum.GetValues<EmployeeRole>().Cast<EmployeeRole>());
        AddEmployeeCommand = new AsyncRelayCommand(item => AddEmployeeAsync(), item => true);
    }

    private async Task AddEmployeeAsync()
    {

        Result<EmployeeInfoResponse> result = await _employeeHttpClient.PostEmployeeAsync(Employee);
        if (result.IsSuccess)
        {
            Navigation.NavigateTo<EmployeeViewModel>();
        }
        else
        {
            MessageBox.Show($"{result.ErrorMessage}");
        }

    }
}
