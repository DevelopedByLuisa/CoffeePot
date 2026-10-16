using System;
using CoffeePot.Domain.Entities.Common;
using CoffeePot.Domain.Enumerations;

namespace Tests.UnitTests.Domain.Helpers;

/// <summary>
///   A TestEntity for testing purposes derived from the BaseEntity.
/// </summary>
public sealed class TestEntity : BaseEntity
{
  /// <summary>
  ///   Allows the modification of the CreationDate and ChangeDate in the BaseEntity.
  /// </summary>
  /// <param name="date">The DateTime.</param>
  public void OverrideDates(DateTime date)
  {
    CreationDate = date;
    ChangeDate = date;
  }

  /// <summary>
  ///   Allows the status to be modified in the BaseEntity.
  /// </summary>
  /// <param name="status">The Status.</param>
  public void OverrideStatus(Status status)
  {
    Status = status;
  }
}
