var builder = DistributedApplication.CreateBuilder(args);

var postgres = builder.AddPostgres("postgres")
    .WithDataVolume(isReadOnly: false)
    .WithLifetime(ContainerLifetime.Persistent)
    .WithPgAdmin();

var db = postgres.AddDatabase("appdb");

var api = builder.AddProject<Projects.Api>("api")
    .WithReference(db)
    .WaitFor(postgres);

builder.Build().Run();
