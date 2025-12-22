using GymManagementSystem.Core.DTO.TrainerContract;
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
using System.Globalization;
using System.IO;
using System.Windows;
using System.Windows.Input;

namespace GymManagementSystem.WPF.ViewModels.TrainerContract;

public class TrainerContractAddViewModel : ViewModel, IParameterReceiver
{
    private readonly TrainerHttpClient _trainerHttpClient;
    private INavigationService _navigation;

    public INavigationService Navigation
    {
        get { return _navigation; }
        set { _navigation = value; OnPropertyChanged(); }
    }

    private TrainerContractAddRequest _trainerContract;

    public TrainerContractAddRequest TrainerContract
    {
        get { return _trainerContract; }
        set { _trainerContract = value; OnPropertyChanged(); }
    }

    public ObservableCollection<TrainerTypeEnum> TrainerTypes { get; set; }

    private TrainerTypeEnum _selectedTrainerType;

    public TrainerTypeEnum SelectedTrainerType
    {
        get { return _selectedTrainerType; }
        set
        {
            _selectedTrainerType = value;
            TrainerContract.TrainerType = _selectedTrainerType;
            OnPropertyChanged();
        }
    }
    public ICommand ReturnToStaffViewCommand { get;  }


    public IEnumerable<ContractTypeEnum> ContractTypes => [
        ContractTypeEnum.ContractOfMandate, ContractTypeEnum.B2B
        ];


    private ContractTypeEnum _selectedContractType;

    public ContractTypeEnum SelectedContractType
    {
        get { return _selectedContractType; }
        set
        {
            if (_selectedContractType != value)
            {

                _selectedContractType = value;
                if (_selectedContractType == ContractTypeEnum.B2B)
                {
                    IsB2B = true;

                }
                else
                {
                    IsB2B = false;
                }
                TrainerContract.ContractType = _selectedContractType;
                OnPropertyChanged();
            }

        }
    }


    private bool _isB2b;


    public bool IsB2B
    {
        get { return _isB2b; }
        set
        {
            _isB2b = value;

            OnPropertyChanged();

        }
    }
    public ICommand AddTrainerCommand { get; }

    public SidebarViewModel SidebarView { get; }

    public TrainerContractAddViewModel(TrainerHttpClient trainerHttpClient, INavigationService navigation, SidebarViewModel sidebarView)
    {
        _trainerHttpClient = trainerHttpClient;
        Navigation = navigation;
        SidebarView = sidebarView;
        TrainerContract = new TrainerContractAddRequest();
        ReturnToStaffViewCommand = new RelayCommand(item => Navigation.NavigateTo<StaffViewModel>(), item => true);
        TrainerTypes = new ObservableCollection<TrainerTypeEnum>(Enum.GetValues<TrainerTypeEnum>().Cast<TrainerTypeEnum>());
        AddTrainerCommand = new AsyncRelayCommand(item => AddTrainerAsync(), item => true);
    }

    private async Task AddTrainerAsync()
    {
        await GenerateTrainerContractPdf(TrainerContract);
        MessageBoxResult response = MessageBox.Show("Is trainer signed contract?", "Confirmation", MessageBoxButton.YesNo);
        if (response == MessageBoxResult.Yes)
        {
            Result<TrainerContractInfoResponse> result = await _trainerHttpClient.PostTrainerContractAsync(TrainerContract);
            if (result.IsSuccess)
            {
                Navigation.NavigateTo<TrainerContractDetailsViewModel>(result.Value!.Id);
            }
            else
            {
                MessageBox.Show($"{result.ErrorMessage}");
            }
        }


    }

