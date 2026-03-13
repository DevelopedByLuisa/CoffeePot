using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CoffeePot.Domain.Enumerations;
using CoffeePot.Domain.Interfaces;
using CoffeePot.Web.DTOs.Product;
using CoffeePot.Web.Mappers;

namespace CoffeePot.Web.Services;

public class ProductService(IProductRepository productRepository)
{
  public async Task<ProductDto> CreateProductAsync(WriteProductDto writeProductDto, CancellationToken cancellationToken)
  {
    var product = ProductMapper.ConvertWriteProductDtoIntoProduct(writeProductDto);
    var processedProduct = await productRepository.CreateProductAsync(product, cancellationToken);
    return ProductMapper.ConvertProductIntoProductDto(processedProduct);
  }

  public async Task<ProductDto> UpdateProductAsync(int id, WriteProductDto writeProductDto, CancellationToken cancellationToken)
  {
    var loadedProduct = await productRepository.GetProductAsync(id, cancellationToken);
    loadedProduct.Name = writeProductDto.Name;
    loadedProduct.Description = writeProductDto.Description;
    loadedProduct.UnitPrice = writeProductDto.UnitPrice;
    loadedProduct.RegisterChange();

    var updatedProduct = await productRepository.UpdateProductAsync(id, loadedProduct, cancellationToken);
    return ProductMapper.ConvertProductIntoProductDto(updatedProduct);
  }

  public async Task<ProductDto> DeleteProductAsync(int id, CancellationToken cancellationToken)
  {
    var loadedProduct = await productRepository.GetProductAsync(id, cancellationToken);
    loadedProduct.Status = Status.Deleted;
    loadedProduct.RegisterChange();

    var deletedProduct = await productRepository.UpdateProductAsync(id, loadedProduct, cancellationToken);
    return ProductMapper.ConvertProductIntoProductDto(deletedProduct);
  }

  public async Task<IEnumerable<ProductDto>> GetProductsAsync(bool includeDeleted, CancellationToken cancellationToken)
  {
    var loadedProducts = await productRepository.GetProductsAsync(cancellationToken);

    if (includeDeleted)
    {
      return loadedProducts.Select(ProductMapper.ConvertProductIntoProductDto);
    }

    return loadedProducts.Where(product => product.Status != Status.Deleted)
      .Select(ProductMapper.ConvertProductIntoProductDto);
  }

  public async Task<ProductDto> GetProductAsync(int id, CancellationToken cancellationToken)
  {
    var loadedProduct = await productRepository.GetProductAsync(id, cancellationToken);
    return ProductMapper.ConvertProductIntoProductDto(loadedProduct);
  }
}
