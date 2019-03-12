using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;

namespace TCSOFT.MQHelper.Consumer
{
    public class MessageSubscriber : MessageConsumerHelper
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
        public MessageSubscriber(IConfiguration configuration
                                , IMessageConsumer messageConsumer) : base(configuration)
        {
            MessageConsumer = messageConsumer;
        }

        /// <summary>
        /// 消费消息
        /// </summary>
        /// <param name="queueInfo">队列信息</param>
        /// <returns></returns>
        public override void StartConsumeMessage(QueueInfo queueInfo)
        {
            //创建连接对象
            using (IConnection con = ConnFactory.CreateConnection())
            {
                //创建连接会话对象
                using (IModel channel = con.CreateModel())
                {
                    //队列名附加
                    string sQueueNameRandom = string.Empty;

                    if (queueInfo.RandomQueue)
                    {
                        sQueueNameRandom = "_" + TCSOFT.Utils.StringTools.GetRandomString(4, true, true, true, false, string.Empty);
                    }

                    //声明交换机
                    channel.ExchangeDeclare(exchange: queueInfo.ExchangeName
                                            , type: queueInfo.TypeName);
                    //声明队列
                    channel.QueueDeclare(
                              queue: queueInfo.QueueName + sQueueNameRandom
                              , durable: queueInfo.Durable
                              , exclusive: queueInfo.Exclusive
                              , autoDelete: queueInfo.AutoDelete
                              , arguments: null
                               );
                    //绑定队列
                    channel.QueueBind(queue: queueInfo.QueueName + sQueueNameRandom
                                    , exchange: queueInfo.ExchangeName
                                    , routingKey: queueInfo.RoutingKey);

                    //声明为手动确认
                    channel.BasicQos(0, 1, false);
                    //定义消费者
                    var consumer = new EventingBasicConsumer(channel);
                    //接收事件
                    consumer.Received += (model, result) =>
                    {
                        //接收到的消息
                        String messageContent = Encoding.UTF8.GetString(result.Body);
                        if (MessageConsumer.ConsumeMessage(messageContent))
                        {
                            channel.BasicAck(result.DeliveryTag, false);
                        }
                    };

                    //开启监听
                    channel.BasicConsume(queue: queueInfo.QueueName + sQueueNameRandom
                                        , autoAck: queueInfo.AutoDelete
                                        , consumer: consumer);
                    while (true) { }
                }
            }
        }
    }
}
