using CoffeePot.Domain.Entities;
using CoffeePot.Domain.Enumerations;
using CoffeePot.API.Mappers;
using Xunit;

namespace Tests.Mappers;

public class ProductMapperTests
{
  [Fact]
  public void ConvertProductIntoProductDto_ShouldMapPropertiesCorrectly()
  {
    var product = new Product { Id = 419, Name = "Some Name", Description = "Some Description", UnitPrice = 9.99M, Status = Status.Active};

    var productDto = ProductMapper.ConvertProductIntoProductDto(product);

    Assert.Equal(product.Id, productDto.Id);
    Assert.Equal(product.Name, productDto.Name);
    Assert.Equal(product.Description, productDto.Description);
    Assert.Equal(product.UnitPrice, productDto.UnitPrice);
    Assert.Equal(product.Status, productDto.Status);
  }
}
