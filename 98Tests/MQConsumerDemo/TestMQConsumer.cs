using System;

namespace MQConsumerDemo
{
    public class TestMQConsumer : TCSOFT.MQHelper.IMessageConsumer
    {
        public bool ConsumeMessage(string messageContent)
        {
            Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " " + messageContent);
            return true;
        }
    }
}
