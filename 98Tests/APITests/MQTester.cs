using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TCSOFT.MQHelper;

namespace APITests
{
    [TestClass]
    public class MQTester
    {
        [TestMethod]
        public void TestSimpleSendMesage()
        {
            IConfiguration configuration = (new ConfigurationBuilder()).AddJsonFile("mqserver.json").Build();
            
            SimpleMQSender simpleMQSender = new SimpleMQSender(configuration);
            simpleMQSender.SendMessageByConfiguration(configuration, "testqueue", "Hello MQ! " + System.DateTime.Now.ToString("yyyyMMddHHmmss"));
        }

        [TestMethod]
        public void TestMessagePublisher()
        {
            IConfiguration configuration = (new ConfigurationBuilder()).AddJsonFile("mqserver.json").Build();
            MessagePublisher messagePublisher = new MessagePublisher(configuration);
            messagePublisher.SendMessageByConfiguration(configuration, "testqueue", "Hello MQ! " + System.DateTime.Now.ToString("yyyyMMddHHmmss"));
        }

        [TestMethod]
        public void TestSimpleConsumeMesage()
        {
            IConfiguration configuration = (new ConfigurationBuilder()).AddJsonFile("mqserver.json").Build();
            IMessageConsumer messageConsumer = null;

            SimpleMQConsumer simapleMQConsumer = new SimpleMQConsumer(configuration, messageConsumer);
            simapleMQConsumer.SendMessageByConfiguration(configuration, "testqueue", "Hello MQ! " + System.DateTime.Now.ToString("yyyyMMddHHmmss"));
        }
    }
}
