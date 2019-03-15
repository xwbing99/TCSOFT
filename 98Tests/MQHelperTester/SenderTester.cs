using Microsoft.VisualStudio.TestTools.UnitTesting;
using TCSOFT.MQHelper.Sender;

namespace MQHelperTester
{
    [TestClass]
    public class SenderTester
    {
        [TestMethod]
        public void TestSimpleSendMesage()
        {
            //By File
            MessageSenderFactory.Instance("mqconfig.json").SendMessageByQueueId("testsimplequeue", "Hello MQ! " + System.DateTime.Now.ToString("yyyyMMddHHmmss"));
            //By Consul
            MessageSenderFactory.Instance("http://192.168.0.24:8500", "MQInfo/MQInfo").SendMessageByQueueId("testsimplequeue", "Hello MQ! " + System.DateTime.Now.ToString("yyyyMMddHHmmss"));
        }

        [TestMethod]
        public void TestMessagePublisher()
        {
            //By File
            MessageSenderFactory.Instance("mqconfig.json").SendMessageByQueueId("testexqueue", "Hello MQ! " + System.DateTime.Now.ToString("yyyyMMddHHmmss"));
            //By Consul
            MessageSenderFactory.Instance("http://192.168.0.24:8500", "MQInfo/MQInfo").SendMessageByQueueId("testexqueue", "Hello MQ! " + System.DateTime.Now.ToString("yyyyMMddHHmmss"));
        }

        [TestMethod]
        public void TestMesageUsaNews()
        {
            MessageSenderFactory.Instance("mqconfig.json").SendMessageByQueueId("usanews", "Hello USA News! " + System.DateTime.Now.ToString("yyyyMMddHHmmss"));
        }

        [TestMethod]
        public void TestMesageUsaWeather()
        {
            MessageSenderFactory.Instance("mqconfig.json").SendMessageByQueueId("usaweather", "Hello USA Weather! " + System.DateTime.Now.ToString("yyyyMMddHHmmss"));
        }

        [TestMethod]
        public void TestMesageEuropeNews()
        {
            MessageSenderFactory.Instance("mqconfig.json").SendMessageByQueueId("europenews", "Hello Europe News! " + System.DateTime.Now.ToString("yyyyMMddHHmmss"));
        }

        [TestMethod]
        public void TestMesageEuropeWeather()
        {
            MessageSenderFactory.Instance("mqconfig.json").SendMessageByQueueId("europeweather", "Hello Europe Weather! " + System.DateTime.Now.ToString("yyyyMMddHHmmss"));
        }
    }
}
