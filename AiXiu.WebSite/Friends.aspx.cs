using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AiXiu.BLL;
using AiXiu.IBLL;
using AiXiu.Model;
using AiXiu.Common;

namespace AiXiu.WebSite
{
    public partial class Friends : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int userId = IdentityManager.ReadUser().Id;
                IFriendManager friendManager = new FriendManager();
                Dictionary<int, Friend> frinList = friendManager.GetFriendList(userId);
                rptFriends.DataSource = frinList;
                rptFriends.DataBind();
            }

        }
        public string ShowDateOrTime(DateTime dateTime)
        {
            DateTime today = DateTime.Today.Date;
            if (dateTime.Date.Equals(today))
                return dateTime.ToString("HH:mm");
            else
                return dateTime.ToString("MM-dd");

        }
    }
}