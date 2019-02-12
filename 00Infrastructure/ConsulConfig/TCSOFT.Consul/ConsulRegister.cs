using System;
using Consul;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace TCSOFT.Consul
{
    public class ConsulRegister
    {
        /// <summary>
        /// 注册到Consul
        /// </summary>
        /// <param name="appLifeTime">生命周期</param>
        /// <param name="configuration">配置对象</param>
        public void ConsulApp(IApplicationLifetime appLifeTime, IConfiguration configuration)
        {
            //注册Consul 
            using (var consulClient = new ConsulClient())
            {
                consulClient.Config.Address = new Uri(configuration[$"{ configuration["ConfigCenter:Tag"] }:{ configuration["ConfigCenter:path"] }:ServerUrl"]);
                consulClient.Config.Datacenter = configuration[$"{ configuration["ConfigCenter:Tag"] }:{ configuration["ConfigCenter:path"] }:DataCenter"];

                AgentServiceRegistration asr = new AgentServiceRegistration
                {
                    Address = configuration[$"{ configuration["ConfigCenter:Tag"] }:{ configuration["ConfigCenter:path"] }:Checker:IP"],
                    Port = Convert.ToInt32(configuration[$"{ configuration["ConfigCenter:Tag"] }:{ configuration["ConfigCenter:path"] }:Checker:Port"]),
                    ID = configuration[$"{ configuration["ConfigCenter:Tag"] }:{ configuration["ConfigCenter:path"] }:ServiceId"] + Guid.NewGuid(),
                    Name = configuration[$"{ configuration["ConfigCenter:Tag"] }:{ configuration["ConfigCenter:path"] }:ServiceName"],
                    Check = new AgentServiceCheck
                    {
                        DeregisterCriticalServiceAfter = TimeSpan.FromSeconds(5),
                        HTTP = $"{configuration[$"{ configuration["ConfigCenter:Tag"] }:{ configuration["ConfigCenter:path"] }:Checker:Protocol"]}://{configuration[$"{ configuration["ConfigCenter:Tag"] }:{ configuration["ConfigCenter:path"] }:Checker:IP"]}:{configuration[$"{ configuration["ConfigCenter:Tag"] }:{ configuration["ConfigCenter:path"] }:Checker:Port"]}/{configuration[$"{ configuration["ConfigCenter:Tag"] }:{ configuration["ConfigCenter:path"] }:Checker:Uri"]}",
                        Interval = TimeSpan.FromSeconds(10),
                        Timeout = TimeSpan.FromSeconds(5)
                    },
                    Tags = new string[] { configuration[$"{ configuration["ConfigCenter:Tag"] }:{ configuration["ConfigCenter:path"] }:Tag"] }
                };
                consulClient.Agent.ServiceRegister(asr).Wait();
            }

            //注销Consul 
            appLifeTime.ApplicationStopped.Register(() =>
            {
                using (var consulClient = new ConsulClient())
                {
                    consulClient.Config.Address = new Uri(configuration[$"{ configuration["ConfigCenter:Tag"] }:{ configuration["ConfigCenter:path"] }:ServerUrl"]);
                    consulClient.Config.Datacenter = configuration[$"{ configuration["ConfigCenter:Tag"] }:{ configuration["ConfigCenter:path"] }:DataCenter"];
                    Console.WriteLine("应用退出，开始从consul注销");
                    consulClient.Agent.ServiceDeregister(configuration[$"{ configuration["ConfigCenter:Tag"] }:{ configuration["ConfigCenter:path"] }:ServiceId"]).Wait();
                }
            });
        }
    }
}
