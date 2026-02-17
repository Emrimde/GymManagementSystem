using GymManagementSystem.Core.DTO.ClientMembership;
using GymManagementSystem.Core.DTO.Employee;
using GymManagementSystem.Core.Enum;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using System.Diagnostics;
using System.IO;
using System.Windows;

namespace GymManagementSystem.WPF.PdfGenerators;
public static class ContractGenerator
{
    public static void GenerateEmploymentContractPdf(EmploymentContractPdfDto employee)
    {
        if (employee == null)
            throw new ArgumentNullException(nameof(employee));

        string employmentTypeText = employee.EmploymentType switch
        {
            EmploymentType.FullTime => "Full-time",
            EmploymentType.HalfTime => "Half-time",
            EmploymentType.QuarterTime => "Quarter-time",
            _ => "-"
        };

        string safeFileName =
            $"EmploymentContract_{employee.ContractType}_{employee.Role}_{Guid.NewGuid()}"
                .Replace(" ", "_")
                .Replace("/", "_")
                .Replace(":", "");

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
                page.PageColor(Colors.White);
                page.DefaultTextStyle(x => x.FontSize(11));

                page.Header()
                    .AlignCenter()
                    .Text(text =>
                    {
                        text.Span(employee.ContractType.ToString())
                            .FontSize(16)
                            .Bold();
                    });

                page.Content().Column(column =>
                {
                    column.Spacing(6);

                    column.Item().Text(
                        $"This agreement is concluded on {employee.ValidFrom} at {employee.GymAddress}.");

                    column.Item().PaddingTop(10).Text("1. Parties:");

                    column.Item().Text(
                        $"a) Employer: {employee.GymName}, {employee.GymAddress}, " +
                        $"Tax ID: {employee.Nip}, Phone: {employee.ContactNumber}");

                    column.Item().Text(
                        $"b) Employee: {employee.FirstName} {employee.LastName}, " +
                        $"Address: {employee.Address}, " +
                        $"Position: {employee.Role}, " +
                        $"Employment type: {employmentTypeText}, " +
                        $"Email: {employee.Email}, Phone: {employee.PhoneNumber}");

                    column.Item().PaddingTop(10).Text("§1 Subject of the Agreement:");
                    column.Item().Text(
                        "The Employer hires the Employee, and the Employee agrees to perform duties in accordance with this agreement.");

                    column.Item().PaddingTop(10).Text("§2 Term of Employment:");
                    column.Item().Text(
                        string.IsNullOrWhiteSpace(employee.ValidTo)
                            ? $"The agreement is concluded starting from {employee.ValidFrom}."
                            : $"The agreement is valid from {employee.ValidFrom} until {employee.ValidTo}.");

                    column.Item().PaddingTop(10).Text("§3 Remuneration:");
                    column.Item().Text(
                        $"The gross salary amounts to {employee.Salary}.");

                    column.Item().PaddingTop(10).Text("§4 Working Time and Duties:");
                    column.Item().Text(
                        "The Employee agrees to perform duties in accordance with the assigned role and internal regulations.");

                    column.Item().PaddingTop(10).Text("§5 Final Provisions:");
                    column.Item().Text(
                        "In matters not regulated by this agreement, applicable labor law provisions shall apply.");

                    column.Item().PaddingTop(30).Row(row =>
                    {
                        row.RelativeItem().Column(left =>
                        {
                            left.Item().Text("Employer Signature:");
                            left.Item().PaddingTop(40).Text("_________________________");
                        });

                        row.RelativeItem().Column(right =>
                        {
                            right.Item().Text("Employee Signature:");
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
            MessageBox.Show($"Error while generating PDF: {ex.Message}");
        }
    }
    public static void GenerateClientMembershipContractPdf(
    ClientMembershipContractPreviewResponse contract)
    {
        if (contract == null)
            throw new ArgumentNullException(nameof(contract));

        string gymName = Application.Current?.Resources["GymName"] as string ?? "XYZ Gym";
        string gymAddress = Application.Current?.Resources["Address"] as string ?? "1 Example Street, City";
        string contactNumber = Application.Current?.Resources["ContactNumber"] as string ?? "000-000-000";
        string gymNip = Application.Current?.Resources["GymNip"] as string ?? "000-000-0000";

        string contractTitle = "MEMBERSHIP AGREEMENT";

        string safeFileName = $"Contract_{contract.FullName}_{contract.MembershipName}"
            .Replace(" ", "_")
            .Replace("/", "_");

        string downloads = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),
            "Downloads");

        string filePath = Path.Combine(downloads, $"{safeFileName}.pdf");

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
                    column.Spacing(6);

                    column.Item().Text(
                        $"This agreement is concluded on {contract.StartDate} at {gymAddress}.");

                    column.Item().PaddingTop(10).Text("1. Parties of the Agreement:");

                    column.Item().Text(
                        $"a) The Club: {gymName}, {gymAddress}, phone: {contactNumber}");

                    column.Item().Text(
                        $"b) The Client: {contract.FullName}");

                    column.Item().PaddingTop(10).Text("§1 Subject of the Agreement:");
                    column.Item().Text(
                        $"The subject of this agreement is the Client’s use of the fitness club services " +
                        $"under the membership plan: \"{contract.MembershipName}\".");

                    column.Item().PaddingTop(10).Text("§2 Duration:");
                    if (contract.EndDate != null)
                    {
                        column.Item().Text(
                            $"The agreement is concluded for a fixed term from {contract.StartDate} to {contract.EndDate}.");
                    }
                    else
                    {
                        column.Item().Text(
                            $"The agreement is concluded for an indefinite period starting from {contract.StartDate}.");
                    }

                    column.Item().PaddingTop(10).Text("§3 Fees:");
                    if (contract.EndDate != null)
                    {
                        column.Item().Text(
                            $"1. The annual membership fee is {contract.Price}$, payable in full at the time of signing this agreement.");
                    }
                    else
                    {
                        column.Item().Text(
                            $"1. The monthly membership fee is {contract.Price}$ per month. " +
                            $"The first monthly payment is due at the time of signing this agreement.");
                    }

                    column.Item().Text(
                        "2. The fee entitles the Client to use the club facilities in accordance with the club regulations.");

                    column.Item().PaddingTop(10).Text("§4 Rights and Obligations of the Client:");
                    column.Item().Text(
                        "1. The Client agrees to comply with the club regulations.");
                    column.Item().Text(
                        "2. The Client declares that their health condition allows them to use the club services.");

                    column.Item().PaddingTop(10).Text("§5 Final Provisions:");
                    column.Item().Text(
                        "1. This agreement enters into force on the date of signing.");
                    column.Item().Text(
                        "2. Matters not regulated herein shall be governed by applicable law.");

                    column.Item().PaddingTop(30).Row(row =>
                    {
                        row.RelativeItem().Column(left =>
                        {
                            left.Item().Text("Club Signature:");
                            left.Item().PaddingTop(40).Text("_________________________");
                        });

                        row.RelativeItem().Column(right =>
                        {
                            right.Item().Text("Client Signature:");
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

}
