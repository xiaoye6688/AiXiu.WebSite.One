using AiXiu.IBLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AiXiu.DAL;

namespace AiXiu.BLL
{
    public class LikeManager : ILikeManager
    {
        private const string keyPrefix = "VideoLike";
        public bool AddLike(string videoId, int userId)
        {
            string key = $"{keyPrefix}_{videoId}";
            return RedisHelper.SetAdd(key, userId);


        }

        public long Count(string videoId)
        {
            string key = $"{keyPrefix}_{videoId}";
            return RedisHelper.SetLength(key);
        }

        public bool IsLike(string videoId, int userId)
        {
            string key = $"{keyPrefix}_{videoId}";
            return RedisHelper.SetContains(key, userId);
        }

        public bool RemoveLike(string videoId, int userId)
        {
            string key = $"{keyPrefix}_{videoId}";
            return RedisHelper.SetRemove(key, userId);
        }
    }
}
