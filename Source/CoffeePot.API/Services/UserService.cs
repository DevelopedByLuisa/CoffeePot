using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CoffeePot.API.DTOs.User;
using CoffeePot.API.Mappers;
using CoffeePot.Domain.Entities;
using CoffeePot.Domain.Enumerations;
using CoffeePot.Domain.Interfaces.Common;

namespace CoffeePot.API.Services;

public class UserService(IGenericRepository<User> userRepository)
{
  public async Task<UserDto> CreateUserAsync(WriteUserDto writeUserDto, CancellationToken cancellationToken)
  {
    var user = UserMapper.ConvertWriteUserDtoIntoUser(writeUserDto);
    var processedUser = await userRepository.CreateAsync(user, cancellationToken);
    return UserMapper.ConvertUserIntoUserDto(processedUser);
  }

  public async Task<UserDto> UpdateUserAsync(int id, WriteUserDto writeUserDto, CancellationToken cancellationToken)
  {
    var loadedUser = await userRepository.GetByIdAsync(id, cancellationToken);
    loadedUser.Update(writeUserDto.Forename, writeUserDto.Surname, writeUserDto.Email);

    loadedUser = await userRepository.UpdateAsync(loadedUser, cancellationToken);
    return UserMapper.ConvertUserIntoUserDto(loadedUser);
  }

  public async Task<UserDto> DeleteUserAsync(int id, CancellationToken cancellationToken)
  {
    var loadedUser = await userRepository.GetByIdAsync(id, cancellationToken);
    loadedUser.Delete();

    loadedUser = await userRepository.UpdateAsync(loadedUser, cancellationToken);
    return UserMapper.ConvertUserIntoUserDto(loadedUser);
  }

  public async Task<IEnumerable<UserDto>> GetUsersAsync(bool includeDeleted, CancellationToken cancellationToken)
  {
    var loadedUsers = await userRepository.GetAsync(cancellationToken);

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
    var loadedUser = await userRepository.GetByIdAsync(id, cancellationToken);
    return UserMapper.ConvertUserIntoUserDto(loadedUser);
  }
}
