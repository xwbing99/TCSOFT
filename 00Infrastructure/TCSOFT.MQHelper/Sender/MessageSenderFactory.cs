using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace TCSOFT.MQHelper.Sender
{
    /// <summary>
    /// 消息发送者工厂
    /// </summary>
    public static class MessageSenderFactory
    {
        /// <summary>
        /// 消息发送者实列容器
        /// </summary>
        public static Dictionary<string, MessageSenderHelper> senderGroup = new Dictionary<string, MessageSenderHelper>();

        /// <summary>
        /// 消息发送者实例（从配置中心获取）
        /// </summary>
        /// <param name="configUrl">配置中心地址</param>
        /// <param name="configPath">配置路径</param>
        /// <returns></returns>
        public static MessageSenderHelper Instance(string configUrl, string configPath)
        {
            MessageSenderHelper messageSenderHelper = null;

            if (senderGroup.ContainsKey(configUrl + configPath))
            {
                messageSenderHelper = senderGroup[configUrl + configPath];
            }
            else
            {
                //获取配置
                IConfiguration configuration = TCSOFT.ConfigManager.ConsulConfigurationExtensions.AddConsul(new ConfigurationBuilder(), new List<string> { configUrl }, configPath).Build();
                //生成新对象
                messageSenderHelper = new MessageSenderHelper(configuration, configPath.Replace("/", ":"));
                senderGroup.Add(configUrl + configPath, messageSenderHelper);
            }
            return messageSenderHelper;
        }

        /// <summary>
        /// 消息发送者实例（根据配置文件）
        /// </summary>
        /// <param name="configFileName">配置文件名</param>
        /// <param name="configPath">配置路径</param>
        /// <returns></returns>
        public static MessageSenderHelper Instance(string configFileName)
        {
            MessageSenderHelper messageSenderHelper = null;

            if (senderGroup.ContainsKey(configFileName))
            {
                messageSenderHelper = senderGroup[configFileName];
            }
            else
            {
                //获取配置
                IConfiguration configuration = TCSOFT.ConfigManager.JsonConfigurationExtensions.AddConfigFile(new ConfigurationBuilder(), configFileName).Build();
                //生成新对象
                messageSenderHelper = new MessageSenderHelper(configuration);
                senderGroup.Add(configFileName, messageSenderHelper);
            }

            return messageSenderHelper;
        }
    }
}
