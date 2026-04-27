using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CoffeePot.API.DTOs.Order;
using CoffeePot.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace CoffeePot.API.Controllers;

[ApiController]
[Route("api/orders")]
public class OrderController(
  OrderService orderService)
  : ControllerBase
{
  /// <summary>
  ///   Returns a list of orders.
  /// </summary>
  /// <param name="consumerId">The Consumer ID.</param>
  /// <param name="includeCancelled">Include cancelled orders.</param>
  /// <param name="cancellationToken">The CancellationToken.</param>
  [HttpGet]
  public async Task<ActionResult<IEnumerable<OrderDto>>> GetOrdersAsync(int consumerId, bool includeCancelled, CancellationToken cancellationToken)
  {
    return consumerId > 0
      ? Ok(await orderService.GetOrdersByConsumerIdAsync(consumerId, includeCancelled, cancellationToken))
      : Ok(await orderService.GetOrdersAsync(includeCancelled, cancellationToken));
  }

  /// <summary>
  ///   Returns a order.
  /// </summary>
  /// <param name="id">The Id.</param>
  /// <param name="cancellationToken">The CancellationToken</param>
  [HttpGet("{id}")]
  public async Task<ActionResult<OrderDto>> GetOrderByIdAsync(int id, CancellationToken cancellationToken)
  {
    return await orderService.GetOrderByIdAsync(id, cancellationToken);
  }

  /// <summary>
  ///   Creates a order.
  /// </summary>
  /// <param name="writeOrderDto">The WriteOrderDto.</param>
  /// <param name="cancellationToken">The CancellationToken.</param>
  [HttpPost]
  public async Task<ActionResult<OrderDto>> CreateOrderAsync([FromBody] WriteOrderDto writeOrderDto,
    CancellationToken cancellationToken)
  {
    return await orderService.CreateOrderAsync(writeOrderDto, cancellationToken);
  }

  /// <summary>
  ///   Cancels an order.
  /// </summary>
  /// <param name="id">The ID.</param>
  /// <param name="cancellationToken">The CancellationToken.</param>
  [HttpDelete("{id}")]
  public async Task<ActionResult<OrderDto>> CancelOrderAsync(int id, CancellationToken cancellationToken)
  {
    return await orderService.CancelOrderAsync(id, cancellationToken);
  }
}
