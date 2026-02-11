using GymManagementSystem.Core.DTO.Email;
using GymManagementSystem.Core.ServiceContracts;
using Microsoft.Extensions.Configuration;
using System.Net;
using System.Net.Mail;

namespace GymManagementSystem.Core.Services;
public class EmailService : IEmailService
{
    private readonly IConfiguration _configuration;

    public EmailService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task SendLink(EmailRequest request)
    {
        using var client = new SmtpClient
        {
            Host = _configuration["Smtp:Host"]!,
            Port = int.Parse(_configuration["Smtp:Port"]!),
            EnableSsl = true,
            Credentials = new NetworkCredential(
                _configuration["Smtp:Username"],
                _configuration["Smtp:Password"]
            )
        };

        using var mail = new MailMessage
        {
            From = new MailAddress(_configuration["Smtp:From"]!),
            Subject = request.Subject,
            Body = request.Body,
            IsBodyHtml = true,
           
        };

        mail.To.Add(request.To);

        await client.SendMailAsync(mail);
    }
}


