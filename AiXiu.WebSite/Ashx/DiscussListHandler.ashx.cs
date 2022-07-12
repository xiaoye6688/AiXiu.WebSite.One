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
    /// DiscussListHandler 的摘要说明
    /// </summary>
    public class DiscussListHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string videoId = "";
            if (context.Request.QueryString["id"] != null)
            {
                videoId = context.Request.QueryString["id"];
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

            IDiscussManager discussSABLL = new DiscussManager();
            List<Discuss> result = discussSABLL.QueryDiscuss(videoId, pageNumber, pageSize);
            string jsonStr = JsonConvert.SerializeObject(result, new UnixDateTimeConverter());
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