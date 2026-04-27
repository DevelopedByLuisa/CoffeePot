using CoffeePot.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoffeePot.Infrastructure.Configurations;

public class ConsumerConfiguration : IEntityTypeConfiguration<Consumer>
{
  public void Configure(EntityTypeBuilder<Consumer> builder)
  {
    builder.ToTable("consumers");

    builder.HasKey(consumer => consumer.Id);

    builder.Property(consumer => consumer.Id)
      .HasColumnName("id")
      .ValueGeneratedOnAdd();

    builder.Property(consumer => consumer.Forename)
      .HasColumnName("forename")
      .HasMaxLength(255)
      .IsRequired();

    builder.Property(consumer => consumer.Surname)
      .HasColumnName("surname")
      .HasMaxLength(255)
      .IsRequired();

    builder.Property(consumer => consumer.CreationDate)
      .HasColumnName("creation_date")
      .HasColumnType("timestamp")
      .IsRequired();

    builder.Property(consumer => consumer.ChangeDate)
      .HasColumnName("change_date")
      .HasColumnType("timestamp")
      .IsRequired();

    builder.Property(consumer => consumer.Status)
      .HasColumnName("status")
      .HasConversion<int>()
      .IsRequired();
  }
}
