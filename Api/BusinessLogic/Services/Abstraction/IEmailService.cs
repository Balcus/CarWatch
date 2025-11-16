namespace Api.BusinessLogic.Services.Abstraction;

public interface IEmailService
{
    Task SendEmailAsync(string toEmail);
}