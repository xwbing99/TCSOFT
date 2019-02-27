using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace APITests
{
    [TestClass]
    public class DemoApiTester
    {
        [TestMethod]
        public void TestGateway()
        {
            DateTime dtStart = DateTime.Now;
            TCSOFT.WebCore.TCRemoteMethodHandler.HttpGet("http://192.168.0.9:57395/UsersApi/Values");
            DateTime dtEnd = DateTime.Now;
            Console.WriteLine("Direct:" + (dtEnd - dtStart));

        }

        [TestMethod]
        public void TestDirect()
        {
            //记录网关访问耗时
            DateTime dtStart = DateTime.Now;
            TCSOFT.WebCore.TCRemoteMethodHandler.HttpGet("http://192.168.0.9:33552/api/Values");
            DateTime dtEnd = DateTime.Now;
            Console.WriteLine("Gateway:" + (dtEnd - dtStart));

        }
    }
}
