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
    private string queue;

    public EmailConsumer(IServiceScopeFactory scopeFactory, IConfiguration configuration)
    {
        _scopeFactory = scopeFactory;
        host = configuration["RabbitMq:Host"];
        port = int.Parse(configuration["RabbitMq:Port"]);
        username = configuration["RabbitMq:Username"];
        password = configuration["RabbitMq:Password"];
        queue = configuration["RabbitMq:Queue"];
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

        IConnection conn = await factory.CreateConnectionAsync();
        IChannel channel = await conn.CreateChannelAsync();

        await channel.QueueDeclareAsync(queue: queue,
            durable: true,
            exclusive: false,
            autoDelete: false);

        var consumer = new AsyncEventingBasicConsumer(channel);
        
        consumer.ReceivedAsync += async (model, ea) =>
        {
            using var scope = _scopeFactory.CreateScope();
            var _emailService = scope.ServiceProvider.GetRequiredService<IEmailService>();
            var email = Encoding.UTF8.GetString(ea.Body.ToArray()); 
            Console.WriteLine("Sunt in consumer. Acesta este mesajul:" + email);
            await _emailService.SendEmailAsync(email);
        };

        await channel.BasicConsumeAsync(queue: queue, autoAck: true, consumer: consumer);
        
    }
}