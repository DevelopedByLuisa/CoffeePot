using System;
using CoffeePot.Domain.Enumerations;

namespace CoffeePot.Domain.Entities.Common;

public abstract class BaseEntity
{
  public int Id { get; init; }
  public DateTime CreationDate { get; private set; } = DateTime.Now;
  public DateTime ChangeDate { get; private set; } = DateTime.Now;
  public Status Status { get; set; } = Status.Active;

  public void RegisterChange()
  {
    ChangeDate = DateTime.Now;
  }
}
