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
    var sut = UserMapper.ConvertWriteUserDtoIntoUser(writeUserDto);

    Assert.Equal(writeUserDto.Forename, sut.Forename);
    Assert.Equal(writeUserDto.Surname, sut.Surname);
    Assert.Equal(writeUserDto.Email, sut.Email);
  }

  [Theory]
  [AutoData]
  public void ConvertUserIntoUserDto_ShouldReturnUserDto(User user)
  {
    var sut = UserMapper.ConvertUserIntoUserDto(user);

    Assert.Equal(user.Id, sut.Id);
    Assert.Equal(user.Forename, sut.Forename);
    Assert.Equal(user.Surname, sut.Surname);
    Assert.Equal(user.Email, sut.Email);
    Assert.Equal(user.Status, sut.Status);
  }
}
