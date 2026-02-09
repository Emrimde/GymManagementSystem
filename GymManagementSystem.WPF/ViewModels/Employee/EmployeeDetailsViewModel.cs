using GymManagementSystem.Core.DTO.Employee;
using GymManagementSystem.Core.DTO.EmploymentTermination;
using GymManagementSystem.WPF.Core;
using GymManagementSystem.WPF.HttpServices;
using GymManagementSystem.WPF.PdfGenerators;
using GymManagementSystem.WPF.Result;
using GymManagementSystem.WPF.ServiceContracts;
using GymManagementSystem.WPF.ViewModels.Auth;
using System.Windows;
using System.Windows.Input;

namespace GymManagementSystem.WPF.ViewModels.Employee;
public class EmployeeDetailsViewModel : ViewModel, IParameterReceiver
{
    private readonly EmployeeHttpClient _employeeHttpClient;
    private readonly EmploymentTerminationHttpClient _employmentTerminationHttpClient;

    private EmployeeDetailsResponse _employee = new();

    public EmployeeDetailsResponse Employee
    {
        get { return _employee; }
        set { _employee = value; OnPropertyChanged(); }
    }

    public EmployeeDetailsViewModel(EmployeeHttpClient employeeHttpClient, SidebarViewModel sidebarView, INavigationService navigation, EmploymentTerminationHttpClient employmentTerminationHttpClient)
    {
        _employeeHttpClient = employeeHttpClient;
        SidebarView = sidebarView;
        Navigation = navigation;
        GenerateTerminationCommand = new AsyncRelayCommand(item => GenerateEmploymentTerminationAsync(), item => true);
        LoadEmployeeCommand = new AsyncRelayCommand(item => LoadEmployeeAsync(), item => true);
        SetNewPasswordCommand = new RelayCommand(item => Navigation.NavigateTo<SetNewPasswordViewModel>(Employee.PersonId), item => true);
        _employmentTerminationHttpClient = employmentTerminationHttpClient;
        
    }

    private async Task GenerateEmploymentTerminationAsync()
    {
        Result<EmploymentTerminationGenerateResponse> result = await _employmentTerminationHttpClient.GetEmploymentTerminationDetailsAsync(Employee.PersonId);
        if (result.IsSuccess)
        {
            EmploymentTerminationGenerateResponse employmentTerminationDetails = result.Value!;
            employmentTerminationDetails.GenerateEmploymentTerminationPdf();
            MessageBoxResult messageResult = MessageBox.Show("Is termination signed?", "Is signed?", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (messageResult == MessageBoxResult.Yes)
            {
                EmploymentTerminationAddRequest request = new EmploymentTerminationAddRequest
                {
                    PersonId = Employee.PersonId,
                    EffectiveDate = result.Value!.EffectiveDate
                };

                Result<Unit> additionResult = await _employmentTerminationHttpClient.CreateEmploymentTerminationAsync(request);
                if (!additionResult.IsSuccess)
                {
                    MessageBox.Show($"{additionResult.GetUserMessage()}");
                }
            }

            Navigation.NavigateTo<EmployeeDetailsViewModel>(Employee.Id);

        }
        else
        {
            MessageBox.Show($"{result.GetUserMessage()}");
        }
    }

    public SidebarViewModel SidebarView { get; set; }
    public INavigationService Navigation {  get; set; }
    public ICommand GenerateTerminationCommand { get; }
    public ICommand SetNewPasswordCommand { get; }
    public ICommand LoadEmployeeCommand { get; }
    private Guid _employeeId;
    public void ReceiveParameter(object parameter)
    {
        if(parameter is Guid employeeId)
        {
            _employeeId = employeeId;
        }
    }

    private async Task LoadEmployeeAsync()
    {
        Result<EmployeeDetailsResponse> result = await _employeeHttpClient.GetEmployeeByIdAsync(_employeeId);
        if (!result.IsSuccess)
        {
            MessageBox.Show($"{result.GetUserMessage()}");
        }
        Employee = result.Value!;
    }
}
