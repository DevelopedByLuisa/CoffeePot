using Microsoft.Extensions.DependencyInjection;

namespace CoffeePot.API.Extensions;

public static class AuthExtension
{
  public static IServiceCollection InitializeAuth(
    this IServiceCollection services)
  {
    services.AddAuthentication();
    services.AddAuthorization();

    return services;
  }
}
