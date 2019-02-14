using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

using TCSOFT.Consul;

namespace WMSDemoApi
{
    /// <summary>
    /// 主线程
    /// </summary>
    public class Program
    {
        /// <summary>
        /// 主函数
        /// </summary>
        /// <param name="args">启动参数</param>
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        /// <summary>
        /// Web服务构建器
        /// </summary>
        /// <param name="args">参数</param>
        /// <returns></returns>
        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((hostContext, configBuilder) =>
                {
                    var configuration = configBuilder.Build();

                    //加载Consul配置中心相关配置
                    configBuilder.AddConsul(new[] { configuration["ConfigCenter:Uri"] }
                                , configuration["ConfigCenter:path"]);
                })
                .UseStartup<Startup>();
    }
}