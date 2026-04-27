using AutoFixture.Xunit3;
using CoffeePot.Domain.Entities;
using CoffeePot.Domain.Enumerations;
using Xunit;

namespace Tests.UnitTests.Entities;

public class ProductTest
{
  [Theory]
  [AutoData]
  public void Update_ShouldUpdatePropertiesAndChangeDate(string name, string description, decimal unitPrice)
  {
    var sut = new Product { Name = "Original Name", Description = "Original Description.", UnitPrice = 0.34M };
    var changeDateBefore = sut.ChangeDate;

    sut.Update(name, description, unitPrice);

    Assert.Equal(name, sut.Name);
    Assert.Equal(description, sut.Description);
    Assert.Equal(unitPrice, sut.UnitPrice);
    Assert.True(sut.ChangeDate >= changeDateBefore);
  }

  [Fact]
  public void RegisterChange_ShouldUpdateChangeDate()
  {
    var sut = new Product { Name = "Original Name", Description = "Original Description.", UnitPrice = 0.34M };
    var changeDateBefore = sut.ChangeDate;

    sut.RegisterChange();

    Assert.True(sut.ChangeDate >= changeDateBefore);
  }

  [Fact]
  public void Delete_ShouldUpdateStatusAndChangeDate()
  {
    var sut = new Product { Name = "Original Name", Description = "Original Description.", UnitPrice = 0.34M };
    var changeDateBefore = sut.ChangeDate;

    sut.Delete();

    Assert.Equal(Status.Deleted, sut.Status);
    Assert.True(sut.ChangeDate >= changeDateBefore);
  }
}
