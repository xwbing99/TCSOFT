using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using TCSOFT.Consul;

namespace TCSOFT.WebCore
{
    public class TCBaseController : ControllerBase
    {
        /// <summary>
        /// 基础配置项
        /// </summary>
        public IConfiguration Configuration { get; }
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
        /// <param name="configuration">基本配置</param>
        /// <param name="appLifeTime">应用生命周期</param>
        /// <param name="options">Consul注册配置项</param>
        public TCBaseController(IConfiguration configuration
                                , IApplicationLifetime appLifeTime
                                , IOptionsSnapshot<ConsulRegisterOptions> options)
        {
            Configuration = configuration;
            AppLifeTime = appLifeTime;
            OptionsConsulRegister = options;
        }
    }
}