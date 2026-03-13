namespace CoffeePot.Web.DTOs.Product;

public record WriteProductDto(
  string Name,
  string Description,
  decimal UnitPrice);
