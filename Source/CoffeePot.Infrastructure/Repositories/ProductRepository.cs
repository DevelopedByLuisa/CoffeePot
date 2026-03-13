using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CoffeePot.Domain.Entities;
using CoffeePot.Domain.Exceptions;
using CoffeePot.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CoffeePot.Infrastructure.Repositories;

public class ProductRepository(ApplicationContext applicationContext)
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
      Console.WriteLine(e);
      throw;
    }
  }

  public async Task<Product> UpdateProductAsync(int id, Product product, CancellationToken cancellationToken)
  {
    var loadedProduct = await applicationContext.Products.Where(x => x.Id == id)
      .FirstOrDefaultAsync(cancellationToken);

    product = loadedProduct ?? throw new NotFoundException();

    try
    {
      await applicationContext.SaveChangesAsync(cancellationToken);
      return product;
    }
    catch (Exception e)
    {
      Console.WriteLine(e);
      throw;
    }
  }
}
