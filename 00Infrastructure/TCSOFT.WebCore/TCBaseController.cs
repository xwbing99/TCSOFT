using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using TCSOFT.Consul;

namespace TCSOFT.WebCore
{
    public class TCBaseController : ControllerBase
    {
        /// <summary>
        /// 应用生命周期
        /// </summary>
        public IApplicationLifetime AppLifeTime { get; }
        /// <summary>
        /// 配置选项
        /// </summary>
        public IOptionsSnapshot<ConsulRegisterOptions> OptionsConsulRegister { get; }

        public TCBaseController(IApplicationLifetime appLifeTime
                                , IOptionsSnapshot<ConsulRegisterOptions> options)
        {
            AppLifeTime = appLifeTime;
            OptionsConsulRegister = options;
        }
    }
}