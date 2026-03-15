using CoffeePot.Domain.Entities;
using CoffeePot.Domain.Interfaces;
using CoffeePot.Domain.Interfaces.Common;
using CoffeePot.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace CoffeePot.Infrastructure.Extensions;

public static class RepositoryExtension
{
  public static IServiceCollection InitializeRepositories(
    this IServiceCollection services)
  {
    services.AddTransient<IGenericRepository<Product>, ProductRepository>();
    services.AddTransient<IGenericRepository<User>, UserRepository>();
    services.AddTransient<IOrderRepository, OrderRepository>();

    return services;
  }
}
