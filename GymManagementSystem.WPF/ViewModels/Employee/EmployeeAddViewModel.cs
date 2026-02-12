using GymManagementSystem.Core.DTO.Employee;
using GymManagementSystem.Core.Enum;
using GymManagementSystem.WPF.Core;
using GymManagementSystem.WPF.HttpServices;
using GymManagementSystem.WPF.PdfGenerators;
using GymManagementSystem.WPF.Result;
using GymManagementSystem.WPF.ServiceContracts;
using GymManagementSystem.WPF.ViewModels.Staff;
using System.Collections;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace GymManagementSystem.WPF.ViewModels.Employee;
public class EmployeeAddViewModel : ViewModel, IParameterReceiver, INotifyDataErrorInfo
{
    private readonly EmployeeHttpClient _employeeHttpClient;

    public ObservableCollection<EmployeeRole> EmployeeRoles => [EmployeeRole.Manager, EmployeeRole.Receptionist];
    private EmployeeRole? _selectedEmployeeRole;

    public EmployeeRole? SelectedEmployeeRole
    {
        get { return _selectedEmployeeRole; }
        set
        {
            if (_selectedEmployeeRole != value)
            {
                _selectedEmployeeRole = value;
                OnPropertyChanged();
            }
        }
    }

    public ICommand ReturnToStaffViewCommand { get; }

    public ObservableCollection<EmploymentType> EmploymentTypes => [EmploymentType.FullTime, EmploymentType.HalfTime , EmploymentType.QuarterTime];

    private EmploymentType? _selectedEmploymentType;

    public EmploymentType? SelectedEmploymentType
    {
        get { return _selectedEmploymentType; }
        set
        {
            if (_selectedEmploymentType != value)
            {
                _selectedEmploymentType = value;
                OnPropertyChanged();
            }
        }
    }


    private int? _monthlySalaryBrutto;

    public int? MonthlySalaryBrutto
    {
        get { return _monthlySalaryBrutto; }
        set
        {
            _monthlySalaryBrutto = value;
            OnPropertyChanged();
            ValidateMonthlySalary();
        }
    }

    public INavigationService Navigation { get; }

    public SidebarViewModel SidebarView { get; set; }


    public ICommand AddEmployeeCommand { get; }
    public EmployeeAddViewModel(EmployeeHttpClient employeeHttpClient, SidebarViewModel sidebarView, INavigationService navigation)
    {
        _employeeHttpClient = employeeHttpClient;
        SidebarView = sidebarView;
        Navigation = navigation;
        ReturnToStaffViewCommand = new RelayCommand(item => Navigation.NavigateTo<StaffViewModel>(), item => true);
        AddEmployeeCommand = new AsyncRelayCommand(item => AddEmployeeAsync(), item => CanAddEmployee());
    }

    private bool CanAddEmployee()
    {
        return SelectedEmploymentType != null && SelectedEmployeeRole != null && !HasErrors;
    }

    private async Task AddEmployeeAsync()
    {
        EmployeeContractRequest contractRequest = new EmployeeContractRequest
        {
            EmploymentType = SelectedEmploymentType!.Value,
            MonthlySalaryBrutto = MonthlySalaryBrutto!.Value,
            PersonId = _personId,
            Role = SelectedEmployeeRole!.Value
        };

        Result<EmploymentContractPdfDto> validationResult = await _employeeHttpClient.GetEmployeeContractAsync(contractRequest);
        if (!validationResult.IsSuccess)
        {
            MessageBox.Show($"{validationResult.GetUserMessage()}");
        }
        else
        {
            EmploymentContractPdfDto employmentContractPdfDto = validationResult.Value!;
            ContractGenerator.GenerateEmploymentContractPdf(employmentContractPdfDto);
            MessageBoxResult isSigned = MessageBox.Show("Is trainer contract signed?", "Confirmation",
    MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (isSigned == MessageBoxResult.Yes)
            {
                EmployeeAddRequest employeeAddRequest = new EmployeeAddRequest()
                {
                    EmploymentType = employmentContractPdfDto.EmploymentType,
                    MonthlySalaryBrutto = MonthlySalaryBrutto.Value,
                    PersonId = _personId,
                    Role = employmentContractPdfDto.Role,
                };

                Result<EmployeeInfoResponse> result = await _employeeHttpClient.PostEmployeeAsync(employeeAddRequest);
                if (result.IsSuccess)
                {
                    Clipboard.SetText(result.Value!.TemporaryPassword);

                    MessageBox.Show(
                        "Temporary password has been copied to clipboard.",
                        $"Temporary password - {result.Value!.TemporaryPassword}",
                        MessageBoxButton.OK,
                        MessageBoxImage.Information);
                    Navigation.NavigateTo<EmployeeDetailsViewModel>(result.Value!.EmployeeId);
                }
                else
                {
                    MessageBox.Show($"{result.GetUserMessage()}");
                }
            }
            else
            {
                Navigation.NavigateTo<StaffViewModel>();
            }
        }

    }

    private Guid _personId;
    public void ReceiveParameter(object parameter)
    {
        if (parameter is Guid personId)
        {
            _personId = personId;
        }
    }

    private readonly Dictionary<string, List<string>> _errors = new();

    public bool HasErrors => _errors.Any();
    public event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged;

    public IEnumerable GetErrors(string? propertyName)
    {
        if (propertyName != null && _errors.ContainsKey(propertyName))
            return _errors[propertyName];
        return Enumerable.Empty<string>();
    }

    private void ValidateMonthlySalary()
    {
        _errors.Remove(nameof(MonthlySalaryBrutto));

        if (MonthlySalaryBrutto == null || MonthlySalaryBrutto <= 0)
        {
            _errors[nameof(MonthlySalaryBrutto)] =
            [
                "Salary mu be bigger than 0"
            ];
        }

        ErrorsChanged?.Invoke(this,
            new DataErrorsChangedEventArgs(nameof(MonthlySalaryBrutto)));

        ((AsyncRelayCommand)AddEmployeeCommand)
                .RaiseCanExecuteChanged();
    }

}

