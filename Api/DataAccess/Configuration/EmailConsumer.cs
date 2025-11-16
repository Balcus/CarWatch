using System.Text;
using Api.BusinessLogic.Services.Abstraction;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Api.DataAccess.Configuration;

public class EmailConsumer : BackgroundService
{
    private readonly IServiceScopeFactory _scopeFactory;
    private string host;
    private int port;
    private string username;
    private string password;
    private string registrationQueue;
    private string activationQueue;

    public EmailConsumer(IServiceScopeFactory scopeFactory, IConfiguration configuration)
    {
        _scopeFactory = scopeFactory;
        host = configuration["RabbitMq:Host"];
        port = int.Parse(configuration["RabbitMq:Port"]);
        username = configuration["RabbitMq:Username"];
        password = configuration["RabbitMq:Password"];
        registrationQueue = configuration["RabbitMq:RegistrationQueue"];
        activationQueue  = configuration["RabbitMq:ActivationQueue"];
        Console.WriteLine("Sunt in consumer");
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var factory = new ConnectionFactory
        {
            HostName = host,
            Port = port,
            UserName = username,
            Password = password
        };

       await using IConnection conn = await factory.CreateConnectionAsync();
       await using IChannel channel = await conn.CreateChannelAsync();

        // Start consumers for both queues
        await StartConsumer(channel, registrationQueue, async email =>
        {
            using var scope = _scopeFactory.CreateScope();
            var emailService = scope.ServiceProvider.GetRequiredService<IEmailService>();
            await emailService.SendEmailRegistrationAsync(email);
        });

        await StartConsumer(channel, activationQueue, async email =>
        {
            using var scope = _scopeFactory.CreateScope();
            var emailService = scope.ServiceProvider.GetRequiredService<IEmailService>();
            await emailService.SendEmailActivationAsync(email);
        });

        // Keep the background service alive
        await Task.Delay(Timeout.Infinite, stoppingToken);
    }

    private async Task StartConsumer(IChannel channel, string queueName, Func<string, Task> handleMessage)
    {
        await channel.QueueDeclareAsync(
            queue: queueName,
            durable: true,
            exclusive: false,
            autoDelete: false
        );

        var consumer = new AsyncEventingBasicConsumer(channel);
        consumer.ReceivedAsync += async (model, ea) =>
        {
            var email = Encoding.UTF8.GetString(ea.Body.ToArray());
            Console.WriteLine($"Received message from {queueName}: {email}");
            await handleMessage(email);
        };

        await channel.BasicConsumeAsync(
            queue: queueName,
            autoAck: true,
            consumer: consumer
        );

        Console.WriteLine($"Started consuming queue: {queueName}");
    }
}