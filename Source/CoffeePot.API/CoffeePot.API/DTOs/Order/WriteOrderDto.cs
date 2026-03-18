using System.Collections.Generic;
using CoffeePot.API.DTOs.OrderDetail;

namespace CoffeePot.API.DTOs.Order;

public record WriteOrderDto(
  int UserId,
  IEnumerable<WriteOrderDetailDto> OrderDetails);
