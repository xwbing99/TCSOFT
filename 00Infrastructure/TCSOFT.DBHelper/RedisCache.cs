using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;
using TCSOFT.RedisCacheHelper;

namespace TCSOFT.DBHelper
{
    public class RedisCache : ICacheService
    {
        RedisStringService service = null;
        public RedisCache()
        {
            service = RedisHelper.StringService; ;
        }

        public void Add<V>(string key, V value)
        {
            service.StringSet(key, value);
        }

        public void Add<V>(string key, V value, int cacheDurationInSeconds)
        {
            service.StringSet(key, value, new TimeSpan(cacheDurationInSeconds));
        }

        public bool ContainsKey<V>(string key)
        {
            return service.KeyExists(key);
        }

        public V Get<V>(string key)
        {
            return service.StringGet<V>(key);
        }

        public IEnumerable<string> GetAllKey<V>()
        {
            return null;//service.();
        }

        public V GetOrCreate<V>(string cacheKey, Func<V> create, int cacheDurationInSeconds = int.MaxValue)
        {
            if (this.ContainsKey<V>(cacheKey))
            {
                return this.Get<V>(cacheKey);
            }
            else
            {
                var result = create();
                this.Add(cacheKey, result, cacheDurationInSeconds);
                return result;
            }
        }

        public void Remove<V>(string key)
        {
            service.KeyDelete(key);
        }
    }
}
