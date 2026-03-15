using CoffeePot.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoffeePot.Infrastructure.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
  public void Configure(EntityTypeBuilder<User> builder)
  {
    builder.ToTable("users");

    builder.HasKey(user => user.Id);

    builder.Property(user => user.Id)
      .HasColumnName("id")
      .ValueGeneratedOnAdd();

    builder.Property(user => user.Forename)
      .HasColumnName("forename")
      .HasMaxLength(255)
      .IsRequired();

    builder.Property(user => user.Surname)
      .HasColumnName("surname")
      .HasMaxLength(255)
      .IsRequired();

    builder.Property(user => user.Email)
      .HasColumnName("email")
      .HasMaxLength(255)
      .IsRequired();

    builder.Property(user => user.CreationDate)
      .HasColumnName("creation_date")
      .HasColumnType("timestamp")
      .IsRequired();

    builder.Property(user => user.ChangeDate)
      .HasColumnName("change_date")
      .HasColumnType("timestamp")
      .IsRequired();

    builder.Property(user => user.Status)
      .HasColumnName("status")
      .HasConversion<int>()
      .IsRequired();
  }
}
