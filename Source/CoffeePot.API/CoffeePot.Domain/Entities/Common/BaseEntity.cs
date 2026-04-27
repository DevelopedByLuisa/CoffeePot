using System;
using CoffeePot.Domain.Enumerations;

namespace CoffeePot.Domain.Entities.Common;

public abstract class BaseEntity
{
  public int Id { get; init; }
  public DateTime CreationDate { get; private set; } = DateTime.UtcNow;
  public DateTime ChangeDate { get; private set; } = DateTime.UtcNow;
  public Status Status { get; set; } = Status.Active;

  public void RegisterChange()
  {
    ChangeDate = DateTime.UtcNow;
  }

  public void Delete()
  {
    Status = Status.Deleted;
    RegisterChange();
  }
}
