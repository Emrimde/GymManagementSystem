using GymManagementSystem.Core.DTO.Client;
using GymManagementSystem.Core.DTO.Employee;
using GymManagementSystem.Core.Enum;
using GymManagementSystem.WPF.Result;
using GymManagementSystem.WPF.Core;
using GymManagementSystem.WPF.HttpServices;
using GymManagementSystem.WPF.ServiceContracts;
using GymManagementSystem.WPF.ViewModels.Staff;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using System.Collections;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
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
            GenerateEmploymentContractPdf(employmentContractPdfDto);
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
                    MessageBox.Show($"Temporary password for this employee {result.Value!.TemporaryPassword}", "Temporary password", MessageBoxButton.OK, MessageBoxImage.Information);
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

    public void GenerateEmploymentContractPdf(EmploymentContractPdfDto employee)
    {
        if (employee == null)
            throw new ArgumentNullException(nameof(employee));

        string employmentTypeText = employee.EmploymentType switch
        {
            EmploymentType.FullTime => "pełny etat",
            EmploymentType.HalfTime => "1/2 etatu",
            EmploymentType.QuarterTime => "1/4 etatu",
            _ => "-"
        };

        string fileName = $"Umowa_{employee.ContractType}_{employee.Role}"
            .Replace(" ", "_")
            .Replace("/", "_");

        string desktop = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        string filePath = Path.Combine(desktop, $"{fileName}.pdf");

        var document = Document.Create(container =>
        {
            container.Page(page =>
            {
                page.Size(PageSizes.A4);
                page.Margin(40);
                page.PageColor(Colors.White);
                page.DefaultTextStyle(x => x.FontSize(11));

                page.Header()
                    .AlignCenter()
                    .Text(employee.ContractType)
                    .FontSize(16)
                    .Bold();

                page.Content().Column(column =>
                {
                    column.Spacing(6);

                    column.Item().Text(
                        $"Zawarta w dniu {employee.ValidFrom} w miejscowości {employee.GymAddress}.");

                    column.Item().PaddingTop(10).Text("1. Strony umowy:");

                    column.Item().Text(
                        $"a) Pracodawca: {employee.GymName}, {employee.GymAddress}, " +
                        $"NIP: {employee.Nip}, tel: {employee.ContactNumber}");

                    column.Item().Text(
                        $"b) Pracownik: {employee.FirstName} {employee.LastName}, " +
                        $"adres: {employee.Address}, " +
                        $"stanowisko: {employee.Role}, " +
                        $"forma zatrudnienia: {employmentTypeText}, " +
                        $"email: {employee.Email}, tel: {employee.PhoneNumber}");

                    column.Item().PaddingTop(10).Text("§1 Przedmiot umowy:");
                    column.Item().Text(
                        "1. Pracodawca zatrudnia Pracownika, a Pracownik zobowiązuje się do wykonywania pracy zgodnie z niniejszą umową.");

                    column.Item().PaddingTop(10).Text("§2 Okres obowiązywania:");
                    column.Item().Text(
                        string.IsNullOrWhiteSpace(employee.ValidTo)
                            ? $"Umowa zostaje zawarta od dnia {employee.ValidFrom}."
                            : $"Umowa zostaje zawarta na czas nieokreślony od {employee.ValidFrom}");

                    column.Item().PaddingTop(10).Text("§3 Wynagrodzenie:");
                    column.Item().Text(
                        $"1. Wynagrodzenie brutto wynosi {employee.Salary}.");

                    column.Item().PaddingTop(10).Text("§4 Czas pracy i obowiązki:");
                    column.Item().Text(
                        "1. Pracownik zobowiązuje się do wykonywania obowiązków zgodnie z zakresem stanowiska oraz regulaminem pracy.");

                    column.Item().PaddingTop(10).Text("§5 Postanowienia końcowe:");
                    column.Item().Text(
                        "1. W sprawach nieuregulowanych niniejszą umową zastosowanie mają przepisy Kodeksu pracy.");

                    column.Item().PaddingTop(30).Row(row =>
                    {
                        row.RelativeItem().Column(left =>
                        {
                            left.Item().Text("Podpis Pracodawcy:");
                            left.Item().PaddingTop(40).Text("_________________________");
                        });

                        row.RelativeItem().Column(right =>
                        {
                            right.Item().Text("Podpis Pracownika:");
                            right.Item().PaddingTop(40).Text("_________________________");
                        });
                    });
                });

                page.Footer()
                    .AlignCenter()
                    .Text(text =>
                    {
                        text.Span($"{employee.Email} | {employee.PhoneNumber} | ");
                        text.CurrentPageNumber();
                        text.Span(" / ");
                        text.TotalPages();
                    });
            });
        });

        try
        {
            document.GeneratePdf(filePath);

            Process.Start(new ProcessStartInfo
            {
                FileName = filePath,
                UseShellExecute = true
            });
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Błąd podczas generowania PDF: {ex.Message}");
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

