using Microsoft.VisualStudio.TestTools.UnitTesting;
using TCSOFT.MQHelper.Consumer;

namespace MQHelperTester
{
    [TestClass]
    public class ConsumerTester
    {
        [TestMethod]
        public void TestSimpleConsumeMesage()
        {
            MessageConsumerFactory.Instance("http://192.168.0.24:8500", "MQInfo/MQInfo", new TestMQConsumer()).StartConsumeByQueueId("testsimplequeue");
        }

        [TestMethod]
        public void TestMesageSubscriber()
        {
            MessageConsumerFactory.Instance("mqconfig.json", new TestMQConsumer()).StartConsumeByQueueId("testexqueue");
        }
    }
}