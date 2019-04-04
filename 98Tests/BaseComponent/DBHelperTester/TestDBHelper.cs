using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DBHelperTester
{
    [TestClass]
    public class TestDBHelper
    {

        [TestMethod]
        public void TestDBBulkInsert()
        {
            IConfiguration configuration = TCSOFT.ConfigManager.JsonConfigurationExtensions.AddConfigFile(new ConfigurationBuilder(), "appsettings.json").Build();
            TCSOFT.DBHelper.DbContext<Student> dbContext = new TCSOFT.DBHelper.DbContext<Student>($"server={configuration["DBInfo:serverip"]};uid={configuration["DBInfo:uid"]};pwd={configuration["DBInfo:pwd"]};database={configuration["DBInfo:database"]}");

            List<Student> listStudents = new List<Student>();
            for (int i = 0; i < 100000; i++)
            {
                Student stu = new Student() { Name = $"herowk{i}" };
                listStudents.Add(stu);
            }

            //批量插入
            dbContext.CurrentDb.BulkInsert(listStudents);
        }

        [TestMethod]
        public void TestDBBulkDelete()
        {

            IConfiguration configuration = TCSOFT.ConfigManager.JsonConfigurationExtensions.AddConfigFile(new ConfigurationBuilder(), "appsettings.json").Build();
            TCSOFT.DBHelper.DbContext<Student> dbContext = new TCSOFT.DBHelper.DbContext<Student>($"server={configuration["DBInfo:serverip"]};uid={configuration["DBInfo:uid"]};pwd={configuration["DBInfo:pwd"]};database={configuration["DBInfo:database"]}");

            List<Student> lstStudent = dbContext.CurrentDb.GetList();

            dbContext.CurrentDb.BulkDelete(lstStudent);
        }

        [TestMethod]
        public void TestDBTrans()
        {

            IConfiguration configuration = TCSOFT.ConfigManager.JsonConfigurationExtensions.AddConfigFile(new ConfigurationBuilder(), "appsettings.json").Build();
            TCSOFT.DBHelper.DbContext<Student> dbContextStudent = new TCSOFT.DBHelper.DbContext<Student>($"server={configuration["DBInfo:serverip"]};uid={configuration["DBInfo:uid"]};pwd={configuration["DBInfo:pwd"]};database={configuration["DBInfo:database"]}");
            TCSOFT.DBHelper.DbContext<Teacher> dbContextTeacher = new TCSOFT.DBHelper.DbContext<Teacher>($"server={configuration["DBInfo:serverip"]};uid={configuration["DBInfo:uid"]};pwd={configuration["DBInfo:pwd"]};database={configuration["DBInfo:database"]}");
            try
            {
                dbContextStudent.BeginTrans();

                dbContextStudent.CurrentDb.Insert(new Student() { Name = "StudentTest" });

                dbContextTeacher.CurrentDb.Insert(new Teacher() { Name = "TeacherTest" });//输出1

                dbContextStudent.CommitTrans();
                //throw new Exception("error");
            }
            catch (Exception ex)
            {
                dbContextStudent.RollbackTrans();
            }
        }

        [TestMethod]
        public void TestDBInsert()
        {

            IConfiguration configuration = TCSOFT.ConfigManager.JsonConfigurationExtensions.AddConfigFile(new ConfigurationBuilder(), "appsettings.json").Build();
            TCSOFT.DBHelper.DbContext<StudentEx> dbContext = new TCSOFT.DBHelper.DbContext<StudentEx>($"server={configuration["DBInfo:serverip"]};uid={configuration["DBInfo:uid"]};pwd={configuration["DBInfo:pwd"]};database={configuration["DBInfo:database"]}");

            StudentEx stu = new StudentEx() { Name = $"herowk" };
            dbContext.CurrentDb.Insert(stu);
        }
    }
}
