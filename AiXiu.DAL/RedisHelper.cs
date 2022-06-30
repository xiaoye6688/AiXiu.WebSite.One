using Newtonsoft.Json;
using StackExchange.Redis;
using System.Collections.Generic;
using System.Configuration;

namespace AiXiu.DAL
{
    /// <summary>
    /// Redis访问帮助类
    /// </summary>
    public class RedisHelper
    {
        private static object objLock = new object();
        private static IConnectionMultiplexer connectionMultiplexer;
        private static IDatabase database;

        #region 集合操作

        /// <summary>
        /// 集合添加值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool SetAdd(string key, object value)
        {
            string valueString = JsonConvert.SerializeObject(value);
            return SetAdd(key, valueString);
        }

        /// <summary>
        /// 集合添加值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool SetAdd(string key, string value)
        {
            IDatabase db = GetDatabase();
            if (db.SetContains(key, value))
                return false;
            return db.SetAdd(key, value);
        }

        /// <summary>
        /// 集合移除值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool SetRemove(string key, object value)
        {
            string valueString = JsonConvert.SerializeObject(value);
            return SetRemove(key, valueString);
        }

        /// <summary>
        /// 集合移除值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool SetRemove(string key, string value)
        {
            IDatabase db = GetDatabase();
            if (!db.SetContains(key, value))
                return false;
            return db.SetRemove(key, value);
        }

        /// <summary>
        /// 集合是否存在值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool SetContains(string key, object value)
        {
            string valueString = JsonConvert.SerializeObject(value);
            return SetContains(key, valueString);
        }

        /// <summary>
        /// 集合是否存在值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool SetContains(string key, string value)
        {
            IDatabase db = GetDatabase();
            return db.SetContains(key, value);
        }

        /// <summary>
        /// 集合长度
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static long SetLength(string key)
        {
            IDatabase db = GetDatabase();
            return db.SetLength(key);
        }

        #endregion

        #region 有序集合操作

        /// <summary>
        /// 有序集合添加值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="score"></param>
        /// <returns></returns>
        public static bool SortedSetAdd(string key, object value, double score)
        {
            string valueString = JsonConvert.SerializeObject(value);
            return SortedSetAdd(key, valueString, score);
        }

        /// <summary>
        /// 有序集合添加值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="score"></param>
        /// <returns></returns>
        public static bool SortedSetAdd(string key, string value, double score)
        {
            IDatabase db = GetDatabase();
            return db.SortedSetAdd(key, value, score);
        }

        /// <summary>
        /// 有序集合长度
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static long SortedSetLength(string key)
        {
            IDatabase db = GetDatabase();
            return db.SortedSetLength(key);
        }

        /// <summary>
        /// 获取有序集合索引区间数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="start"></param>
        /// <param name="stop"></param>
        /// <param name="ascending"></param>
        /// <returns></returns>
        public static List<T> SortedSetRangeByRank<T>(string key, long start, long stop, bool ascending = true)
        {
            List<string> stringList = SortedSetRangeByRank(key, start, stop, ascending);
            if (stringList.Count == 0)
                return new List<T>();
            List<T> objectList = new List<T>(stringList.Count);
            foreach (string value in stringList)
            {
                objectList.Add(JsonConvert.DeserializeObject<T>(value));
            }
            return objectList;
        }

        /// <summary>
        /// 获取有序集合索引区间数据
        /// </summary>
        /// <param name="key"></param>
        /// <param name="start"></param>
        /// <param name="stop"></param>
        /// <param name="ascending"></param>
        /// <returns></returns>
        public static List<string> SortedSetRangeByRank(string key, long start, long stop, bool ascending = true)
        {
            IDatabase db = GetDatabase();
            Order order = ascending ? Order.Ascending : Order.Descending;
            RedisValue[] values = db.SortedSetRangeByRank(key, start, stop, order);
            if (values.Length == 0)
                return new List<string>();
            List<string> stringList = new List<string>(values.Length);
            foreach (string value in values)
            {
                if (string.IsNullOrWhiteSpace(value))
                    continue;
                stringList.Add(value);
            }
            return stringList;
        }

