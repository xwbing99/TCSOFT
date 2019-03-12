using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using TCSOFT.RedisCacheHelper;

namespace BaseComponetTester
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            ////IDistributedCache distributedCache = 
            //RedisCache redisCache = new RedisCache(new RedisCacheOptions()
            //{
            //    Configuration = "172.16.102.97:7000",
            //    InstanceName = "test:"
            //});
            //redisCache.SetString("key", "value");

            ////redisCache.m

            //var val = redisCache.GetString("key");
            //RedisStringService redisHelper = new RedisStringService(0);
            RedisStringService redisStringService = RedisHelper.StringService;
            redisStringService.StringSet("key", "value");
            redisStringService.StringGet("key");
            //redisStringService.KeyFulsh();
        }
    }
}
