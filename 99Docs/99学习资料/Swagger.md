## 配置示例

位置：Api->Startup.cs

``` C#
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

    services.AddOptions();
    services.Configure<ConsulRegisterOptions>(Configuration.GetSection(Configuration["ConfigCenter:path"].Replace("/", ":")));

    services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
}
```



```c#
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
```



## Info说明

* Info.Title 

  标题，示例：Title = "示例API"

* Info.Version 

  版本，示例：Version = "v1"

* Info.Description 

  描述，示例：Description = "这只是一个示例API"

* Info.TermsOfService 

  服务条款链接地址，示例：TermsOfService = "http://tcsoftware.com/tos"

* Info.Contact 

  联系信息，其中包含：

  * Name : 名称
  * Email : 邮箱地址



## 隐藏想隐藏的API

**在需要隐藏的方法上面加上**  *[ApiExplorerSettings(IgnoreApi = true)]*     

``` C#
[ApiExplorerSettings(IgnoreApi = true)]
[HttpGet]
public string Value (){
	return "Value";
}
```