        #endregion

        #region 哈希操作

        /// <summary>
        /// 哈希设置键值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="field"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool HashSet(string key, string field, object value)
        {
            string valueString = JsonConvert.SerializeObject(value);
            return HashSet(key, field, value: valueString);
        }

        /// <summary>
        /// 哈希设置键值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="field"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool HashSet(string key, string field, string value)
        {
            IDatabase db = GetDatabase();
            return db.HashSet(key, hashField: field, value: value);
        }

        /// <summary>
        /// 哈希是否存在键
        /// </summary>
        /// <param name="key"></param>
        /// <param name="field"></param>
        /// <returns></returns>
        public static bool HashExists(string key, string field)
        {
            IDatabase db = GetDatabase();
            return db.HashExists(key, field);
        }

        /// <summary>
        /// 哈希获取键的值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="field"></param>
        /// <returns></returns>
        public static T HashGet<T>(string key, string field)
        {
            string valueString = HashGet(key, field);
            if (string.IsNullOrWhiteSpace(valueString))
                return default(T);
            return JsonConvert.DeserializeObject<T>(valueString);
        }

        /// <summary>
        /// 哈希获取键的值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="field"></param>
        /// <returns></returns>
        public static string HashGet(string key, string field)
        {
            IDatabase db = GetDatabase();
            return db.HashGet(key, field);
        }

        /// <summary>
        /// 哈希获取所有键值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public static Dictionary<string, T> HashGetAll<T>(string key)
        {
            Dictionary<string, string> stringDictionary = HashGetAll(key);
            if (stringDictionary.Count == 0)
                return new Dictionary<string, T>();
            Dictionary<string, T> objectDictionary = new Dictionary<string, T>(stringDictionary.Count);
            foreach (KeyValuePair<string, string> pair in stringDictionary)
            {
                objectDictionary.Add(pair.Key, JsonConvert.DeserializeObject<T>(pair.Value));
            }
            return objectDictionary;
        }

        /// <summary>
        /// 哈希获取所有键的值
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static Dictionary<string, string> HashGetAll(string key)
        {
            IDatabase db = GetDatabase();
            HashEntry[] hashEntries = db.HashGetAll(key);
            if (hashEntries.Length == 0)
                return new Dictionary<string, string>();
            Dictionary<string, string> stringDictionary = new Dictionary<string, string>(hashEntries.Length);
            foreach (HashEntry entry in hashEntries)
            {
                if (string.IsNullOrWhiteSpace(entry.Name) || string.IsNullOrWhiteSpace(entry.Value))
                    continue;
                stringDictionary.Add(entry.Name, entry.Value);
            }
            return stringDictionary;
        }

        #endregion

        #region 数据库连接

        /// <summary>
        /// 获取连接复用器
        /// </summary>
        /// <returns></returns>
        private static IConnectionMultiplexer GetConnectionMultiplexer()
        {
            if (connectionMultiplexer == null)
            {
                lock (objLock)
                {
                    if (connectionMultiplexer == null)
                    {
                        string connectionString = ConfigurationManager.ConnectionStrings["AiXiuRedis"].ConnectionString;
                        connectionMultiplexer = ConnectionMultiplexer.Connect(connectionString);
                    }
                }
            }
            return connectionMultiplexer;
        }

        /// <summary>
        /// 获取数据库
        /// </summary>
        /// <returns></returns>
        private static IDatabase GetDatabase()
        {
            if (database == null)
            {
                lock (objLock)
                {
                    if (database == null)
                    {
                        database = GetConnectionMultiplexer().GetDatabase();
                    }
                }
            }
            return database;
        }

        #endregion
    }
}