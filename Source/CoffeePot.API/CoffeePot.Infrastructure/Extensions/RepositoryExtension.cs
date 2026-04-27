using CoffeePot.Domain.Entities;
using CoffeePot.Domain.Interfaces;
using CoffeePot.Domain.Interfaces.Common;
using CoffeePot.Infrastructure.Repositories;
using CoffeePot.Infrastructure.Repositories.Common;
using Microsoft.Extensions.DependencyInjection;

namespace CoffeePot.Infrastructure.Extensions;

public static class RepositoryExtension
{
  public static IServiceCollection InitializeRepositories(
    this IServiceCollection services)
  {
    services.AddTransient<IGenericRepository<Product>, GenericRepository<Product>>();
    services.AddTransient<IGenericRepository<Consumer>, GenericRepository<Consumer>>();
    services.AddTransient<IOrderRepository, OrderRepository>();

    return services;
  }
}
