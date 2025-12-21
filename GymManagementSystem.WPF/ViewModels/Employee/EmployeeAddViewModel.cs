using GymManagementSystem.Core.DTO.Employee;
using GymManagementSystem.Core.Enum;
using GymManagementSystem.Core.Result;
using GymManagementSystem.WPF.Core;
using GymManagementSystem.WPF.HttpServices;
using GymManagementSystem.WPF.ServiceContracts;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using Syncfusion.Windows.Shared;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Windows;
using System.Windows.Input;


namespace GymManagementSystem.WPF.ViewModels.Employee;
public class EmployeeAddViewModel : ViewModel, IParameterReceiver
{
    private readonly EmployeeHttpClient _employeeHttpClient;
    private EmployeeAddRequest _employee;

    private bool _isFixedTerm;

    public bool IsFixedTerm
    {
        get { return _isFixedTerm; }
        set { _isFixedTerm = value; OnPropertyChanged(); }
    }


    public EmployeeAddRequest Employee
    {
        get { return _employee; }
        set { _employee = value; OnPropertyChanged(); }
    }


    public IEnumerable<ContractTypeEnum> AvailableContractsForEmployees =>
    [
        ContractTypeEnum.Probation,
        ContractTypeEnum.FixedTerm,
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


    private ContractTypeEnum _selectedContractType;

    public ContractTypeEnum SelectedContractType
    {
        get { return _selectedContractType; }
        set
        {
            if (_selectedContractType != value)
            {
                _selectedContractType = value;

                if (_selectedContractType == ContractTypeEnum.FixedTerm)
                {
                    IsFixedTerm = true;
                }
                else
                {
                    IsFixedTerm = false;
                }
                Employee.ContractTypeEnum = _selectedContractType;
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
        SelectedContractType = ContractTypeEnum.Permanent;

        EmploymentTypes = new ObservableCollection<EmploymentType>(Enum.GetValues<EmploymentType>().Cast<EmploymentType>());
        EmployeeRoles = new ObservableCollection<EmployeeRole>(Enum.GetValues<EmployeeRole>().Cast<EmployeeRole>());

        AddEmployeeCommand = new AsyncRelayCommand(item => AddEmployeeAsync(), item => true);
    }

    private async Task AddEmployeeAsync()
    {
        Result<bool> validationResult = await _employeeHttpClient.ValidateEmployee(Employee);
        if (!validationResult.IsSuccess)
        {
            MessageBox.Show($"{validationResult.ErrorMessage}");
        }
        else
        {
            await GenerateEmploymentContractPdf(Employee);
            MessageBoxResult isSigned = MessageBox.Show("Is trainer contract signed?", "Confirmation",
    MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (isSigned == MessageBoxResult.Yes)
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
            else
            {
                Navigation.NavigateTo<EmployeeViewModel>();
            }
        }

    }

    public async Task GenerateEmploymentContractPdf(EmployeeAddRequest employee)
    {
        if (employee == null) throw new ArgumentNullException(nameof(employee));

        // Dane siłowni z resources (jeśli ich nie masz, zamień na stałe stringi)
        string gymName = Application.Current.Resources["GymName"] as string ?? "Siłownia XYZ";
        string gymAddress = Application.Current.Resources["Address"] as string ?? "ul. Przykładowa 1, Miasto";
        string contactNumber = Application.Current.Resources["ContactNumber"] as string ?? "000-000-000";
        string nip = Application.Current.Resources["GymNip"] as string ?? "000-000-0000"; // opcjonalnie

        // Formatowanie pensji w PL
        var plCulture = CultureInfo.CreateSpecificCulture("pl-PL");
        string salaryText = employee.MonthlySalaryBrutto.ToString("C", plCulture);

        // Typ umowy jako tekst
        string contractTypeText = employee.ContractTypeEnum switch
        {
            ContractTypeEnum.Probation => "Umowa na okres próbny",
            ContractTypeEnum.FixedTerm => "Umowa na czas określony",
            ContractTypeEnum.Permanent => "Umowa o pracę na czas nieokreślony",
            _ => "Umowa"
        };

        // Daty
        string validFromText = employee.ValidFrom?.ToString("yyyy-MM-dd") ?? "—";
        string validToText = employee.ValidTo?.ToString("yyyy-MM-dd") ?? "—";

        // Dodatkowe klauzule zależne od typu umowy
        string specialClause = string.Empty;
        if (employee.ContractTypeEnum == ContractTypeEnum.Probation)
        {
            // jeśli jest podana data zakończenia użyj jej, w przeciwnym wypadku załóż 3 miesiące (możesz zmienić)
            if (employee.ValidTo.HasValue)
                specialClause = $"Strony ustalają okres próbny trwający do dnia {validToText}.";
            else
                specialClause = "Strony ustalają okres próbny trwający maksymalnie 3 miesiące od dnia rozpoczęcia pracy.";
        }
        else if (employee.ContractTypeEnum == ContractTypeEnum.FixedTerm)
        {
            specialClause = $"Umowa zawarta jest na czas określony — od {validFromText} do {validToText}.";
        }
        else // Permanent
        {
            specialClause = $"Umowa zawarta jest na czas nieokreślony, ze skutkiem od dnia {validFromText}.";
        }

        // EmploymentType i rola
        string employmentTypeText = employee.EmploymentType == EmploymentType.FullTime ? "Pełny etat" : "Część etatu";
        string roleText = employee.Role.ToString();

        // Nazwa pliku
        string safeName = $"{employee.ValidFrom}_{employee.ValidFrom}_{contractTypeText}".Replace(" ", "_");
        string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), $"{safeName}.pdf");

        // Tworzenie dokumentu PDF
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
                    .Text($"{contractTypeText}")
                    .FontSize(16)
                    .Bold();

                page.Content().Column(column =>
                {
                    //column.Item().PaddingTop(8).Text($"Zawarta w dniu: {DateTime.Now:yyyy-MM-dd} w mieście: {gymAddress.Split(',')[0]}.");

                    //column.Item().PaddingTop(8).Text("1. Strony umowy:");
                    //column.Item().Text($"a) Pracodawca: {gymName}, {gymAddress}, NIP: {nip}, numer kontaktowy: {contactNumber}");
                    //column.Item().Text($"b) Pracownik: {employee.ValidFrom} {employee.ValidFrom}, zamieszkały: {(string.IsNullOrWhiteSpace(employee.ValidTo) ? "-" : employee.ValidFrom)} (email), telefon: {employee.ValidFrom ?? "-"}");
                    //column.Item().Text($"   Stanowisko / rola: {roleText}");
                    //column.Item().Text($"   Typ zatrudnienia: {employmentTypeText}");
                    //column.Item().Text($"   Wynagrodzenie: {salaryText} brutto miesięcznie");

                    column.Item().PaddingTop(10).Text("§1 Przedmiot umowy:");
                    column.Item().Text("1. Pracodawca zatrudnia Pracownika, a Pracownik podejmuje pracę na warunkach określonych niniejszą umową oraz obowiązującym regulaminem pracy.");

                    column.Item().PaddingTop(10).Text("§2 Okres obowiązywania:");
                    column.Item().Text(specialClause);

                    column.Item().PaddingTop(10).Text("§3 Wynagrodzenie i sposób płatności:");
                    column.Item().Text($"1. Wynagrodzenie miesięczne brutto wynosi {salaryText}.");
                    column.Item().Text("2. Wynagrodzenie będzie wypłacane na rachunek bankowy Pracownika, w terminie do 10 dnia następnego miesiąca kalendarzowego.");

                    column.Item().PaddingTop(10).Text("§4 Czas pracy i obowiązki:");
                    column.Item().Text("1. Pracownik zobowiązuje się do wykonywania powierzonych obowiązków zgodnie z zakresem obowiązków na stanowisku.");
                    column.Item().Text("2. Czas pracy (w wymiarze określonym powyżej) jest zgodny z obowiązującymi przepisami i harmonogramem ustalonym przez Pracodawcę.");

                    column.Item().PaddingTop(10).Text("§5 Okres wypowiedzenia i rozwiązanie umowy:");
                    column.Item().Text("1. W sprawach nieuregulowanych niniejszą umową zastosowanie mają przepisy Kodeksu pracy oraz obowiązujący regulamin.");
                    if (employee.ContractTypeEnum == ContractTypeEnum.FixedTerm)
                        column.Item().Text("2. Umowa wygasa z upływem terminu, o którym mowa w §2, bez konieczności składania oświadczeń stron.");
                    else
                        column.Item().Text("2. Zasady wypowiedzenia i tryb rozwiązania umowy określone będą właściwymi przepisami prawa pracy.");

                    column.Item().PaddingTop(10).Text("§6 Oświadczenia Pracownika:");
                    column.Item().Text("1. Pracownik oświadcza, że jest zdolny do wykonywania pracy na powierzonym stanowisku oraz nie toczą się przeciwko niemu prawomocne orzeczenia zabraniające wykonywania pracy.");
                    column.Item().Text("2. Pracownik wyraża zgodę na przetwarzanie danych osobowych w zakresie niezbędnym do realizacji niniejszej umowy.");

                    column.Item().PaddingTop(10).Text("§7 Postanowienia końcowe:");
                    column.Item().Text("1. Wszelkie zmiany niniejszej umowy wymagają formy pisemnej pod rygorem nieważności.");
                    column.Item().Text("2. W sprawach spornych właściwy jest sąd właściwy dla siedziby Pracodawcy.");

                    column.Item().PaddingTop(24).Row(row =>
                    {
                        row.RelativeItem().Text($"Data rozpoczęcia pracy: {validFromText}");

                        row.RelativeItem().Text($"Data zakończenia (jeśli dotyczy): {validToText}");
                    });

                    column.Item().PaddingTop(30).Row(row =>
                    {
                        row.ConstantItem(250).Column(left =>
                        {
                            left.Item().Text("Podpis Pracodawcy:");
                            left.Item().PaddingTop(40).Text("_________________________");
                        });



                        row.ConstantItem(250).Column(right =>
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
