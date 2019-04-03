using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace TCSOFT.DBHelper
{
    /// <summary>
    /// 数据库操作集
    /// </summary>
    /// <typeparam name="T">实体类型</typeparam>
    public class DbSet<T> : SimpleClient<T> where T : class, new()
    {
        /// <summary>
        /// 数据操作集实例化
        /// </summary>
        /// <param name="context">Db实例</param>
        public DbSet(SqlSugarClient context) : base(context)
        {

        }

        /// <summary>
        /// 根据ID获取数据
        /// </summary>
        /// <param name="ids">需获取的ID</param>
        /// <returns></returns>
        public List<T> GetByIds(dynamic[] ids)
        {
            return Context.Queryable<T>().In(ids).ToList(); 
        }

        /// <summary>
        /// 批量插入
        /// </summary>
        /// <param name="models">实体集</param>
        /// <returns></returns>
        public int BulkInsert(List<T> models)
        {
            return Context.Insertable<T>(models).ExecuteCommand();
        }

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="models">待删除实体</param>
        /// <returns></returns>
        public int BulkDelete(List<T> models)
        {
            return Context.Deleteable<T>().Where(models).ExecuteCommand();
        }
    }
}
