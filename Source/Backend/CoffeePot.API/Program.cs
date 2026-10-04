using System;
using CoffeePot.API.Extensions;
using CoffeePot.Infrastructure.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;

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
      return;
    }

    builder.Services.AddApiDocumentation();
    builder.Services.AddDatabase(connectionString);

    var app = builder.Build();

    if (enableApiDocumentation)
    {
      app.UseSwagger();
      app.UseSwaggerUI();
    }

    app.MapGet("/", () => "System is up!");
    app.Run();
  }
}
