using CoffeePot.Domain.Enumerations;

namespace CoffeePot.API.DTOs.Product;

public record ReadProductDto(
  int Id,
  string Product,
  string Description, 
  decimal UnitPrice,
  Status Status);
