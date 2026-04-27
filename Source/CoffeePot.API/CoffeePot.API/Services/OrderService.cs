using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CoffeePot.API.DTOs.Order;
using CoffeePot.API.Mappers;
using CoffeePot.Domain.Entities;
using CoffeePot.Domain.Enumerations;
using CoffeePot.Domain.Interfaces;
using CoffeePot.Domain.Interfaces.Common;

namespace CoffeePot.API.Services;

public class OrderService(
  IOrderRepository orderRepository,
  IGenericRepository<Consumer> consumerRepository,
  IGenericRepository<Product> productRepository)
{
  public async Task<IEnumerable<OrderDto>> GetOrdersAsync(bool includeCancelled, CancellationToken cancellationToken)
  {
    var loadedOrders = await orderRepository.GetAsync(cancellationToken);

    return includeCancelled
      ? loadedOrders.Select(OrderMapper.ConvertOrderIntoOrderDto).ToList()
      : loadedOrders.Where(order => order.Status != Status.Canceled).Select(OrderMapper.ConvertOrderIntoOrderDto)
      .ToList();
  }

  public async Task<IEnumerable<OrderDto>> GetOrdersByConsumerIdAsync(int id, bool includeCancelled,
    CancellationToken cancellationToken)
  {
    var loadedOrders = await orderRepository.GetByConsumerIdAsync(id, cancellationToken);

    return includeCancelled
      ? loadedOrders.Select(OrderMapper.ConvertOrderIntoOrderDto).ToList()
      : loadedOrders.Where(order => order.Status != Status.Canceled).Select(OrderMapper.ConvertOrderIntoOrderDto)
      .ToList();
  }

  public async Task<OrderDto> GetOrderByIdAsync(int id, CancellationToken cancellationToken)
  {
    var loadedOrder = await orderRepository.GetByIdAsync(id, cancellationToken);
    return OrderMapper.ConvertOrderIntoOrderDto(loadedOrder);
  }

  public async Task<OrderDto> CreateOrderAsync(WriteOrderDto writeOrderDto, CancellationToken cancellationToken)
  {
    var consumer = await consumerRepository.GetByIdAsync(writeOrderDto.ConsumerId, cancellationToken);
    var orderDetails = new List<OrderDetail>();
    decimal totalAmount = 0;

    foreach (var orderDetail in writeOrderDto.OrderDetails)
    {
      var product = await productRepository.GetByIdAsync(orderDetail.ProductId, cancellationToken);
      totalAmount += orderDetail.Quantity * product.UnitPrice;

      orderDetails.Add(new OrderDetail
      {
        ProductId = product.Id, Quantity = orderDetail.Quantity, UnitPrice = product.UnitPrice
      });
    }

    var order = new Order { OrderDetails = orderDetails, ConsumerId = consumer.Id, TotalAmount = totalAmount };

    var processedOrder = await orderRepository.CreateAsync(order, cancellationToken);
    return OrderMapper.ConvertOrderIntoOrderDto(processedOrder);
  }

  public async Task<OrderDto> CancelOrderAsync(int id, CancellationToken cancellationToken)
  {
    var originalOrder = await orderRepository.GetByIdAsync(id, cancellationToken);

    if (originalOrder.Status == Status.Canceled)
    {
      throw new InvalidOperationException("A canceled order cannot be canceled again.");
    }

    var orderDetailsForReversalOrder = originalOrder.OrderDetails.Select(orderDetail => new OrderDetail
    {
      ProductId = orderDetail.ProductId, Quantity = orderDetail.Quantity, UnitPrice = -orderDetail.UnitPrice
    }).ToList();

    var reversalOrder = new Order
    {
      ConsumerId = originalOrder.ConsumerId,
      Annotation = $"Cancellation for order #{originalOrder.Id}",
      TotalAmount = -originalOrder.TotalAmount,
      OrderDetails = orderDetailsForReversalOrder,
      Status = Status.Canceled
    };

    originalOrder.CancelOrder();
    var processedOrder = await orderRepository.CreateAsync(reversalOrder, cancellationToken);
    return OrderMapper.ConvertOrderIntoOrderDto(processedOrder);
  }
}
