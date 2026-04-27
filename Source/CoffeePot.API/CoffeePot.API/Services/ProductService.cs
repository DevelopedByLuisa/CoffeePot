using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CoffeePot.API.DTOs.Product;
using CoffeePot.API.Exceptions;
using CoffeePot.API.Mappers;
using CoffeePot.Domain.Entities;
using CoffeePot.Domain.Enumerations;
using CoffeePot.Domain.Interfaces.Common;

namespace CoffeePot.API.Services;

public class ProductService(IGenericRepository<Product> productRepository)
{
  public async Task<ProductDto> CreateProductAsync(WriteProductDto writeProductDto, CancellationToken cancellationToken)
  {
    ValidatePrice(writeProductDto.UnitPrice);
    var product = ProductMapper.ConvertWriteProductDtoIntoProduct(writeProductDto);
    var processedProduct = await productRepository.CreateAsync(product, cancellationToken);
    return ProductMapper.ConvertProductIntoProductDto(processedProduct);
  }

  public async Task<ProductDto> UpdateProductAsync(int id, WriteProductDto writeProductDto,
    CancellationToken cancellationToken)
  {
    ValidatePrice(writeProductDto.UnitPrice);
    var loadedProduct = await productRepository.GetByIdAsync(id, cancellationToken);
    loadedProduct.Update(writeProductDto.Name, writeProductDto.Description, writeProductDto.UnitPrice);

    loadedProduct = await productRepository.UpdateAsync(loadedProduct, cancellationToken);
    return ProductMapper.ConvertProductIntoProductDto(loadedProduct);
  }

  public async Task<ProductDto> DeleteProductAsync(int id, CancellationToken cancellationToken)
  {
    var loadedProduct = await productRepository.GetByIdAsync(id, cancellationToken);
    loadedProduct.Delete();

    loadedProduct = await productRepository.UpdateAsync(loadedProduct, cancellationToken);
    return ProductMapper.ConvertProductIntoProductDto(loadedProduct);
  }

  public async Task<IEnumerable<ProductDto>> GetProductsAsync(bool includeDeleted, CancellationToken cancellationToken)
  {
    var loadedProducts = await productRepository.GetAsync(cancellationToken);

    if (includeDeleted)
    {
      return loadedProducts.Select(ProductMapper.ConvertProductIntoProductDto);
    }

    return loadedProducts
      .Where(product => product.Status != Status.Deleted)
      .Select(ProductMapper.ConvertProductIntoProductDto);
  }

  public async Task<ProductDto> GetProductByIdAsync(int id, CancellationToken cancellationToken)
  {
    var loadedProduct = await productRepository.GetByIdAsync(id, cancellationToken);
    return ProductMapper.ConvertProductIntoProductDto(loadedProduct);
  }

  private static void ValidatePrice(decimal price)
  {
    if (price < 0)
    {
      throw new PriceTooLowException("The price cannot be less than 0.");
    }
  }
}
