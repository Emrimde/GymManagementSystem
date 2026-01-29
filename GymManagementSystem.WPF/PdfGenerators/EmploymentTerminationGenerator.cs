using GymManagementSystem.Core.Domain.Entities;
using GymManagementSystem.Core.DTO.EmploymentTermination;
using GymManagementSystem.Core.DTO.GeneralGymDetail;
using GymManagementSystem.Core.DTO.Person;
using GymManagementSystem.Core.DTO.TrainerContract;
using GymManagementSystem.Core.Enum;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Windows;

namespace GymManagementSystem.WPF.PdfGenerators;
public static class EmploymentTerminationGenerator
{
    public static void GenerateEmploymentTerminationPdf(this EmploymentTerminationGenerateResponse model)
    {
        if (model == null) throw new ArgumentNullException(nameof(model));

        // Dane siłowni z Resources (fallbacky)
        string gymName = Application.Current?.Resources["GymName"] as string ?? "Siłownia XYZ";
        string gymAddress = Application.Current?.Resources["Address"] as string ?? "ul. Przykładowa 1, Miasto";
        string contactNumber = Application.Current?.Resources["ContactNumber"] as string ?? "000-000-000";
        string gymNip = Application.Current?.Resources["GymNip"] as string ?? "000-000-0000";

        var pl = CultureInfo.CreateSpecificCulture("pl-PL");
        string requestedDateText = model.RequestedDate.ToString("yyyy-MM-dd", pl);
        string effectiveDateText = model.EffectiveDate.ToString("yyyy-MM-dd", pl);

        // Tytuł i teksty zależne od typu umowy (model.ContractType powinien być czytelnym stringiem np. "B2B", "Umowa o pracę", "Zlecenie")
        string contractTitle = $"Wypowiedzenie umowy — {model.ContractType}";
        string terminationClause = model.ContractType?.ToLowerInvariant() switch
        {
            var s when s.Contains("umowa o pracę") || s.Contains("umowaoprace") || s.Contains("praca") =>
                "Zgodnie z obowiązującymi przepisami Kodeksu pracy okres wypowiedzenia ustala się zgodnie z długością zatrudnienia. Strony potwierdzają, że obowiązuje okres wypowiedzenia wskazany w niniejszym dokumencie.",
            var s when s.Contains("b2b") || s.Contains("b-2-b") =>
                "Umowa B2B może być rozwiązana przez każdą ze stron z zachowaniem warunków określonych w treści umowy. Strony uzgadniają w niniejszym wypowiedzeniu okres wypowiedzenia określony powyżej.",
            var s when s.Contains("zlecenie") || s.Contains("zlec") =>
                "Umowa zlecenie może być rozwiązana przez każdą ze stron na zasadach określonych w umowie. Strony ustalają okres wypowiedzenia podany powyżej.",
            _ =>
                "Zasady rozwiązania umowy określone są w treści umowy i niniejszym wypowiedzeniu."
        };

        // Bezpieczna nazwa pliku
        string safeFileName = $"{model.FirstName}_{model.LastName}_Wypowiedzenie_{Guid.NewGuid()}"
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
                page.Header()
                    .AlignCenter()
                    .Text(contractTitle)
                    .FontSize(16)
                    .Bold();

                page.Content().Column(column =>
                {
                    column.Item().Text($"Wypowiedzenie wygenerowano: {requestedDateText}.");
                    column.Item().PaddingTop(8).Text("Strony:");
                    column.Item().Text($"a) Klub: {gymName}, {gymAddress}, NIP: {gymNip}, tel: {contactNumber}");
                    column.Item().Text($"b) Osoba: {model.FirstName} {model.LastName} (typ umowy: {model.ContractType ?? "—"})");
                    if (!string.IsNullOrWhiteSpace(model.EmploymentType))
                        column.Item().Text($"   Forma zatrudnienia: {model.EmploymentType}");

                    column.Item().PaddingTop(10).Text("§1 Przedmiot wypowiedzenia:");
                    column.Item().Text("Niniejszy dokument stanowi wypowiedzenie umowy zawartej między stronami w zakresie określonym w umowie.");

                    column.Item().PaddingTop(10).Text("§2 Okres wypowiedzenia:");
                    column.Item().Text($"Wypowiedzenie zostało zgłoszone dnia: {requestedDateText}. Niniejsze wypowiedzenie staje się skuteczne z dniem: {effectiveDateText}.");
                    column.Item().Text(terminationClause);

                    column.Item().PaddingTop(10).Text("§3 Skutki wypowiedzenia:");
                    column.Item().Text("1. Po upływie okresu wypowiedzenia osoba przestaje wykonywać obowiązki wynikające z umowy oraz traci prawa wynikające z aktywnych uprawnień pracowniczych / kontraktowych.");
                    column.Item().Text("2. Klub może rozliczyć należności wynikające z dotychczasowej współpracy zgodnie z umową.");

                    column.Item().PaddingTop(10).Text("§4 Postanowienia końcowe:");
                    column.Item().Text("1. Wszelkie zmiany niniejszego wypowiedzenia wymagają formy pisemnej.");
                    column.Item().Text("2. W sprawach nieuregulowanych zastosowanie mają odpowiednie przepisy prawa polskiego.");

                    column.Item().PaddingTop(24).Row(row =>
                    {
                        row.RelativeItem().Text($"Data wypowiedzenia: {requestedDateText}");
                        row.RelativeItem().Text($"Data skuteczności: {effectiveDateText}");
                    });

                    column.Item().PaddingTop(30).Row(row =>
                    {
                        row.ConstantItem(250).Column(left =>
                        {
                            left.Item().Text("Podpis przedstawiciela Klubu:");
                            left.Item().PaddingTop(40).Text("_________________________");
                        });

                        row.ConstantItem(250).Column(right =>
                        {
                            right.Item().Text(model.ContractType != null && model.ContractType.ToLowerInvariant().Contains("b2b")
                                ? "Podpis Wykonawcy (firma):"
                                : "Podpis Pracownika / Zleceniobiorcy:");
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

    public static void GenerateTrainerContractPdf(this TrainerContractAddRequest request,GeneralGymResponse gym, PersonDetailsResponse person)
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
            $"{person.FirstName}_{person.LastName}_TrainerContract_{Guid.NewGuid()}"
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
                        $"The Trainer: {person.FirstName} {person.LastName}, " +
                        $"Address: {person.Address}, " +
                        $"Email: {person.Email}, Phone: {person.PhoneNumber}");

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
