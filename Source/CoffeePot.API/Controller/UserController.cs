using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CoffeePot.API.DTOs.User;
using CoffeePot.API.Services;
using CoffeePot.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CoffeePot.API.Controller;

[ApiController]
[Route("api/users")]
public class UserController(IUserRepository userRepository, ILogger<UserController> logger)
  : ControllerBase
{
  private readonly UserService _userService = new(userRepository);

  /// <summary>
  ///   Returns a list of users.
  /// </summary>
  /// <param name="includeDeleted">Include deleted users.</param>
  /// <param name="cancellationToken">The CancellationToken.</param>
  [HttpGet]
  public async Task<ActionResult<IEnumerable<UserDto>>> GetUsersAsync(bool includeDeleted,
    CancellationToken cancellationToken)
  {
    return Ok(await _userService.GetUsersAsync(includeDeleted, cancellationToken));
  }

  /// <summary>
  ///   Returns a user.
  /// </summary>
  /// <param name="id">The Id.</param>
  /// <param name="cancellationToken">The CancellationToken.</param>
  [HttpGet("{id}")]
  public async Task<ActionResult<UserDto>> GetUserAsync(int id, CancellationToken cancellationToken)
  {
    return await _userService.GetUserAsync(id, cancellationToken);
  }

  /// <summary>
  ///   Creates a user.
  /// </summary>
  /// <param name="writeUserDto">The WriteUserDto.</param>
  /// <param name="cancellationToken">The CancellationToken.</param>
  [HttpPost]
  public async Task<ActionResult<UserDto>> CreateUserAsync([FromBody] WriteUserDto writeUserDto,
    CancellationToken cancellationToken)
  {
    return await _userService.CreateUserAsync(writeUserDto, cancellationToken);
  }

  /// <summary>
  ///   Updates a user.
  /// </summary>
  /// <param name="id">The id.</param>
  /// <param name="writeUserDto">The WriteUserDto.</param>
  /// <param name="cancellationToken">The CancellationToken.</param>
  [HttpPut("{id}")]
  public async Task<ActionResult<UserDto>> UpdateUserAsync(int id, WriteUserDto writeUserDto,
    CancellationToken cancellationToken)
  {
    return await _userService.UpdateUserAsync(id, writeUserDto, cancellationToken);
  }

  /// <summary>
  ///   Deletes a user.
  /// </summary>
  /// <param name="id">The Id.</param>
  /// <param name="cancellationToken">The CancellationToken.</param>
  [HttpDelete("{id}")]
  public async Task<ActionResult<UserDto>> DeleteUserAsync(int id, CancellationToken cancellationToken)
  {
    return await _userService.DeleteUserAsync(id, cancellationToken);
  }
}
