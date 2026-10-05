using CoffeePot.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CoffeePot.Infrastructure;

public class ApplicationContext(DbContextOptions<ApplicationContext> contextOptions) : DbContext(contextOptions)
{
  public DbSet<Product> Products { get; set; }
}
