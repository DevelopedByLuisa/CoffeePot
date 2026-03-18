using CoffeePot.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoffeePot.Infrastructure.Configurations;

public class OrderDetailConfiguration : IEntityTypeConfiguration<OrderDetail>
{
  public void Configure(EntityTypeBuilder<OrderDetail> builder)
  {
    builder.ToTable("order_details");

    builder.HasKey(orderDetail => new { orderDetail.OrderId, orderDetail.ProductId });

    builder.Property(orderDetail => orderDetail.OrderId)
      .HasColumnName("order_id")
      .IsRequired();

    builder.Property(orderDetail => orderDetail.ProductId)
      .HasColumnName("product_id")
      .IsRequired();

    builder.Property(orderDetail => orderDetail.Quantity)
      .HasColumnName("quantity")
      .IsRequired();

    builder.Property(orderDetail => orderDetail.UnitPrice)
      .HasColumnName("unit_price")
      .HasPrecision(10, 2)
      .IsRequired();

    builder.HasOne(orderDetail => orderDetail.Order)
      .WithMany(orderDetail => orderDetail.OrderDetails)
      .HasForeignKey(orderDetail => orderDetail.OrderId)
      .HasConstraintName("fk_order_id")
      .OnDelete(DeleteBehavior.NoAction);

    builder.HasOne(orderDetail => orderDetail.Product)
      .WithMany()
      .HasForeignKey(orderDetail => orderDetail.ProductId)
      .HasConstraintName("fk_product_id")
      .OnDelete(DeleteBehavior.NoAction);
  }
}
