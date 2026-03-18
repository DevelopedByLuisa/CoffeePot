using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CoffeePot.Domain.Entities;
using CoffeePot.Domain.Interfaces.Common;

namespace CoffeePot.Domain.Interfaces;

public interface IOrderRepository : IGenericRepository<Order>
{
  Task<IEnumerable<Order>> GetByUserIdAsync(int id, CancellationToken cancellationToken);
}
