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
        public void ConsulRegister(IConfiguration configuration
                                , IApplicationLifetime appLifeTime
                                , IOptions<ConsulRegisterOptions> consulRegisterOptions)
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
                        DeregisterCriticalServiceAfter = TimeSpan.FromSeconds(consulRegisterOptions.Value.Checker.DeregisterCriticalServiceAfter),
                        HTTP = $"{consulRegisterOptions.Value.Checker.Protocol}://{consulRegisterOptions.Value.Checker.IP}:{consulRegisterOptions.Value.Checker.Port}/{consulRegisterOptions.Value.Checker.Uri}",
                        Interval = TimeSpan.FromSeconds(consulRegisterOptions.Value.Checker.Interval),
                        Timeout = TimeSpan.FromSeconds(consulRegisterOptions.Value.Checker.Timeout)
                    },
                    Tags = new string[] { consulRegisterOptions.Value.Tag }
                };
                consulClient.Agent.ServiceRegister(asr).Wait();
                serviceIdFull = asr.ID;
            }

            //写入本地配置
            configuration["ConfigCenter:serviceIdFull"] = serviceIdFull;
            configuration["ConfigCenter:serviceUrl"] = consulRegisterOptions.Value.ServerUrl;
            configuration["ConfigCenter:serviceDataCenter"] = consulRegisterOptions.Value.DataCenter;

            //注销Consul 
            appLifeTime.ApplicationStopped.Register(() =>
            {
                ConsulUnRegister(configuration);
            });

            //更新远程配置
            consulRegisterOptions.Value.ReRegister = false;
            ConsulConfigurationExtensions.UpdateConsulConfigAsync(configuration, consulRegisterOptions).Wait();
        }

        /// <summary>
        /// 注销Consul服务
        /// </summary>
        /// <param name="configuration">配置项</param>
        public void ConsulUnRegister(IConfiguration configuration)
        {
            using (var consulClient = new ConsulClient())
            {
                consulClient.Config.Address = new Uri(configuration["ConfigCenter:serviceUrl"]);
                consulClient.Config.Datacenter = configuration["ConfigCenter:serviceDataCenter"];
                consulClient.Agent.ServiceDeregister(configuration["ConfigCenter:serviceIdFull"]).Wait();
            }
        }
    }
}
