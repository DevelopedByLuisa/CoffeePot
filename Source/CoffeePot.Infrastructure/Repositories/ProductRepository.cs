using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CoffeePot.Domain.Entities;
using CoffeePot.Domain.Exceptions;
using CoffeePot.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CoffeePot.Infrastructure.Repositories;

public class ProductRepository(ApplicationContext applicationContext, ILogger<ProductRepository> logger)
  : IProductRepository
{
  public async Task<IEnumerable<Product>> GetProductsAsync(CancellationToken cancellationToken)
  {
    return await applicationContext.Products.ToListAsync(cancellationToken);
  }

  public async Task<Product> GetProductAsync(int id, CancellationToken cancellationToken)
  {
    var loadedProduct = await applicationContext.Products.Where(x => x.Id == id)
      .FirstOrDefaultAsync(cancellationToken);

    return loadedProduct ?? throw new NotFoundException();
  }

  public async Task<Product> CreateProductAsync(Product product, CancellationToken cancellationToken)
  {
    try
    {
      applicationContext.Products.Add(product);
      await applicationContext.SaveChangesAsync(cancellationToken);
      return product;
    }
    catch (Exception e)
    {
      logger.LogError(e, e.Message);
      throw;
    }
  }

  public async Task UpdateProductAsync(CancellationToken cancellationToken)
  {
    await applicationContext.SaveChangesAsync(cancellationToken);
  }
}
