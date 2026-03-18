using AutoFixture.Xunit3;
using CoffeePot.Domain.Entities;
using Xunit;

namespace Tests.UnitTests.Entities;

public class UserTest
{
  [Theory]
  [AutoData]
  public void Update_ShouldUpdatePropertiesAndChangeDate(string forename, string surname, string email)
  {
    var sut = new User { Forename = "Test", Surname = "Person", Email = "test@test.local" };
    var changeDateBefore = sut.ChangeDate;

    sut.Update(forename, surname, email);

    Assert.Equal(forename, sut.Forename);
    Assert.Equal(surname, sut.Surname);
    Assert.Equal(email, sut.Email);
    Assert.True(sut.ChangeDate >= changeDateBefore);
  }
}
