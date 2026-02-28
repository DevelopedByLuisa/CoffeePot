using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CoffeePot.Domain.Entities;

namespace CoffeePot.Domain.Interfaces;

public interface IOrderRepository
{
  Task<IEnumerable<Order>> GetOrdersAsync(CancellationToken cancellationToken);
  Task<Order> GetOrderAsync(int id, CancellationToken cancellationToken);
  Task<Order> CreateOrderAsync(Order order, CancellationToken cancellationToken);
}
