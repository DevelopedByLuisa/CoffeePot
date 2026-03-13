using CoffeePot.Domain.Enumerations;

namespace CoffeePot.Web.DTOs.User;

public record UserDto(
  int Id,
  string Forename,
  string Surname,
  string Email,
  Status Status);
