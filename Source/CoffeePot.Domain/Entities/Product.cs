using CoffeePot.Domain.Entities.Common;

namespace CoffeePot.Domain.Entities;

public class Product : BaseEntity
{
  public string Name { get; set; }
  public string Description { get; set; }
  public decimal UnitPrice { get; set; }
}
