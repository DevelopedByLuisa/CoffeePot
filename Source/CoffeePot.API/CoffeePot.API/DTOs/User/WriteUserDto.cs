using System.Text.Json.Serialization;

namespace CoffeePot.API.DTOs.User;

public record WriteUserDto(
  [property: JsonRequired] string Forename,
  [property: JsonRequired] string Surname);
