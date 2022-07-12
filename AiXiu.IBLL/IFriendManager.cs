using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AiXiu.IBLL
{
    public interface IFriendManager
    {
        //是否好友，selfId为自己的id，other为对方id
        bool IsFriend(int selfId, int otherId);
        //添加好友，selfId为自己的id，other为对方id
        bool AddFriend(int selfId, int otherId);
    }
}
