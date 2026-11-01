using System.Diagnostics.CodeAnalysis;
using CoffeePot.API.Services;
using CoffeePot.Domain.Entities;
using CoffeePot.Domain.Repositories;
using CoffeePot.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace CoffeePot.API.Extensions;

[ExcludeFromCodeCoverage(Justification = "No custom code logic has been implemented.")]
public static class RepositoryExtension
{
  public static IServiceCollection AddRepositories(this IServiceCollection services)
  {
    services.AddScoped<IGenericRepository<Product>, GenericRepository<Product>>();

    services.AddScoped<ProductService>();
    return services;
  }
}
