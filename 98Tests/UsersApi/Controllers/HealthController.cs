using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using TCSOFT.Consul;
using TCSOFT.WebCore;

namespace UsersApi.Controllers
{
    /// <summary>
    /// @author herowk
    /// 服务健康检查控制器
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class HealthController : TCBaseController
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="configuration">基本配置</param>
        /// <param name="appLifeTime">应用生命周期</param>
        /// <param name="options">Consul注册配置项</param>
        public HealthController(IConfiguration configuration
                                , IApplicationLifetime appLifeTime
                                , IOptionsSnapshot<ConsulRegisterOptions> options) : base(configuration, appLifeTime, options)
        {
        }

        /// <summary>
        /// 健康检查
        /// </summary>
        /// <returns>是否可用</returns>
        [HttpGet]
        public ActionResult<Boolean> Get()
        {
            //是否需要重新注册到Consul
            if (OptionsConsulRegister.Value.ReRegister)
            {
                TCSOFT.Consul.ConsulServiceRegister csRegister = new ConsulServiceRegister();

                //注销原服务
                csRegister.ConsulUnRegister(Configuration);
                //重注册
                csRegister.ConsulRegister(Configuration, AppLifeTime, OptionsConsulRegister);
            }
            return true;
        }
    }
}