using System;
using CoffeePot.API.DTOs.Product;
using CoffeePot.API.Mappers;
using CoffeePot.Domain.Entities;
using Xunit;

namespace Tests.UnitTests.API.Mappers;

public class ProductMapperTest
{
  private const string Name = "foo";
  private const string Description = "bar";
  private const decimal UnitPrice = 0.11m;

  [Fact]
  public void MapProductToReadProductDto_ShouldMapAllProperties()
  {
    var product = Product.Create(Name, Description, UnitPrice);
    product.Delete();

    var result = ProductMapper.MapProductToReadProductDto(product);

    Assert.Equal(product.Name, result.Product);
    Assert.Equal(product.Description, result.Description);
    Assert.Equal(product.UnitPrice, result.UnitPrice);
    Assert.Equal(product.Status, result.Status);
  }

  [Fact]
  public void MapWriteProductDtoToProduct_ShouldMapAllProperties()
  {
    var writeProduct = new WriteProductDto(Name, Description, UnitPrice);

    var result = ProductMapper.MapWriteProductDtoToProduct(writeProduct);

    Assert.Equal(writeProduct.Product, result.Name);
    Assert.Equal(writeProduct.Description, result.Description);
    Assert.Equal(writeProduct.UnitPrice, result.UnitPrice);
  }

}
