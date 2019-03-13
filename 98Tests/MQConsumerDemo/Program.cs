using Microsoft.Extensions.Configuration;
using System;
using TCSOFT.MQHelper;
using TCSOFT.MQHelper.Consumer;

namespace MQConsumerDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            string queueId = string.Empty;
            if (args.Length > 0)
            {
                queueId = args[0];
            }
            else
            {
                Console.WriteLine("请输入队列ID：");
                queueId = Console.ReadLine();
            }

            Console.WriteLine("Hello " + queueId);
            //启动消费者
            MessageConsumerFactory.Instance("mqconfig.json", new TestMQConsumer()).StartConsumeByQueueId(queueId);
        }
    }
}
