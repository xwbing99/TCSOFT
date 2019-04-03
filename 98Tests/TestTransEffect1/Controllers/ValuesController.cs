using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using TCSOFT.DBHelper;

namespace TestTransEffect1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        public IConfiguration Config;
        static int Count = 0;

        public ValuesController(IConfiguration configuration)
        {
            Config = configuration;
        }
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            TCSOFT.DBHelper.DbContext<Student> dbContextStudent = new TCSOFT.DBHelper.DbContext<Student>($"server={Config["DBInfo:serverip"]};uid={Config["DBInfo:uid"]};pwd={Config["DBInfo:pwd"]};database={Config["DBInfo:database"]}");
            TCSOFT.DBHelper.DbContext<Teacher> dbContextTeacher = new TCSOFT.DBHelper.DbContext<Teacher>($"server={Config["DBInfo:serverip"]};uid={Config["DBInfo:uid"]};pwd={Config["DBInfo:pwd"]};database={Config["DBInfo:database"]}");


            string appString = string.Empty;
            try
            {
                dbContextStudent.BeginTrans();

                dbContextStudent.CurrentDb.Insert(new Student() { Name = $"StudentTest{Count}" });

                dbContextTeacher.CurrentDb.Insert(new Teacher() { Name = $"TeacherTest{Count}" });

                Count++;

                if (Count % 2 == 0)
                {
                    System.Threading.Thread.Sleep(5000);
                    appString = DateTime.Now.ToString("yyyyMMddHHmmssfff");
                    dbContextStudent.CommitTrans();
                }
                else
                {
                    throw new Exception("error");
                }

            }
            catch (Exception ex)
            {
                dbContextStudent.RollbackTrans();
                appString = DateTime.Now.ToString("yyyyMMddHHmmssfff");
                return new string[] { "errvalue1", "errvalue2", appString };
            }

            return new string[] { "value1", "value2", appString };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
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
