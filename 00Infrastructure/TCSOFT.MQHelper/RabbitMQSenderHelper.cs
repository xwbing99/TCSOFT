using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace TCSOFT.MQHelper
{
    public class RabbitMQSenderHelper : RabbitMQHelper
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="configuration">配置项</param>
        public RabbitMQSenderHelper(IConfiguration configuration) : base(configuration) { }

        #region "消息发送相关"
        /// <summary>
        /// 发送消息(指定队列类型)
        /// </summary>
        /// <param name="queueId">队列ID</param>
        /// <param name="messageContent">消息内容</param>
        /// <returns></returns>
        public bool SendMessageByQueueId(string queueId
                                        , string messageContent)
        {
            QueueInfo queueInfo = GetQueueInfo(Configuration, queueId);

            return SendMessage(queueInfo, messageContent);
        }

        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="queueInfo">队列信息</param>
        /// <param name="messageContent">消息内容</param>
        /// <returns></returns>
        public virtual bool SendMessage(QueueInfo queueInfo, string messageContent) { return false; }
        #endregion "消息发送相关"
    }
}
