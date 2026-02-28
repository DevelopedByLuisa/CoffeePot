using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CoffeePot.Domain.Interfaces;
using CoffeePot.Web.Converter;
using CoffeePot.Web.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CoffeePot.Web.Controller;

[ApiController]
[Route("api/products")]
public class ProductController(IProductRepository productRepository, ILogger<ProductController> logger)
  : ControllerBase
{
  /// <summary>
  ///   Returns a list of products.
  /// </summary>
  /// <param name="cancellationToken">The CancellationToken.</param>
  [HttpGet]
  public async Task<IEnumerable<ProductDto>> GetProductsAsync(CancellationToken cancellationToken)
  {
    return (await productRepository.GetProductsAsync(cancellationToken)).Select(EntityConverter.ConvertProductIntoProductDto);
  }

  /// <summary>
  ///   Returns a product.
  /// </summary>
  /// <param name="id">The Id.</param>
  /// <param name="cancellationToken">The CancellationToken.</param>
  [HttpGet("{id}")]
  public async Task<ProductDto> GetProductAsync(int id, CancellationToken cancellationToken)
  {
    var product = await productRepository.GetProductAsync(id, cancellationToken);
    return EntityConverter.ConvertProductIntoProductDto(product);
  }

  /// <summary>
  ///   Creates a product.
  /// </summary>
  /// <param name="productDto">The ProductDto.</param>
  /// <param name="cancellationToken">The CancellationToken.</param>
  [HttpPost]
  public async Task<ProductDto> CreateProductAsync([FromBody] ProductDto productDto,
    CancellationToken cancellationToken)
  {
    var product = EntityConverter.ConvertProductDtoIntoProduct(productDto);
    var result = await productRepository.CreateProductAsync(product, cancellationToken);
    return EntityConverter.ConvertProductIntoProductDto(result);
  }

  /// <summary>
  ///   Updates a product.
  /// </summary>
  /// <param name="id">The Id.</param>
  /// <param name="productDto">The ProductDto.</param>
  /// <param name="cancellationToken">The CancellationToken.</param>
  [HttpPut("{id}")]
  public async Task<ProductDto> UpdateProductAsync(int id, [FromBody] ProductDto productDto,
    CancellationToken cancellationToken)
  {
    var product = EntityConverter.ConvertProductDtoIntoProduct(productDto);
    var result = await productRepository.UpdateProductAsync(id, product, cancellationToken);
    return EntityConverter.ConvertProductIntoProductDto(result);
  }

  /// <summary>
  ///   Deletes a product.
  /// </summary>
  /// <param name="id">The Id.</param>
  /// <param name="cancellationToken">The CancellationToken.</param>
  [HttpDelete("{id}")]
  public async Task<ProductDto> DeleteProductAsync(int id, CancellationToken cancellationToken)
  {
    var product = await productRepository.DeleteProductAsync(id, cancellationToken);
    return EntityConverter.ConvertProductIntoProductDto(product);
  }
}
