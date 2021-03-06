﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebApplication1.Models;
using WebApplication1.Services;

namespace WebApplication1
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
	        services.AddMvc();
			//Create a stance for each http request
	        services.AddScoped<IRestaurant, InMemoryRestaurant>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IConfiguration config)
        {
			if (env.IsDevelopment())
			{
				//Middleware
				app.UseDeveloperExceptionPage();
			}

	        app.UseStaticFiles();

	        app.UseMvc(ConfigureRoutes);

			//Simple middleware
			app.Run(async (context) =>
			{
				await context.Response.WriteAsync("Not Found");
			});
		}

	    private void ConfigureRoutes(IRouteBuilder routeBuilder)
	    {
		    routeBuilder.MapRoute("Default", "{controller=Home}/{action=Index}/{id?}");
	    }
    }
}
