using CoffeePot.Domain.Entities;
using CoffeePot.Web.DTOs.Product;

namespace CoffeePot.Web.Mappers;

public static class ProductMapper
{
  /// <summary>
  ///   Converts a WriteProductDto into a Product.
  /// </summary>
  /// <param name="writeProductDto">The WriteProductDto.</param>
  public static Product ConvertWriteProductDtoIntoProduct(WriteProductDto writeProductDto)
  {
    return new Product
    {
      Name = writeProductDto.Name,
      Description = writeProductDto.Description,
      UnitPrice = writeProductDto.UnitPrice
    };
  }

  /// <summary>
  ///   Converts a Product into a ProductDto.
  /// </summary>
  /// <param name="product">The product.</param>
  public static ProductDto ConvertProductIntoProductDto(Product product)
  {
    return new ProductDto(product.Id, product.Name, product.Description, product.UnitPrice, product.Status);
  }
}
