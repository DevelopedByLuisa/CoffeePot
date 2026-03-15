using CoffeePot.API.DTOs.OrderDetail;
using CoffeePot.Domain.Entities;

namespace CoffeePot.API.Mappers;

public static class OrderDetailMapper
{
  public static OrderDetailDto ConvertOrderDetailIntoOrderDetailDto(OrderDetail orderDetail)
  {
    return new OrderDetailDto(orderDetail.Product.Name, orderDetail.Quantity, orderDetail.UnitPrice);
  }
}
