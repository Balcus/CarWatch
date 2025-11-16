namespace Api.BusinessLogic.Services.Abstraction;

public interface IEmailService
{
    Task SendEmailRegistrationAsync(string toEmail);

    Task SendEmailActivationAsync(string toEmail);
}