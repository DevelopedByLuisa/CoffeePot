using CoffeePot.API.DTOs.User;
using CoffeePot.Domain.Entities;

namespace CoffeePot.API.Mappers;

public static class UserMapper
{
  /// <summary>
  ///   Converts a WriteUserDto into a User. 
  /// </summary>
  /// <param name="writeUserDto">The WriteUserDto.</param>
  public static User ConvertWriteUserDtoIntoUser(WriteUserDto writeUserDto)
  {
    return new User { Forename = writeUserDto.Forename, Surname = writeUserDto.Surname };
  }

  /// <summary>
  ///   Converts a User into a UserDto
  /// </summary>
  /// <param name="user">The User.</param>
  public static UserDto ConvertUserIntoUserDto(User user)
  {
    return new UserDto(user.Id, user.Forename, user.Surname, user.Status);
  }
}
