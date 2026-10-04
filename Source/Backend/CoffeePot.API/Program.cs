using CoffeePot.API.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;

namespace CoffeePot.API;

public static class Program
{
  public static void Main(string[] args)
  {
    var builder = WebApplication.CreateBuilder(args);
    var enableApiDocumentation = builder.Configuration.GetValue<bool>("EnableApiDocumentation");
    
    builder.Services.AddApiDocumentation();
    
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
