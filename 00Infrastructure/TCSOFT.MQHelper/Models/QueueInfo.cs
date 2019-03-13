using System.Collections.Generic;

namespace TCSOFT.MQHelper
{
    public class QueueInfo
    {
        #region "属性区"
        //队列名
        public string QueueName { get; set; }
        //是否持久化
        public bool Durable { get; set; }
        //是否排他
        public bool Exclusive { get; set; }
        //自动删除
        public bool AutoDelete { get; set; }
        //其他参数
        public Dictionary<string, object> Arguments { get; set; }
        //交换机名称
        public string ExchangeName { get; set; }
        //路由KEY
        public string RoutingKey { get; set; }
        //消息模式类型
        public string TypeName { get; set; }
        //是否生成随机队列
        public bool RandomQueue { get; set; }
        #endregion "属性区"

        /// <summary>
        /// 构造函数(赋默认值)
        /// </summary>
        public QueueInfo(string queueName = ""
                        , string exchangeName = ""
                        , bool durable = true
                        , bool exclusive = false
                        , string routingKey = ""
                        , bool autoDelete = false
                        , string typeName = ""
                        , bool randomQueue = false)
        {
            QueueName = queueName;
            ExchangeName = exchangeName;
            Durable = durable;
            Exclusive = exclusive;
            RoutingKey = routingKey;
            AutoDelete = autoDelete;
            TypeName = typeName;
            RandomQueue = randomQueue;
        }
    }
}
