﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

using TCSOFT.Consul;

namespace UsersApi
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
                .ConfigureAppConfiguration(cb =>
                {
                    var configuration = cb.Build();
                    //加载Consul配置中心相关配置
                    cb.AddConsul(new[] { configuration["ConfigCenter:Uri"] }
                                , configuration["ConfigCenter:path"]);
                })
                .UseStartup<Startup>();
    }
}
