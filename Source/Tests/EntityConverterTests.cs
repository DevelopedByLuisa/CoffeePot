using CoffeePot.Domain.Entities;
using CoffeePot.Web.Converter;
using CoffeePot.Web.DTOs;
using Xunit;

namespace Tests;

public class EntityConverterTests
{
  [Fact]
  public void ConvertProductIntoProductDto_ShouldMapPropertiesCorrectly()
  {
    var product = new Product { Id = 419, Name = "Some Name", Description = "Some Description", UnitPrice = 9.99 };

    var productDto = EntityConverter.ConvertProductIntoProductDto(product);

    Assert.Equal(product.Id, productDto.Id);
    Assert.Equal(product.Name, productDto.Name);
    Assert.Equal(product.Description, productDto.Description);
    Assert.Equal(product.UnitPrice, productDto.UnitPrice);
  }

  [Fact]
  public void ConvertProductDtoIntoProduct_ShouldMapPropertiesCorrectly()
  {
    var productDto = new ProductDto(12, "Some Name", "Some Description", 9.99);

    var product = EntityConverter.ConvertProductDtoIntoProduct(productDto);

    Assert.Equal(productDto.Id, product.Id);
    Assert.Equal(productDto.Name, product.Name);
    Assert.Equal(productDto.Description, product.Description);
    Assert.Equal(productDto.UnitPrice, product.UnitPrice);
  }
}
