using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CoffeePot.API.DTOs.User;
using CoffeePot.API.Mappers;
using CoffeePot.Domain.Enumerations;
using CoffeePot.Domain.Interfaces;

namespace CoffeePot.API.Services;

public class UserService(IUserRepository userRepository)
{
  public async Task<UserDto> CreateUserAsync(WriteUserDto writeUserDto, CancellationToken cancellationToken)
  {
    var user = UserMapper.ConvertWriteUserDtoIntoUser(writeUserDto);
    var processedUser = await userRepository.CreateUserAsync(user, cancellationToken);
    return UserMapper.ConvertUserIntoUserDto(processedUser);
  }

  public async Task<UserDto> UpdateUserAsync(int id, WriteUserDto writeUserDto, CancellationToken cancellationToken)
  {
    var loadedUser = await userRepository.GetUserByIdAsync(id, cancellationToken);
    loadedUser.UpdateUser(writeUserDto.Forename, writeUserDto.Surname, writeUserDto.Email);

    await userRepository.SaveChangesAsync(cancellationToken);
    return UserMapper.ConvertUserIntoUserDto(loadedUser);
  }

  public async Task<UserDto> DeleteUserAsync(int id, CancellationToken cancellationToken)
  {
    var loadedUser = await userRepository.GetUserByIdAsync(id, cancellationToken);
    loadedUser.Delete();

    await userRepository.SaveChangesAsync(cancellationToken);
    return UserMapper.ConvertUserIntoUserDto(loadedUser);
  }

  public async Task<IEnumerable<UserDto>> GetUsersAsync(bool includeDeleted, CancellationToken cancellationToken)
  {
    var loadedUsers = await userRepository.GetUsersAsync(cancellationToken);

    if (includeDeleted)
    {
      return loadedUsers.Select(UserMapper.ConvertUserIntoUserDto);
    }

    return loadedUsers
      .Where(user => user.Status != Status.Deleted)
      .Select(UserMapper.ConvertUserIntoUserDto);
  }

  public async Task<UserDto> GetUserAsync(int id, CancellationToken cancellationToken)
  {
    var loadedUser = await userRepository.GetUserByIdAsync(id, cancellationToken);
    return UserMapper.ConvertUserIntoUserDto(loadedUser);
  }
}
