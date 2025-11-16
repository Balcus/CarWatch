var builder = DistributedApplication.CreateBuilder(args);

var postgres = builder.AddPostgres("postgres")
    .WithDataVolume(isReadOnly: false)
    .WithLifetime(ContainerLifetime.Persistent)
    .WithPgAdmin();

var db = postgres.AddDatabase("appdb");

var rabbit = builder.AddContainer("rabbitmq", "rabbitmq", "3-management")
    .WithEndpoint(5672, 5672, "amqp")
    .WithEndpoint(15672, 15672, "management")
    .WithEnvironment("RABBITMQ_DEFAULT_USER", "admin")
    .WithEnvironment("RABBITMQ_DEFAULT_PASS", "admin")
    .WithLifetime(ContainerLifetime.Persistent);

var maildev = builder.AddContainer("maildev", "maildev/maildev")
    .WithEndpoint(name: "smtp", port: 1025, targetPort: 1025)      // SMTP port
    .WithEndpoint(name: "web", port: 1080, targetPort: 1080)
    .WithLifetime(ContainerLifetime.Persistent); 


var api = builder.AddProject<Projects.Api>("api")
    .WithReference(db)
    .WaitFor(postgres)
    .WaitFor(rabbit)
    .WaitFor(maildev);

builder.Build().Run();
