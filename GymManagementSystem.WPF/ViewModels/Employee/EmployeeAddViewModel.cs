using GymManagementSystem.Core.DTO.Employee;
using GymManagementSystem.Core.Enum;
using GymManagementSystem.Core.Result;
using GymManagementSystem.WPF.Core;
using GymManagementSystem.WPF.HttpServices;
using GymManagementSystem.WPF.ServiceContracts;
using GymManagementSystem.WPF.ViewModels.Staff;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Input;


namespace GymManagementSystem.WPF.ViewModels.Employee;
public class EmployeeAddViewModel : ViewModel, IParameterReceiver
{
    private readonly EmployeeHttpClient _employeeHttpClient;

    private EmployeeAddRequest _employee = new();

    public EmployeeAddRequest Employee
    {
        get { return _employee; }
        set { _employee = value; OnPropertyChanged(); }
    }


    public IEnumerable<ContractTypeEnum> AvailableContractsForEmployees =>
    [
        ContractTypeEnum.Probation,
        ContractTypeEnum.Permanent
    ];

    public ObservableCollection<EmployeeRole> EmployeeRoles { get; set; }
    private EmployeeRole _selectedEmployeeRole;

    public EmployeeRole SelectedEmployeeRole
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


    private ContractTypeEnum _selectedContractType = ContractTypeEnum.Permanent;

    public ContractTypeEnum SelectedContractType  
    {
        get { return _selectedContractType; }
        set
        {
            if (_selectedContractType != value)
            {
                _selectedContractType = value;
                Employee.ContractTypeEnum = _selectedContractType;
                OnPropertyChanged();
            }
        }
    }

    public ICommand ReturnToStaffViewCommand { get;  }

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

    public INavigationService Navigation { get; }

    public SidebarViewModel SidebarView { get; set; }


    public ICommand AddEmployeeCommand { get; }
    public EmployeeAddViewModel(EmployeeHttpClient employeeHttpClient, SidebarViewModel sidebarView, INavigationService navigation)
    {
        _employeeHttpClient = employeeHttpClient;
        SidebarView = sidebarView;
        Navigation = navigation;
        ReturnToStaffViewCommand = new RelayCommand(item => Navigation.NavigateTo<StaffViewModel>(), item => true);
        EmploymentTypes = new ObservableCollection<EmploymentType>(Enum.GetValues<EmploymentType>().Cast<EmploymentType>());
        EmployeeRoles = new ObservableCollection<EmployeeRole>(Enum.GetValues<EmployeeRole>().Cast<EmployeeRole>());
        AddEmployeeCommand = new AsyncRelayCommand(item => AddEmployeeAsync(), item => true);
    }

    private async Task AddEmployeeAsync()
    {
        EmployeeContractRequest contractRequest = new EmployeeContractRequest
        {
            ContractTypeEnum = Employee.ContractTypeEnum,
            EmploymentType = Employee.EmploymentType,
            MonthlySalaryBrutto = Employee.MonthlySalaryBrutto,
            PersonId = Employee.PersonId,
            Role = Employee.Role
        };

        Result<EmploymentContractPdfDto> validationResult = await _employeeHttpClient.GetEmployeeContractAsync(contractRequest);
        if (!validationResult.IsSuccess)
        {
            MessageBox.Show($"{validationResult.ErrorMessage}");
        }
        else
        {
            EmploymentContractPdfDto employmentContractPdfDto = validationResult.Value!;
            GenerateEmploymentContractPdf(employmentContractPdfDto);
            MessageBoxResult isSigned = MessageBox.Show("Is trainer contract signed?", "Confirmation",
    MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (isSigned == MessageBoxResult.Yes)
            {
                Result<EmployeeInfoResponse> result = await _employeeHttpClient.PostEmployeeAsync(Employee);
                if (result.IsSuccess)
                {
                    Navigation.NavigateTo<StaffViewModel>();
                }
                else
                {
                    MessageBox.Show($"{result.ErrorMessage}");
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
                        $"a) Pracodawca: {employee.GymName}, {employee.GymAddress}, NIP: {employee.Nip}, tel: {employee.ContactNumber}");

                    column.Item().Text(
                        $"b) Pracownik: stanowisko {employee.Role}, forma zatrudnienia: {employee.EmploymentType}");

                    column.Item().PaddingTop(10).Text("§1 Przedmiot umowy:");
                    column.Item().Text(
                        "1. Pracodawca zatrudnia Pracownika, a Pracownik zobowiązuje się do wykonywania pracy zgodnie z niniejszą umową.");

                    column.Item().PaddingTop(10).Text("§2 Okres obowiązywania:");
                    column.Item().Text(
                        string.IsNullOrWhiteSpace(employee.ValidTo)
                            ? $"Umowa zostaje zawarta od dnia {employee.ValidFrom}."
                            : $"Umowa zostaje zawarta na okres od {employee.ValidFrom} do {employee.ValidTo}.");

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


    public void ReceiveParameter(object parameter)
    {
        if(parameter is Guid personId)
        {
            Employee.PersonId = personId;
        }
    }
}
