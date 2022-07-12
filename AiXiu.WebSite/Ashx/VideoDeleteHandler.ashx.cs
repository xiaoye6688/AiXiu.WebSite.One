using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AiXiu.BLL;
using AiXiu.IBLL;

namespace AiXiu.WebSite.Ashx
{
    /// <summary>
    /// VideoDeleteHandler 的摘要说明
    /// </summary>
    public class VideoDeleteHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            //读取参数
            string videoId = context.Request.QueryString["videoId"];
            //删除视频 
            IVideoManager videoManager = new VideoManager();
            videoManager.DeleteVideo(videoId);
            //输出结果
            context.Response.ContentType = "text/plain";
            context.Response.Write("OK");
            context.Response.End();
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