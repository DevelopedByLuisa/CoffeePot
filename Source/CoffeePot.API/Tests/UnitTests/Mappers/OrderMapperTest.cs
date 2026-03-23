using System.Collections.Generic;
using CoffeePot.API.Mappers;
using CoffeePot.Domain.Entities;
using CoffeePot.Domain.Enumerations;
using Xunit;

namespace Tests.UnitTests.Mappers;

public class OrderMapperTest
{
  [Fact]
  public void ConvertOrderIntoOrderDto_ShouldReturnOrderDto()
  {
    var orderDetails = new List<OrderDetail>
    {
      new()
      {
        OrderId = 231,
        ProductId = 3,
        Quantity = 1,
        UnitPrice = 0.22M,
        Product = new Product
        {
          Id = 3,
          Name = "Test",
          Description = "Testing.",
          Status = Status.Active,
          UnitPrice = 0.22M
        }
      }
    };

    var user = new User
    {
      Id = 34,
      Forename = "Test",
      Surname = "Testing",
      Status = Status.Active
    };
    var userFullName = $"{user.Forename} {user.Surname}";

    var order = new Order
    {
      Id = 231,
      User = user,
      UserId = 34,
      TotalAmount = 0.44M,
      OrderDetails = orderDetails
    };

    var result = OrderMapper.ConvertOrderIntoOrderDto(order);

    Assert.Equal(order.Id, result.Id);
    Assert.Equal(order.CreationDate, result.OrderDate);
    Assert.Equal(userFullName, result.Purchaser);
    Assert.Equal(order.TotalAmount, result.TotalAmount);
  }
}
