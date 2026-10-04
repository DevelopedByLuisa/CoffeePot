using System;
using CoffeePot.Domain.Enumerations;

namespace CoffeePot.Domain.Entities.Common;

public abstract class BaseEntity
{
  public int Id { get; set; }
  public DateTime CreationDate { get; set; } = DateTime.UtcNow;
  public DateTime ChangeDate { get; set; } = DateTime.UtcNow;
  public Status Status { get; set; } = Status.Enabled;

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
