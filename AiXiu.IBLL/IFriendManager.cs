using AiXiu.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AiXiu.IBLL
{
    public interface IFriendManager
    {
        /// <summary>
        /// 是否好友
        /// </summary>
        /// <param name="selfId">自己的id</param>
        /// <param name="otherId">对方id</param>
        /// <returns></returns>
        bool IsFriend(int selfId, int otherId);
        /// <summary>
        /// 添加好友
        /// </summary>
        /// <param name="selfId">自己的id</param>
        /// <param name="otherId">对方id</param>
        /// <returns></returns>
        bool AddFriend(int selfId, int otherId);
        Dictionary<int, Friend> GetFriendList(int userId);
        bool SendMessage(int selfId, int otherId, string context);
        List<Message> GetMessageList(int selfId, int otherId, int pageNumber, int pageSize);
    }
}
