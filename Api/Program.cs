using Api.BusinessLogic.Dto;
using Api.BusinessLogic.Services.Abstraction;
using Api.BusinessLogic.Services.Implementation;
using Api.DataAccess;
using Api.DataAccess.Abstractions;
using Api.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.AddServiceDefaults();
builder.AddNpgsqlDbContext<DatabaseContext>("appdb");
builder.Services.AddOpenApi();

// Service Registration Section
builder.Services.AddScoped<IRepository<Report, int>, BaseRepository<Report, int>>();
builder.Services.AddScoped<ICrudService<ReportDto, int>, ReportService>();

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

app.UseHttpsRedirection();
app.MapControllers();
app.UseCors("AllowAnyOrigin");
app.Run();
