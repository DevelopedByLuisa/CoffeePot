using System.Text.Json.Serialization;

namespace CoffeePot.API.DTOs.Consumer;

public record WriteConsumerDto(
  [property: JsonRequired] string Forename,
  [property: JsonRequired] string Surname);
