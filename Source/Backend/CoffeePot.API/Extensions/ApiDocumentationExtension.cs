using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi;

namespace CoffeePot.API.Extensions;

[ExcludeFromCodeCoverage(Justification = "No custom code logic has been implemented.")]
public static class ApiDocumentationExtension
{
  public static IServiceCollection AddApiDocumentation(this IServiceCollection services)
  {
    services.AddEndpointsApiExplorer();
    services.AddSwaggerGen(options =>
    {
      options.SwaggerDoc("v1",
        new OpenApiInfo
        {
          Version = "v1",
          Title = "CoffeePot",
          Description = "The modern office beverage tally sheet alternative.",
          Contact = new OpenApiContact
          {
            Name = "Luisa Snelinski",
            Url = new Uri(new UriBuilder("https", "github.com", 443, "DevelopedByLuisa").ToString())
          },
          License = new OpenApiLicense
          {
            Name = "MIT license",
            Url = new Uri(new UriBuilder("https", "github.com", 443, "DevelopedByLuisa/CoffeePot/blob/main/LICENSE")
              .ToString())
          }
        });

      var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
      options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
    });

    return services;
  }
}
