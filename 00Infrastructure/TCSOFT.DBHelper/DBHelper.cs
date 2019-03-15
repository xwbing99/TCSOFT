using SqlSugar;

namespace TCSOFT.DBHelper
{
    public static class DBHelper
    {
        public static SqlSugarClient MySqlInstance(string connectString)
        {
            return new SqlSugarClient(new ConnectionConfig()
            {
                ConnectionString = connectString,
                DbType = DbType.MySql,//设置数据库类型
                IsAutoCloseConnection = true,//自动释放事务，如果存在事务，在事务结束后释放
                InitKeyType = InitKeyType.SystemTable //从实体特性中读取主键自增列信息
            });
        }

        public static SqlSugarClient SqlServerInstance(string connectString)
        {
            return new SqlSugarClient(new ConnectionConfig()
            {
                ConnectionString = connectString,
                DbType = DbType.SqlServer,//设置数据库类型
                IsAutoCloseConnection = true,//自动释放事务，如果存在事务，在事务结束后释放
                InitKeyType = InitKeyType.SystemTable //从实体特性中读取主键自增列信息
            });
        }
    }
}
