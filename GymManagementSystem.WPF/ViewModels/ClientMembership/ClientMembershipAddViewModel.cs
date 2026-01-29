using GymManagementSystem.Core.DTO.Client;
using GymManagementSystem.Core.DTO.ClientMembership;
using GymManagementSystem.Core.DTO.ClientMembership.Models;
using GymManagementSystem.Core.DTO.Membership;
using GymManagementSystem.WPF.Result;
using GymManagementSystem.WPF.Core;
using GymManagementSystem.WPF.HttpServices;
using GymManagementSystem.WPF.ServiceContracts;
using GymManagementSystem.WPF.ViewModels.Client;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Windows;
using System.Windows.Input;

namespace GymManagementSystem.WPF.ViewModels.ClientMembership;

public class ClientMembershipAddViewModel : ViewModel, IParameterReceiver
{
    private readonly ClientMembershipHttpClient _httpClient;
    private readonly ClientHttpClient _clientHttpClient;
    private readonly MembershipHttpClient _membershipHttpClient;

    private ClientMembershipAddRequest _clientMembershipAddRequest = new();
    public Guid ClientId { get; set; }
    public ClientMembershipAddRequest ClientMembershipAddRequest
    {
        get { return _clientMembershipAddRequest; }
        set { _clientMembershipAddRequest = value; OnPropertyChanged(); }
    }
    private ClientInfoResponse _client = new();

    public ClientInfoResponse Client
    {
        get { return _client; }
        set { _client = value; OnPropertyChanged(); }
    }

    public SidebarViewModel SidebarView { get; set; }

    private ObservableCollection<MembershipSelectItem> _membershipSelectItem = new();
    public ObservableCollection<MembershipSelectItem> MembershipSelectItem
    {
        get => _membershipSelectItem;
        set
        {
            if (_membershipSelectItem != value)
            {
                _membershipSelectItem = value;
                OnPropertyChanged();
            }
        }
    }
    public ICommand AddClientMembershipCommand { get; }
    public ICommand LoadClientMembershipViewData { get; }
    public ICommand CancelCommand { get; }

    public INavigationService Navigation { get; set; }


    private MembershipSelectItem _selectedMembership = new();
    public MembershipSelectItem SelectedMembership
    {
        get => _selectedMembership;
        set
        {
            if (_selectedMembership != value)
            {
                _selectedMembership = value;
                OnPropertyChanged(nameof(SelectedMembership));
                ClientMembershipAddRequest.MembershipId = _selectedMembership.Id;
                ((AsyncRelayCommand)AddClientMembershipCommand)
                .RaiseCanExecuteChanged();
            }
        }
    }


    public ClientMembershipAddViewModel(SidebarViewModel sidebarView, INavigationService navigation, ClientMembershipHttpClient httpClient, MembershipHttpClient membershipHttpClient, ClientHttpClient clientHttpClient)
    {
        _clientHttpClient = clientHttpClient;
        SidebarView = sidebarView;
        Navigation = navigation;
        _httpClient = httpClient;
        _membershipHttpClient = membershipHttpClient;
        CancelCommand = new RelayCommand(item => Navigation.NavigateTo<ClientDetailsViewModel>(Client.Id), item => true);
        AddClientMembershipCommand = new AsyncRelayCommand(item => AddClientMembershipAsync(), item => CanAddClientMembership());
        LoadClientMembershipViewData = new AsyncRelayCommand(item => LoadClientMembershipViewDataAsync(), item => true);
    }

    private bool CanAddClientMembership()
    {
        return ClientId != Guid.Empty && _clientMembershipAddRequest.MembershipId != Guid.Empty;
    }

    private async Task LoadClientMembershipViewDataAsync()
    {
        await LoadMemberships();
        await LoadClientName();
    }

