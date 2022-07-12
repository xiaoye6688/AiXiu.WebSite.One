using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AiXiu.BLL;
using AiXiu.Model;

namespace AiXiu.WebSite
{
    public partial class VideoDiscuss : System.Web.UI.Page
    {
        public string VideoId;
        public string NickName;
        public string Avatar;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["id"]!=null)
                {
                    VideoId = Request.QueryString["id"];
                }
                TBUsers tBUsers = IdentityManager.ReadUser();
                if (tBUsers != null)
                {
                    NickName = tBUsers.NickName;
                    Avatar = tBUsers.Avatar;
                }

            }
        }
    }
}