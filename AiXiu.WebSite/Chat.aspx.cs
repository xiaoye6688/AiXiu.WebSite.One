using AiXiu.BLL;
using AiXiu.IBLL;
using AiXiu.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AiXiu.WebSite
{
    public partial class Chat : System.Web.UI.Page
    {
        public int otherId;
        public TBUsers friend;
        public TBUsers user;
        public int selfId;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!string.IsNullOrWhiteSpace(Request.QueryString["to"]))
                {
                    otherId = int.Parse(Request.QueryString["to"]);
                }
                IUserManager userManager = new UserManager();
                friend = userManager.GetAuthorById(otherId).Result;
                user = IdentityManager.ReadUser();
                if (user!=null)
                {
                    selfId = user.Id;

                }
            }
           

        }
    }
}