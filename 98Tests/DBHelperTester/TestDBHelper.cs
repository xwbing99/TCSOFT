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

            //������ӡSql�������ʽ    
            db.Aop.OnLogExecuting = (sql, pars) =>
            {
                Console.WriteLine(sql + "\r\n" +
                db.Utilities.SerializeObject(pars.ToDictionary(it => it.ParameterName, it => it.Value)));
                Console.WriteLine();
            };

            //db.DbFirst.CreateClassFile("d:/");

            /*��ѯ*/
            var list = db.Queryable<Student>().ToList();//��ѯ����
            var getById = db.Queryable<Student>().InSingle(1);//����������ѯ
            var getByWhere = db.Queryable<Student>().Where(it => it.Id == 1).ToList();//����������ѯ
            var total = 0;
            var getPage = db.Queryable<Student>().Where(it => it.Id == 1).ToPageList(1, 2, ref total);//���ݷ�ҳ��ѯ

            var data1 = new Student() { Name = "jimmy" };
            db.Insertable<Student>(data1);

            /*����*/
            var data = new Student() { Name = "jack" };
            db.Insertable(data).ExecuteCommand();

            /*����*/
            var data2 = new Student() { Id = 1, Name = "herowk" };
            db.Updateable(data2).ExecuteCommand();


            /*ɾ��*/
            db.Deleteable<Student>(1).ExecuteCommand();
        }
    }
}
