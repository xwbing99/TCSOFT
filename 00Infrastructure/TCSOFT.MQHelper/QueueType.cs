namespace TCSOFT.MQHelper
{
    public static class QueueType
    {
        #region "变量定义区"
        public const string Test = "testqueue";
        #endregion "变量定义区"

        public static QueueInfo GetQueueInfo(string queueType)
        {
            QueueInfo queueInfo = new QueueInfo();
            switch (queueType)
            {
                case Test:
                    queueInfo.QueueName = string.Empty;
                    queueInfo.ExchangeName = "testqueue";
                    queueInfo.Durable = true;
                    queueInfo.Exclusive = false;
                    queueInfo.RoutingKey = "testqueue";
                    queueInfo.AutoDelete = false;
                    queueInfo.TypeName = "direct";
                    break;
            }
            return queueInfo;
        }
    }
}
