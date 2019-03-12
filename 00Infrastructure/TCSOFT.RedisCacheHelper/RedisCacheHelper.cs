using Microsoft.Extensions.Caching.Redis;
using StackExchange.Redis;
using System;
using System.Text;
using Newtonsoft.Json;

namespace TCSOFT.RedisCacheHelper
{
    public class RedisCacheHelper
    {
        #region "属性定义区"
        /// <summary>
        /// 数据库
        /// </summary>
        protected IDatabase _cache;
        /// <summary>
        /// 连接管理器
        /// </summary>
        private ConnectionMultiplexer _connection;
        /// <summary>
        /// 实例
        /// </summary>
        private readonly string _instance;
        #endregion "属性定义区"

        #region "构造/析构函数"
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="options">配置项</param>
        /// <param name="database">数据库编号</param>
        public RedisCacheHelper(RedisCacheOptions options, int database = 0)
        {
            _connection = ConnectionMultiplexer.Connect(options.Configuration);
            _cache = _connection.GetDatabase(database);
            _instance = options.InstanceName;
        }

        /// <summary>
        /// 析构函数
        /// </summary>
        public void Dispose()
        {
            if (_connection != null)
                _connection.Dispose();
            GC.SuppressFinalize(this);
        }
        #endregion "构造/析构函数"

        /// <summary>
        /// key转换
        /// </summary>
        /// <param name="key">key值</param>
        /// <returns></returns>
        public string GetKeyForRedis(string key)
        {
            return _instance + key;
        }

        /// <summary>
        /// 验证缓存项是否存在
        /// </summary>
        /// <param name="key">缓存Key</param>
        /// <returns></returns>
        public bool Exists(string key)
        {
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }
            return _cache.KeyExists(GetKeyForRedis(key));
        }

        /// <summary>
        /// 添加缓存
        /// </summary>
        /// <param name="key">缓存Key</param>
        /// <param name="value">缓存Value</param>
        /// <returns></returns>
        public bool Add(string key, object value)
        {
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            return _cache.StringSet(GetKeyForRedis(key)
                                    , Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(value)));
        }

        /// <summary>
        /// 添加缓存
        /// </summary>
        /// <param name="key">缓存Key</param>
        /// <param name="value">缓存Value</param>
        /// <param name="expiresSliding">滑动过期时长（如果在过期时间内有操作，则以当前时间点延长过期时间,Redis中无效）</param>
        /// <param name="expiressAbsoulte">绝对过期时长</param>
        /// <returns></returns>
        public bool Add(string key, object value, TimeSpan expiresSliding, TimeSpan expiressAbsoulte)
        {
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }
            return _cache.StringSet(GetKeyForRedis(key)
                                    , Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(value)), expiressAbsoulte);
        }

        /// <summary>
        /// 添加缓存
        /// </summary>
        /// <param name="key">缓存Key</param>
        /// <param name="value">缓存Value</param>
        /// <param name="expiresIn">缓存时长</param>
        /// <param name="isSliding">是否滑动过期（如果在过期时间内有操作，则以当前时间点延长过期时间,Redis中无效）</param>
        /// <returns></returns>
        public bool Add(string key, object value, TimeSpan expiresIn, bool isSliding = false)
        {
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            return _cache.StringSet(GetKeyForRedis(key)
                                    , Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(value)), expiresIn);
        }
    }
}
