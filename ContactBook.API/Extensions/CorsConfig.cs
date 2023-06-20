using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ContactBook.API.Extensions
{
    public static class CorsConfig
    {
        public static IServiceCollection ConfigureCorsPolicies(this IServiceCollection services, IConfiguration configuration)
        {
            // Create Develop policy
            services.AddCors(options =>
                options.AddPolicy(
                    "AllowAllOrigins",
                    builder =>
                    {
                        builder
                            .AllowAnyOrigin()
                            .AllowAnyMethod()
                            .AllowAnyHeader();
                    }
                )
            );

            // Create Production policy
            services.AddCors(options =>
                options.AddPolicy(
                    "ProductionPolicy",
                    builder =>
                    {
                        builder
                            .WithOrigins(new string[] { configuration["AllowedOrigins"] })
                            .AllowAnyMethod()
                            .AllowAnyHeader();
                    }
                )
            );

            return services;
        }
    }
}
