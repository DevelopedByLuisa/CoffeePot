using CoffeePot.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoffeePot.Infrastructure.Configurations;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
  public void Configure(EntityTypeBuilder<Product> builder)
  {
    builder.ToTable("products");

    builder.HasKey(product => product.Id);

    builder.Property(product => product.Id)
      .HasColumnName("id")
      .ValueGeneratedOnAdd();

    builder.Property(product => product.Name)
      .HasColumnName("name")
      .HasMaxLength(55)
      .IsRequired();

    builder.Property(product => product.Description)
      .HasColumnName("description")
      .HasMaxLength(55)
      .IsRequired();

    builder.Property(product => product.UnitPrice)
      .HasColumnName("unit_price")
      .HasPrecision(10, 2)
      .IsRequired();

    builder.Property(product => product.CreationDate)
      .HasColumnName("creation_date")
      .HasColumnType("timestamp")
      .IsRequired();

    builder.Property(product => product.ChangeDate)
      .HasColumnName("change_date")
      .HasColumnType("timestamp")
      .IsRequired();

    builder.Property(product => product.Status)
      .HasColumnName("status")
      .HasConversion<int>()
      .IsRequired();
  }
}
