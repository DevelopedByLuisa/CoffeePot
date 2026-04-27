using CoffeePot.Domain.Enumerations;

namespace CoffeePot.API.DTOs.Consumer;

public record ConsumerDto(
  int Id,
  string Forename,
  string Surname,
  Status Status);
