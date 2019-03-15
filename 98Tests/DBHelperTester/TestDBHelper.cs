using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace DBHelperTester
{
    [TestClass]
    public class TestDBHelper
    {
        [TestMethod]
        public void TestDBOperation()
        {
            var db = TCSOFT.DBHelper.DBHelper.MySqlInstance("server=localhost;uid=root;pwd=123456;database=testdb");

            //用来打印Sql方便你调式    
            db.Aop.OnLogExecuting = (sql, pars) =>
            {
                Console.WriteLine(sql + "\r\n" +
                db.Utilities.SerializeObject(pars.ToDictionary(it => it.ParameterName, it => it.Value)));
                Console.WriteLine();
            };

            //db.DbFirst.CreateClassFile("d:/");

            /*查询*/
            var list = db.Queryable<Student>().ToList();//查询所有
            var getById = db.Queryable<Student>().InSingle(1);//根据主键查询
            var getByWhere = db.Queryable<Student>().Where(it => it.Id == 1).ToList();//根据条件查询
            var total = 0;
            var getPage = db.Queryable<Student>().Where(it => it.Id == 1).ToPageList(1, 2, ref total);//根据分页查询

            var data1 = new Student() { Name = "jimmy" };
            db.Insertable<Student>(data1);

            /*插入*/
            var data = new Student() { Name = "jack" };
            db.Insertable(data).ExecuteCommand();

            /*更新*/
            var data2 = new Student() { Id = 1, Name = "herowk" };
            db.Updateable(data2).ExecuteCommand();


            /*删除*/
            db.Deleteable<Student>(1).ExecuteCommand();
        }
    }
}
