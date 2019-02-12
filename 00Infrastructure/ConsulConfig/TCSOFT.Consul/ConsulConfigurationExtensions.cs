using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TCSOFT.Consul
{
    /// <summary>
    /// Consul配置扩展类
    /// </summary>
    public static class ConsulConfigurationExtensions
    {
        /// <summary>
        /// 添加Consul配置项
        /// </summary>
        /// <param name="configurationBuilder">配置构造器</param>
        /// <param name="consulUrls">Consul地址</param>
        /// <param name="consulPath">配置项路径</param>
        /// <returns></returns>
        public static IConfigurationBuilder AddConsul(this IConfigurationBuilder configurationBuilder
                                                        , IEnumerable<Uri> consulUrls
                                                        , string consulPath)
        {
            return configurationBuilder.Add(new ConsulConfigurationSource(consulUrls, consulPath));
        }

        /// <summary>
        /// 添加Consul配置项
        /// </summary>
        /// <param name="configurationBuilder">配置构造器</param>
        /// <param name="consulUrls">Consul地址</param>
        /// <param name="consulPath">配置项路径</param>
        /// <returns></returns>
        public static IConfigurationBuilder AddConsul(this IConfigurationBuilder configurationBuilder
                                                        , IEnumerable<string> consulUrls
                                                        , string consulPath)
        {
            return configurationBuilder.AddConsul(consulUrls.Select(u => new Uri(u)), consulPath);
        }
    }
}
