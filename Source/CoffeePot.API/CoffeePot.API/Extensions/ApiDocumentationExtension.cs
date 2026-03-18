using System;
using System.IO;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi;

namespace CoffeePot.API.Extensions;

public static class ApiDocumentationExtension
{
  public static IServiceCollection InitializeApiDocumentation(
    this IServiceCollection services)
  {
    services.AddEndpointsApiExplorer();
    services.AddSwaggerGen(options =>
    {
      options.SwaggerDoc("v1",
        new OpenApiInfo
        {
          Contact = new OpenApiContact
          {
            Email = "luisa-s-1996@protonmail.com",
            Name = "DevelopedByLuisa",
            Url = new Uri("https://github.com/DevelopedByLuisa")
          },
          Description = "A digital coffee fund for teams.",
          License = new OpenApiLicense
          {
            Name = "License",
            Url = new Uri("https://github.com/DevelopedByLuisa/CoffeePot/blob/main/LICENSE")
          },
          Title = "CoffeePot API",
          Version = "v1"
        });

      var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
      options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
    });

    return services;
  }
}
