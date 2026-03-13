using CoffeePot.Domain.Interfaces;
using CoffeePot.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace CoffeePot.Infrastructure.Extensions;

public static class RepositoryExtension
{
  public static IServiceCollection InitializeRepositories(
    this IServiceCollection services)
  {
    services.AddTransient<IProductRepository, ProductRepository>();
    services.AddTransient<IUserRepository, UserRepository>();

    return services;
  }
}
