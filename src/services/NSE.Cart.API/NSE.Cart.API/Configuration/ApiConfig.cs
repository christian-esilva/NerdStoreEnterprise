using Microsoft.EntityFrameworkCore;
using NSE.Cart.API.Data;
using NSE.WebApi.Core.Identity;

namespace NSE.Cart.API.Configuration
{
    public static class ApiConfig
    {
        public static IServiceCollection AddApiConfiguration(this IServiceCollection services,
                                                 ConfigurationManager configuration,
                                                 WebApplicationBuilder builder)
        {
            services.AddDbContext<CartContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            if (builder.Environment.IsDevelopment())
                configuration.AddUserSecrets<Program>();

            services.AddControllers()
                .ConfigureApiBehaviorOptions(options => options.SuppressModelStateInvalidFilter = true);

            services.AddCors(options =>
            {
                options.AddPolicy("Total",
                    builder =>
                        builder
                            .AllowAnyOrigin()
                            .AllowAnyMethod()
                            .AllowAnyHeader());
            });

            return services;
        }

        public static void UseApiConfiguration(this WebApplication app)
        {
            if (app.Environment.IsDevelopment())
                app.UseDeveloperExceptionPage();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors("Total");

            app.UseAuthConfiguration();

            app.MapControllers();

            app.Run();
        }
    }
}
