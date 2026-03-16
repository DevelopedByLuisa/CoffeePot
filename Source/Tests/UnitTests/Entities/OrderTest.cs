using System.Collections.Generic;
using CoffeePot.Domain.Entities;
using CoffeePot.Domain.Enumerations;
using Xunit;

namespace Tests.UnitTests.Entities;

public class OrderTest
{
  [Fact]
  public void CancelOrder_ShouldUpdateProperties()
  {
    var sut = new Order
    {
      Id = 235,
      UserId = 12,
      TotalAmount = 22.58M,
      Annotation = string.Empty,
      OrderDetails = new List<OrderDetail>(),
      Status = Status.Active
    };
    var changeDateBefore = sut.ChangeDate;

    sut.CancelOrder();

    Assert.Equal(Status.Canceled, sut.Status);
    Assert.True(sut.ChangeDate >= changeDateBefore);
  }
}
