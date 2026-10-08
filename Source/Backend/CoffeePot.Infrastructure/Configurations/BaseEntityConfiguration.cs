using CoffeePot.Domain.Entities.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoffeePot.Infrastructure.Configurations;

public abstract class BaseEntityConfiguration<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity : BaseEntity
{
  public virtual void Configure(EntityTypeBuilder<TEntity> builder)
  {
    builder.HasKey(entity => entity.Id);

    builder.Property(entity => entity.Id)
      .HasColumnName("id")
      .ValueGeneratedOnAdd();

    builder.Property(entity => entity.CreationDate)
      .HasColumnName("creation_date")
      .HasColumnType("timestamp")
      .IsRequired();

    builder.Property(entity => entity.ChangeDate)
      .HasColumnName("change_date")
      .HasColumnType("timestamp")
      .IsRequired();

    builder.Property(entity => entity.Status)
      .HasColumnName("status")
      .HasConversion<int>()
      .IsRequired();
  }
}
