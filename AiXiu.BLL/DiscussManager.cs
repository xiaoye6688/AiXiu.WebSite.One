using AiXiu.IBLL;
using AiXiu.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AiXiu.DAL;
using AiXiu.Common;

namespace AiXiu.BLL
{
    public class DiscussManager : IDiscussManager
    {
        private const string keyPrefix = "VideoDiscuss2022";
        public bool Add(string videoId, Discuss discuss)
        {
            string key = $"{keyPrefix}_{videoId}";
            double time = TimeHelper.ConvertDateTimeByUnix(discuss.AddTime);
            return RedisHelper.SortedSetAdd(key, discuss, time);

        }

        public long Count(string videoId)
        {
            string key = $"{keyPrefix}_{videoId}";
            return RedisHelper.SortedSetLength(key);
        }

        public List<Discuss> QueryDiscuss(string videoId, int pageNumber, int pageSize=10)
        {
            string key = $"{keyPrefix}_{videoId}";
            int start = (pageNumber - 1) * pageSize;
            int stop = start + pageSize - 1;
            return RedisHelper.SortedSetRangeByRank<Discuss>(key, start, stop, false);
        }
    }
}
