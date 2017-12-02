﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Meteo.Api.Framework;
using Meteo.Core.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Meteo.Api
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
            // var options = new WeatherServiceOptions();
            // Configuration.GetSection("weatherService").Bind(options);

            services.AddMvc()
                .AddXmlSerializerFormatters();
            services.AddLogging();
            services.Configure<WeatherServiceOptions>(Configuration.GetSection("weatherService"));
            services.AddScoped<IWeatherService,WeatherService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env,
            ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment() || env.IsEnvironment("local"))
            {
                app.UseDeveloperExceptionPage();
            }
            loggerFactory.AddConsole().AddDebug();
            //app.Run(async ctx => Console.WriteLine($"PATH: {ctx.Request.Path}"));
            // app.Use(async (ctx, next) =>
            // {
            //     Console.WriteLine($"PATH: {ctx.Request.Path}");
            //     await next();
            // });
            //app.UseExceptionHandler("/error");
            app.UseErrorHandler();
            app.UseMvc();
        }
    }
}
