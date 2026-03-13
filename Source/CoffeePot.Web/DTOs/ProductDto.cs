using CoffeePot.Domain.Enumerations;

namespace CoffeePot.Web.DTOs;

public record ProductDto(
  int Id,
  string Name,
  string Description,
  decimal UnitPrice,
  Status Status);
