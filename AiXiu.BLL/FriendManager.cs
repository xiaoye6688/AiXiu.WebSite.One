using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AiXiu.IBLL;
using AiXiu.DAL;
using AiXiu.Model;

namespace AiXiu.BLL
{
    public class FriendManager : IFriendManager
    {
        private const string keyPrefix = "Friend";
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
                    Content = "hi~我是"+ otherUser.NickName,
                    UserId = otherId
                }
            };
            bool addSelfFriendResult=RedisHelper.HashSet(selfKey, otherId.ToString(), selfAddFriend);
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

        public bool IsFriend(int selfId, int otherId)
        {
            string selfKey = $"{keyPrefix}_{selfId}";
            return RedisHelper.HashExists(selfKey, otherId.ToString());
        }
    }
}
