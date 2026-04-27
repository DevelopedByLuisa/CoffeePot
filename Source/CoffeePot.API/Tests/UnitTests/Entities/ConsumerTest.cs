using AutoFixture.Xunit3;
using CoffeePot.Domain.Entities;
using Xunit;

namespace Tests.UnitTests.Entities;

public class ConsumerTest
{
  [Theory]
  [AutoData]
  public void Update_ShouldUpdatePropertiesAndChangeDate(string forename, string surname)
  {
    var sut = new Consumer { Forename = "Test", Surname = "Person" };
    var changeDateBefore = sut.ChangeDate;

    sut.Update(forename, surname);

    Assert.Equal(forename, sut.Forename);
    Assert.Equal(surname, sut.Surname);
    Assert.True(sut.ChangeDate >= changeDateBefore);
  }
}
