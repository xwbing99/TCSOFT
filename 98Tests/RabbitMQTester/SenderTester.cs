using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TCSOFT.MQHelper;

namespace RabbitMQTester
{
    [TestClass]
    public class SenderTester
    {
        [TestMethod]
        public void TestSimpleSendMesage()
        {
            IConfiguration configuration = (new ConfigurationBuilder()).AddJsonFile("mqconfig.json").Build();

            RabbitMQSenderHelper simpleMQSender = new SimpleMQSender(configuration);
            simpleMQSender.SendMessageByQueueId("testsimplequeue", "Hello MQ! " + System.DateTime.Now.ToString("yyyyMMddHHmmss"));
        }

        [TestMethod]
        public void TestMessagePublisher()
        {
            IConfiguration configuration = (new ConfigurationBuilder()).AddJsonFile("mqconfig.json").Build();
            RabbitMQSenderHelper messagePublisher = new MessagePublisher(configuration);
            messagePublisher.SendMessageByQueueId("testexqueue", "Hello MQ! " + System.DateTime.Now.ToString("yyyyMMddHHmmss"));
        }
    }
}
