using System;
using CoffeePot.Domain.Enumerations;

namespace CoffeePot.Domain.Common;

public abstract class BaseEntity
{
  public int Id { get; set; }
  public DateTime CreationDate { get; set; }
  public DateTime ChangeDate { get; set; }
  public Status Status { get; set; }
}
