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
        if (model == null)
            throw new ArgumentNullException(nameof(model));

        string gymName = Application.Current?.Resources["GymName"] as string ?? "Gym XYZ";
        string gymAddress = Application.Current?.Resources["Address"] as string ?? "Sample Street 1, City";
        string contactNumber = Application.Current?.Resources["ContactNumber"] as string ?? "000-000-000";
        string gymTaxId = Application.Current?.Resources["GymNip"] as string ?? "000-000-0000";

        string requestedDateText = model.RequestedDate.ToString("yyyy-MM-dd");
        string effectiveDateText = model.EffectiveDate.ToString("yyyy-MM-dd");

        string contractType = model.ContractType?.ToLowerInvariant() ?? "";

        string terminationClause;

        if (contractType.Contains("employment"))
        {
            terminationClause =
                "The termination period is determined according to applicable labor law and the length of employment.";
        }
        else if (contractType.Contains("b2b"))
        {
            terminationClause =
                "The B2B agreement may be terminated by either party according to the terms specified in the contract.";
        }
        else if (contractType.Contains("commission"))
        {
            terminationClause =
                "The commission agreement may be terminated by either party according to the contract terms.";
        }
        else
        {
            terminationClause =
                "The termination conditions are defined in the agreement between the parties.";
        }

        string safeFileName = $"{model.FirstName}_{model.LastName}_Termination_{Guid.NewGuid()}"
            .Replace(" ", "_")
            .Replace(":", "")
            .Replace("/", "_");

        string downloadsPath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),
            "Downloads");

        if (!Directory.Exists(downloadsPath))
            Directory.CreateDirectory(downloadsPath);

        string filePath = Path.Combine(downloadsPath, $"{safeFileName}.pdf");

        var document = Document.Create(container =>
        {
            container.Page(page =>
            {
                page.Size(PageSizes.A4);
                page.Margin(40);
                page.DefaultTextStyle(x => x.FontSize(11));

                page.Header()
                    .AlignCenter()
                    .Text($"Contract Termination — {model.ContractType ?? "N/A"}")
                    .FontSize(16)
                    .Bold();

                page.Content().Column(column =>
                {
                    column.Item().Text($"Generated on: {requestedDateText}");
                    column.Item().PaddingTop(8).Text("Parties:");
                    column.Item().Text($"a) Gym: {gymName}, {gymAddress}, Tax ID: {gymTaxId}, Phone: {contactNumber}");
                    column.Item().Text($"b) Person: {model.FirstName} {model.LastName}");

                    if (!string.IsNullOrWhiteSpace(model.EmploymentType))
                        column.Item().Text($"   Employment type: {model.EmploymentType}");

                    column.Item().PaddingTop(10).Text("1. Subject of Termination:");
                    column.Item().Text("This document constitutes formal termination of the agreement between the parties.");

                    column.Item().PaddingTop(10).Text("2. Notice Period:");
                    column.Item().Text($"The termination was submitted on {requestedDateText} and becomes effective on {effectiveDateText}.");
                    column.Item().Text(terminationClause);

                    column.Item().PaddingTop(10).Text("3. Legal Effects:");
                    column.Item().Text("After the notice period, the person ceases to perform contractual duties and loses related rights.");
                    column.Item().Text("The Gym may settle any outstanding obligations in accordance with the agreement.");

                    column.Item().PaddingTop(24).Row(row =>
                    {
                        row.RelativeItem().Text($"Notice Date: {requestedDateText}");
                        row.RelativeItem().Text($"Effective Date: {effectiveDateText}");
                    });

                    column.Item().PaddingTop(30).Row(row =>
                    {
                        row.ConstantItem(250).Column(left =>
                        {
                            left.Item().Text("Gym Representative Signature:");
                            left.Item().PaddingTop(40).Text("_________________________");
                        });

                        row.ConstantItem(250).Column(right =>
                        {
                            right.Item().Text(contractType.Contains("b2b")
                                ? "Contractor Signature:"
                                : "Employee Signature:");
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
            MessageBox.Show($"Error while generating PDF: {ex.Message}");
        }
    }


    public static void GenerateTrainerContractPdf(
      this TrainerContractAddRequest request,
      GeneralGymResponse gym,
      PersonDetailsResponse person)
    {
        string contractTitle = "Contract of Mandate (Trainer Agreement)";

        string trainerRole = request.TrainerType switch
        {
            TrainerTypeEnum.PersonalTrainer => "Personal Trainer",
            TrainerTypeEnum.GroupInstructor => "Group Fitness Instructor",
            _ => request.TrainerType.ToString()
        };

        string commissionText = request.TrainerType == TrainerTypeEnum.PersonalTrainer
            ? $"{request.ClubCommissionPercent:0.##}% club commission from each client payment."
            : string.Empty;

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
                "The remuneration is determined in accordance with the currently applicable rates defined by the Club.",

            _ => string.Empty
        };

        string terminationClause =
            "Either party may terminate this agreement with a 14-day notice period.";

        string safeFileName =
            $"{person.FirstName}_{person.LastName}_TrainerContract_{Guid.NewGuid()}"
                .Replace(" ", "_")
                .Replace(":", "")
                .Replace("/", "_");

        string downloadsPath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),
            "Downloads");

        if (!Directory.Exists(downloadsPath))
            Directory.CreateDirectory(downloadsPath);

        string filePath = Path.Combine(downloadsPath, $"{safeFileName}.pdf");

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

                    if (!string.IsNullOrWhiteSpace(commissionText))
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
