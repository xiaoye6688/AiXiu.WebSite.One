using AiXiu.BLL;
using AiXiu.IBLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AiXiu.WebSite.Ashx
{
    /// <summary>
    /// VideoLikeHandler 的摘要说明
    /// </summary>
    public class VideoLikeHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string videoId = "";
            if (context.Request.QueryString["videoId"] != null)
            {
                videoId = context.Request.QueryString["videoId"];
            }
            int userId = 0;
            if (context.Request.QueryString["videoId"] != null)
            {
                userId = int.Parse(context.Request.QueryString["userId"]);
            }
            string action = "";
            if (context.Request.QueryString["videoId"] != null)
            {
                action = context.Request.QueryString["action"];
            }

            if (!string.IsNullOrWhiteSpace(videoId) && !string.IsNullOrWhiteSpace(action) && userId > 0)
            {
                ILikeManager likeManager = new LikeManager();
                if (action == "add")
                {
                    likeManager.AddLike(videoId, userId);
                }
                else
                {
                    likeManager.RemoveLike(videoId, userId);
                }

            }
            else
            {
                context.Response.Write("error");
            }
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