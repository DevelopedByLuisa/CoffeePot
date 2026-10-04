using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CoffeePot.Infrastructure.Extensions;

[ExcludeFromCodeCoverage(Justification = "No custom code logic has been implemented.")]
public static class DatabaseExtension
{
  public static IServiceCollection AddDatabase(this IServiceCollection services, string connectionString)
  {
    var serverVersion = ServerVersion.AutoDetect(connectionString);
    services.AddDbContext<ApplicationContext>(options => options.UseMySql(connectionString, serverVersion));

    return services;
  }
}
