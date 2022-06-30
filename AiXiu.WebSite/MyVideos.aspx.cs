using AiXiu.IBLL;
using AiXiu.BLL;
using AiXiu.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AiXiu.WebSite
{
    public partial class MyVideos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                IVideoManager videoManager = new VideoManager();
                TBUsers tBUsers = IdentityManager.ReadUser();
                if (tBUsers != null)
                {
                    rptVideos.DataSource = videoManager.GetVideoListById(tBUsers.Id).Result;
                    rptVideos.DataBind();
                }
                else
                {
                    Response.Redirect("~/Login.aspx");
                }
            }
        }
        public string GetVideoStatus(int status)
        {
            switch (status)
            {
                case 1:
                    return "上传中";
                case 2:
                    return "上传失败";
                case 3:
                    return "上传完成";
                case 4:
                    return "转码中";
                case 5:
                    return "转码失败";
                case 6:
                    return "屏蔽";
                case 7:
                    return "正常";
                case 0:
                default:
                    return "未知";

            }
        }
    }
}