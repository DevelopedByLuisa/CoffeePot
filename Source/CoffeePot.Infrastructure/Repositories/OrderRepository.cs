/*using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CoffeePot.Domain.Entities;
using CoffeePot.Domain.Exceptions;
using CoffeePot.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CoffeePot.Infrastructure.Repositories;

public class OrderRepository : IOrderRepository
{
  private readonly ApplicationContext _applicationContext;
  private readonly ILogger<OrderRepository> _logger;

  public OrderRepository(ApplicationContext applicationContext, ILogger<OrderRepository> logger)
  {
    _applicationContext = applicationContext;
    _logger = logger;
  }

  public async Task<IEnumerable<Order>> GetOrdersAsync(CancellationToken cancellationToken)
  {
    return await _applicationContext.Orders.ToListAsync(cancellationToken);
  }

  public async Task<Order> GetOrderAsync(int id, CancellationToken cancellationToken)
  {
    var loadedOrder = await _applicationContext.Orders.Where(order => order.Id == id)
      .FirstOrDefaultAsync(cancellationToken);

    if (loadedOrder != null)
    {
      return loadedOrder;
    }

    _logger.LogError("No order with ID {id} can be found.", id);
    throw new NotFoundException();
  }

  public async Task<Order> CreateOrderAsync(Order order, CancellationToken cancellationToken)
  {
    try
    {
      _applicationContext.Orders.Add(order);
      await _applicationContext.SaveChangesAsync(cancellationToken);
      return order;
    }
    catch (DbUpdateException e)
    {
      throw new DbUpdateException(e.Message);
    }
  }
}
*/
