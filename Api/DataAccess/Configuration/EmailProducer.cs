using System.Text;
using RabbitMQ.Client;

namespace Api.DataAccess.Configuration;

public class EmailProducer
{
    private string host;
    private int port;
    private string username;
    private string password;
    private string queue;

    public EmailProducer(IConfiguration config)
    {
        host = config["RabbitMq:Host"];
        port = int.Parse(config["RabbitMq:Port"]);
        username = config["RabbitMq:Username"];
        password = config["RabbitMq:Password"];
        queue = config["RabbitMq:Queue"];
        Console.WriteLine("Sunt in producer");
    }

    public async Task PublishEmail(string email)
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
        // string exchangeName = "exchange-email";
        // string routingKey = "routing-key";
        // await channel.ExchangeDeclareAsync(exchangeName, ExchangeType.Direct);
        await channel.QueueDeclareAsync(queue: queue,
            durable: true,
            exclusive: false,
            autoDelete: false);

        byte[] messageBodyBytes = System.Text.Encoding.UTF8.GetBytes(email);
        var props = new BasicProperties();
        await channel.BasicPublishAsync(exchange: string.Empty, routingKey: "emailQueue", false, props, messageBodyBytes);

    }
}