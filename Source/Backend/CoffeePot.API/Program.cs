using System;
using CoffeePot.API.Extensions;
using CoffeePot.Infrastructure.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CoffeePot.API;

public static class Program
{
  public static void Main(string[] args)
  {
    var builder = WebApplication.CreateBuilder(args);
    var enableApiDocumentation = builder.Configuration.GetValue<bool>("EnableApiDocumentation");
    var connectionString = Environment.GetEnvironmentVariable("CoffeePot");

    if (connectionString == null)
    {
      throw new InvalidOperationException("The connection string for coffeepot has not been set or is missing.");
    }

    builder.Services.AddApiDocumentation();
    builder.Services.AddDatabase(connectionString);
    builder.Services.AddRepositories();
    builder.Services.AddControllers();

    var app = builder.Build();

    if (enableApiDocumentation)
    {
      app.UseSwagger();
      app.UseSwaggerUI();
    }

    app.MapControllers();
    app.MapGet("/", () => "System is up!");
    app.Run();
  }
}
