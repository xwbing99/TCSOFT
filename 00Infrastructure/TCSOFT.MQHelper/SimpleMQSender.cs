using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using System.Text;

namespace TCSOFT.MQHelper
{
    /// <summary>
    /// 简单队列消息发送者
    /// @author
    /// 一对一或一对多，一条消息一个消费者
    /// </summary>
    public class SimpleMQSender : RabbitMQHelper
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="configuration">配置项</param>
        public SimpleMQSender(IConfiguration configuration) : base(configuration) { }

        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="queueInfo">队列信息</param>
        /// <param name="messageContent">消息内容</param>
        /// <returns></returns>
        public new bool SendMessage(QueueInfo queueInfo, string messageContent)
        {
            //创建连接对象
            using (IConnection con = ConnFactory.CreateConnection())
            {
                //创建连接会话对象
                using (IModel channel = con.CreateModel())
                {
                    //声明一个队列
                    channel.QueueDeclare(
                      queue: queueInfo.QueueName,
                      durable: queueInfo.Durable,
                      exclusive: queueInfo.Exclusive,
                      autoDelete: queueInfo.AutoDelete,
                      arguments: null
                       );

                    //消息内容
                    byte[] body = Encoding.UTF8.GetBytes(messageContent);
                    //发送消息
                    channel.BasicPublish(exchange: queueInfo.ExchangeName
                                    , routingKey: queueInfo.RoutingKey
                                    , basicProperties: null
                                    , body: body);
                }
            }
            return true;
        }
    }
}
