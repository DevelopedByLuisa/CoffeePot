using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CoffeePot.Domain.Entities.Common;
using CoffeePot.Domain.Repositories;

namespace CoffeePot.Infrastructure.Repositories;

public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseEntity
{
  public async Task<IEnumerable<TEntity>> GetEntitiesAsync(CancellationToken cancellationToken)
  {
    throw new System.NotImplementedException();
  }

  public async Task<TEntity> GetEntityByIdAsync(int id, CancellationToken cancellationToken)
  {
    throw new System.NotImplementedException();
  }

  public async Task<TEntity> CreateEntityAsync(TEntity entity, CancellationToken cancellationToken)
  {
    throw new System.NotImplementedException();
  }

  public async Task<TEntity> UpdateEntityByIdAsync(int id, TEntity entity, CancellationToken cancellationToken)
  {
    throw new System.NotImplementedException();
  }
}
