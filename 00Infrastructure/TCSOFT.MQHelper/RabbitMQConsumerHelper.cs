using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace TCSOFT.MQHelper
{
    public class RabbitMQConsumerHelper : RabbitMQHelper
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="configuration">配置项</param>
        public RabbitMQConsumerHelper(IConfiguration configuration) : base(configuration) { }

        #region "消息消费相关"
        /// <summary>
        /// 消费消息(根据配置信息)
        /// </summary>
        /// <param name="queueId">队列ID</param>
        /// <returns></returns>
        public void StartConsumeByQueueId(string queueId)
        {
            QueueInfo queueInfo = GetQueueInfo(Configuration, queueId);
            StartConsumeMessage(queueInfo);
        }

        /// <summary>
        /// 消费消息
        /// </summary>
        /// <param name="queueInfo">队列信息</param>
        /// <returns></returns>
        public virtual void StartConsumeMessage(QueueInfo queueInfo) { }
        #endregion "消息消费相关"
    }
}
