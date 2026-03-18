using CoffeePot.Domain.Enumerations;

namespace CoffeePot.API.DTOs.Product;

public record ProductDto(
  int Id,
  string Name,
  string Description,
  decimal UnitPrice,
  Status Status);
