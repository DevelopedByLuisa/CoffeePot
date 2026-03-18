using System;
using System.Collections.Generic;
using CoffeePot.API.DTOs.OrderDetail;

namespace CoffeePot.API.DTOs.Order;

public record OrderDto(
  int Id,
  DateTime OrderDate,
  string Purchaser,
  decimal TotalAmount,
  IEnumerable<OrderDetailDto> OrderDetails);
