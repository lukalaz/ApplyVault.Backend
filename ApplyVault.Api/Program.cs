using ApplyVault.Api.Extensions;
using ApplyVault.Application.Interfaces;
using ApplyVault.Infrastructure;
using ApplyVault.Infrastructure.Services;
using Microsoft.AspNetCore.HttpOverrides;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IJobApplicationService, JobApplicationService>();

builder.Services.AddControllers();

builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var corsPolicyName = "frontend";

builder.Services.AddCors(options =>
{
    options.AddPolicy(corsPolicyName, p =>
        p.WithOrigins(
            "http://localhost:5173",
            "https://localhost:5173",
            "https://applyvault-frontend.onrender.com"
        )
         .AllowAnyHeader()
         .AllowAnyMethod());
});

// Helps behind reverse proxies (like Render) so HTTPS redirection behaves correctly
builder.Services.Configure<ForwardedHeadersOptions>(options =>
{
    options.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
});

var app = builder.Build();

app.UseForwardedHeaders();

app.UseCors(corsPolicyName);

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "ApplyVault API v1");
        c.RoutePrefix = "swagger";
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.ApplyMigrations();

app.MapControllers();

app.Run();