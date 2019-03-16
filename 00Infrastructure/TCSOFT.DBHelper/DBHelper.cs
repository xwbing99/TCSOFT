using SqlSugar;

namespace TCSOFT.DBHelper
{
    /// <summary>
    /// 数据库操作类
    /// </summary>
    public static class DBHelper
    {
        /// <summary>
        /// MySql实例
        /// </summary>
        /// <param name="connectString">连接字符串</param>
        /// <returns></returns>
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

        /// <summary>
        /// SqlServer实例
        /// </summary>
        /// <param name="connectString">连接字符串</param>
        /// <returns></returns>
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
