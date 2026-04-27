using System.Text.Json.Serialization;

namespace CoffeePot.API.DTOs.Product;

public record WriteProductDto(
  [property: JsonRequired] string Name,
  [property: JsonRequired] string Description,
  [property: JsonRequired] decimal UnitPrice);
