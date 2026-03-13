using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CoffeePot.Infrastructure.Extensions;

public static class DatabaseExtension
{
  public static IServiceCollection InitializeDatabase(
    this IServiceCollection services, string connectionString)
  {
    try
    {
      var serverVersion = ServerVersion.AutoDetect(connectionString);
      services.AddDbContext<ApplicationContext>(options =>
        options.UseMySql(connectionString, serverVersion));
    }
    catch (Exception e)
    {
      Console.WriteLine(e.Message);
      throw;
    }

    return services;
  }
}
