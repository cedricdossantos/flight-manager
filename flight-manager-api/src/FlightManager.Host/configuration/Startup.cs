using System;
using System.IO;
using System.Linq;
using System.Reflection;
using FlightManager.Api.Controllers;
using FlightManager.Repositories;
using FlightManager.Repositories.Models;
using FlightManager.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using Microsoft.EntityFrameworkCore;

namespace FlightManager.Host
{
    /// <inheritdoc cref="Startup" />
    public class Startup
    {
        /// <inheritdoc />
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        private IConfiguration Configuration { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IFlightService, FlightService>();
            services.AddTransient<IFlightRepository, FlightRepository>();
            services.AddDbContext<FlightManagerDbContext>(opt =>
                opt.UseInMemoryDatabase("inMemoryDB"));

            services.AddMvc()
                .AddApplicationPart(typeof(FlightsController).GetTypeInfo().Assembly)
                .AddControllersAsServices()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info {Title = "Flight manager", Version = "v1"});
                var filePath = Path.Combine(System.AppContext.BaseDirectory, "FlightManager.Host.xml");
                c.IncludeXmlComments(filePath);
            });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "Flight manager"); });

            app.UseMvc();
        }
    }
}