using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using AiXiu.BLL;
using AiXiu.Model;
using AiXiu.IBLL;

namespace AiXiu.WebSite
{
    public class ChatHub : Hub
    {
        public void sendMessage(int selfId, int otherId, string content, int timestamp)
        {

            Clients.User(otherId.ToString()).consumerMessage(selfId, content, timestamp);
            //存储信息
            IFriendManager friendManager = new FriendManager();
            bool result = friendManager.SendMessage(selfId, otherId, content);

        }
    }
}