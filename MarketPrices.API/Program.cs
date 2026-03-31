using MarketPrices.API.Middleware;
using MarketPrices.Application;
using MarketPrices.Application.UseCases.Assets.Commands.SeedAssets;
using MarketPrices.Infrastructure;
using MarketPrices.Presentation;
using MediatR;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;

var presentationAssembly = typeof(AssemblyReference).Assembly;
builder.Services.AddControllers().AddApplicationPart(presentationAssembly);

builder.Services.AddOpenApi();
builder.Services.AddTransient<ExceptionHandlingMiddleware>();

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

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var sender = scope.ServiceProvider.GetRequiredService<ISender>();
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

    db.Database.EnsureCreated();
    await sender.Send(new SeedAssetsCommand());
}

app.Run();
