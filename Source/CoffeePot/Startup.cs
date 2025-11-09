using System;
using System.IO;
using System.Reflection;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace CoffeePot;

public class Startup
{
  private readonly WebApplicationBuilder _builder = WebApplication.CreateBuilder();

  public WebApplication BuildWebApplication()
  {
    _builder.Services.AddControllers();
    _builder.Services.ConfigureHttpJsonOptions(options =>
    {
      options.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });

    if (_builder.Environment.IsDevelopment())
    {
      InitializeSwagger();
    }

    return _builder.Build();
  }

  private void InitializeSwagger()
  {
    _builder.Services.AddEndpointsApiExplorer();
    _builder.Services.AddSwaggerGen();
    _builder.Services.AddSwaggerGen(options =>
    {
      options.SwaggerDoc("v1",
        new OpenApiInfo
        {
          Version = "v1",
          Title = "CoffeePot API",
          Description = "A digital coffee fund for teams.",
          Contact = new OpenApiContact
          {
            Name = "DevelopedByLuisa",
            Email = "luisa-s-1996@protonmail.com",
            Url = new Uri("https://github.com/DevelopedByLuisa")
          },
          License = new OpenApiLicense
          {
            Name = "License", Url = new Uri("https://github.com/DevelopedByLuisa/CoffeePot/blob/main/LICENSE")
          }
        });

      var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
      options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
    });
  }
}
