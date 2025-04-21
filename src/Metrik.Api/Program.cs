using Metrik.Api.Extensions;
using Metrik.Application;
using Metrik.Infrastructure;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddOpenApi();

builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference(options =>
    {
        options.WithTitle("Metrik API");
    });

    app.ApplyMigrations();

    // Uncomment the following line to seed data for development purposes
    //app.SeedData();
}

app.UseHttpsRedirection();

app.UseLocalization();

app.UseAuthorization();

app.UseCustomExceptionHandler();

app.MapControllers();

app.Run();