    private async Task AddClientMembershipAsync()
    {
        Result<ClientMembershipContractPreviewResponse> contractDetails = await _httpClient.GetContractPreviewDetailsAsync(_clientMembershipAddRequest.ClientId, _clientMembershipAddRequest.MembershipId);
        await GenerateClientMembershipContractPdf(contractDetails.Value!);
        MessageBoxResult messageBoxResult = MessageBox.Show("Is client signed a contract?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
        if (messageBoxResult == MessageBoxResult.Yes)
        {
            Result<ClientMembershipInfoResponse> result = await _httpClient.PostClientMembershipAsync(_clientMembershipAddRequest);
            if (!result.IsSuccess)
            {
                MessageBox.Show($"Error: {result.GetUserMessage()}");
                return;
            }
        }
        Navigation.NavigateTo<ClientDetailsViewModel>(ClientId);

    }

    public async Task GenerateClientMembershipContractPdf(
        ClientMembershipContractPreviewResponse contract)
    {
        if (contract == null)
            throw new ArgumentNullException(nameof(contract));

        // Dane siłowni (config / resources)
        string gymName = Application.Current?.Resources["GymName"] as string ?? "Siłownia XYZ";
        string gymAddress = Application.Current?.Resources["Address"] as string ?? "ul. Przykładowa 1, Miasto";
        string contactNumber = Application.Current?.Resources["ContactNumber"] as string ?? "000-000-000";
        string gymNip = Application.Current?.Resources["GymNip"] as string ?? "000-000-0000";

        var pl = CultureInfo.CreateSpecificCulture("pl-PL");

        string contractTitle = "UMOWA CZŁONKOWSKA – KARNET";

        string safeFileName = $"Umowa_{contract.FullName}_{contract.MembershipName}"
            .Replace(" ", "_")
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

                // ===== HEADER =====
                page.Header()
                    .AlignCenter()
                    .Text(contractTitle)
                    .FontSize(16)
                    .Bold();

                // ===== CONTENT =====
                page.Content().Column(column =>
                {
                    column.Spacing(6);

                    column.Item().Text(
                        //$"Zawarta dnia {contract.StartDate} w miejscowości {gymAddress.Split(',')[0]}.");
                        $"Zawarta dnia {contract.StartDate} w miejscowości {gymAddress}.");

                    column.Item().PaddingTop(10).Text("1. Strony umowy:");

                    column.Item().Text(
                        $"a) Klub: {gymName}, {gymAddress}, tel: {contactNumber}");

                    column.Item().Text(
                        $"b) Klient: {contract.FullName}");

                    column.Item().PaddingTop(10).Text("§1 Przedmiot umowy:");
                    column.Item().Text(
                        $"Przedmiotem niniejszej umowy jest korzystanie przez Klienta z usług klubu fitness " +
                        $"w ramach członkostwa: \"{contract.MembershipName}\".");

                    column.Item().PaddingTop(10).Text("§2 Okres obowiązywania:");
                    if (contract.EndDate != null)
                    {
                        column.Item().Text(
                            $"Umowa zostaje zawarta na okres od {contract.StartDate} do {contract.EndDate}.");
                    }
                    else
                    {
                        column.Item().Text(
                            $"Umowa zostaje zawarta na czas nieokreślony od {contract.StartDate}");
                    }

                    column.Item().PaddingTop(10).Text("§3 Opłaty:");
                    if (contract.EndDate != null)
                    {

                        column.Item().Text(
                            $"1. Cena członkostwa w ramach karnetu rocznego wynosi jednorazowo {contract.Price}$ w momencie podpisania umowy.");
                    }
                    else
                    {

                        column.Item().Text(
                            $"1. Cena członkostwa w ramach karnetu miesięcznego wynosi comiesięcznie {contract.Price}$. Pierwsza opłata miesięczna za pierwszy miesiąc następuje w trakcie podpisywania umowy");
                    }
                    column.Item().Text(
                        "2. Opłata uprawnia Klienta do korzystania z infrastruktury klubu zgodnie z regulaminem.");

                    column.Item().PaddingTop(10).Text("§4 Prawa i obowiązki Klienta:");
                    column.Item().Text(
                        "1. Klient zobowiązuje się do przestrzegania regulaminu klubu.");
                    column.Item().Text(
                        "2. Klient oświadcza, że stan zdrowia pozwala mu na korzystanie z usług klubu.");

                    column.Item().PaddingTop(10).Text("§5 Postanowienia końcowe:");
                    column.Item().Text(
                        "1. Umowa wchodzi w życie z dniem jej podpisania.");
                    column.Item().Text(
                        "2. W sprawach nieuregulowanych zastosowanie mają przepisy prawa polskiego.");

                    // ===== PODPISY =====
                    column.Item().PaddingTop(30).Row(row =>
                    {
                        row.RelativeItem().Column(left =>
                        {
                            left.Item().Text("Podpis Klubu:");
                            left.Item().PaddingTop(40).Text("_________________________");
                        });

                        row.RelativeItem().Column(right =>
                        {
                            right.Item().Text("Podpis Klienta:");
                            right.Item().PaddingTop(40).Text("_________________________");
                        });
                    });
                });

                // ===== FOOTER =====
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


    private async Task LoadMemberships()
    {
        Result<ObservableCollection<MembershipResponse>> result = await _membershipHttpClient.GetAllMembershipsAsync();
        if (!result.IsSuccess)
        {
            MessageBox.Show($"{result.GetUserMessage()}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
        else
        {
            MembershipSelectItem = new ObservableCollection<MembershipSelectItem>(
                result.Value!.Select(item => new MembershipSelectItem
                {
                    Id = item.Id,
                    Name = item.Name + " " + item.MembershipType,
                }));
        }
    }
    private async Task LoadClientName()
    {
        Result<ClientInfoResponse> result = await _clientHttpClient.GetClientNameById(ClientId);
        if (!result.IsSuccess)
        {
            MessageBox.Show($"{result.GetUserMessage()}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            return;
        }
        Client = result.Value!;
    }

    public void ReceiveParameter(object parameter)
    {
        if (parameter is Guid clientId)
        {
            ClientId = clientId;
            _clientMembershipAddRequest.ClientId = clientId;
        }
    }
}
