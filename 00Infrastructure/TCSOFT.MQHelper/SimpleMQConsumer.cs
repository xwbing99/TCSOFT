using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;

namespace TCSOFT.MQHelper
{
    public class SimpleMQConsumer : RabbitMQHelper
    {
        /// <summary>
        /// 消息消费者实例
        /// </summary>
        IMessageConsumer MessageConsumer;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="configuration">配置项</param>
        /// <param name="messageConsumer">消费者实例</param>
        public SimpleMQConsumer(IConfiguration configuration, IMessageConsumer messageConsumer) : base(configuration)
        {
            MessageConsumer = messageConsumer;
        }

        /// <summary>
        /// 消费消息
        /// </summary>
        /// <param name="queueInfo">队列信息</param>
        /// <returns></returns>
        public new void StartConsumeMessage(QueueInfo queueInfo)
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
                    //每个消费者每次只投递一条消息
                    channel.BasicQos(0, 1, false);
                    //创建消费者对象
                    var consumer = new EventingBasicConsumer(channel);
                    consumer.Received += (model, result) =>
                    {
                        //接收到的消息
                        String messageContent = Encoding.UTF8.GetString(result.Body);
                        if (MessageConsumer.ConsumeMessage(messageContent))
                        {
                            channel.BasicAck(result.DeliveryTag, false);
                        }
                    };
                    //消费者开启监听
                    channel.BasicConsume(queue: queueInfo.QueueName
                                        , autoAck: queueInfo.AutoDelete
                                        , consumer: consumer);
                    while (true) { }
                }
            }
        }
    }
}
