using Microsoft.AspNetCore.Builder;

namespace CoffeePot.Web;

public static class Program
{
  public static void Main(string[] args)
  {
    var startup = new Startup();
    var app = startup.BuildWebApplication();

    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseRouting();
    app.MapControllers();
    app.MapGet("/", () => "Hello World!");
    app.Run();
  }
}
