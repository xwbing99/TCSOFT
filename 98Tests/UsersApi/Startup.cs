using System;
using System.Collections.Generic;
using System.IO;
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

using Consul;
using Swashbuckle.AspNetCore.Swagger;

namespace UsersApi
{
    /// <summary>
    /// @author herowk
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// 构造函数-配置对象赋值
        /// </summary>
        /// <param name="configuration">配置对象</param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// 配置读取对象
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// 服务配置
        /// </summary>
        /// <param name="services">相关服务</param>
        public void ConfigureServices(IServiceCollection services)
        {
            //Swagger配置
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc(Configuration["Swagger:Name"]
                    , new Info { Title = Configuration["Swagger:Info:Title"]
                                , Version = Configuration["Swagger:Info:Version"]
                    });
                var basePath = Path.GetDirectoryName(typeof(Program).Assembly.Location);
                var xmlPath = Path.Combine(basePath, Configuration["Swagger:XmlDocName"]);
                options.IncludeXmlComments(xmlPath);
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        /// <summary>
        /// 配置
        /// </summary>
        /// <param name="app">构建器</param>
        /// <param name="env">环境</param>
        /// <param name="appLifeTime">生命周期</param>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IApplicationLifetime appLifeTime)
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
            app.UseMvc();

            //Swagger配置
            app.UseSwagger(c =>
            {
                c.RouteTemplate = "{documentName}/swagger.json";
            })
            .UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint($"/{Configuration["Swagger:Name"]}/swagger.json"
                                        , Configuration["Swagger:Name"]);
            });

            //注册到Consul
            ConsulApp(appLifeTime);
        }

        #region "注册到Consul"
        /// <summary>
        /// 注册到Consul
        /// </summary>
        /// <param name="appLifeTime">生命周期</param>
        private void ConsulApp(IApplicationLifetime appLifeTime)
        {
            //注册Consul 
            using (var consulClient = new ConsulClient(ConsulConfig))
            {
                AgentServiceRegistration asr = new AgentServiceRegistration
                {
                    Address = Configuration["Consul:Checker:IP"],
                    Port = Convert.ToInt32(Configuration["Consul:Checker:Port"]),
                    ID = Configuration["Consul:ServiceId"] + Guid.NewGuid(),
                    Name = Configuration["Consul:ServiceName"],
                    Check = new AgentServiceCheck
                    {
                        DeregisterCriticalServiceAfter = TimeSpan.FromSeconds(5),
                        HTTP = $"{Configuration["Consul:Checker:Protocol"]}://{Configuration["Consul:Checker:IP"]}:{Configuration["Consul:Checker:Port"]}/{Configuration["Consul:Checker:Uri"]}",
                        Interval = TimeSpan.FromSeconds(10),
                        Timeout = TimeSpan.FromSeconds(5)
                    },
                    Tags = new string[] { Configuration["Consul:Tag"] }
                };
                consulClient.Agent.ServiceRegister(asr).Wait();

            }

            //注销Consul 
            appLifeTime.ApplicationStopped.Register(() =>
            {
                using (var consulClient = new ConsulClient(ConsulConfig))
                {
                    Console.WriteLine("应用退出，开始从consul注销");
                    consulClient.Agent.ServiceDeregister(Configuration["Consul:ServiceId"]).Wait();
                }
            });
        }

        /// <summary>
        /// Consul配置委托
        /// </summary>
        /// <param name="config">配置项</param>
        private void ConsulConfig(ConsulClientConfiguration config)
        {
            config.Address = new Uri(Configuration["Consul:ServerUrl"]); 
            config.Datacenter = Configuration["Consul:DataCenter"];
        }
        #endregion "注册到Consul"
    }
}
