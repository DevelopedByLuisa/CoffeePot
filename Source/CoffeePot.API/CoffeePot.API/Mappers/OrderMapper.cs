using System.Linq;
using CoffeePot.API.DTOs.Order;
using CoffeePot.Domain.Entities;

namespace CoffeePot.API.Mappers;

public static class OrderMapper
{
  public static OrderDto ConvertOrderIntoOrderDto(Order order)
  {
    var orderDetailDtos = order.OrderDetails
      .Select(OrderDetailMapper.ConvertOrderDetailIntoOrderDetailDto)
      .ToList();

    return new OrderDto(order.Id, order.CreationDate, $"{order.User.Forename} {order.User.Surname}", order.TotalAmount,
      orderDetailDtos);
  }
}
