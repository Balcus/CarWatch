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
    public Task SendEmailRegistrationAsync(string toEmail)
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
    
    public Task SendEmailActivationAsync(string toEmail)
    {
        MailAddress from = new MailAddress(fromEmail);
        MailAddress to = new MailAddress(toEmail);

        MailMessage message = new MailMessage(from, to)
        {
            Subject = "Account Activated",
            IsBodyHtml = true,
            Body = $@"
<html>
    <body style='font-family: Arial, sans-serif; background-color: #f4f4f4; padding: 20px;'>
        <div style='max-width: 600px; margin: auto; background-color: #ffffff; padding: 20px; border-radius: 8px;'>
            <h2 style='color: #333;'>Your Account is Now Active!</h2>
            <p>Hi <strong>{toEmail}</strong>,</p>
            <p>Congratulations! Your account has been successfully activated. You can now log in and start using our services.</p>
            <p style='text-align: center; margin: 30px 0;'>
                <a href='https://yourapp.com/login' style='background-color: #4CAF50; color: white; padding: 10px 20px; text-decoration: none; border-radius: 5px;'>Log In</a>
            </p>
            <p style='color: #888; font-size: 12px;'>If you did not request this activation, please contact our support team immediately.</p>
        </div>
    </body>
</html>"
        };

        var client = new SmtpClient(server, port);
        client.Send(message);

        return Task.CompletedTask;
    }
}