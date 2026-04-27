using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CoffeePot.API.DTOs.Consumer;
using CoffeePot.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace CoffeePot.API.Controllers;

[ApiController]
[Route("api/consumers")]
public class ConsumerController(ConsumerService consumerService)
  : ControllerBase
{
  /// <summary>
  ///   Returns a list of consumers.
  /// </summary>
  /// <param name="includeDeleted">Include deleted consumers.</param>
  /// <param name="cancellationToken">The CancellationToken.</param>
  [HttpGet]
  public async Task<ActionResult<IEnumerable<ConsumerDto>>> GetConsumersAsync(bool includeDeleted,
    CancellationToken cancellationToken)
  {
    return Ok(await consumerService.GetConsumersAsync(includeDeleted, cancellationToken));
  }

  /// <summary>
  ///   Returns a consumer.
  /// </summary>
  /// <param name="id">The Id.</param>
  /// <param name="cancellationToken">The CancellationToken.</param>
  [HttpGet("{id}")]
  public async Task<ActionResult<ConsumerDto>> GetConsumerByIdAsync(int id, CancellationToken cancellationToken)
  {
    return await consumerService.GetConsumerByIdAsync(id, cancellationToken);
  }

  /// <summary>
  ///   Creates a consumer.
  /// </summary>
  /// <param name="writeConsumerDto">The WriteConsumerDto.</param>
  /// <param name="cancellationToken">The CancellationToken.</param>
  [HttpPost]
  public async Task<ActionResult<ConsumerDto>> CreateConsumerAsync([FromBody] WriteConsumerDto writeConsumerDto,
    CancellationToken cancellationToken)
  {
    return await consumerService.CreateConsumerAsync(writeConsumerDto, cancellationToken);
  }

  /// <summary>
  ///   Updates a consumer.
  /// </summary>
  /// <param name="id">The id.</param>
  /// <param name="writeConsumerDto">The WriteConsumerDto.</param>
  /// <param name="cancellationToken">The CancellationToken.</param>
  [HttpPut("{id}")]
  public async Task<ActionResult<ConsumerDto>> UpdateConsumerAsync(int id, WriteConsumerDto writeConsumerDto,
    CancellationToken cancellationToken)
  {
    return await consumerService.UpdateConsumerAsync(id, writeConsumerDto, cancellationToken);
  }

  /// <summary>
  ///   Deletes a consumer.
  /// </summary>
  /// <param name="id">The Id.</param>
  /// <param name="cancellationToken">The CancellationToken.</param>
  [HttpDelete("{id}")]
  public async Task<ActionResult<ConsumerDto>> DeleteConsumerAsync(int id, CancellationToken cancellationToken)
  {
    return await consumerService.DeleteConsumerAsync(id, cancellationToken);
  }
}
