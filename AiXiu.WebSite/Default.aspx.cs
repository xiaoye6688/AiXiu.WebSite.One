using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AiXiu.BLL;
using AiXiu.IBLL;

namespace AiXiu.WebSite
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            IVideoManager videoManager = new VideoManager();
            rptVideos.DataSource = videoManager.GetVideoList().Result;
            rptVideos.DataBind();
        }
    }
}