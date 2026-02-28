namespace CoffeePot.Web.DTOs;

public record ProductDto(
  int Id,
  string Name,
  string Description,
  double UnitPrice);
