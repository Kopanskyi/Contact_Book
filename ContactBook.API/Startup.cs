using ContactBook.API.Middleware;
using ContactBook.API.Repositories;
using ContactBook.API.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace ContactBook.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ContactBook.API", Version = "v1" });
            });

            var allowedOrigins = Configuration.GetValue<string>("AllowedOrigins");
            services.AddCors(c =>
            {
                c.AddPolicy("AllowOrigin", options => options.WithOrigins(allowedOrigins));
            });

            services.AddScoped<IAccountService, AccountService>();
            services.AddSingleton<IAccountRepository, AccountRepository>();
            services.AddScoped<ICountryService, CountryService>();
            services.AddSingleton<ICountryRepository, CountryRepository>();
            services.AddScoped<IContactService, ContactService>();
            services.AddSingleton<IContactRepository, ContactRepository>();
            services.AddScoped<IAuthService, AuthService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ContactBook.API v1"));
            }

            var allowedOrigins = Configuration.GetValue<string>("AllowedOrigins");
            app.UseCors(options => options.WithOrigins(allowedOrigins).AllowAnyMethod().AllowAnyHeader());

            app.UseRouting();            
            app.UseMiddleware<ErrorHandlerMiddleware>();
            app.UseMiddleware<AuthMiddleware>();            
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
