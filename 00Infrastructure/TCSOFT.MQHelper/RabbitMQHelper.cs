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
        /// 消费消息(指定队列类型)
        /// </summary>
        /// <param name="configuration">配置项</param>
        /// <param name="queueId">队列ID</param>
        /// <returns></returns>
        public QueueInfo GetQueueInfo(IConfiguration configuration
                                                , string queueId)
        {
            QueueInfo queueInfo = new QueueInfo();

            queueInfo.QueueName = configuration[$"MQInfo:{queueId}:queueName"];
            queueInfo.Durable = System.Convert.ToBoolean(configuration[$"MQInfo:{queueId}:durable"]);
            queueInfo.Exclusive = System.Convert.ToBoolean(configuration[$"MQInfo:{queueId}:exclusive"]);
            queueInfo.AutoDelete = System.Convert.ToBoolean(configuration[$"MQInfo:{queueId}:autoDelete"]);
            queueInfo.ExchangeName = configuration[$"MQInfo:{queueId}:exchangeName"];
            queueInfo.RoutingKey = configuration[$"MQInfo:{queueId}:routingKey"];
            queueInfo.TypeName = configuration[$"MQInfo:{queueId}:typeName"];
            queueInfo.PreDeclareQueue = System.Convert.ToBoolean(configuration[$"MQInfo:{queueId}:preDeclareQueue"]);
            queueInfo.RandomQueue = System.Convert.ToBoolean(configuration[$"MQInfo:{queueId}:randomQueue"]);

            return queueInfo;
        }
    }
}
