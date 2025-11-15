using Api.BusinessLogic.Services.Abstraction;
using Api.BusinessLogic.Services.Implementation;
using Api.DataAccess;
using Api.DataAccess.Abstractions;
using Api.DataAccess.Entities;
using Api.DataAccess.Exceptions;
using Api.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.AddServiceDefaults();
builder.AddNpgsqlDbContext<DatabaseContext>("appdb");
builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddScoped<IRepository<User, int>, BaseRepository<User, int>>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();

var app = builder.Build();


using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetService<DatabaseContext>();
    await dbContext!.Database.MigrateAsync();
}

app.MapDefaultEndpoints();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseMiddleware<GlobalExceptionMiddleware>();

app.MapControllers();
//app.UseHttpsRedirection();
app.Run();
