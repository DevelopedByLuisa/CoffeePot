using AutoFixture.Xunit3;
using CoffeePot.API.DTOs.Product;
using CoffeePot.API.Mappers;
using CoffeePot.Domain.Entities;
using Xunit;

namespace Tests.UnitTests.Mappers;

public class ProductMapperTest
{
  [Theory]
  [AutoData]
  public void ConvertWriteProductDtoIntoProduct_ShouldReturnProduct(WriteProductDto writeProductDto)
  {
    var result = ProductMapper.ConvertWriteProductDtoIntoProduct(writeProductDto);

    Assert.Equal(writeProductDto.Name, result.Name);
    Assert.Equal(writeProductDto.Description, result.Description);
    Assert.Equal(writeProductDto.UnitPrice, result.UnitPrice);
  }

  [Theory]
  [AutoData]
  public void ConvertProductIntoProductDto_ShouldReturnProductDto(Product product)
  {
    var result = ProductMapper.ConvertProductIntoProductDto(product);

    Assert.Equal(product.Id, result.Id);
    Assert.Equal(product.Name, result.Name);
    Assert.Equal(product.Description, result.Description);
    Assert.Equal(product.UnitPrice, result.UnitPrice);
    Assert.Equal(product.Status, result.Status);
  }
}
