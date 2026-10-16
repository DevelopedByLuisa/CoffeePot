using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CoffeePot.Domain.Entities.Common;
using CoffeePot.Domain.Enumerations;
using CoffeePot.Domain.Exceptions;
using CoffeePot.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CoffeePot.Infrastructure.Repositories;

public class GenericRepository<TEntity>(ApplicationContext applicationContext) : IGenericRepository<TEntity>
  where TEntity : BaseEntity
{
  public async Task<IEnumerable<TEntity>> GetEntitiesAsync(bool includeDeleted, CancellationToken cancellationToken)
  {
    if (includeDeleted)
    {
      return await applicationContext.Set<TEntity>().ToListAsync(cancellationToken);
    }

    return await applicationContext.Set<TEntity>().Where(entity => entity.Status != Status.Deleted)
      .ToListAsync(cancellationToken);
  }

  public async Task<TEntity> GetEntityByIdAsync(int id, CancellationToken cancellationToken)
  {
    var loadedEntity = await applicationContext.Set<TEntity>().Where(entity => entity.Id == id).FirstOrDefaultAsync(cancellationToken);

    return loadedEntity ?? throw new EntityNotFoundException();
  }

  public async Task<TEntity> CreateEntityAsync(TEntity entity, CancellationToken cancellationToken)
  {
    try
    {
      applicationContext.Set<TEntity>().Add(entity);
      await applicationContext.SaveChangesAsync(cancellationToken);
      return entity;
    }
    catch (Exception e)
    {
      Console.WriteLine(e);
      throw;
    }
  }

  public Task SaveChangesAsync(CancellationToken cancellationToken)
  {
    return applicationContext.SaveChangesAsync(cancellationToken);
  }
}
