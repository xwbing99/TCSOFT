using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
        /// 健康检查
        /// </summary>
        /// <returns>是否可用</returns>
        [HttpGet]
        public ActionResult<Boolean> Get()
        {
            return true;
        }
    }
}