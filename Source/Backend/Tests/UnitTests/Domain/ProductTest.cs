using System;
using System.Threading;
using CoffeePot.Domain.Entities;
using Xunit;

namespace Tests.UnitTests.Domain;

public class ProductTest
{
  private const string Name = "foo";
  private const string NewName = "hello";
  private const string Description = "bar";
  private const string NewDescription = "world";
  private const decimal UnitPrice = 0.11m;
  private const decimal NewUnitPrice = 9.99m;

  [Fact]
  public void Create_ShouldCreateAProduct()
  {
    var product = Product.Create(Name, Description, UnitPrice);

    Assert.Equal(Name, product.Name);
    Assert.Equal(Description, product.Description);
    Assert.Equal(UnitPrice, product.UnitPrice);
  }

  [Fact]
  public void Create_NameIsNull_ShouldThrowArgumentNullException()
  {
    Assert.Throws<ArgumentNullException>(() => Product.Create(null!, Description, UnitPrice));
  }

  [Fact]
  public void Create_NameIsEmpty_ShouldThrowArgumentException()
  {
    Assert.Throws<ArgumentException>(() => Product.Create(string.Empty, Description, UnitPrice));
  }

  [Fact]
  public void Create_DescriptionIsNull_ShouldThrowArgumentNullException()
  {
    Assert.Throws<ArgumentNullException>(() => Product.Create(Name, null!, UnitPrice));
  }

  [Fact]
  public void Create_DescriptionIsEmpty_ShouldRemainEmpty()
  {
    var product = Product.Create(Name, string.Empty, UnitPrice);

    Assert.Equal(string.Empty, product.Description);
  }

  [Fact]
  public void Create_UnitPriceIsNegative_ShouldThrowArgumentOutOfRangeException()
  {
    Assert.Throws<ArgumentOutOfRangeException>(() => Product.Create(Name, Description, -UnitPrice));
  }

  [Fact]
  public void Create_UnitPriceIsZero_ShouldRemainZero()
  {
    var product = Product.Create(Name, Description, 0);

    Assert.Equal(0, product.UnitPrice);
  }

  [Fact]
  public void Create_NameIsExactNameMaxLength_ShouldSucceed()
  {
    var validName = new string('a', Product.NameMaxLength);

    var product = Product.Create(validName, Description, UnitPrice);

    Assert.Equal(validName, product.Name);
  }

  [Fact]
  public void Create_NameIsLongerThanNameMaxLength_ShouldThrowArgumentException()
  {
    var invalidName = new string('a', Product.NameMaxLength + 1);

    Assert.Throws<ArgumentException>(() => Product.Create(invalidName, Description, UnitPrice));
  }

  [Fact]
  public void Create_DescriptionIsExactDescriptionMaxLength_ShouldSucceed()
  {
    var validDescription = new string('a', Product.DescriptionMaxLength);

    var product = Product.Create(Name, validDescription, UnitPrice);

    Assert.Equal(validDescription, product.Description);
  }

  [Fact]
  public void Create_DescriptionIsLongerThanDescriptionMaxLength_ShouldThrowArgumentException()
  {
    var invalidDescription = new string('a', Product.DescriptionMaxLength + 1);

    Assert.Throws<ArgumentException>(() => Product.Create(Name, invalidDescription, UnitPrice));
  }

  [Fact]
  public void Update_ShouldUpdateProduct()
  {
    var product = Product.Create(Name, Description, UnitPrice);

    product.Update(NewName, NewDescription, NewUnitPrice);

    Assert.Equal(NewName, product.Name);
    Assert.Equal(NewDescription, product.Description);
    Assert.Equal(NewUnitPrice, product.UnitPrice);
  }

  [Fact]
  public void Update_NameIsLongerThanNameMaxLength_ShouldThrowArgumentException()
  {
    var invalidName = new string('a', Product.NameMaxLength + 1);
    var product = Product.Create(Name, Description, UnitPrice);

    Assert.Throws<ArgumentException>(() => product.Update(invalidName, Description, UnitPrice));
  }

  [Fact]
  public void Update_PriceIsNegative_ShouldThrowArgumentOutOfRangeException()
  {
    var product = Product.Create(Name, Description, UnitPrice);

    Assert.Throws<ArgumentOutOfRangeException>(() => product.Update(Name, Description, -UnitPrice));
  }

  [Fact]
  public void Update_ShouldUpdateChangeDate()
  {
    var product = Product.Create(Name, Description, UnitPrice);
    SpinWait.SpinUntil(() => DateTime.UtcNow > product.ChangeDate);

    var beforeTime = DateTime.UtcNow;
    product.Update(NewName, NewDescription, NewUnitPrice);
    var afterTime = DateTime.UtcNow;

    Assert.InRange(product.ChangeDate, beforeTime, afterTime);
  }
}
