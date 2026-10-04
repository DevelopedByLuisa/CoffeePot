using Microsoft.EntityFrameworkCore;

namespace CoffeePot.Infrastructure;

public class ApplicationContext(DbContextOptions<ApplicationContext> contextOptions) : DbContext(contextOptions);
