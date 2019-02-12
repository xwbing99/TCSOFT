using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using TCSOFT.Consul;

namespace UsersApi.Controllers
{
    /// <summary>
    /// @author herowk
    /// 服务健康检查控制器
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class HealthController : ControllerBase
    {
        /// <summary>
        /// 应用生命周期
        /// </summary>
        public IApplicationLifetime AppLifeTime { get; }
        /// <summary>
        /// 配置选项
        /// </summary>
        public IOptionsSnapshot<ConsulRegisterOptions> OptionsConsulRegister { get; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="appLifeTime">应用生命周期</param>
        /// <param name="options">配置项</param>
        public HealthController(IApplicationLifetime appLifeTime
                                , IOptionsSnapshot<ConsulRegisterOptions> options)
        {
            AppLifeTime = appLifeTime;
            OptionsConsulRegister = options;
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