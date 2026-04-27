using CoffeePot.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoffeePot.Infrastructure.Configurations;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
  public void Configure(EntityTypeBuilder<Order> builder)
  {
    builder.ToTable("orders");

    builder.HasKey(order => order.Id);

    builder.Property(order => order.Id)
      .HasColumnName("id")
      .ValueGeneratedOnAdd();

    builder.Property(order => order.ConsumerId)
      .HasColumnName("consumer_id")
      .IsRequired();

    builder.Property(order => order.TotalAmount)
      .HasColumnName("total_amount")
      .HasPrecision(10, 2)
      .IsRequired();

    builder.Property(order => order.Annotation)
      .HasColumnName("annotation")
      .HasMaxLength(255)
      .IsRequired();

    builder.Property(order => order.CreationDate)
      .HasColumnName("creation_date")
      .HasColumnType("timestamp")
      .IsRequired();

    builder.Property(order => order.ChangeDate)
      .HasColumnName("change_date")
      .HasColumnType("timestamp")
      .IsRequired();

    builder.Property(order => order.Status)
      .HasColumnName("status")
      .HasConversion<int>()
      .IsRequired();

    builder.HasMany(order => order.OrderDetails)
      .WithOne(order => order.Order)
      .HasForeignKey(order => order.OrderId)
      .OnDelete(DeleteBehavior.NoAction);

    builder.HasOne(order => order.Consumer)
      .WithMany()
      .HasForeignKey(order => order.ConsumerId)
      .HasConstraintName("fk_consumer_id")
      .OnDelete(DeleteBehavior.NoAction);
  }
}
