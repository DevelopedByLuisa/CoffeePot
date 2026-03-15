using System;
using System.Text.Json.Serialization;
using CoffeePot.API.Extensions;
using CoffeePot.Infrastructure.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CoffeePot.API;

public static class Program
{
  public static void Main(string[] args)
  {
    var appSettings = new ConfigurationBuilder()
      .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
      .AddJsonFile("appsettings.json")
      .Build();

    var connectionString = appSettings.GetConnectionString("MariaDB") ??
                           throw new InvalidOperationException(
                             "The connection string for MariaDB could not be loaded.");
    var enableApiDocumentation = appSettings.GetValue<bool>("EnableApiDocumentation");
    var enableWindowsService = appSettings.GetValue<bool>("EnableWindowsService");

    var builder = WebApplication.CreateBuilder(args);
    if (enableWindowsService)
    {
      builder.Host.UseWindowsService();
    }
    builder.Services.InitializeLogging();
    builder.Services.InitializeDatabase(connectionString);
    builder.Services.InitializeRepositories();
    builder.Services.InitializeServices();
    builder.Services.AddControllers()
      .AddJsonOptions(options => options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

    if (enableApiDocumentation)
    {
      builder.Services.InitializeApiDocumentation();
    }
    
    builder.Services.AddCors(options =>
    {
      options.AddPolicy("AllowAll", policy =>
      {
        policy.AllowAnyOrigin()
          .AllowAnyMethod()
          .AllowAnyHeader();
      });
    });

    var app = builder.Build();

    if (enableApiDocumentation)
    {
      app.UseSwagger();
      app.UseSwaggerUI();
    }

    app.UseExceptionHandler(appError =>
    {
      appError.Run(async context =>
      {
        context.Response.StatusCode = 500;
        context.Response.ContentType = "application/json";

        await context.Response.WriteAsJsonAsync(new
        {
          StatusCode = 500,
          Message = "An internal server error occurred. Please check the log files for more details."
        });
      });
    });
    app.UseCors("AllowAll");
    app.UseRouting();
    app.MapControllers();
    app.MapGet("/", () => "System is up!");

    app.Run();
  }
}
