using GymManagementSystem.Core.DTO.Contract;
using GymManagementSystem.Core.DTO.Membership;
using GymManagementSystem.Core.Result;
using GymManagementSystem.WPF.Core;
using GymManagementSystem.WPF.HttpServices;
using GymManagementSystem.WPF.ServiceContracts;
using GymManagementSystem.WPF.ViewModels.Membership;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using System.Collections.ObjectModel;
using System.Data.Common;
using System.Diagnostics;
using System.IO;
using System.Reflection.Metadata;
using System.Windows;
using System.Windows.Input;

namespace GymManagementSystem.WPF.ViewModels.Contract;

public class ContractViewModel : ViewModel
{
    public SidebarViewModel SidebarView { get; }
    private INavigationService _navigation;
    public ContractUpdateRequest UpdateRequest { get; }
    public ICommand GeneratePdfContractCommand { get;  }
    public ICommand OpenAddMembershipView { get; }
    public ICommand OpenEditMembershipCommand { get; }
    public ICommand SetToSignedCommand { get; }
    public ICommand OpenContractDetailsCommand { get; }
    public ICommand TerminateContractCommand { get; }
    private readonly ContractHttpClient _contractHttpCLient;
    public INavigationService Navigation
    {
        get { return _navigation; }
        set
        {
            _navigation = value; OnPropertyChanged();
        }
    }

    private ObservableCollection<ContractResponse> _contracts;

    public ObservableCollection<ContractResponse> Contracts
    {
        get { return _contracts; }
        set
        {
            if (_contracts != value)
            {
                _contracts = value;
                OnPropertyChanged();
            }
        }
    }


    public ContractViewModel(INavigationService navigationService, SidebarViewModel sidebarView, ContractHttpClient contractHttpCLient)
    {
        _navigation = navigationService;
        SidebarView = sidebarView;
        _contractHttpCLient = contractHttpCLient;
        Contracts = new ObservableCollection<ContractResponse>();
        _ = LoadContractsAsync();
        GeneratePdfContractCommand = new RelayCommand(param => GeneratePdfContract(param), item => true);
        TerminateContractCommand = new RelayCommand(param => TerminateContract(param), item => true);
        SetToSignedCommand = new AsyncRelayCommand(param => SetToSigned(param), item => true);
        OpenContractDetailsCommand = new RelayCommand(item => {

            if (item is Guid id)
                Navigation.NavigateTo<ContractDetailsViewModel>(id);
            else
                MessageBox.Show("Fail to open contract details");
            
            }, item => true);
    }

    private void TerminateContract(object? param)
    {
        if (param is not ContractResponse contract)
            return;

        // Ścieżka do pliku na pulpicie
        string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "Termination.pdf");
        string gymName = Application.Current.Resources["GymName"] as string;
        string gymAddress = Application.Current.Resources["Address"] as string;
        string contactNumber = Application.Current.Resources["ContactNumber"] as string;

