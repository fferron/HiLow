using HiLow.API.Validators;
using HiLow.Application.Services;
using HiLow.Application.Services.Interfaces;
using HiLow.Infrasctructure;
using HiLow.Infrastructure.SeedWorks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection;

namespace HiLow
{
    /// <summary>
    /// Startup class for the HiLow ClientAPI
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// HiLow appsettings.json configuration handler
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// Startup class constructor
        /// </summary>
        /// <param name="configuration"></param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// Configures the services injected for the clientAPI
        /// </summary>
        /// <param name="services"></param>
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
             services.AddControllers(options =>
                    {
                        options.Filters.Add(typeof(ApiExceptionFilterAttribute)); // custom exception to return status code mapping
                    }
                );

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "HigherOrLower", Version = "v1" });
            });

            //services.AddEntityFramework()
            //.AddSqlite()
            //.AddDbContext<AppDbContext>(
            //    options => { options.UseSqlite($"Data Source={_appEnv.ApplicationBasePath}/data.db"); });

            services.AddDbContext<AppDbContext>(options => options.UseSqlite(Configuration["ConnectionString"]));

            services
                .AddScoped<IUnitOfWork, UnitOfWork>()
                .AddScoped(typeof(IRepository<>), typeof(Repository<>))
                .AddScoped<ITourneyService, TourneyService>()
                .AddScoped<ITourneyRoundService, TourneyRoundService>()
                .AddScoped<ICardService, CardService>();
        }

        /// <summary>
        /// This method gets called by the runtime. Method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Configure(IApplicationBuilder app)
        {
            //app.UseEndpoints(x => x.MapControllers());

            app.UseSwagger();
            app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "HiLow Service API V1.0");
                }
            );

            app.UseCors(options =>
            {
                options
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
            });

            app.UseRouting();
            app.UseAuthentication();
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}
