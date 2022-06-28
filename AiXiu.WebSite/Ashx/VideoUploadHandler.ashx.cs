using AiXiu.BLL;
using AiXiu.IBLL;
using AiXiu.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AiXiu.WebSite.Ashx
{
    /// <summary>
    /// VideoUploadHandler 的摘要说明
    /// </summary>
    public class VideoUploadHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            //触发ajax调用后台，通过传入的标题 文件名 调用我们之前已经封装好的关于视频点播接口的工具类获取视频上传地址和凭证信息以及视频ID
            //数据库通过接口返回的信息向数据库加入该视频对应基本信息
            //前端通过ajax调用接口返回的基本信息即视频id 上传地址   上传验证凭证 调用视频点播接口客户端SDK实现视频的上传
            IVideoManager videoManager = new VideoManager();
            string filename = "";
            string headline = "";
            string location = "";
            int userId = 0;
            if (!string.IsNullOrWhiteSpace(context.Request.QueryString["filename"]))
            {
                filename = HttpUtility.UrlDecode(context.Request.QueryString["filename"]);
            }
            if (!string.IsNullOrWhiteSpace(context.Request.QueryString["headline"]))
            {
                headline = HttpUtility.UrlDecode(context.Request.QueryString["headline"]);
            }
            if (!string.IsNullOrWhiteSpace(context.Request.QueryString["location"]))
            {
                location = HttpUtility.UrlDecode(context.Request.QueryString["location"]);
            }
            TBUsers tBUsers = IdentityManager.ReadUser();
            if (tBUsers != null)
            {
                userId = tBUsers.Id;
            }
            OperResult<CreateUploadVideoResult> operResult = videoManager.GetUploadVideoResult(filename, headline, location, userId);
            string resultJson = "";
            if (operResult != null)
            {
                resultJson = JsonConvert.SerializeObject(operResult.ReadResult());
            }
            context.Response.Write(resultJson);
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