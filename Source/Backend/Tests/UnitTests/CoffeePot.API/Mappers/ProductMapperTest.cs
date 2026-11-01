using AutoFixture.Xunit3;
using CoffeePot.API.DTOs.Product;
using CoffeePot.API.Mappers;
using CoffeePot.Domain.Entities;
using Xunit;

namespace Tests.UnitTests.CoffeePot.API.Mappers;

public class ProductMapperTest
{
  [Theory]
  [AutoData]
  public void MapProductToReadProductDto_ShouldMapAllProperties(Product product)
  {
    var result = ProductMapper.MapProductToReadProductDto(product);

    Assert.Equal(product.Name, result.Name);
    Assert.Equal(product.Description, result.Description);
    Assert.Equal(product.UnitPrice, result.UnitPrice);
    Assert.Equal(product.Status, result.Status);
  }

  [Theory]
  [AutoData]
  public void MapWriteProductDtoToProduct_ShouldMapAllProperties(WriteProductDto writeProductDto)
  {
    var result = ProductMapper.MapWriteProductDtoToProduct(writeProductDto);

    Assert.Equal(writeProductDto.Name, result.Name);
    Assert.Equal(writeProductDto.Description, result.Description);
    Assert.Equal(writeProductDto.UnitPrice, result.UnitPrice);
  }
}
