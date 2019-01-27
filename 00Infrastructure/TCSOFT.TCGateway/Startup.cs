﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Ocelot.Provider.Consul;
using Swashbuckle.AspNetCore;
using Swashbuckle.AspNetCore.Swagger;

namespace TCSOFT.TCGateway
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
            services.AddOcelot(new ConfigurationBuilder()
                    .AddJsonFile("ocelot.json", optional: true, reloadOnChange: true)
                    .Build()).AddConsul();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("ApiGateway", new Info { Title = "网关服务", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();


            var apis = new List<string>();

            using (var consul = new Consul.ConsulClient(c =>
                    {
                        c.Address = new Uri($"http://{Configuration["ConsulServer:IP"]}:{Configuration["ConsulServer:Port"]}");
                    }))
            {
                //取在Consul注册的全部服务
                var services = consul.Agent.Services().Result.Response;
                foreach (var s in services.Values)
                {
                    apis.Add(s.Service);
                }
            }

            app.UseMvc()
               .UseSwagger()
               .UseSwaggerUI(options =>
               {
                   apis.ForEach(m =>
                   {
                       options.SwaggerEndpoint($"/{m}/swagger.json", m);
                   });
               });

            app.UseOcelot().Wait();
        }
    }
}
