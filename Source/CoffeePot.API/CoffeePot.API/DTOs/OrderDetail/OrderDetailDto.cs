namespace CoffeePot.API.DTOs.OrderDetail;

public record OrderDetailDto(
  string Product,
  int Quantity,
  decimal UnitPrice);
