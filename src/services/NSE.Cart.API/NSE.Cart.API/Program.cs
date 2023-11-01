using NSE.Cart.API.Configuration;
using NSE.WebApi.Core.Identity;

var builder = WebApplication.CreateBuilder(args);

builder.SetEnvironment();

builder.Services.AddApiConfiguration(builder.Configuration, builder);
builder.Services.AddJwtConfiguration(builder.Configuration);

builder.Services.AddSwaggerConfiguration();

builder.Services.RegisterServices();

var app = builder.Build();

app.UseSwaggerConfiguration();
app.UseApiConfiguration();
