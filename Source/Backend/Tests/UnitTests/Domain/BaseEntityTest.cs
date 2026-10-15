using System;
using CoffeePot.Domain.Entities.Common;
using CoffeePot.Domain.Enumerations;
using Xunit;

namespace Tests.UnitTests.Domain;

public class BaseEntityTest
{
  private sealed class TestEntity : BaseEntity
  {
  }

  [Fact]
  public void RegisterChange_ShouldUpdateChangeDate()
  {
    var oldDate = new DateTime(1999, 12, 24, 11, 50, 2, DateTimeKind.Utc);
    var entity = new TestEntity { CreationDate = oldDate, ChangeDate = oldDate };

    var timeBefore = DateTime.UtcNow;
    entity.RegisterChange();
    var timeAfter = DateTime.UtcNow;

    Assert.InRange(entity.ChangeDate, timeBefore, timeAfter);
    Assert.Equal(oldDate, entity.CreationDate);
  }

  [Fact]
  public void Delete_ShouldUpdateStatusToDeleted()
  {
    var entity = new TestEntity { Status = Status.Enabled };

    entity.Delete();

    Assert.Equal(Status.Deleted, entity.Status);
  }

  [Fact]
  public void Delete_ShouldUpdateChangeDate()
  {
    var oldDate = new DateTime(1999, 12, 24, 11, 50, 2, DateTimeKind.Utc);
    var entity = new TestEntity { CreationDate = oldDate, ChangeDate = oldDate };

    var timeBefore = DateTime.UtcNow;
    entity.Delete();
    var timeAfter = DateTime.UtcNow;

    Assert.InRange(entity.ChangeDate, timeBefore, timeAfter);
    Assert.Equal(oldDate, entity.CreationDate);
  }
}
