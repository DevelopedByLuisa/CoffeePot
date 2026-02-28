using CoffeePot.Domain.Common;

namespace CoffeePot.Domain.Entities;

public class Order : BaseEntity
{
  public int UserId { get; set; }
  public double TotalPrice { get; set; }
  public OrderDetail OrderDetail { get; set; }
}
