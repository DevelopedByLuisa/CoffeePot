using AutoFixture.Xunit3;
using CoffeePot.API.DTOs.User;
using CoffeePot.API.Mappers;
using CoffeePot.Domain.Entities;
using Xunit;

namespace Tests.UnitTests.Mappers;

public class UserMapperTest
{
  [Theory]
  [AutoData]
  public void ConvertWriteUserDtoIntoUser_ShouldReturnUser(WriteUserDto writeUserDto)
  {
    var result = UserMapper.ConvertWriteUserDtoIntoUser(writeUserDto);

    Assert.Equal(writeUserDto.Forename, result.Forename);
    Assert.Equal(writeUserDto.Surname, result.Surname);
    Assert.Equal(writeUserDto.Email, result.Email);
  }

  [Theory]
  [AutoData]
  public void ConvertUserIntoUserDto_ShouldReturnUserDto(User user)
  {
    var result = UserMapper.ConvertUserIntoUserDto(user);

    Assert.Equal(user.Id, result.Id);
    Assert.Equal(user.Forename, result.Forename);
    Assert.Equal(user.Surname, result.Surname);
    Assert.Equal(user.Email, result.Email);
    Assert.Equal(user.Status, result.Status);
  }
}
