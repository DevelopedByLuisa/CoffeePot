using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CoffeePot.Domain.Entities.Common;

namespace CoffeePot.Domain.Repositories;

public interface IGenericRepository<TEntity> where TEntity : BaseEntity
{
  Task<IEnumerable<TEntity>> GetEntitiesAsync(CancellationToken cancellationToken);
  Task<TEntity> GetEntityByIdAsync(int id, CancellationToken cancellationToken);
  Task<TEntity> CreateEntityAsync(TEntity entity, CancellationToken cancellationToken);
  Task<TEntity> UpdateEntityByIdAsync(int id, TEntity entity, CancellationToken cancellationToken);
}
