using System.Text.Json.Serialization;

namespace CoffeePot.API.DTOs.OrderDetail;

public record WriteOrderDetailDto(
  [property: JsonRequired] int ProductId,
  [property: JsonRequired] int Quantity);
