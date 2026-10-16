using System.Text.Json.Serialization;

namespace CoffeePot.API.DTOs.Product;

public record WriteProductDto(
  [property: JsonRequired] string Product,
  [property: JsonRequired] string Description,
  [property: JsonRequired] decimal UnitPrice);
