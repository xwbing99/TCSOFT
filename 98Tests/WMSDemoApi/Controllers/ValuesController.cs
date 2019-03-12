using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using TCSOFT.Consul;
using TCSOFT.WebCore;

namespace WMSDemoApi.Controllers
{
    /// <summary>
    /// API示例
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class ValuesController : TCBaseController
    {
        private static string userForPut = string.Empty;
        private IDistributedCache _Cache;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="configuration">基本配置</param>
        /// <param name="appLifeTime">应用生命周期</param>
        /// <param name="options">Consul注册配置项</param>
        public ValuesController(IConfiguration configuration
                                , IApplicationLifetime appLifeTime
                                , IOptionsSnapshot<ConsulRegisterOptions> options
                                , IDistributedCache Cache) : base(configuration, appLifeTime, options)
        {
            _Cache = Cache;
        }

        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "demo1", "demo2", System.Text.Encoding.Default.GetString(_Cache.Get("hello")) };
        }

        /// <summary>
        /// 获取值
        /// </summary>
        /// <param name="id">值ID</param>
        /// <returns>值</returns>
        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return userForPut + id.ToString();
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
            userForPut = value;
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
            userForPut = $"id:{id.ToString()} val:{value}";
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            userForPut = $"id:{id.ToString()} val:deleted";
        }
    }
}
