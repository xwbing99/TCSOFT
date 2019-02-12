using Consul;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;

namespace TCSOFT.Consul
{
    public class ConsulServiceRegister
    {
        /// <summary>
        /// 注册到Consul
        /// </summary>
        /// <param name="appLifeTime">生命周期</param>
        /// <param name="consulRegisterOptions">配置对象</param>
        public void ConsulApp(IApplicationLifetime appLifeTime, IOptions<ConsulRegisterOptions> consulRegisterOptions)
        {
            string serviceIdFull = string.Empty;

            //注册Consul 
            using (var consulClient = new ConsulClient())
            {
                consulClient.Config.Address = new Uri(consulRegisterOptions.Value.ServerUrl);
                consulClient.Config.Datacenter = consulRegisterOptions.Value.DataCenter;

                AgentServiceRegistration asr = new AgentServiceRegistration
                {
                    Address = consulRegisterOptions.Value.Checker.IP,
                    Port = Convert.ToInt32(consulRegisterOptions.Value.Checker.Port),
                    ID = consulRegisterOptions.Value.ServiceId + Guid.NewGuid(),
                    Name = consulRegisterOptions.Value.ServiceName,
                    Check = new AgentServiceCheck
                    {
                        DeregisterCriticalServiceAfter = TimeSpan.FromSeconds(5),
                        HTTP = $"{consulRegisterOptions.Value.Checker.Protocol}://{consulRegisterOptions.Value.Checker.IP}:{consulRegisterOptions.Value.Checker.Port}/{consulRegisterOptions.Value.Checker.Uri}",
                        Interval = TimeSpan.FromSeconds(10),
                        Timeout = TimeSpan.FromSeconds(5)
                    },
                    Tags = new string[] { consulRegisterOptions.Value.Tag }
                };
                consulClient.Agent.ServiceRegister(asr).Wait();
                serviceIdFull = asr.ID;
            }

            //注销Consul 
            appLifeTime.ApplicationStopped.Register(() =>
            {
                using (var consulClient = new ConsulClient())
                {
                    consulClient.Config.Address = new Uri(consulRegisterOptions.Value.ServerUrl);
                    consulClient.Config.Datacenter = consulRegisterOptions.Value.DataCenter;
                    Console.WriteLine("应用退出，开始从consul注销");
                    consulClient.Agent.ServiceDeregister(serviceIdFull).Wait();
                }
            });
        }
    }
}
