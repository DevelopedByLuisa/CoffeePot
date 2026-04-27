using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CoffeePot.Domain.Interfaces.Common;

public interface IGenericRepository<T>
{
  Task<IEnumerable<T>> GetAsync(CancellationToken cancellationToken);
  Task<T> GetByIdAsync(int id, CancellationToken cancellationToken);
  Task<T> CreateAsync(T entity, CancellationToken cancellationToken);
  Task<T> UpdateAsync(T entity, CancellationToken cancellationToken);
}
