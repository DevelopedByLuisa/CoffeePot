using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CoffeePot.API.DTOs.Product;
using CoffeePot.API.Mappers;
using CoffeePot.Domain.Enumerations;
using CoffeePot.Domain.Interfaces;

namespace CoffeePot.API.Services;

public class ProductService(IProductRepository productRepository)
{
  public async Task<ProductDto> CreateProductAsync(WriteProductDto writeProductDto, CancellationToken cancellationToken)
  {
    var product = ProductMapper.ConvertWriteProductDtoIntoProduct(writeProductDto);
    var processedProduct = await productRepository.CreateProductAsync(product, cancellationToken);
    return ProductMapper.ConvertProductIntoProductDto(processedProduct);
  }

  public async Task<ProductDto> UpdateProductAsync(int id, WriteProductDto writeProductDto,
    CancellationToken cancellationToken)
  {
    var loadedProduct = await productRepository.GetProductByIdAsync(id, cancellationToken);
    loadedProduct.UpdateProduct(writeProductDto.Name, writeProductDto.Description, writeProductDto.UnitPrice);

    await productRepository.SaveChangesAsync(cancellationToken);
    return ProductMapper.ConvertProductIntoProductDto(loadedProduct);
  }

  public async Task<ProductDto> DeleteProductAsync(int id, CancellationToken cancellationToken)
  {
    var loadedProduct = await productRepository.GetProductByIdAsync(id, cancellationToken);
    loadedProduct.Delete();

    await productRepository.SaveChangesAsync(cancellationToken);
    return ProductMapper.ConvertProductIntoProductDto(loadedProduct);
  }

  public async Task<IEnumerable<ProductDto>> GetProductsAsync(bool includeDeleted, CancellationToken cancellationToken)
  {
    var loadedProducts = await productRepository.GetProductsAsync(cancellationToken);

    if (includeDeleted)
    {
      return loadedProducts.Select(ProductMapper.ConvertProductIntoProductDto);
    }

    return loadedProducts
      .Where(product => product.Status != Status.Deleted)
      .Select(ProductMapper.ConvertProductIntoProductDto);
  }

  public async Task<ProductDto> GetProductAsync(int id, CancellationToken cancellationToken)
  {
    var loadedProduct = await productRepository.GetProductByIdAsync(id, cancellationToken);
    return ProductMapper.ConvertProductIntoProductDto(loadedProduct);
  }
}
