using CoffeePot.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CoffeePot.Infrastructure;

public class ApplicationContext(DbContextOptions<ApplicationContext> options) : DbContext(options)
{
  public DbSet<Order> Orders { get; set; }
}
