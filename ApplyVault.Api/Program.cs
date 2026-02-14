using ApplyVault.Infrastructure;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Add infrastructure (EF Core DbContext)
builder.Services.AddInfrastructure(builder.Configuration);
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "ApplyVault API v1");
        c.RoutePrefix = "swagger";
    });
}

// Apply EF Core migrations (or create database if migrations are not present)
using (var scope = app.Services.CreateScope())
{
    var logger = app.Logger;
    try
    {
        var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        db.Database.Migrate();
        logger.LogInformation("Database migrated successfully.");
    }
    catch (Exception ex)
    {
        // If migrations cannot be applied in this environment, fall back to EnsureCreated
        app.Logger.LogWarning(ex, "Migrations could not be applied; attempting EnsureCreated().");
        try
        {
            var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            db.Database.EnsureCreated();
            logger.LogInformation("Database ensured/created successfully.");
        }
        catch (Exception e)
        {
            logger.LogError(e, "Failed to create or migrate the database.");
        }
    }
}



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
