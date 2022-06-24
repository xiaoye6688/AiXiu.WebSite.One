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
                TBUsers tBUsers = IdentityManager.ReadUser();
                if (tBUsers != null)
                {
                    imgAvatar.ImageUrl = tBUsers.Avatar ?? "/imgs/avatar.jpg";

                    lblNickName.Text = tBUsers.NickName ?? tBUsers.Id.ToString(); 
                    switch (tBUsers.Sex)
                    {
                        case 1:
                            this.lblSex.Text = "小哥哥";
                            this.lblSex.CssClass += " bg-color-blue";
                            break;
                        case 0:
                            this.lblSex.Text = "小姐姐";
                            this.lblSex.CssClass += " bg-color-pink";
                            break;
                        default:
                            this.lblSex.Visible = false;
                            break;
                    }

                    lblBirthday.Text = tBUsers.Birthday.HasValue ? tBUsers.Birthday.Value.ToString("M") : "未知";
                    lblHobby.Text = tBUsers.Hobby;
                    lblAddress.Text = tBUsers.ADDress;

                }
                //显示用户资料
                //this.imgAvatar.ImageUrl = tBUsers.Avatar  ??  "/imgs/avatar.jpg";
                //this.lblNickName.Text = tBUsers.NickName ?? $"用户{tBUsers.Id}";
                //switch (tBUsers.Sex)
                //{
                //    case 1:
                //        this.lblSex.Text = "小哥哥";
                //        this.lblSex.CssClass += " bg-color-blue";
                //        break;
                //    case 2:
                //        this.lblSex.Text = "小姐姐";
                //        this.lblSex.CssClass += " bg-color-pink";
                //        break;
                //    default:
                //        this.lblSex.Visible = false;
                //        break;
                //}
                //this.lblAddress.Text = tBUsers.ADDress ?? "未知地";
                //this.lblBirthday.Text = tBUsers.Birthday.HasValue ? tBUsers.Birthday.Value.ToString("M"):"未设置";
                //this.lblHobby.Text = tBUsers.Hobby ?? "未添加";

            }

        }

    }
}