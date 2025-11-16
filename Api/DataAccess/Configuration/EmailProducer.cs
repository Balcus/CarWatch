using System.Text;
using RabbitMQ.Client;

namespace Api.DataAccess.Configuration;

public class EmailProducer
{
    private string host;
    private int port;
    private string username;
    private string password;
    private string registrationQueue;
    private string activationQueue;

    public EmailProducer(IConfiguration config)
    {
        host = config["RabbitMq:Host"];
        port = int.Parse(config["RabbitMq:Port"]);
        username = config["RabbitMq:Username"];
        password = config["RabbitMq:Password"];
        registrationQueue = config["RabbitMq:RegistrationQueue"];
        activationQueue  = config["RabbitMq:ActivationQueue"];
        Console.WriteLine("Sunt in producer");
    }

    public async Task EmailRegistration(string email)
    {
        string queueName = registrationQueue;
        await PublishToQueue(email, queueName);

    }
    
    public async Task EmailActivation(string email)
    {
        string queueName = activationQueue;
        await PublishToQueue(email, queueName);
    }
    
    private async Task PublishToQueue(string message, string queueName)
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

        await channel.QueueDeclareAsync(queue: registrationQueue,
            durable: true,
            exclusive: false,
            autoDelete: false);

        byte[] messageBodyBytes = System.Text.Encoding.UTF8.GetBytes(message);
        var props = new BasicProperties();
        await channel.BasicPublishAsync(exchange: string.Empty, routingKey: queueName, false, props, messageBodyBytes);
    }
}