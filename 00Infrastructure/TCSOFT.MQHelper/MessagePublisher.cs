﻿using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using System.Text;

namespace TCSOFT.MQHelper
{
    /// <summary>
    /// 发布订阅模式消息发布者
    /// @author herowk
    /// 一条消息多个消费者可同时消费
    /// </summary>
    public class MessagePublisher : RabbitMQSenderHelper
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="configuration">配置项</param>
        public MessagePublisher(IConfiguration configuration) : base(configuration) { }
        
        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="queueInfo">队列信息</param>
        /// <param name="messageContent">消息内容</param>
        /// <returns></returns>
        public override bool SendMessage(QueueInfo queueInfo, string messageContent)
        {
            //创建连接对象
            using (IConnection con = ConnFactory.CreateConnection())
            {
                //创建连接会话对象
                using (IModel channel = con.CreateModel())
                {
                    //声明交换机
                    channel.ExchangeDeclare(exchange: queueInfo.ExchangeName
                                            , type: queueInfo.TypeName);
                    //预声明队列
                    if (queueInfo.PreDeclareQueue)
                    {
                        channel.QueueDeclare(
                          queue: queueInfo.QueueName,
                          durable: queueInfo.Durable,
                          exclusive: queueInfo.Exclusive,
                          autoDelete: queueInfo.AutoDelete,
                          arguments: null
                           );
                    }
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