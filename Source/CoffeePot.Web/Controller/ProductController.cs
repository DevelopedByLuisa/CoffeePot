using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CoffeePot.Domain.Interfaces;
using CoffeePot.Web.DTOs.Product;
using CoffeePot.Web.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CoffeePot.Web.Controller;

[ApiController]
[Route("api/products")]
public class ProductController(IProductRepository productRepository, ILogger<ProductController> logger)
  : ControllerBase
{
  private readonly ProductService _productService = new(productRepository);

  /// <summary>
  ///   Returns a list of products.
  /// </summary>
  /// <param name="cancellationToken">The CancellationToken.</param>
  /// <param name="includeDeleted">Includes deleted products.</param>
  [HttpGet]
  public async Task<ActionResult<IEnumerable<ProductDto>>> GetProductsAsync(CancellationToken cancellationToken,
    bool includeDeleted = false)
  {
    return Ok(await _productService.GetProductsAsync(includeDeleted, cancellationToken));
  }

  /// <summary>
  ///   Returns a product.
  /// </summary>
  /// <param name="id">The Id.</param>
  /// <param name="cancellationToken">The CancellationToken.</param>
  [HttpGet("{id}")]
  public async Task<ActionResult<ProductDto>> GetProductAsync(int id, CancellationToken cancellationToken)
  {
    return await _productService.GetProductAsync(id, cancellationToken);
  }

  /// <summary>
  ///   Creates a product.
  /// </summary>
  /// <param name="productDto">The ProductDto.</param>
  /// <param name="cancellationToken">The CancellationToken.</param>
  [HttpPost]
  public async Task<ActionResult<ProductDto>> CreateProductAsync([FromBody] WriteProductDto productDto,
    CancellationToken cancellationToken)
  {
    return await _productService.CreateProductAsync(productDto, cancellationToken);
  }

  /// <summary>
  ///   Updates a product.
  /// </summary>
  /// <param name="id">The Id.</param>
  /// <param name="productDto">The ProductDto.</param>
  /// <param name="cancellationToken">The CancellationToken.</param>
  [HttpPut("{id}")]
  public async Task<ActionResult<ProductDto>> UpdateProductAsync(int id, [FromBody] WriteProductDto productDto,
    CancellationToken cancellationToken)
  {
    return await _productService.UpdateProductAsync(id, productDto, cancellationToken);
  }

  /// <summary>
  ///   Deletes a product.
  /// </summary>
  /// <param name="id">The Id.</param>
  /// <param name="cancellationToken">The CancellationToken.</param>
  [HttpDelete("{id}")]
  public async Task<ActionResult<ProductDto>> DeleteProductAsync(int id, CancellationToken cancellationToken)
  {
    return await _productService.DeleteProductAsync(id, cancellationToken);
  }
}
