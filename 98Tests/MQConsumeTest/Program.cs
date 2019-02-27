using Microsoft.Extensions.Configuration;
using System;
using TCSOFT.MQHelper;

namespace MQConsumeTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            IConfiguration configuration = (new ConfigurationBuilder()).AddJsonFile("mqserver.json").Build();

            IMessageConsumer messageConsumer = new TestMQConsumer();

            //测试简单消息队列
            //SimapleMQConsumer simapleMQConsumer = new SimapleMQConsumer(configuration, messageConsumer);
            //simapleMQConsumer.StartConsumeByQueueType(QueueType.Test);

            //测试发布订阅模式（广播模式）
            QueueInfo qi = new QueueInfo(queueName: "testqueue" + DateTime.Now.ToString("yyyyMMddHHmmssfff")
                                        , routingKey: "testqueue"
                                        , typeName: "direct");
            MessageSubscriber messageSubscriber = new MessageSubscriber(configuration, messageConsumer);
            messageSubscriber.StartConsumeMessage(qi);


        }
    }
}
