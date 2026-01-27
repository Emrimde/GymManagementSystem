using GymManagementSystem.Core.Domain.Entities;
using GymManagementSystem.Core.DTO.GeneralGymDetail;
using GymManagementSystem.Core.DTO.Person;
using GymManagementSystem.Core.DTO.TrainerContract;
using GymManagementSystem.Core.Enum;
using GymManagementSystem.WPF.Core;
using GymManagementSystem.WPF.HttpServices;
using GymManagementSystem.WPF.Result;
using GymManagementSystem.WPF.ServiceContracts;
using GymManagementSystem.WPF.ViewModels.Employee;
using GymManagementSystem.WPF.ViewModels.TrainerContract;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Windows;
using System.Windows.Input;

namespace GymManagementSystem.WPF.ViewModels.Staff;
public class StaffDetailsViewModel : ViewModel, IParameterReceiver
{
    private readonly StaffHttpClient _staffHttpClient;
    private readonly TrainerHttpClient _trainerHttpClient;
    private readonly GeneralGymDetailsHttpClient _generalGymHttpClient;
    public SidebarViewModel SidebarView { get; set; }
    public INavigationService Navigation { get; set; }
    public ICommand OpenAddTrainerViewCommand { get; }
    public ICommand OpenEmployeeViewCommand { get; }
    public ICommand AddPersonalTrainerRoleCommand { get; }
    public ICommand AddGroupTrainerRoleCommand { get; }
    public ICommand AddEmployeeRoleCommand { get; }

    private PersonDetailsResponse _person = new();

    public StaffDetailsViewModel(StaffHttpClient staffHttpClient, SidebarViewModel sidebarView, INavigationService navigation, TrainerHttpClient trainerHttpClient, GeneralGymDetailsHttpClient generalGymHttpClient)
    {
        _staffHttpClient = staffHttpClient;
        SidebarView = sidebarView;
        Navigation = navigation;
        _generalGymHttpClient = generalGymHttpClient;
        OpenAddTrainerViewCommand = new RelayCommand(item => Navigation.NavigateTo<TrainerContractAddViewModel>(PersonId), item => true);
        OpenEmployeeViewCommand = new RelayCommand(item => Navigation.NavigateTo<EmployeeAddViewModel>(PersonId), item => true);
        AddPersonalTrainerRoleCommand = new AsyncRelayCommand(item => AddPersonalTrainerRoleAsync(TrainerTypeEnum.PersonalTrainer), item => true);
        AddGroupTrainerRoleCommand = new AsyncRelayCommand(item => AddGroupInstructorRoleAsync(TrainerTypeEnum.GroupInstructor), item => true);
        Person = new PersonDetailsResponse();
        _trainerHttpClient = trainerHttpClient;
    }

    private async Task AddGroupInstructorRoleAsync(TrainerTypeEnum groupInstructor)
    {
        await AddTrainerAsync(groupInstructor);
    }

    private async Task AddPersonalTrainerRoleAsync(TrainerTypeEnum personalTrainer)
    {
        await AddTrainerAsync(personalTrainer);
    }

    public PersonDetailsResponse Person
    {
        get { return _person; }
        set { _person = value; OnPropertyChanged(); }
    }


    public Guid PersonId { get; set; }
    public void ReceiveParameter(object parameter)
    {
        if (parameter is Guid personId)
        {
            PersonId = personId;
            _ = LoadPersonDetailsAsync();
        }
    }

    private async Task LoadPersonDetailsAsync()
    {
        Result<PersonDetailsResponse> result = await _staffHttpClient.GetPersonDetailsAsync(PersonId);
        if (!result.IsSuccess)
        {
            MessageBox.Show(result.GetUserMessage());
            return;
        }
        Person = result.Value!;
    }

