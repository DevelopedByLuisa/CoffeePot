using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CoffeePot.API.DTOs.Product;
using CoffeePot.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace CoffeePot.API.Controller;

[ApiController]
[Route("api/products")]
public class ProductController(ProductService productService)
  : ControllerBase
{
  /// <summary>
  ///   Returns a list of products.
  /// </summary>
  /// <param name="includeDeleted">Include deleted products.</param>
  /// <param name="cancellationToken">The CancellationToken.</param>
  [HttpGet]
  public async Task<ActionResult<IEnumerable<ProductDto>>> GetProductsAsync(bool includeDeleted,
    CancellationToken cancellationToken)
  {
    return Ok(await productService.GetProductsAsync(includeDeleted, cancellationToken));
  }

  /// <summary>
  ///   Returns a product.
  /// </summary>
  /// <param name="id">The Id.</param>
  /// <param name="cancellationToken">The CancellationToken.</param>
  [HttpGet("{id}")]
  public async Task<ActionResult<ProductDto>> GetProductAsync(int id, CancellationToken cancellationToken)
  {
    return await productService.GetProductAsync(id, cancellationToken);
  }

  /// <summary>
  ///   Creates a product.
  /// </summary>
  /// <param name="writeProductDto">The WriteProductDto.</param>
  /// <param name="cancellationToken">The CancellationToken.</param>
  [HttpPost]
  public async Task<ActionResult<ProductDto>> CreateProductAsync([FromBody] WriteProductDto writeProductDto,
    CancellationToken cancellationToken)
  {
    return await productService.CreateProductAsync(writeProductDto, cancellationToken);
  }

  /// <summary>
  ///   Updates a product.
  /// </summary>
  /// <param name="id">The Id.</param>
  /// <param name="writeProductDto">The WriteProductDto.</param>
  /// <param name="cancellationToken">The CancellationToken.</param>
  [HttpPut("{id}")]
  public async Task<ActionResult<ProductDto>> UpdateProductAsync(int id, [FromBody] WriteProductDto writeProductDto,
    CancellationToken cancellationToken)
  {
    return await productService.UpdateProductAsync(id, writeProductDto, cancellationToken);
  }

  /// <summary>
  ///   Deletes a product.
  /// </summary>
  /// <param name="id">The Id.</param>
  /// <param name="cancellationToken">The CancellationToken.</param>
  [HttpDelete("{id}")]
  public async Task<ActionResult<ProductDto>> DeleteProductAsync(int id, CancellationToken cancellationToken)
  {
    return await productService.DeleteProductAsync(id, cancellationToken);
  }
}
