using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;

namespace CoffeePot.Infrastructure.Extensions;

public static class LoggingExtension
{
  public static IServiceCollection InitializeLogging(
    this IServiceCollection services)
  {
    Log.Logger = new LoggerConfiguration()
      .WriteTo.Console()
      .WriteTo.File($"{AppContext.BaseDirectory}/Logs/CoffeePot.log", rollingInterval: RollingInterval.Day)
      .CreateLogger();

    services.AddLogging(logging =>
    {
      logging.ClearProviders();
      logging.AddSerilog();
    });

    return services;
  }
}
