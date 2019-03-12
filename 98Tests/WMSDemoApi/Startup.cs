using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Redis;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Swagger;
using System.IO;
using TCSOFT.Consul;
using TCSOFT.RedisCacheHelper;

namespace WMSDemoApi
{
    /// <summary>
    /// @author herowk
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// 配置读取对象
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// 构造函数-配置对象赋值
        /// </summary>
        /// <param name="configuration">配置对象</param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

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
                    , new Info
                    {
                        Title = Configuration["Swagger:Info:Title"],
                        Version = Configuration["Swagger:Info:Version"],
                        Description = Configuration["Swagger:Info:Description"],
                        TermsOfService = Configuration["Swagger:Info:TermsOfService"],
                        Contact = new Contact
                        {
                            Name = Configuration["Swagger:Info:ContactName"],
                            Email = Configuration["Swagger:Info:ContactEmail"]
                        }
                    });
                var basePath = Path.GetDirectoryName(typeof(Program).Assembly.Location);
                var xmlPath = Path.Combine(basePath, Configuration["Swagger:XmlDocName"]);
                options.IncludeXmlComments(xmlPath);
            });

            //Consul相关配置
            services.AddOptions();
            services.Configure<ConsulRegisterOptions>(Configuration.GetSection(Configuration["ConfigCenter:path"].Replace("/", ":")));

            //IdentityServer相关配置
            services.AddAuthentication("Bearer")
                .AddIdentityServerAuthentication(options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.Authority = "http://localhost:5000";
                    options.ApiName = "socialnetwork";
                });

            //将Redis分布式缓存服务添加到服务中
            services.AddDistributedRedisCache(options =>
            {
                //用于连接Redis的配置  Configuration.GetConnectionString("RedisConnectionString")读取配置信息的串
                options.Configuration = "localhost:7000";// Configuration.GetConnectionString("RedisConnectionString");
                //Redis实例名RedisDistributedCache
                options.InstanceName = "RedisDistributedCache";
                //options.
            });

            //services.AddSingleton(typeof(ICacheService), new RedisCacheHelper(new RedisCacheOptions
            //{
            //    Configuration = "",
            //    InstanceName = "tcwms"
            //}, 0));

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        /// <summary>
        /// 配置
        /// </summary>
        /// <param name="app">构建器</param>
        /// <param name="env">环境</param>
        /// <param name="appLifeTime">生命周期</param>
        /// <param name="consulRegisterOptions">配置选项</param>
        public void Configure(IApplicationBuilder app
                                , IHostingEnvironment env
                                , IApplicationLifetime appLifeTime
                                , IOptions<ConsulRegisterOptions> consulRegisterOptions)
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

            app.UseAuthentication();

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
            TCSOFT.Consul.ConsulServiceRegister consulRegister = new TCSOFT.Consul.ConsulServiceRegister();
            consulRegister.ConsulRegister(Configuration, appLifeTime, consulRegisterOptions);

        }
    }
}
