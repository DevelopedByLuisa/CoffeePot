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
    var sut = ProductMapper.ConvertWriteProductDtoIntoProduct(writeProductDto);

    Assert.Equal(writeProductDto.Name, sut.Name);
    Assert.Equal(writeProductDto.Description, sut.Description);
    Assert.Equal(writeProductDto.UnitPrice, sut.UnitPrice);
  }

  [Theory]
  [AutoData]
  public void ConvertProductIntoProductDto_ShouldReturnProductDto(Product product)
  {
    var sut = ProductMapper.ConvertProductIntoProductDto(product);

    Assert.Equal(product.Id, sut.Id);
    Assert.Equal(product.Name, sut.Name);
    Assert.Equal(product.Description, sut.Description);
    Assert.Equal(product.UnitPrice, sut.UnitPrice);
    Assert.Equal(product.Status, sut.Status);
  }
}
