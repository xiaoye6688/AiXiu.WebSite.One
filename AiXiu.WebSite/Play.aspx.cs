using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AiXiu.Model;
using AiXiu.BLL;
using AiXiu.IBLL;

namespace AiXiu.WebSite
{
    public partial class Play : System.Web.UI.Page
    {
        public TBVideos Video;
        public TBUsers Author;
        public bool IsLike;
        public string VideoId;
        public long LikeCount;
        public long DiscussCount;
        public int UserId;
        public GetPlayInfoResult playInfo;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["id"] != null)
            {
                VideoId = Request.QueryString["id"].ToString();
                //1、视频信息
                IVideoManager videoManager = new VideoManager();
                Video = videoManager.GetVideoById(VideoId).Result;
                //2、作者信息
                IUserManager userManager = new UserManager();
                Author = userManager.GetAuthorById((int)Video.UserId).Result;
                TBUsers loginUser = IdentityManager.ReadUser();
                if (loginUser != null)
                {
                    UserId = loginUser.Id;
                }
                //3、播放视频信息
                playInfo = videoManager.GetPlayInfoResultById(VideoId).Result;
                //评论点赞
                ILikeManager likeManager = new LikeManager();
                
                IsLike = likeManager.IsLike(VideoId, UserId);
                LikeCount = likeManager.Count(VideoId);
                IDiscussManager discuss = new DiscussManager();
                DiscussCount = discuss.Count(VideoId);
            }
            else
            {
                Response.Redirect("/Default.aspx");
            }


        }
    }
}