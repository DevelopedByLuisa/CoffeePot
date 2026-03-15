using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CoffeePot.Domain.Entities;
using CoffeePot.Domain.Interfaces.Common;

namespace CoffeePot.Domain.Interfaces;

public interface IOrderRepository : IRepository
{
  Task<IEnumerable<Order>> GetOrdersAsync(CancellationToken cancellationToken);
  Task<IEnumerable<Order>> GetOrdersByUserIdAsync(int id, CancellationToken cancellationToken);
  Task<Order> GetOrderByIdAsync(int id, CancellationToken cancellationToken);
  Task<Order> CreateOrderAsync(Order order, CancellationToken cancellationToken);
}
