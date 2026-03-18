using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CoffeePot.Domain.Entities;
using CoffeePot.Domain.Exceptions;
using CoffeePot.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CoffeePot.Infrastructure.Repositories;

public class OrderRepository(ApplicationContext applicationContext, ILogger<OrderRepository> logger)
  : IOrderRepository
{
  public async Task<IEnumerable<Order>> GetAsync(CancellationToken cancellationToken)
  {
    return await applicationContext.Orders
      .Include(order => order.User)
      .Include(order => order.OrderDetails)
      .ThenInclude(orderDetail => orderDetail.Product)
      .ToListAsync(cancellationToken);
  }

  public async Task<Order> GetByIdAsync(int id, CancellationToken cancellationToken)
  {
    var loadedOrder = await applicationContext.Orders
      .Where(order => order.Id == id)
      .Include(order => order.User)
      .Include(order => order.OrderDetails)
      .ThenInclude(orderDetail => orderDetail.Product)
      .FirstOrDefaultAsync(cancellationToken);

    return loadedOrder ?? throw new EntityNotFoundException($"No order with the ID {id} could be found.");
  }

  public async Task<Order> CreateAsync(Order entity, CancellationToken cancellationToken)
  {
    try
    {
      applicationContext.Orders.Add(entity);
      await applicationContext.SaveChangesAsync(cancellationToken);
      return entity;
    }
    catch (Exception e)
    {
      logger.LogError(e, e.Message);
      throw;
    }
  }

  public async Task<Order> UpdateAsync(Order entity, CancellationToken cancellationToken)
  {
    await applicationContext.SaveChangesAsync(cancellationToken);
    return entity;
  }

  public async Task<IEnumerable<Order>> GetByUserIdAsync(int id, CancellationToken cancellationToken)
  {
    return await applicationContext.Orders
      .Include(order => order.User)
      .Include(order => order.OrderDetails)
      .ThenInclude(orderDetail => orderDetail.Product)
      .Where(order => order.UserId == id)
      .ToListAsync(cancellationToken);
  }
}
