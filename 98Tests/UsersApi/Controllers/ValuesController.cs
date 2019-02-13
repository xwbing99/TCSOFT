using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using TCSOFT.Consul;
using TCSOFT.WebCore;

namespace UsersApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : TCBaseController
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="configuration">基本配置</param>
        /// <param name="appLifeTime">应用生命周期</param>
        /// <param name="options">Consul注册配置项</param>
        public ValuesController(IConfiguration configuration
                                , IApplicationLifetime appLifeTime
                                , IOptionsSnapshot<ConsulRegisterOptions> options) : base(configuration, appLifeTime, options)
        {
        }

        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "user1", "user2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "user";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
