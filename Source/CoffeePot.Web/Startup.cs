using System;
using System.IO;
using System.Reflection;
using System.Text.Json.Serialization;
using CoffeePot.Domain.Interfaces;
using CoffeePot.Infrastructure;
using CoffeePot.Infrastructure.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi;

namespace CoffeePot.Web;

public class Startup
{
  private readonly WebApplicationBuilder _builder = WebApplication.CreateBuilder();
  private readonly IConfigurationRoot _configuration = LoadAppSettings();

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
    InitializeRepositories();

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
            Url = new Uri("https://github.com/DevelopedByLuisa")
          },
          Description = "A digital coffee fund for teams.",
          License = new OpenApiLicense
          {
            Name = "License", Url = new Uri("https://github.com/DevelopedByLuisa/CoffeePot/blob/main/LICENSE")
          },
          Title = "CoffeePot API",
          Version = "v1"
        });

      var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
      options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
    });
  }

  private static IConfigurationRoot LoadAppSettings()
  {
    return new ConfigurationBuilder()
      .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
      .AddJsonFile("appsettings.json")
      .Build();
  }

  private void InitializeDatabase()
  {
    var connectionString = _configuration.GetConnectionString("MariaDB");
    var serverVersion = ServerVersion.AutoDetect(connectionString);

    _builder.Services.AddDbContext<ApplicationContext>(dbContextOptionsBuilder =>
      dbContextOptionsBuilder.UseMySql(connectionString, serverVersion));
  }

  private void InitializeRepositories()
  {
    _builder.Services.AddTransient<IOrderRepository, OrderRepository>();
  }
}
