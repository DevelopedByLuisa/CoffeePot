using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CoffeePot.API.DTOs.Product;
using CoffeePot.API.Mappers;
using CoffeePot.Domain.Entities;
using CoffeePot.Domain.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace CoffeePot.API.Services;

public class ProductService(IGenericRepository<Product> productRepository)
{
  /// <summary>
  ///   Returns a list of products.
  /// </summary>
  /// <param name="includeDeleted">Specifies whether deleted items should also be included in the output.</param>
  /// <param name="cancellationToken">The CancellationToken.</param>
  /// <returns>Returns a list of products of type <see cref="ReadProductDto"/>.</returns>
  public async Task<IEnumerable<ReadProductDto>> GetAllProductsAsync(bool includeDeleted,
    CancellationToken cancellationToken)
  {
    return (await productRepository.GetEntitiesAsync(includeDeleted, cancellationToken)).Select(ProductMapper
      .MapProductToReadProductDto);
  }

  /// <summary>
  ///   Returns a product.
  /// </summary>
  /// <param name="id">The product ID.</param>
  /// <param name="cancellationToken">The CancellationToken.</param>
  /// <returns>Returns a product of type <see cref="ReadProductDto"/>.</returns>
  public async Task<ReadProductDto> GetProductByIdAsync(int id, CancellationToken cancellationToken)
  {
    var result = await productRepository.GetEntityByIdAsync(id, cancellationToken);
    return ProductMapper.MapProductToReadProductDto(result);
  }

  /// <summary>
  ///   Updates a product based on the product ID.
  /// </summary>
  /// <param name="id">The product ID.</param>
  /// <param name="writeProductDto">The WriteProductDto.</param>
  /// <param name="cancellationToken">The CancellationToken</param>
  /// <returns>Returns the updated product of type <see cref="ReadProductDto"/>.</returns>
  public async Task<ReadProductDto> UpdateProductByIdAsync(int id, WriteProductDto writeProductDto,
    CancellationToken cancellationToken)
  {
    var loadedProduct = await productRepository.GetEntityByIdAsync(id, cancellationToken);
    loadedProduct.Update(writeProductDto.Product, writeProductDto.Description, writeProductDto.UnitPrice);
    await productRepository.SaveChangesAsync(cancellationToken);
    return ProductMapper.MapProductToReadProductDto(loadedProduct);
  }

  /// <summary>
  ///   Soft-deletes a product based on the product ID.
  /// </summary>
  /// <param name="id">The product ID.</param>
  /// <param name="cancellationToken">The CancellationToken.</param>
  /// <returns>Returns the deleted product of type <see cref="ReadProductDto"/>.</returns>
  public async Task<ReadProductDto> DeleteProductByIdAsync(int id, CancellationToken cancellationToken)
  {
    var loadedProduct = await productRepository.GetEntityByIdAsync(id, cancellationToken);
    loadedProduct.Delete();
    await productRepository.SaveChangesAsync(cancellationToken);
    return ProductMapper.MapProductToReadProductDto(loadedProduct);
  }

  /// <summary>
  ///   Creates a new product.
  /// </summary>
  /// <param name="writeProductDto">The WriteProductDto.</param>
  /// <param name="cancellationToken">The CancellationToken.</param>
  /// <returns>Returns the new created product of type <see cref="ReadProductDto"/>.</returns>
  public async Task<ActionResult<ReadProductDto>> CreateProductAsync(WriteProductDto writeProductDto,
    CancellationToken cancellationToken)
  {
    var product = ProductMapper.MapWriteProductDtoToProduct(writeProductDto);
    var result = await productRepository.CreateEntityAsync(product, cancellationToken);
    return ProductMapper.MapProductToReadProductDto(result);
  }
}
