using System;
using System.Collections.Generic;
using System.Text;

namespace TCSOFT.MQHelper
{
    /// <summary>
    /// 消息消费者接口
    /// </summary>
    public interface IMessageConsumer
    {
        /// <summary>
        /// 消息消费方法
        /// </summary>
        /// <param name="messageContent">消息内容</param>
        bool ConsumeMessage(string messageContent);
    }
}
