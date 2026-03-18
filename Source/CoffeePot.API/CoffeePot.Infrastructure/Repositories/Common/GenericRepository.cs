using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CoffeePot.Domain.Entities.Common;
using CoffeePot.Domain.Exceptions;
using CoffeePot.Domain.Interfaces.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CoffeePot.Infrastructure.Repositories.Common;

public class GenericRepository<T>(ApplicationContext applicationContext, ILogger<GenericRepository<T>> logger)
  : IGenericRepository<T> where T : BaseEntity
{
  public async Task<IEnumerable<T>> GetAsync(CancellationToken cancellationToken)
  {
    return await applicationContext.Set<T>().ToListAsync(cancellationToken);
  }

  public async Task<T> GetByIdAsync(int id, CancellationToken cancellationToken)
  {
    var loadedEntity = await applicationContext.Set<T>().Where(x => x.Id == id)
      .FirstOrDefaultAsync(cancellationToken);
    return loadedEntity ?? throw new EntityNotFoundException($"No entity with the ID {id} could be found.");
  }

  public async Task<T> CreateAsync(T entity, CancellationToken cancellationToken)
  {
    try
    {
      applicationContext.Set<T>().Add(entity);
      await applicationContext.SaveChangesAsync(cancellationToken);
      return entity;
    }
    catch (Exception e)
    {
      logger.LogError(e, e.Message);
      throw;
    }
  }

  public async Task<T> UpdateAsync(T entity, CancellationToken cancellationToken)
  {
    await applicationContext.SaveChangesAsync(cancellationToken);
    return entity;
  }
}
