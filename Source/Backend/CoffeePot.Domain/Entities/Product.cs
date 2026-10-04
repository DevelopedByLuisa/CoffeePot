using System;
using CoffeePot.Domain.Entities.Common;

namespace CoffeePot.Domain.Entities;

public class Product : BaseEntity
{
  public const int NameMaxLength = 55;
  public const int DescriptionMaxLength = 55;

  public string Name { get; private set; } = string.Empty;
  public string Description { get; private set; } = string.Empty;
  public decimal UnitPrice { get; private set; }

  private Product() { }

  public static Product Create(string name, string description, decimal unitPrice)
  {
    var product = new Product();
    product.SetValues(name, description, unitPrice);
    return product;
  }

  public void Update(string name, string description, decimal unitPrice)
  {
    SetValues(name, description, unitPrice);
    RegisterChange();
  }

  private void SetValues(string name, string description, decimal unitPrice)
  {
    ArgumentException.ThrowIfNullOrEmpty(name);
    ArgumentNullException.ThrowIfNull(description);
    ArgumentOutOfRangeException.ThrowIfNegative(unitPrice);

    if (name.Length > NameMaxLength)
    {
      throw new ArgumentException($"The name can't be longer than {NameMaxLength} characters!", nameof(name));
    }

    if (description.Length > DescriptionMaxLength)
    {
      throw new ArgumentException($"The description can't be longer thane {DescriptionMaxLength} characters!",
        nameof(description));
    }

    Name = name;
    Description = description;
    UnitPrice = unitPrice;
  }
}
