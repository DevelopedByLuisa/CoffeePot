using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CoffeePot.API.DTOs.Order;
using CoffeePot.API.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CoffeePot.API.Controller;

[ApiController]
[Route("api/orders")]
public class OrderController(
  OrderService orderService,
  ILogger<OrderController> logger)
  : ControllerBase
{
  /// <summary>
  ///   Returns a list of orders.
  /// </summary>
  /// <param name="userId">The User ID.</param>
  /// <param name="cancellationToken">The CancellationToken.</param>
  [HttpGet]
  public async Task<ActionResult<IEnumerable<OrderDto>>> GetOrdersAsync(int userId, CancellationToken cancellationToken)
  {
    return userId > 0
      ? Ok(await orderService.GetOrdersByUserIdAsync(userId, cancellationToken))
      : Ok(await orderService.GetOrdersAsync(cancellationToken));
  }

  /// <summary>
  ///   Returns a order.
  /// </summary>
  /// <param name="id">The Id.</param>
  /// <param name="cancellationToken">The CancellationToken</param>
  [HttpGet("{id}")]
  public async Task<ActionResult<OrderDto>> GetOrderAsync(int id, CancellationToken cancellationToken)
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
