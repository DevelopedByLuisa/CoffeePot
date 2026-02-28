using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CoffeePot.Domain.Entities;
using CoffeePot.Domain.Interfaces;
using Microsoft.Extensions.Logging;

namespace CoffeePot.Infrastructure.Repositories;

public class ProductRepository(ApplicationContext applicationContext, ILogger<ProductRepository> logger)
  : IProductRepository
{
  private readonly ApplicationContext _applicationContext = applicationContext;
  private readonly ILogger<ProductRepository> _logger = logger;

  public async Task<IEnumerable<Product>> GetProductsAsync(CancellationToken cancellationToken)
  {
    throw new System.NotImplementedException();
  }

  public async Task<Product> GetProductAsync(int id, CancellationToken cancellationToken)
  {
    throw new System.NotImplementedException();
  }

  public async Task<Product> CreateProductAsync(Product product, CancellationToken cancellationToken)
  {
    throw new System.NotImplementedException();
  }

  public async Task<Product> UpdateProductAsync(int id, Product product, CancellationToken cancellationToken)
  {
    throw new System.NotImplementedException();
  }

  public async Task<Product> DeleteProductAsync(int id, CancellationToken cancellationToken)
  {
    throw new System.NotImplementedException();
  }
}
