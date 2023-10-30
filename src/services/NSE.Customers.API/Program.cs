using MediatR;
using NSE.Customers.API.Configuration;
using NSE.WebApi.Core.Identity;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.SetEnvironment();

builder.Services.AddApiConfiguration(builder.Configuration, builder);

builder.Services.AddJwtConfiguration(builder.Configuration);

builder.Services.AddSwaggerConfiguration();

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));

builder.Services.RegisterServices();

builder.Services.AddMessageBusConfiguration(builder.Configuration);

var app = builder.Build();

app.UseSwaggerConfiguration();
app.UseApiConfiguration();
