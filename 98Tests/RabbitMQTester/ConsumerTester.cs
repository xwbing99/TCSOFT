using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TCSOFT.MQHelper;

namespace RabbitMQTester
{
    [TestClass]
    public class ConsumerTester
    {
        [TestMethod]
        public void TestSimpleConsumeMesage()
        {
            IConfiguration configuration = (new ConfigurationBuilder()).AddJsonFile("mqserver.json").Build();
            IMessageConsumer messageConsumer = new TestMQConsumer();

            RabbitMQConsumerHelper simpleMQConsumer = new SimpleMQConsumer(configuration, messageConsumer);
            simpleMQConsumer.StartConsumeByQueueId("testsimplequeue");
        }

        [TestMethod]
        public void TestMesageSubscriber()
        {
            IConfiguration configuration = (new ConfigurationBuilder()).AddJsonFile("mqserver.json").Build();
            IMessageConsumer messageConsumer = new TestMQConsumer();

            RabbitMQConsumerHelper simpleMQConsumer = new SimpleMQConsumer(configuration, messageConsumer);
            simpleMQConsumer.StartConsumeByQueueId("testexqueue");
        }
    }
}
