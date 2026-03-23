using CoffeePot.Domain.Enumerations;

namespace CoffeePot.API.DTOs.User;

public record UserDto(
  int Id,
  string Forename,
  string Surname,
  Status Status);
