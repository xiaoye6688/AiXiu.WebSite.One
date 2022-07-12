using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AiXiu.Model;
using AiXiu.BLL;
using AiXiu.IBLL;
using AiXiu.Common;

namespace AiXiu.WebSite.Ashx
{
    /// <summary>
    /// DiscussPublishHandler 的摘要说明
    /// </summary>
    public class DiscussPublishHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            IDiscussManager discussManager = new DiscussManager();
            string videoId = "";
            if (context.Request.QueryString["id"] != null)
            {
                videoId = context.Request.QueryString["id"];
            }
            Discuss discuss = new Discuss();
            if (context.Request.Form["Content"] != null)
            {
                discuss.Content = context.Request.Form["Content"];
            }
            if (context.Request.Form["NickName"] != null)
            {
                discuss.NickName = context.Request.Form["NickName"];
            }
            if (context.Request.Form["Avatar"] != null)
            {
                discuss.Avatar = context.Request.Form["Avatar"];
            }
            if (context.Request.Form["AddTime"] != null)
            {
                discuss.AddTime = TimeHelper.GetTimeByUnix(long.Parse(context.Request.Form["AddTime"]));
            }
           bool resule=  discussManager.Add(videoId, discuss);
            context.Response.Write(resule.ToString());
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