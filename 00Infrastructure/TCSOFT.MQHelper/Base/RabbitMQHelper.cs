using Microsoft.Extensions.Configuration;
using System;
using RabbitMQ.Client;
using System.Text;

namespace TCSOFT.MQHelper
{
    public class RabbitMQHelper
    {
        #region "属性定义区"
        /// <summary>
        /// 基础配置项
        /// </summary>
        public IConfiguration Configuration { get; }
        /// <summary>
        /// 基础配置项
        /// </summary>
        public IConnectionFactory ConnFactory { get; }

        /// <summary>
        /// 键前缀
        /// </summary>
        public string KeyPre { get; set; }
        #endregion "变量定义区"

        /// <summary>
        /// 带配置信息构造
        /// </summary>
        /// <param name="configuration">配置信息</param>
        public RabbitMQHelper(IConfiguration configuration)
        {
            Configuration = configuration;

            //创建连接工厂对象
            ConnFactory = new ConnectionFactory
            {
                HostName = configuration["RabbitMQ:IP"],//IP地址
                Port = Convert.ToInt32(configuration["RabbitMQ:Port"]),//端口号
                UserName = configuration["RabbitMQ:UserName"],//用户账号
                Password = configuration["RabbitMQ:Password"]//用户密码
            };
        }

        /// <summary>
        /// 带配置信息构造
        /// </summary>
        /// <param name="configuration">配置信息</param
        /// <param name="keyPre">键前缀</param>>
        public RabbitMQHelper(IConfiguration configuration, string keyPre)
        {
            Configuration = configuration;

            KeyPre = $"{keyPre}:";

            //创建连接工厂对象
            ConnFactory = new ConnectionFactory
            {
                HostName = configuration[$"{KeyPre}RabbitMQ:IP"],//IP地址
                Port = Convert.ToInt32(configuration[$"{KeyPre}RabbitMQ:Port"]),//端口号
                UserName = configuration[$"{KeyPre}RabbitMQ:UserName"],//用户账号
                Password = configuration[$"{KeyPre}RabbitMQ:Password"]//用户密码
            };
        }

        /// <summary>
        /// 消费消息(指定队列类型)
        /// </summary>
        /// <param name="configuration">配置项</param>
        /// <param name="queueId">队列ID</param>
        /// <returns></returns>
        public QueueInfo GetQueueInfo(IConfiguration configuration
                                                , string queueId)
        {
            QueueInfo queueInfo = new QueueInfo();

            queueInfo.QueueName = configuration[$"{KeyPre}MQInfo:{queueId}:queueName"];
            queueInfo.Durable = System.Convert.ToBoolean(configuration[$"{KeyPre}MQInfo:{queueId}:durable"]);
            queueInfo.Exclusive = System.Convert.ToBoolean(configuration[$"{KeyPre}MQInfo:{queueId}:exclusive"]);
            queueInfo.AutoDelete = System.Convert.ToBoolean(configuration[$"{KeyPre}MQInfo:{queueId}:autoDelete"]);
            queueInfo.ExchangeName = configuration[$"{KeyPre}MQInfo:{queueId}:exchangeName"];
            queueInfo.RoutingKey = configuration[$"{KeyPre}MQInfo:{queueId}:routingKey"];
            queueInfo.TypeName = configuration[$"{KeyPre}MQInfo:{queueId}:typeName"];
            queueInfo.DeclareExchange = System.Convert.ToBoolean(configuration[$"{KeyPre}MQInfo:{queueId}:declareExchange"]);
            queueInfo.DeclareQueue = System.Convert.ToBoolean(configuration[$"{KeyPre}MQInfo:{queueId}:declareQueue"]);
            queueInfo.RandomQueue = System.Convert.ToBoolean(configuration[$"{KeyPre}MQInfo:{queueId}:randomQueue"]);

            return queueInfo;
        }
    }
}
