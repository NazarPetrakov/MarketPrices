using MarketPrices.Application;
using MarketPrices.Application.Assets.Commands.SeedAssets;
using MarketPrices.Infrastructure;
using MarketPrices.Presentation;
using MediatR;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;

builder.Services.AddOpenApi();

var presentationAssembly = typeof(AssemblyReference).Assembly;
builder.Services.AddControllers().AddApplicationPart(presentationAssembly);

builder.Services
    .AddApplication()
    .AddInfrastructure(configuration);

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

using (var scope = app.Services.CreateScope())
{
    var sender = scope.ServiceProvider.GetRequiredService<ISender>();

    await sender.Send(new SeedAssetsCommand());
}

app.Run();
