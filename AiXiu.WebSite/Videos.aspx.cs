using AiXiu.BLL;
using AiXiu.Common;
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
    public partial class Videos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                //1、获取作者id
                //2、通过作者ID获取基本信息
                int avatarId = 0;
                if (!string.IsNullOrWhiteSpace(Request.QueryString["id"]))
                {
                    int.TryParse(Request.QueryString["id"], out avatarId);
                }
                if (avatarId>0)
                {
                    IUserManager userManager = new UserManager();
                    TBUsers AvatarUsers = userManager.GetAuthorById(avatarId).Result;
                    if (AvatarUsers!=null)
                    {
                        lblNickName.Text = AvatarUsers.NickName ?? $"用户{AvatarUsers.Id}";
                        switch (AvatarUsers.Sex)
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

                        lblBirthday.Text = AvatarUsers.Birthday.HasValue ? AvatarUsers.Birthday.Value.ToString("M") : "未知";
                        lblHobby.Text = AvatarUsers.Hobby;
                        lblAddress.Text = AvatarUsers.ADDress;
                        //3、获取当前登录用户的id和作者判断（按钮显示）
                        TBUsers loginUser = IdentityManager.ReadUser();
                        if (loginUser!=null)
                        {
                            if (loginUser.Id==avatarId)
                            {
                                Response.Redirect("Personal.aspx");
                            }
                            else
                            {
                                //是否是好友
                                IFriendManager friendManager = new FriendManager();
                                bool isFrienf = friendManager.IsFriend(loginUser.Id,avatarId);
                                if (isFrienf)
                                {
                                    btnMessage.Visible = true;
                                    btnMessage.Enabled = true;
                                }
                                else
                                {
                                    btnFriend.Visible = true;
                                    btnFriend.Enabled = true;
                                }
                            }
                        }
                        //4、通过作者id得到该作者的视频列表（数据库）
                        IVideoManager videoManager = new VideoManager();
                        rptVideos.DataSource = videoManager.GetVideoListById(avatarId).Result;
                        rptVideos.DataBind();
                    }
                    else
                    {
                        Response.Redirect("Default.aspx");
                    }
                }
                else
                {
                    Response.Redirect("Default.aspx");
                }



            }
        }

        protected void btnFriend_Click(object sender, EventArgs e)
        {
            int avatarId = 0;
            if (!string.IsNullOrWhiteSpace(Request.QueryString["id"]))
            {
                int.TryParse(Request.QueryString["id"],out avatarId);
            }
            TBUsers loginUser = IdentityManager.ReadUser();
            IFriendManager friendManager = new FriendManager();
            if (friendManager.AddFriend(loginUser.Id,avatarId))
            {
                PageExtensions.AlertAndRedirect(this, "refSucces", "添加好友成功", "Chat.aspx?to="+avatarId);
            }
            else
            {
                PageExtensions.Alert(this, "regSucces", "添加好友失败");
            }
        }

        protected void btnMessage_Click(object sender, EventArgs e)
        {
            int avatarId = 0;
            if (!string.IsNullOrWhiteSpace(Request.QueryString["id"]))
            {
                int.TryParse(Request.QueryString["id"], out avatarId);
            }
            //跳转到chat页面
            PageExtensions.Redirect(this, "refSucces", "Chat.aspx?to=" + avatarId);
        }
    }
}