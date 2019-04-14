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
        public IConfiguration Configuration { get; set; }
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
                Password = configuration["RabbitMQ:Password"],//用户密码
                VirtualHost = configuration["RabbitMQ:VirtualHost"]//虚拟主机
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
                Password = configuration[$"{KeyPre}RabbitMQ:Password"],//用户密码
                VirtualHost = configuration[$"{KeyPre}RabbitMQ:VirtualHost"]//虚拟主机
            };
        }


        /// <summary>
        /// 传参构造
        /// </summary>
        /// <param name="hostName">主机地址</param>
        /// <param name="port">端口号</param>
        /// <param name="userName">用户名</param>
        /// <param name="password">密码</param>
        /// <param name="visualHost">虚拟主机</param>
        public RabbitMQHelper(string hostName
                            , int port
                            , string userName
                            , string password
                            , string visualHost)
        {
            //创建连接工厂对象
            ConnFactory = new ConnectionFactory
            {
                HostName = hostName,//IP地址
                Port = port,//端口号
                UserName = userName,//用户账号
                Password = password,//用户密码
                VirtualHost = visualHost //主机
            };
        }

        /// <summary>
        /// 获取队列信息
        /// </summary>
        /// <param name="configuration">配置项</param>
        /// <param name="queueId">队列ID</param>
        /// <returns></returns>
        public QueueInfo GetQueueInfo(IConfiguration configuration
                                                , string queueId)
        {
            if (string.IsNullOrEmpty(configuration[$"{KeyPre}MQInfo:{queueId}:queueName"]))
            {
                throw new Exception("队列配置信息不完整！");
            }

            QueueInfo queueInfo = new QueueInfo();

            queueInfo.QueueName = configuration[$"{KeyPre}MQInfo:{queueId}:queueName"];
            queueInfo.Durable = System.Convert.ToBoolean(configuration[$"{KeyPre}MQInfo:{queueId}:durable"]);
            queueInfo.Exclusive = System.Convert.ToBoolean(configuration[$"{KeyPre}MQInfo:{queueId}:exclusive"]);
            queueInfo.AutoDelete = System.Convert.ToBoolean(configuration[$"{KeyPre}MQInfo:{queueId}:autoDelete"]);
            queueInfo.ExchangeName = configuration[$"{KeyPre}MQInfo:{queueId}:exchangeName"];
            queueInfo.RoutingKey = configuration[$"{KeyPre}MQInfo:{queueId}:routingKey"];
            queueInfo.TypeName = configuration[$"{KeyPre}MQInfo:{queueId}:typeName"];
            queueInfo.RandomQueue = System.Convert.ToBoolean(configuration[$"{KeyPre}MQInfo:{queueId}:randomQueue"]);

            return queueInfo;
        }

        /// <summary>
        /// 根据参数生成队列信息
        /// </summary>
        /// <param name="queueName">队列名称</param>
        /// <param name="durable">是否持久化</param>
        /// <param name="exclusive">是否排他</param>
        /// <param name="autoDelete">是否自动删除</param>
        /// <param name="exchangeName">交换机名</param>
        /// <param name="routingKey">路由键</param>
        /// <param name="typeName">队列类型</param>
        /// <param name="randomQueue">是否随机队列</param>
        /// <returns></returns>
        public QueueInfo GetQueueInfo(string queueName
                                      , bool durable
                                      , bool exclusive
                                      , bool autoDelete
                                      , string exchangeName
                                      , string routingKey
                                      , string typeName
                                      , bool randomQueue)
        {
            QueueInfo queueInfo = new QueueInfo();

            queueInfo.QueueName = queueName;
            queueInfo.Durable = durable;
            queueInfo.Exclusive = exclusive;
            queueInfo.AutoDelete = autoDelete;
            queueInfo.ExchangeName = exchangeName;
            queueInfo.RoutingKey = routingKey;
            queueInfo.TypeName = typeName;
            queueInfo.RandomQueue = randomQueue;

            return queueInfo;
        }
    }
}
