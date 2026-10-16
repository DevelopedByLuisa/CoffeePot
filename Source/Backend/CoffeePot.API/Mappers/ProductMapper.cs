using CoffeePot.API.DTOs.Product;
using CoffeePot.Domain.Entities;

namespace CoffeePot.API.Mappers;

public static class ProductMapper
{
  public static ReadProductDto MapProductToReadProductDto(Product product)
  {
    return new ReadProductDto(product.Id, product.Name, product.Description, product.UnitPrice, product.Status);
  }

  public static Product MapWriteProductDtoToProduct(WriteProductDto writeProductDto)
  {
    return Product.Create(writeProductDto.Product, writeProductDto.Description, writeProductDto.UnitPrice);
  }
}
