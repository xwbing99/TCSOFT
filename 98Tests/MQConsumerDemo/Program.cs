using Microsoft.Extensions.Configuration;
using System;
using TCSOFT.MQHelper;

namespace MQConsumerDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello " + args[0]);

            IConfiguration configuration = (new ConfigurationBuilder()).AddJsonFile("mqconfig.json").Build();
            IMessageConsumer messageConsumer = new TestMQConsumer();
            //Simple&Worker测试
            //RabbitMQConsumerHelper simpleMessageConsumer = new SimpleMQConsumer(configuration, messageConsumer);
            //simpleMessageConsumer.StartConsumeByQueueId(args[0]);

            //订阅者测试
            RabbitMQConsumerHelper messageSubscriber = new MessageSubscriber(configuration, messageConsumer);
            messageSubscriber.StartConsumeByQueueId(args[0]);
        }
    }
}
