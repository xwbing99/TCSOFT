using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Text;

namespace TCSOFT.MQHelper.Sender
{
    public class MessageSenderHelper : RabbitMQHelper
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="configuration">配置项</param>
        public MessageSenderHelper(IConfiguration configuration) : base(configuration) { }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="configuration">配置项</param>
        /// <param name="keyPre">键前缀</param>
        public MessageSenderHelper(IConfiguration configuration, string keyPre) : base(configuration, keyPre) { }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="hostName">主机地址</param>
        /// <param name="port">端口号</param>
        /// <param name="userName">用户名</param>
        /// <param name="password">密码</param>
        /// <param name="visualHost">虚拟主机</param>
        public MessageSenderHelper(string hostName
                            , int port
                            , string userName
                            , string password
                            , string visualHost): base(hostName, port, userName, password, visualHost) { }
        #region "消息发送相关"
        /// <summary>
        /// 发送消息(指定队列类型)
        /// </summary>
        /// <param name="queueId">队列ID</param>
        /// <param name="messageContent">消息内容</param>
        /// <returns></returns>
        public bool SendMessageByQueueId<T>(string queueId
                                        , T messageContent)
        {
            QueueInfo queueInfo = GetQueueInfo(Configuration, queueId);

            return SendMessage<T>(queueInfo, messageContent);
        }

        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="queueInfo">队列信息</param>
        /// <param name="messageContent">消息内容</param>
        /// <returns></returns>
        public virtual bool SendMessage<T>(QueueInfo queueInfo, T messageObject)
        {
            string messageContent = (messageObject is string) ? messageObject.ToString() : Newtonsoft.Json.JsonConvert.SerializeObject(messageObject);
            //创建连接对象
            using (IConnection con = ConnFactory.CreateConnection())
            {
                //创建连接会话对象
                using (IModel channel = con.CreateModel())
                {
                    //声明交换机
                    if (!string.IsNullOrEmpty(queueInfo.ExchangeName))
                    {
                        channel.ExchangeDeclare(exchange: queueInfo.ExchangeName
                                                , type: queueInfo.TypeName);
                    }

                    //声明队列
                    if (!string.IsNullOrEmpty(queueInfo.QueueName))
                    {
                        channel.QueueDeclare(
                          queue: queueInfo.QueueName,
                          durable: queueInfo.Durable,
                          exclusive: queueInfo.Exclusive,
                          autoDelete: queueInfo.AutoDelete,
                          arguments: null
                           );

                        //绑定队列
                        if (!string.IsNullOrEmpty(queueInfo.ExchangeName))
                        {
                            channel.QueueBind(queue: queueInfo.QueueName
                                            , exchange: queueInfo.ExchangeName
                                            , routingKey: queueInfo.RoutingKey);
                        }
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
        #endregion "消息发送相关"
    }
}
