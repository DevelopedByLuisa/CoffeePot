using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CoffeePot.API.DTOs.Product;
using CoffeePot.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace CoffeePot.API.Controllers;

[ApiController]
[Route("api/products")]
public class ProductController(ProductService productService) : ControllerBase
{
  /// <summary>
  ///   Returns a list of products.
  /// </summary>
  /// <param name="includeDeleted">Specifies whether deleted items should also be included in the output.</param>
  /// <param name="cancellationToken">The CancellationToken.</param>
  /// <returns>Returns a list of products of type <see cref="ReadProductDto"/>.</returns>
  [HttpGet]
  public async Task<ActionResult<IEnumerable<ReadProductDto>>> GetAllProductsAsync(bool includeDeleted,
    CancellationToken cancellationToken)
  {
    return Ok(await productService.GetAllProductsAsync(includeDeleted, cancellationToken));
  }

  /// <summary>
  ///   Returns a product.
  /// </summary>
  /// <param name="id">The product ID.</param>
  /// <param name="cancellationToken">The CancellationToken.</param>
  /// <returns>Returns a product of type <see cref="ReadProductDto"/>.</returns>
  [HttpGet("{id}")]
  public async Task<ActionResult<ReadProductDto>> GetProductByIdAsync(int id, CancellationToken cancellationToken)
  {
    return await productService.GetProductByIdAsync(id, cancellationToken);
  }

  /// <summary>
  ///   Updates a product based on the product ID.
  /// </summary>
  /// <param name="id">The product ID.</param>
  /// <param name="writeProductDto">The WriteProductDto.</param>
  /// <param name="cancellationToken">The CancellationToken</param>
  /// <returns>Returns the updated product of type <see cref="ReadProductDto"/>.</returns>
  [HttpPut("{id}")]
  public async Task<ActionResult<ReadProductDto>> UpdateProductByIdAsync(int id,
    [FromBody] WriteProductDto writeProductDto,
    CancellationToken cancellationToken)
  {
    return await productService.UpdateProductByIdAsync(id, writeProductDto, cancellationToken);
  }

  /// <summary>
  ///   Soft-deletes a product based on the product ID.
  /// </summary>
  /// <param name="id">The product ID.</param>
  /// <param name="cancellationToken">The CancellationToken.</param>
  /// <returns>Returns the deleted product of type <see cref="ReadProductDto"/>.</returns>
  [HttpDelete("{id}")]
  public async Task<ActionResult<ReadProductDto>> DeleteProductByIdAsync(int id, CancellationToken cancellationToken)
  {
    return await productService.DeleteProductByIdAsync(id, cancellationToken);
  }

  /// <summary>
  ///   Creates a new product.
  /// </summary>
  /// <param name="writeProductDto">The WriteProductDto.</param>
  /// <param name="cancellationToken">The CancellationToken.</param>
  /// <returns>Returns the new created product of type <see cref="ReadProductDto"/>.</returns>
  [HttpPost]
  public async Task<ActionResult<ReadProductDto>> CreateProductAsync(WriteProductDto writeProductDto,
    CancellationToken cancellationToken)
  {
    return await productService.CreateProductAsync(writeProductDto, cancellationToken);
  }
}
