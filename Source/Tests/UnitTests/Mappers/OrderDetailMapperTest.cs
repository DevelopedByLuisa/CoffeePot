using CoffeePot.API.Mappers;
using CoffeePot.Domain.Entities;
using CoffeePot.Domain.Enumerations;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.Utilities;
using Xunit;

namespace Tests.UnitTests.Mappers;

public class OrderDetailMapperTest
{
  [Fact]
  public void ConvertOrderDetailIntoOrderDetailDto_ShouldReturnOrderDetailDto()
  {
    var orderDetail = new OrderDetail
    {
      Product = new Product
      {
        Id = 12,
        Name = "Test",
        Description = "Some Description",
        UnitPrice = 2.11M,
        Status = Status.Active
      },
      Quantity = 1,
      UnitPrice = 2.11M
    };

    var sut = OrderDetailMapper.ConvertOrderDetailIntoOrderDetailDto(orderDetail);

    Assert.Equal(orderDetail.Product.Name, sut.Product);
    Assert.Equal(orderDetail.Quantity, sut.Quantity);
    Assert.Equal(orderDetail.UnitPrice, sut.UnitPrice);
  }
}
