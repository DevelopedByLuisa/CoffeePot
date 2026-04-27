using System;
using System.Text.Json.Serialization;
using CoffeePot.API.Extensions;
using CoffeePot.Domain.Exceptions;
using CoffeePot.Infrastructure.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using PriceTooLowException = CoffeePot.API.Exceptions.PriceTooLowException;

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

    //builder.Services.AddCors(options =>
    //{
    //  options.AddPolicy("AllowFrontend", policy =>
    //  {
    //    policy.WithOrigins(allowedOrigins)
    //      .AllowAnyMethod()
    //      .AllowAnyHeader();
    //  });
    //});

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
        var exceptionFeature = context.Features.Get<IExceptionHandlerFeature>();
        var exception = exceptionFeature?.Error;

        var (statusCode, message) = exception switch
        {
          PriceTooLowException e => (400, e.Message),
          EntityNotFoundException e => (404, e.Message),
          InvalidOperationException e => (400, e.Message),
          _ => (500, "An internal server error occurred.Please check the log files for more details.")
        };

        context.Response.StatusCode = statusCode;
        context.Response.ContentType = "application/json";

        await context.Response.WriteAsJsonAsync(new { StatusCode = statusCode, Message = message });
      });
    });

    app.UseCors("AllowAll");
    app.UseRouting();
    app.MapControllers();
    app.MapGet("/", () => "System is up!");

    app.Run();
  }
}
