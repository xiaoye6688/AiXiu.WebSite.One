using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace AiXiu.WebSite
{
    public class ChatHub : Hub
    {
        public void sendMessage(int selfId, int otherId, string content, int timestamp)
        {

            Clients.User(otherId.ToString()).consumerMessage(selfId, content, timestamp);
        }
    }
}