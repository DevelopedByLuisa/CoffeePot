using CoffeePot.Domain.Entities;
using CoffeePot.Web.DTOs;

namespace CoffeePot.Web.Converter;

public static class EntityConverter
{
  /// <summary>
  ///   Converts a ProductDto into a Product.
  /// </summary>
  /// <param name="productDto">The ProductDto.</param>
  public static Product ConvertProductDtoIntoProduct(ProductDto productDto)
  {
    return new Product
    {
      Id = productDto.Id,
      Name = productDto.Name,
      Description = productDto.Description,
      UnitPrice = productDto.UnitPrice
    };
  }

  /// <summary>
  ///   Converts a Product into a ProductDto.
  /// </summary>
  /// <param name="product">The product.</param>
  public static ProductDto ConvertProductIntoProductDto(Product product)
  {
    return new ProductDto(product.Id, product.Name, product.Description, product.UnitPrice);
  }
}
