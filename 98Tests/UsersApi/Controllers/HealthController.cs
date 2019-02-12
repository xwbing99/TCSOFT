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

        public HealthController(IApplicationLifetime appLifeTime
                                , IOptionsSnapshot<ConsulRegisterOptions> options) : base(appLifeTime, options)
        {
            //AppLifeTime = appLifeTime;
            //OptionsConsulRegister = options;
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
                csRegister.ConsulApp(AppLifeTime, OptionsConsulRegister);
            }
            return true;
        }
    }
}