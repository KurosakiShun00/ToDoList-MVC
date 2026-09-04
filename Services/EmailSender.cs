using System.Net;
using System.Net.Mail;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace ToDoList_MVC.Services;

public class EmailSender : IEmailSender
{
    private readonly IConfiguration _configuration;
    private readonly IWebHostEnvironment _env;
    private readonly ILogger<EmailSender> _logger;

    public EmailSender(IConfiguration configuration, ILogger<EmailSender> logger, IWebHostEnvironment env)
    {
        _configuration = configuration;
        _logger = logger;
        _env = env;
    }

    public async Task SendEmailAsync(string email, string subject, string htmlMessage)
    {
        if (_env.IsDevelopment() || string.IsNullOrEmpty(_configuration["Smtp:Host"]))
        {
            _logger.LogInformation("================ MOCK EMAIL GENERATA ================");
            _logger.LogInformation("A: {Email}", email);
            _logger.LogInformation("Oggetto: {Subject}", subject);
            _logger.LogInformation("Messaggio HTML:\n{Body}", htmlMessage);
            _logger.LogInformation("====================================================");

            await Task.CompletedTask;
            return;
        }

        var host = _configuration["Smtp:Host"];
        var port = int.Parse(_configuration["Smtp:Port"] ?? "587");
        var username = _configuration["Smtp:Username"];
        var password = _configuration["Smtp:Password"];
        var fromEmail = _configuration["Smtp:From"];

        using (var client = new SmtpClient(host, port))
        {
            client.Credentials = new NetworkCredential(username, password);
            client.EnableSsl = true;

            var mailMessage = new MailMessage
            {
                From = new MailAddress(fromEmail ?? "noreply@todolist.com", "ToDoList App"),
                Subject = subject,
                Body = htmlMessage,
                IsBodyHtml = true
            };

            mailMessage.To.Add(email);

            await client.SendMailAsync(mailMessage);
        }
    }
}