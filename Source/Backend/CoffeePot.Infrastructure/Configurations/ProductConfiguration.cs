using CoffeePot.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoffeePot.Infrastructure.Configurations;

public class ProductConfiguration : BaseEntityConfiguration<Product>
{
  public override void Configure(EntityTypeBuilder<Product> builder)
  {
    base.Configure(builder);

    builder.ToTable("products");

    builder.Property(product => product.Name)
      .HasColumnName("name")
      .HasMaxLength(Product.NameMaxLength)
      .IsRequired();

    builder.Property(product => product.Description)
      .HasColumnName("description")
      .HasMaxLength(Product.DescriptionMaxLength)
      .IsRequired();

    builder.Property(product => product.UnitPrice)
      .HasColumnName("unit_price")
      .HasPrecision(10, 2)
      .IsRequired();
  }
}
