using System;
using CoffeePot.Domain.Enumerations;

namespace CoffeePot.Domain.Entities.Common;

public abstract class BaseEntity
{
  public int Id { get; private set; }
  public DateTime CreationDate { get; protected set; } = DateTime.UtcNow;
  public DateTime ChangeDate { get; protected set; } = DateTime.UtcNow;
  public Status Status { get; protected set; } = Status.Enabled;

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
