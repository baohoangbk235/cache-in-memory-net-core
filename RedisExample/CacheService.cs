using StackExchange.Redis;
using Newtonsoft.Json;

namespace RedisExample.Cache
{
    public class CacheService: ICacheService
    {
        private IDatabase _db;

        public CacheService()
        {
            CongfigureRedis();
        }

        public void CongfigureRedis()
        {
            _db = ConnectionHelper.Connection.GetDatabase();
        }

        public T Get<T>(string key)
        {
            var value = _db.StringGet(key);
            if (!string.IsNullOrEmpty(value))
            {
                return JsonConvert.DeserializeObject<T>(value);
            }

            return default;
        }

        public bool Set<T>(string key, T value, DateTimeOffset expirationTime)
        {
            TimeSpan expiryTime = expirationTime.DateTime.Subtract(DateTime.Now);
            return _db.StringSet(key, JsonConvert.SerializeObject(value), expiryTime);
        }

        public object Remove(string key)
        {
            bool isKeyExist = _db.KeyExists(key);
            if (isKeyExist)
            {
                _db.KeyDelete(key);
            }
            return false;
        }
    }
}
