using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Consul;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Ocelot.Provider.Consul;
using Swashbuckle.AspNetCore.Swagger;

namespace TCSOFT.TCGateWay
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            //添加ocelot服务、添加Consul服务
            services.AddOcelot().AddConsul();
            
            //Swagger 配置
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("TCGateWay", new Info
                {
                    Version = "v1",
                    Title = "网关服务",
                    Description = "A simple example ASP.NET Core Web API",
                    TermsOfService = "None",
                    Contact = new Contact
                    {
                        Name = "王凯",
                        Email = string.Empty,
                        Url = "http://www.github.com/herowk/"
                    },
                    License = new License
                    {
                        Name = "许可证名字",
                        Url = "http://www.github.com/herowk/"
                    }
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)//, IConfiguration configuration, IApplicationLifetime lifetime, IApplicationBuilder builder)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //设置所有的Ocelot中间件
            app.UseOcelot().Wait();

            //启用中间件服务生成Swagger作为JSON终结点
            var apis = new List<string> { "count"};
            app.UseSwagger()
               .UseSwaggerUI(options =>
                {
                    apis.ForEach(m =>
                    {
                        options.SwaggerEndpoint($"/swagger/{m}/swagger.json", m);
                    });
                });

            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("Hello World!");
            });
        }
    }
}
