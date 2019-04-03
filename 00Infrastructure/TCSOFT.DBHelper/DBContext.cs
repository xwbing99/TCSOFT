using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TCSOFT.DBHelper
{
    public class DbContext<T> where T : class, new()
    {
        #region 属性字段
        /// <summary>
        /// 连接字符串
        /// </summary>
        private static string _connectionString;

        /// <summary>
        /// 连接字符串
        /// </summary>
        public static string ConnectionString
        {
            get { return _connectionString; }
            set { _connectionString = value; }
        }

        /// <summary>
        /// 数据库类型
        /// </summary>
        private static DbType _dbType;

        /// <summary>
        /// 数据库类型
        /// </summary>
        public static DbType DbType
        {
            get { return _dbType; }
            set { _dbType = value; }
        }

        /// <summary>
        /// Db实例
        /// </summary>
        public SqlSugarClient Db;
        #endregion

        /// <summary>
        /// 生成数据库操作对象
        /// </summary>
        /// <param name="connectString">数据库字符串</param>
        /// <param name="enmDbType">数据库类型</param>
        public DbContext(string connectString, DbType enmDbType = SqlSugar.DbType.MySql)
        {
            Init(connectString, enmDbType);
        }

        /// <summary>
        /// 功能描述:初始化设置
        /// </summary>
        /// <param name="strConnectionString">连接字符串</param>
        /// <param name="enmDbType">数据库类型</param>
        public void Init(string strConnectionString, DbType enmDbType = SqlSugar.DbType.MySql)
        {
            _connectionString = strConnectionString;
            _dbType = enmDbType;
            Db = new SqlSugarClient(new ConnectionConfig()
            {
                ConnectionString = _connectionString,
                DbType = _dbType,
                IsShardSameThread = true,
                IsAutoCloseConnection = true,
                ConfigureExternalServices = new ConfigureExternalServices()
                {
                    DataInfoCacheService = new RedisCache()
                },
                MoreSettings = new ConnMoreSettings()
                {
                    IsAutoRemoveDataCache = true
                }

            });
            //调试代码 用来打印SQL 
            Db.Aop.OnLogExecuting = (sql, pars) =>
            {
                System.Diagnostics.Debug.WriteLine(sql + "\r\n" +
                    Db.Utilities.SerializeObject(pars.ToDictionary(it => it.ParameterName, it => it.Value)));
            };
        }

        #region "事务相关"
        public void BeginTrans()
        {
            Db.Ado.BeginTran();
        }

        public void CommitTrans()
        {
            Db.Ado.CommitTran();
        }

        public void RollbackTrans()
        {
            Db.Ado.RollbackTran();
        }
        #endregion

        /// <summary>
        /// 数据库操作对象
        /// </summary>
        public DbSet<T> CurrentDb { get { return new DbSet<T>(Db); } }
    }
}
