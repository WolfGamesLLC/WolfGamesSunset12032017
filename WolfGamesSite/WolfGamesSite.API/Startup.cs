﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using WolfGamesSite.DAL.Models.SimpleGameModels;
using Microsoft.EntityFrameworkCore;
using WolfGamesSite.DAL.Data;

namespace WolfGamesSite.API
{
    /// <summary>
    /// Default startup class for the WG API app
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="configuration">Application configuration</param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// Configuration accessor
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// Configure application services
        /// </summary>
        /// <param name="services">Collection of app services</param>
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<MarbleMotionDBContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddMvc();
        }

        /// <summary>
        /// Configure the HTTP request pipeline
        /// </summary>
        /// <param name="app">app's pipeline configuration object</param>
        /// <param name="env">app's web hosting environment</param>
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
