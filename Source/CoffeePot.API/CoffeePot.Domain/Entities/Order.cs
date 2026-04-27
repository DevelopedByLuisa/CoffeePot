using System.Collections.Generic;
using CoffeePot.Domain.Entities.Common;
using CoffeePot.Domain.Enumerations;

namespace CoffeePot.Domain.Entities;

public class Order : BaseEntity
{
  public int ConsumerId { get; set; }
  public Consumer Consumer { get; set; }
  public decimal TotalAmount { get; set; }
  public string Annotation { get; set; } = string.Empty;
  public ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

  public void CancelOrder()
  {
    Status = Status.Canceled;
    RegisterChange();
  }
}
