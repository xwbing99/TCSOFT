using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace TCSOFT.MQHelper.Consumer
{
    /// <summary>
    /// 消息发送者工厂
    /// </summary>
    public static class MessageConsumerFactory
    {
        /// <summary>
        /// 消息发送者实例（从配置中心获取）
        /// </summary>
        /// <param name="configUrl">配置中心地址</param>
        /// <param name="configPath">配置路径</param>
        /// <returns></returns>
        public static MessageConsumerHelper Instance(string configUrl, string configPath, IMessageConsumer messageConsumer)
        {
            //获取配置
            IConfiguration configuration = TCSOFT.ConfigManager.ConsulConfigurationExtensions.AddConsul(new ConfigurationBuilder(), new List<string> { configUrl }, configPath).Build();
            return new MessageConsumerHelper(configuration, configPath.Replace("/", ":"), messageConsumer);
        }

        /// <summary>
        /// 消息发送者实例（从文件获取）
        /// </summary>
        /// <param name="configUrl">配置中心地址</param>
        /// <param name="configPath">配置路径</param>
        /// <returns></returns>
        public static MessageConsumerHelper Instance(string configFileName, IMessageConsumer messageConsumer)
        {
            //获取配置
            IConfiguration configuration = TCSOFT.ConfigManager.JsonConfigurationExtensions.AddConfigFile(new ConfigurationBuilder(), configFileName).Build();
            return new MessageConsumerHelper(configuration, messageConsumer);
        }
    }
}