    private async Task AddTrainerAsync(TrainerTypeEnum trainerType)
    {
        TrainerContractAddRequest request = new TrainerContractAddRequest()
        {
            PersonId = PersonId,
            ContractType = ContractTypeEnum.ContractOfMandate,
            TrainerType = trainerType,
            ClubCommissionPercent = 10m
        };

        Result<GeneralGymResponse> generalGymResult = await _generalGymHttpClient.GetGeneralGymSettingsAsync();
        if (!generalGymResult.IsSuccess)
        {
            MessageBox.Show("Error during loading gym data", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }


        GenerateTrainerContractPdf(request, generalGymResult.Value!);
        MessageBoxResult response = MessageBox.Show("Is trainer signed contract?", "Confirmation", MessageBoxButton.YesNo);
        if (response == MessageBoxResult.Yes)
        {
            Result<TrainerContractInfoResponse> result = await _trainerHttpClient.PostTrainerContractAsync(request);
            if (result.IsSuccess)
            {
                Navigation.NavigateTo<TrainerContractDetailsViewModel>(result.Value!.Id);
            }
            else
            {
                MessageBox.Show($"{result.GetUserMessage()}");
            }
        }


    }

    public void GenerateTrainerContractPdf(
    TrainerContractAddRequest request,
    GeneralGymResponse gym
   )
    {
        var culture = CultureInfo.InvariantCulture;

        string contractTitle = "Contract of Mandate (Trainer Agreement)";

        string trainerRole = request.TrainerType switch
        {
            TrainerTypeEnum.PersonalTrainer => "Personal Trainer",
            TrainerTypeEnum.GroupInstructor => "Group Fitness Instructor",
            _ => request.TrainerType.ToString()
        };

        string commissionText = request.TrainerType == TrainerTypeEnum.PersonalTrainer
    ? $"{request.ClubCommissionPercent:0.##}% club commission from each client payment."
    : "";


        string mainClause =
            "The subject of this agreement is the provision of training services " +
            "(training sessions, classes, consultations) by the Contractor for the Club.";

        string paymentClause = request.TrainerType switch
        {
            TrainerTypeEnum.PersonalTrainer =>
                $"Remuneration is settled per completed service. " +
                $"The Club commission amounts to {request.ClubCommissionPercent:0.##}%. " +
                $"Payments are made within 14 days based on settlement documents.",

            TrainerTypeEnum.GroupInstructor =>
                "The remuneration is determined in accordance with the currently applicable price list/rates specified in the appendix/settlement system of the Club.”",

            _ => string.Empty
        };


        string terminationClause =
            "Either party may terminate this agreement with a 14-day notice period.";

        // SAFE FILE NAME
        string safeFileName =
            $"{Person.FirstName}_{Person.LastName}_TrainerContract_{Guid.NewGuid()}"
                .Replace(" ", "_");

        string desktop = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        string filePath = Path.Combine(desktop, $"{safeFileName}.pdf");

        var document = Document.Create(container =>
        {
            container.Page(page =>
            {
                page.Size(PageSizes.A4);
                page.Margin(40);
                page.DefaultTextStyle(x => x.FontSize(11));

                page.Header()
                    .AlignCenter()
                    .Text(contractTitle)
                    .FontSize(16)
                    .Bold();

                page.Content().Column(column =>
                {
                    column.Item().Text(
                        $"This agreement is concluded on {DateTime.UtcNow:yyyy-MM-dd}.");

                    column.Item().PaddingTop(10).Text("1. Parties:");

                    column.Item().Text(
                        $"The Club: {gym.GymName}, {gym.Address}, phone: {gym.ContactNumber}");

                    column.Item().PaddingTop(5).Text(
                        $"The Trainer: {Person.FirstName} {Person.LastName}, " +
                        $"Address: {Person.Address}, " +
                        $"Email: {Person.Email}, Phone: {Person.PhoneNumber}");

                    column.Item().PaddingTop(10).Text($"Role: {trainerRole}");

                    column.Item().PaddingTop(10).Text("§1 Subject of the Agreement");
                    column.Item().Text(mainClause);

                    column.Item().PaddingTop(10).Text("§2 Duties");
                    column.Item().Text(
                        "1. Conducting training sessions according to the Club schedule.\n" +
                        "2. Maintaining professional standards and due diligence.");

                    column.Item().PaddingTop(10).Text("§3 Remuneration");
                    column.Item().Text(paymentClause);
                    column.Item().Text(commissionText);

                    column.Item().PaddingTop(10).Text("§4 Termination");
                    column.Item().Text(terminationClause);

                    column.Item().PaddingTop(20).Row(row =>
                    {
                        row.RelativeItem().Text("Club signature:");
                        row.RelativeItem().Text("Trainer signature:");
                    });

                    column.Item().PaddingTop(30).Row(row =>
                    {
                        row.RelativeItem().Text("______________________");
                        row.RelativeItem().Text("______________________");
                    });
                });

                page.Footer()
                    .AlignCenter()
                    .Text(t =>
                    {
                        t.CurrentPageNumber();
                        t.Span(" / ");
                        t.TotalPages();
                    });
            });
        });

        document.GeneratePdf(filePath);

        Process.Start(new ProcessStartInfo
        {
            FileName = filePath,
            UseShellExecute = true
        });
    }


}
