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
    public partial class Personal : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                //读取用户资料
                TBUsers user = IdentityManager.ReadUser();
                //显示用户资料
                this.imgAvatar.ImageUrl = user.Avatar??"imgs/avatar.jpg";
                this.lblNickName.Text = user.NickName??$"用户{user.Id}";
                switch (user.Sex)
                {
                    case 1:
                        this.lblSex.Text = "小哥哥";
                        this.lblSex.CssClass += " bg-color-blue";
                        break;
                    case 2:
                        this.lblSex.Text = "小姐姐";
                        this.lblSex.CssClass += " bg-color-pink";
                        break;
                    default:
                        this.lblSex.Visible = false;
                        break;
                }
                this.lblAddress.Text = user.ADDress ?? "未知地";
                this.lblBirthday.Text = user.Birthday.HasValue ? user.Birthday.Value.ToString("M"):"未设置";
                this.lblHobby.Text = user.Hobby ?? "未添加";

            }

        }
    }
}