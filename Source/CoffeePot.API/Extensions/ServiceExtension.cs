using CoffeePot.API.Services;
using Microsoft.Extensions.DependencyInjection;

namespace CoffeePot.API.Extensions;

public static class ServiceExtension
{
  public static IServiceCollection InitializeServices(
    this IServiceCollection services)
  {
    services.AddTransient<ProductService>();
    services.AddTransient<UserService>();
    services.AddTransient<OrderService>();

    return services;
  }
}
