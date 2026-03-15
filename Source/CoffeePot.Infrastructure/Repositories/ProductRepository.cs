using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CoffeePot.Domain.Entities;
using CoffeePot.Domain.Exceptions;
using CoffeePot.Domain.Interfaces.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CoffeePot.Infrastructure.Repositories;

public class ProductRepository(ApplicationContext applicationContext, ILogger<ProductRepository> logger)
  : IGenericRepository<Product>
{
  public async Task<IEnumerable<Product>> GetAsync(CancellationToken cancellationToken)
  {
    return await applicationContext.Products.ToListAsync(cancellationToken);
  }

  public async Task<Product> GetByIdAsync(int id, CancellationToken cancellationToken)
  {
    var loadedProduct = await applicationContext.Products.Where(x => x.Id == id)
      .FirstOrDefaultAsync(cancellationToken);

    return loadedProduct ?? throw new EntityNotFoundException($"No product with the ID {id} could be found.");
  }

  public async Task<Product> CreateAsync(Product entity, CancellationToken cancellationToken)
  {
    try
    {
      applicationContext.Products.Add(entity);
      await applicationContext.SaveChangesAsync(cancellationToken);
      return entity;
    }
    catch (Exception e)
    {
      logger.LogError(e, e.Message);
      throw;
    }
  }

  public async Task<Product> UpdateAsync(Product entity, CancellationToken cancellationToken)
  {
    await applicationContext.SaveChangesAsync(cancellationToken);
    return entity;
  }
}