    public async Task GenerateTrainerContractPdf(TrainerContractAddRequest request)
    {
        if (request == null) throw new ArgumentNullException(nameof(request));

        // Dane siłowni - możesz trzymać w resources lub w konfiguracji
        string gymName = Application.Current?.Resources["GymName"] as string ?? "Siłownia XYZ";
        string gymAddress = Application.Current?.Resources["Address"] as string ?? "ul. Przykładowa 1, Miasto";
        string contactNumber = Application.Current?.Resources["ContactNumber"] as string ?? "000-000-000";
        string gymNip = Application.Current?.Resources["GymNip"] as string ?? "000-000-0000";

        var pl = CultureInfo.CreateSpecificCulture("pl-PL");
        //string validFromText = request.ValidFrom?.ToString("yyyy-MM-dd", pl) ?? "—"; 
        //string validToText = request.ValidTo.HasValue ? request.ValidTo.Value.ToString("yyyy-MM-dd", pl) : "—";

        // Typ umowy (czytelny)
        string contractTitle = request.ContractType switch
        {
            ContractTypeEnum.ContractOfMandate => "Umowa zlecenie",
            ContractTypeEnum.B2B => "Umowa B2B (umowa o świadczenie usług)",
            ContractTypeEnum.Probation => "Umowa (okres próbny)",
            ContractTypeEnum.FixedTerm => "Umowa na czas określony",
            ContractTypeEnum.Permanent => "Umowa na czas nieokreślony",
            _ => "Umowa"
        };

        // Rola trenera jako tekst
        string trainerRoleText = request.TrainerType switch
        {
            TrainerTypeEnum.PersonalTrainer => "Trener personalny",
            TrainerTypeEnum.GroupInstructor => "Instruktor grupowy",
            _ => request.TrainerType.ToString()
        };

        // Firma (dla B2B)
        string companyName = string.IsNullOrWhiteSpace(request.CompanyName) ? "—" : request.CompanyName!;
        string taxId = string.IsNullOrWhiteSpace(request.TaxId) ? "—" : request.TaxId!;
        string companyAddress = string.IsNullOrWhiteSpace(request.CompanyAddress) ? "—" : request.CompanyAddress!;

        // Prowizja klubu (jeśli dotyczy)
        string commissionText = request.ClubCommissionPercent > 0
            ? $"{request.ClubCommissionPercent.ToString("0.##", pl)}% prowizji klubu od każdej opłaty za zajęcia prowadzonych przez trenera."
            : "Brak prowizji określonej (0%).";

        // Specjalne klauzule w zależności od typu umowy
        string mainClause = string.Empty;
        string paymentClause = string.Empty;
        string terminationClause = string.Empty;

        if (request.ContractType == ContractTypeEnum.ContractOfMandate)
        {
            mainClause = "Przedmiotem niniejszej umowy jest wykonywanie przez Zleceniobiorcę usług trenerskich (prowadzenie zajęć, treningów, konsultacji) na rzecz Zleceniodawcy zgodnie z ustalonym harmonogramem.";
            paymentClause =
                "Wynagrodzenie: Strony ustalają wynagrodzenie za wykonane zlecenia. " +
                $"Klub pobiera prowizję w wysokości: {request.ClubCommissionPercent.ToString("0.##", pl)}% od każdej opłaty wniesionej przez klienta za zajęcia. " +
                "Wynagrodzenie będzie wypłacane na podstawie rachunku/wyciągu do 14 dni od wystawienia dokumentu potwierdzającego wykonanie usługi.";
            terminationClause =
                "Umowa zlecenie może być wypowiedziana przez każdą ze stron z zachowaniem 14-dniowego okresu wypowiedzenia, chyba że strony postanowią inaczej.";
        }
        else if (request.ContractType == ContractTypeEnum.B2B)
        {
            mainClause =
                "Przedmiotem niniejszej umowy jest świadczenie przez Wykonawcę (prowadzącego działalność gospodarczą) usług trenerskich na rzecz Zleceniodawcy na warunkach określonych poniżej.";
            paymentClause =
                "Rozliczenia: Wykonawca wystawia faktury VAT/rachunki za wykonane usługi. Termin płatności: do 14 dni od otrzymania faktury. " +
                commissionText;
            terminationClause =
                "Umowa B2B może być rozwiązana przez każdą ze stron z zachowaniem 30-dniowego okresu wypowiedzenia, o ile strony nie ustaliły innego terminu w treści umowy.";
        }
        else
        {
            // fallback ogólny
            mainClause = "Przedmiotem niniejszej umowy jest świadczenie usług trenerskich.";
            paymentClause = commissionText;
            terminationClause = "Zasady rozwiązania umowy określone są w dalszych postanowieniach.";
        }

        // Nazwa pliku bez niedozwolonych znaków
        string safeFileName = $"{request.TaxId}_{request.TaxId}_{contractTitle}"
            .Replace(" ", "_")
            .Replace(":", "")
            .Replace("/", "_");

        string desktop = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        string filePath = Path.Combine(desktop, $"{safeFileName}.pdf");

        var document = Document.Create(container =>
        {
            container.Page(page =>
            {
                page.Size(PageSizes.A4);
                page.Margin(40);
                page.DefaultTextStyle(x => x.FontSize(11));
                page.PageColor(Colors.White);

                page.Header()
                    .AlignCenter()
                    .Text(contractTitle)
                    .FontSize(16)
                    .Bold();

                page.Content().Column(column =>
                {
                    column.Item().Text($"Zawarta dnia: {DateTime.Now.ToString("yyyy-MM-dd", pl)} w miejscowości: {gymAddress.Split(',')[0]}.");
                    column.Item().PaddingTop(8).Text("1. Strony umowy:");
                    column.Item().Text($"a) Zleceniodawca / Klub: {gymName}, {gymAddress}, NIP: {gymNip}, tel: {contactNumber}");

                    if (request.ContractType == ContractTypeEnum.B2B)
                    {
                        column.Item().Text($"b) Wykonawca (firma): {companyName}, NIP: {taxId}, adres: {companyAddress}");
                        column.Item().Text($"   Osoba kontaktowa: {request.TaxId} {request.TaxId}, e-mail: {request.TaxId ?? "-"}, tel: {request.TaxId ?? "-"}");
                    }
                    else
                    {
                        column.Item().Text($"b) Zleceniobiorca: {request.TaxId} {request.TaxId}, e-mail: {request.TaxId ?? "-"}, tel: {request.TaxId ?? "-"}");
                    }

                    column.Item().Text($"   Rola: {trainerRoleText}");
                    //column.Item().Text($"   Okres obowiązywania: od {validFromText} do {(request.ValidTo.HasValue ? validToText : "— (umowa na czas nieokreślony)")}");
                    column.Item().PaddingTop(10).Text("§1 Przedmiot umowy:");
                    column.Item().Text(mainClause);

                    column.Item().PaddingTop(10).Text("§2 Zakres obowiązków:");
                    column.Item().Text("1. Prowadzenie zajęć zgodnie z harmonogramem i standardami klubu.");
                    column.Item().Text("2. Zapewnienie należytej staranności i kwalifikacji wymaganych do prowadzenia zajęć.");

                    column.Item().PaddingTop(10).Text("§3 Wynagrodzenie i rozliczenia:");
                    column.Item().Text(paymentClause);

                    column.Item().PaddingTop(10).Text("§4 Obowiązki podatkowe i ubezpieczeniowe:");
                    if (request.ContractType == ContractTypeEnum.B2B)
                    {
                        column.Item().Text("1. Wykonawca odpowiada za swoje zobowiązania podatkowe i ubezpieczeniowe wynikające z prowadzonej działalności gospodarczej.");
                    }
                    else
                    {
                        column.Item().Text("1. Zleceniobiorca odpowiada za kwestie podatkowe związane z otrzymanym wynagrodzeniem; w przypadku konieczności potrąceń, strony postąpią zgodnie z obowiązującymi przepisami.");
                    }

                    column.Item().PaddingTop(10).Text("§5 Okres wypowiedzenia i rozwiązanie umowy:");
                    column.Item().Text(terminationClause);

                    column.Item().PaddingTop(10).Text("§6 Postanowienia końcowe:");
                    column.Item().Text("1. Wszelkie zmiany niniejszej umowy wymagają formy pisemnej pod rygorem nieważności.");
                    column.Item().Text("2. W sprawach nieuregulowanych zastosowanie mają odpowiednie przepisy prawa polskiego.");

                    column.Item().PaddingTop(24).Row(row =>
                    {
                        //row.RelativeItem().Text($"Data rozpoczęcia: {validFromText}");
                        //row.RelativeItem().Text($"Data zakończenia: {validToText}");
                    });

                    column.Item().PaddingTop(30).Row(row =>
                    {
                        row.ConstantItem(250).Column(left =>
                        {
                            left.Item().Text("Podpis Zleceniodawcy / Klubu:");
                            left.Item().PaddingTop(40).Text("_________________________");
                        });

                        row.ConstantItem(250).Column(right =>
                        {
                            right.Item().Text(request.ContractType == ContractTypeEnum.B2B ? "Podpis Wykonawcy (firma):" : "Podpis Zleceniobiorcy:");
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
            // Obsłuż błąd odpowiednio w Twoim UI
            System.Windows.MessageBox.Show($"Błąd podczas generowania PDF: {ex.Message}");
        }
    }

    public void ReceiveParameter(object parameter)
    {
        if(parameter is Guid personId)
        {
            TrainerContract.PersonId = personId;
        }
    }
}
