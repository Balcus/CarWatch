using System.Net.Mail;
using Api.BusinessLogic.Services.Abstraction;

namespace Api.BusinessLogic.Services.Implementation;

public class EmailService: IEmailService
{
    private string fromEmail;
    private string server;
    private int port;
    public EmailService(IConfiguration config)
    {
        fromEmail = config["EmailSettings:From"];
        server = config["EmailSettings:SmtpServer"];
        port = int.Parse(config["EmailSettings:Port"]);
    }
    public Task SendEmailAsync(string toEmail)
    {
        MailAddress from = new MailAddress(fromEmail);
        MailAddress to = new  MailAddress(toEmail);
        MailMessage message = new MailMessage(from, to);
        message.Subject = "User Registration";
        message.IsBodyHtml = true;
        message.Body = $@"
    <html>
        <body style='font-family: Arial, sans-serif; background-color: #f4f4f4; padding: 20px;'>
            <div style='max-width: 600px; margin: auto; background-color: #ffffff; padding: 20px; border-radius: 8px;'>
                <h2 style='color: #333;'>Welcome to YourApp!</h2>
                <p>Hi <strong>{toEmail}</strong>,</p>
                <p>Thank you for registering. We will review your application shortly, so you can start using your account.</p>
                <p style='text-align: center; margin: 30px 0;'>
                </p>
                <p style='color: #888; font-size: 12px;'>If you did not sign up for this account, please ignore this email.</p>
            </div>
        </body>
    </html>";
        
        var client = new SmtpClient(server, port);
        client.Send(message);
        return Task.CompletedTask;
    }
}