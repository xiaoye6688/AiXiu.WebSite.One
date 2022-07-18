using AiXiu.BLL;
using AiXiu.IBLL;
using AiXiu.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AiXiu.WebSite.Ashx
{
    /// <summary>
    /// ChatRecordHandler 的摘要说明
    /// </summary>
    public class ChatRecordHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            int selfId = 0;
            if (context.Request.QueryString["selfId"] != null)
            {
                selfId = int.Parse( context.Request.QueryString["selfId"]);
            }
            int otherId = 10;
            if (context.Request.QueryString["otherId"] != null)
            {
                otherId = int.Parse(context.Request.QueryString["otherId"]);
            }
            int pageNumber = 1;
            if (context.Request.QueryString["pageNumber"] != null)
            {
                pageNumber = int.Parse(context.Request.QueryString["pageNumber"]);
            }
            int pageSize = 10;
            if (context.Request.QueryString["pageSize"] != null)
            {
                pageSize = int.Parse(context.Request.QueryString["pageSize"]);
            }
            
            IFriendManager friendManager = new FriendManager();
            List<Message> result = friendManager.GetMessageList(selfId, otherId, pageNumber, pageSize);
            string jsonStr = JsonConvert.SerializeObject(result);
            context.Response.Write(jsonStr);
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}