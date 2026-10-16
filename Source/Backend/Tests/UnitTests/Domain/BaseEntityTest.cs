using System;
using CoffeePot.Domain.Enumerations;
using Tests.UnitTests.Domain.Helpers;
using Xunit;

namespace Tests.UnitTests.Domain;

public class BaseEntityTest
{
  private static readonly DateTime OldStaticDate = new(1999, 12, 24, 11, 50, 2, DateTimeKind.Utc);

  [Fact]
  public void RegisterChange_ShouldUpdateChangeDate()
  {
    var entity = new TestEntity();
    entity.OverrideDates(OldStaticDate);

    var timeBefore = DateTime.UtcNow;
    entity.RegisterChange();
    var timeAfter = DateTime.UtcNow;

    Assert.InRange(entity.ChangeDate, timeBefore, timeAfter);
    Assert.Equal(OldStaticDate, entity.CreationDate);
  }

  [Fact]
  public void Delete_ShouldUpdateStatusToDeleted()
  {
    var entity = new TestEntity();
    entity.OverrideStatus(Status.Enabled);

    entity.Delete();

    Assert.Equal(Status.Deleted, entity.Status);
  }

  [Fact]
  public void Delete_ShouldUpdateChangeDate()
  {
    var entity = new TestEntity();
    entity.OverrideDates(OldStaticDate);

    var timeBefore = DateTime.UtcNow;
    entity.Delete();
    var timeAfter = DateTime.UtcNow;

    Assert.InRange(entity.ChangeDate, timeBefore, timeAfter);
    Assert.Equal(OldStaticDate, entity.CreationDate);
  }
}
