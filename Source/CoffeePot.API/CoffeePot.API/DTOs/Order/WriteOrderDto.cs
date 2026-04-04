using System.Collections.Generic;
using System.Text.Json.Serialization;
using CoffeePot.API.DTOs.OrderDetail;

namespace CoffeePot.API.DTOs.Order;

public record WriteOrderDto(
  [property: JsonRequired] int UserId,
  [property: JsonRequired] IEnumerable<WriteOrderDetailDto> OrderDetails);
