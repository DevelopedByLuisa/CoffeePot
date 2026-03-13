using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CoffeePot.Domain.Enumerations;
using CoffeePot.Infrastructure.Repositories;
using CoffeePot.Web.DTOs.User;
using CoffeePot.Web.Mappers;

namespace CoffeePot.Web.Services;

public class UserService(UserRepository userRepository)
{
  public async Task<UserDto> CreateUserAsync(WriteUserDto writeUserDto, CancellationToken cancellationToken)
  {
    var user = UserMapper.ConvertWriteUserDtoIntoUser(writeUserDto);
    var processedUser = await userRepository.CreateUserAsync(user, cancellationToken);
    return UserMapper.ConvertUserIntoUserDto(processedUser);
  }

  public async Task<UserDto> UpdateUserAsync(int id, WriteUserDto writeUserDto, CancellationToken cancellationToken)
  {
    var loadedUser = await userRepository.GetUserAsync(id, cancellationToken);
    loadedUser.Forename = writeUserDto.Forename;
    loadedUser.Surname = writeUserDto.Surname;
    loadedUser.Email = writeUserDto.Email;
    loadedUser.RegisterChange();

    await userRepository.UpdateUserAsync(cancellationToken);
    return UserMapper.ConvertUserIntoUserDto(loadedUser);
  }

  public async Task<UserDto> DeleteUserAsync(int id, CancellationToken cancellationToken)
  {
    var loadedUser = await userRepository.GetUserAsync(id, cancellationToken);
    loadedUser.Status = Status.Deleted;
    loadedUser.RegisterChange();

    await userRepository.UpdateUserAsync(cancellationToken);
    return UserMapper.ConvertUserIntoUserDto(loadedUser);
  }

  public async Task<IEnumerable<UserDto>> GetUsersAsync(bool includeDeleted, CancellationToken cancellationToken)
  {
    var loadedUsers = await userRepository.GetUsersAsync(cancellationToken);

    if (includeDeleted)
    {
      return loadedUsers.Select(UserMapper.ConvertUserIntoUserDto);
    }

    return loadedUsers.Where(user => user.Status != Status.Deleted)
      .Select(UserMapper.ConvertUserIntoUserDto);
  }
}
