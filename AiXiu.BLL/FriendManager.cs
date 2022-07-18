using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AiXiu.IBLL;
using AiXiu.DAL;
using AiXiu.Model;
using AiXiu.Common;

namespace AiXiu.BLL
{
    public class FriendManager : IFriendManager
    {
        private const string keyPrefix = "Friend";
        private const string messageKeyPrefix = "Message";
        public bool AddFriend(int selfId, int otherId)
        {
            string selfKey = $"{keyPrefix}_{selfId}";
            string otherKey = $"{keyPrefix}_{otherId}";
            IUserManager userManager = new UserManager();
            TBUsers selfUser = userManager.GetAuthorById(selfId).ReadResult();
            TBUsers otherUser = userManager.GetAuthorById(otherId).ReadResult();
            string chatId = Guid.NewGuid().ToString();
            DateTime nowTime = DateTime.Now;
            //我添加好友
            Friend selfAddFriend = new Friend()
            {
                Avatar = otherUser.Avatar,
                ChatId = chatId,
                NickName = otherUser.NickName,
                LastMessage = new Message()
                {
                    AddTime = nowTime,
                    Content = "hi~我是" + otherUser.NickName,
                    UserId = otherId
                }
            };
            bool addSelfFriendResult = RedisHelper.HashSet(selfKey, otherId.ToString(), selfAddFriend);
            //好友添加我
            Friend otherAddFriend = new Friend()
            {
                Avatar = selfUser.Avatar,
                ChatId = chatId,
                NickName = selfUser.NickName,
                LastMessage = new Message()
                {
                    AddTime = nowTime,
                    Content = "hi~我是" + selfUser.NickName,
                    UserId = selfId
                }
            };
            bool addOtherFriendResult = RedisHelper.HashSet(otherKey, selfId.ToString(), otherAddFriend);
            return addSelfFriendResult && addOtherFriendResult;
        }

        public Dictionary<int, Friend> GetFriendList(int userId)
        {
            string selfKey = $"{keyPrefix}_{userId}";
            Dictionary<string, Friend> list = RedisHelper.HashGetAll<Friend>(selfKey);
            return list.ToDictionary(x => int.Parse(x.Key), x => x.Value);
        }

        public List<Message> GetMessageList(int selfId, int otherId, int pageNumber, int pageSize=10)
        {
            string selfKey = $"{keyPrefix}_{selfId}";
            Friend selfFriend = RedisHelper.HashGet<Friend>(selfKey, otherId.ToString());

            string chatId = selfFriend.ChatId;

            string messageKey = $"{messageKeyPrefix}_{chatId}";
            int start = (pageNumber - 1) * pageSize;
            int stop = start * pageSize-1;
            return RedisHelper.SortedSetRangeByRank<Message>(messageKey, start,stop,false);
        }

        public bool IsFriend(int selfId, int otherId)
        {
            string selfKey = $"{keyPrefix}_{selfId}";
            return RedisHelper.HashExists(selfKey, otherId.ToString());
        }

        public bool SendMessage(int selfId, int otherId, string content)
        {
            string selfKey = $"{keyPrefix}_{selfId}";
            Friend selfFriend = RedisHelper.HashGet<Friend>(selfKey, otherId.ToString());

            string chatId=selfFriend.ChatId;
            string messageKey = $"{messageKeyPrefix}_{chatId}";
            Message message = new Message()
            {
                AddTime = DateTime.Now,
                Content = content,
                UserId = selfId
            };
            //1、存信息
            long time = TimeHelper.ConvertDateTimeByUnix(DateTime.Now);
            bool addMsg=RedisHelper.SortedSetAdd(messageKey, message, time);
            //2、更新好友信息里的最后一次信息
           
            selfFriend.LastMessage = message;
            bool updateSelfFriendRes = RedisHelper.HashSet(selfKey, otherId.ToString(), selfFriend);
            string otherKey = $"{keyPrefix}_{otherId}";
            Friend otherFriend = RedisHelper.HashGet<Friend>(otherKey, selfId.ToString());
            otherFriend.LastMessage = message;
            bool updateOtherFriendRes = RedisHelper.HashSet(otherKey, selfId.ToString(), otherFriend);

            return addMsg && updateSelfFriendRes&& updateOtherFriendRes;
        }
    }
}