        var document = QuestPDF.Fluent.Document.Create(container =>
        {
            container.Page(page =>
            {
                page.Size(QuestPDF.Helpers.PageSizes.A4);
                page.Margin(40);
                page.PageColor(QuestPDF.Helpers.Colors.White);
                page.DefaultTextStyle(x => x.FontSize(12));

                // Nagłówek
                page.Header()
                    .AlignCenter()
                    .Text("WYPOWIEDZENIE UMOWY O ŚWIADCZENIE USŁUG FITNESS / KARNET")
                    .FontSize(16)
                    .Bold();

                // Zawartość
                page.Content().Column(column =>
                {
                    column.Item().PaddingTop(10).Text($"Wypowiedzenie umowy zawartej w dniu {contract.ClientMembership?.StartDate:yyyy-MM-dd}");

                    column.Item().PaddingTop(5).Text($"1. Strony umowy:");
                    column.Item().Text($"a) Siłownia: {gymName}, {gymAddress}, NIP: 6969, numer kontaktowy: {contactNumber}");
                    column.Item().Text($"b) Klient: {contract.ClientMembership?.Client.FirstName ?? "-"} {contract.ClientMembership?.Client.LastName ?? "-"}, zamieszkały: {contract.ClientMembership.Client.City ?? "-"}, PESEL: , email: {contract.ClientMembership.Client.Email ?? "-"}, telefon: {contract.ClientMembership.Client.PhoneNumber ?? "-"}");

                    column.Item().PaddingTop(10).Text("§1 Przedmiot wypowiedzenia:");
                    column.Item().Text("Niniejszym Klient wypowiada umowę o świadczenie usług fitness / karnet na siłowni.");

                    column.Item().PaddingTop(10).Text("§2 Okres obowiązywania:");
                    column.Item().Text($"Umowa obowiązuje od dnia {contract.ClientMembership?.  StartDate:yyyy-MM-dd} do dnia {contract.ClientMembership?.EndDate}, wypowiedzenie skutkuje zakończeniem świadczenia usług od dnia złożenia dokumentu.");

                    column.Item().PaddingTop(10).Text("§3 Zasady zwrotu środków:");
                    column.Item().Text("1. W przypadku rezygnacji przysługuje zwrot niewykorzystanej części karnetu zgodnie z regulaminem Siłowni.");
                    column.Item().Text("2. Zwrot środków odbywa się na konto wskazane przez Klienta.");

                    column.Item().PaddingTop(10).Text("§4 Postanowienia końcowe:");
                    column.Item().Text("1. Wszelkie zmiany niniejszego wypowiedzenia wymagają formy pisemnej.");
                    column.Item().Text("2. Ewentualne spory rozstrzyga właściwy sąd dla siedziby Siłowni.");

                    column.Item().PaddingTop(30).Row(row =>
                    {
                        row.ConstantItem(200).Text("Podpis Siłowni:");
                        row.ConstantItem(200).Text("Podpis Klienta:");
                    });
                });

                // Footer z numeracją stron
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

        // Generowanie PDF
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
            MessageBox.Show($"Błąd: {ex.Message}");
        }

    }

    private async Task SetToSigned(object param)
    {
        if (param is not ContractResponse contract) {
            return;
        }

        ContractUpdateRequest request = new ContractUpdateRequest() { ContractStatus = GymManagementSystem.Core.Enum.ContractStatus.Signed};
        Result<ContractResponse> result = await _contractHttpCLient.PutContractAsync(request, contract.Id);
        if (result.IsSuccess)
        {
            MessageBox.Show($"Contract for client {contract.ClientMembership.Client.FirstName} {contract.ClientMembership.Client.LastName} is signed");
            Contracts = await _contractHttpCLient.GetContractsAsync();
        }
        else
        {
            MessageBox.Show("Contract is not signed Error");
        }
    }

    private void GeneratePdfContract(object? parameter)
    {
        if (parameter is not ContractResponse contract)
            return;

        // Ścieżka do pliku na pulpicie
        string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "Contract.pdf");
        string gymName = Application.Current.Resources["GymName"] as string;
        string gymAddress = Application.Current.Resources["Address"] as string;
        string contactNumber = Application.Current.Resources["ContactNumber"] as string;

        // Tworzenie dokumentu PDF
        var document = QuestPDF.Fluent.Document.Create(container =>
        {
            container.Page(page =>
            {
                page.Size(QuestPDF.Helpers.PageSizes.A4);
                page.Margin(40);
                page.PageColor(QuestPDF.Helpers.Colors.White);
                page.DefaultTextStyle(x => x.FontSize(12));

                // Nagłówek
                page.Header()
                    .AlignCenter()
                    .Text("UMOWA O ŚWIADCZENIE USŁUG FITNESS / KARNET NA SIŁOWNIĘ")
                    .FontSize(16)
                    .Bold();

                // Zawartość
                page.Content().Column(column =>
                {
                    column.Item().PaddingTop(10).Text($"Zawarta w dniu: {contract.ClientMembership?.StartDate:yyyy-MM-dd} w mieście: ");

                    column.Item().PaddingTop(5).Text($"1. Strony umowy:");
                    column.Item().Text($"a) Siłownia: {gymName}, {gymAddress}, NIP: 6969, numer kontaktowy: {contactNumber}");
                    column.Item().Text($"b) Klient: {contract.ClientMembership?.Client?.FirstName ?? "-"} {contract.ClientMembership?.Client?.LastName ?? "-"}, zamieszkały: {contract.ClientMembership?.Client?.City + " " + contract.ClientMembership?.Client?.Street ?? "-"}, PESEL: , email: {contract.ClientMembership?.Client?.Email ?? "-"}, telefon: {contract.ClientMembership?.Client?.PhoneNumber ?? "-"}");

                    column.Item().PaddingTop(10).Text("§1 Przedmiot umowy:");
                    column.Item().Text("Siłownia zobowiązuje się do świadczenia usług fitness, polegających na umożliwieniu Klientowi korzystania z obiektu zgodnie z wybranym karnetem.");

                    column.Item().PaddingTop(10).Text("§2 Okres obowiązywania:");
                    column.Item().Text($"Umowa obowiązuje od dnia {contract.ClientMembership?.StartDate:yyyy-MM-dd} do dnia {contract.ClientMembership?.EndDate:yyyy-MM-dd}.");

                    column.Item().PaddingTop(10).Text("§3 Cena i sposób płatności:");
                    column.Item().Text($"Cena karnetu: {contract.ClientMembership?.Membership?.Price.ToString("C") ?? "-"}");
                    column.Item().Text($"Forma płatności: ");

                    column.Item().PaddingTop(10).Text("§4 Zasady korzystania:");
                    column.Item().Text("1. Klient zobowiązuje się do przestrzegania regulaminu Siłowni.");
                    column.Item().Text("2. Klient ponosi odpowiedzialność za ewentualne szkody wyrządzone w trakcie korzystania z obiektu.");

                    column.Item().PaddingTop(10).Text("§5 Rezygnacja i zawieszenie:");
                    column.Item().Text("1. Klient ma prawo odstąpić od umowy w terminie 14 dni od dnia jej zawarcia.");
                    column.Item().Text("2. Zasady zawieszenia karnetu określa regulamin Siłowni.");

                    column.Item().PaddingTop(10).Text("§6 Oświadczenia klienta:");
                    column.Item().Text("1. Klient oświadcza, że jest świadomy ryzyka związanego z ćwiczeniami fizycznymi.");
                    column.Item().Text("2. Klient wyraża zgodę na przetwarzanie danych osobowych w celach związanych z usługą.");

                    column.Item().PaddingTop(10).Text("§7 Postanowienia końcowe:");
                    column.Item().Text("1. Wszelkie zmiany niniejszej umowy wymagają formy pisemnej.");
                    column.Item().Text("2. Ewentualne spory rozstrzyga właściwy sąd dla siedziby Siłowni.");

                    column.Item().PaddingTop(30).Row(row =>
                    {
                        row.ConstantItem(200).Text("Podpis Siłowni:");
                        row.ConstantItem(200).Text("Podpis Klienta:");
                    });
                });

                // Footer z numeracją stron
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
            // Generowanie PDF
            document.GeneratePdf(filePath);

            // Otwieranie PDF w domyślnej aplikacji
            Process.Start(new ProcessStartInfo
            {
                FileName = filePath,
                UseShellExecute = true
            });
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Błąd: {ex.Message}");
        }

    }


    private async Task LoadContractsAsync()
    {
        Contracts = await _contractHttpCLient.GetContractsAsync();
    }
}
