using System;
using System.IO;
using System.Reflection;
using System.Text.Json.Serialization;
using CoffeePot.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace CoffeePot.Web;

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

    InitializeDatabase();

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
          Contact = new OpenApiContact
          {
            Email = "luisa-s-1996@protonmail.com",
            Name = "DevelopedByLuisa",
#pragma warning disable S1075
            Url = new Uri("https://github.com/DevelopedByLuisa")
#pragma warning restore S1075
          },
          Description = "A digital coffee fund for teams.",
          License = new OpenApiLicense
          {
            Name = "License",
#pragma warning disable S1075
            Url = new Uri("https://github.com/DevelopedByLuisa/CoffeePot/blob/main/LICENSE")
#pragma warning restore S1075
          },
          Title = "CoffeePot API",
          Version = "v1"
        });

      var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
      options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
    });
  }

  private void InitializeDatabase()
  {
    var connectionString = _builder.Configuration.GetConnectionString("CoffeePotDb");
    var serverVersion = ServerVersion.AutoDetect(connectionString);

    _builder.Services.AddDbContext<ApplicationContext>(optionsBuilder =>
      optionsBuilder.UseMySql(connectionString, serverVersion));
  }
}
