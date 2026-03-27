using MarketPrices.Application;
using MarketPrices.Infrastructure;
using MarketPrices.Presentation;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;

builder.Services.AddOpenApi();

var presentationAssembly = typeof(AssemblyReference).Assembly;
builder.Services.AddControllers().AddApplicationPart(presentationAssembly);

builder.Services
    .AddApplication()
    .AddInfrastructure(configuration.GetConnectionString("DefaultConnection"));

builder.Host.UseSerilog((context, config) =>
{
    config.ReadFrom.Configuration(configuration);
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
