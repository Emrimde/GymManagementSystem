using GymManagementSystem.Core.DTO.Contract;
using GymManagementSystem.Core.Enum;
using GymManagementSystem.Core.Result;
using GymManagementSystem.WPF.Core;
using GymManagementSystem.WPF.HttpServices;
using GymManagementSystem.WPF.ServiceContracts;
using QuestPDF.Fluent;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace GymManagementSystem.WPF.ViewModels.Contract;

public class ContractDetailsViewModel : ViewModel, IParameterReceiver
{
    private ContractDetailsResponse _contract;

    public ContractDetailsResponse Contract
    {
        get { return _contract; }
        set { _contract = value; ContractStatus = value.ContractStatus; OnPropertyChanged(); }
    }
    public ICommand GeneratePdfContractCommand { get; }
    public ICommand SetToSignedCommand { get; }



    private ContractStatus _contractStatus ;
    public ContractStatus ContractStatus
    {
        get => _contractStatus;
        set { _contractStatus = value; OnPropertyChanged(); }
    }


    private INavigationService _navigation;
    private Guid _contractId;

    public INavigationService Navigation
    {
        get { return _navigation; }
        set { _navigation = value; OnPropertyChanged(); }
    }
    public SidebarViewModel SidebarView { get; set; }

    private readonly ContractHttpClient _contractHttpClient;
    public ContractDetailsViewModel(ContractHttpClient contractHttpClient, INavigationService _navigation, SidebarViewModel sidebarView)
    {
        Navigation = _navigation;
        _contractHttpClient = contractHttpClient;
        SidebarView = sidebarView;
        Contract = new ContractDetailsResponse();
        GeneratePdfContractCommand = new AsyncRelayCommand(item => GeneratePdf(), item => true);
        SetToSignedCommand = new AsyncRelayCommand(param => SetToSigned(param), item => true);

    }

    private async Task SetToSigned(object param = null)
    {
        ContractUpdateRequest request = new ContractUpdateRequest();
        if (param == null)
        {

            request.ContractStatus = ContractStatus.Signed;

        }
        else
        {
            if (param is ContractStatus contractStatus)
            {
                request.ContractStatus = ContractStatus.Generated;
            }
        }

        Result<ContractResponse> result = await _contractHttpClient.PutContractAsync(request, _contractId);
        if (result.IsSuccess)
        {
            ContractStatus = result.Value!.ContractStatus;
            if (request.ContractStatus == ContractStatus.Signed)
            {
                MessageBox.Show($"Contract for client {Contract.Client?.FirstName} {Contract.Client?.LastName} is signed");
            }
        }
        else
        {
            MessageBox.Show("Contract is not signed Error");
        }
    }




    private async Task GeneratePdf()
    {

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
                    column.Item().PaddingTop(10).Text($"Zawarta w dniu: {Contract.StartDate:yyyy-MM-dd} w mieście: ");

                    column.Item().PaddingTop(5).Text($"1. Strony umowy:");
                    column.Item().Text($"a) Siłownia: {gymName}, {gymAddress}, NIP: 6969, numer kontaktowy: {contactNumber}");
                    column.Item().Text($"b) Klient: {Contract.Client?.FirstName ?? "-"} {Contract.Client?.LastName ?? "-"}, zamieszkały: {Contract.Client?.City + " " + Contract.Client?.Street ?? "-"}, PESEL: , email: {Contract.Client?.Email ?? "-"}, telefon: {Contract.Client?.PhoneNumber ?? "-"}");

                    column.Item().PaddingTop(10).Text("§1 Przedmiot umowy:");
                    column.Item().Text("Siłownia zobowiązuje się do świadczenia usług fitness, polegających na umożliwieniu Klientowi korzystania z obiektu zgodnie z wybranym karnetem.");

                    column.Item().PaddingTop(10).Text("§2 Okres obowiązywania:");
                    column.Item().Text($"Umowa obowiązuje od dnia {Contract.StartDate:yyyy-MM-dd} do dnia {Contract.EndDate:yyyy-MM-dd}.");

                    column.Item().PaddingTop(10).Text("§3 Cena i sposób płatności:");
                    column.Item().Text($"Cena karnetu: {Contract.Price.ToString("C") ?? "-"}");
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
            await SetToSigned(ContractStatus.Generated);
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Błąd: {ex.Message}");
        }

    }


    public void ReceiveParameter(object parameter)
    {
        if (parameter is Guid id)
        {
            _ = LoadContract(id);
            _contractId = id;
        }
    }

    private async Task LoadContract(Guid id)
    {
        Result<ContractDetailsResponse> result = await _contractHttpClient.GetContractByIdAsync(id);
        if (result.IsSuccess)
        {
            Contract = result.Value!;
        }
        else
        {
            MessageBox.Show($"Fail {result.ErrorMessage}");
        }
    }
}
