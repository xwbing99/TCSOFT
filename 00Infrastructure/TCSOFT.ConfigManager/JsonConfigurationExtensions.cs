using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace TCSOFT.ConfigManager
{
    public static class JsonConfigurationExtensions
    {

        /// <summary>
        /// 添加Consul配置项
        /// </summary>
        /// <param name="configurationBuilder">配置构造器</param>
        /// <param name="configFileName">配置文件路径</param>
        /// <returns></returns>
        public static IConfigurationBuilder AddConfigFile(this IConfigurationBuilder configurationBuilder
                                                        , string configFileName)
        {
            return configurationBuilder.AddJsonFile(configFileName);
        }
    }
}
