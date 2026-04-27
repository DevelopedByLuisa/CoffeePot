using CoffeePot.Domain.Entities;
using CoffeePot.Infrastructure.Configurations;
using Microsoft.EntityFrameworkCore;

namespace CoffeePot.Infrastructure;

public class ApplicationContext(DbContextOptions<ApplicationContext> options) : DbContext(options)
{
  public DbSet<Product> Products { get; set; }
  public DbSet<Consumer> Consumers { get; set; }
  public DbSet<Order> Orders { get; set; }

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    modelBuilder.ApplyConfiguration(new ProductConfiguration());
    modelBuilder.ApplyConfiguration(new ConsumerConfiguration());
    modelBuilder.ApplyConfiguration(new OrderConfiguration());
    modelBuilder.ApplyConfiguration(new OrderDetailConfiguration());
  }
}
