using AiXiu.Common;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Web;
using System.Web.SessionState;

namespace AiXiu.WebSite.Ashx
{
    /// <summary>
    /// CaptchaHandler 的摘要说明
    /// </summary>
    public class CaptchaHandler : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            //5.图形验证码  img src = 一般处理程序（1、图片   2、数字字母 == session）
            //a.一般处理程序：CaptchaHandler.ashx
            //b.在一般处理程序中处理session（添加引用using System.Web.SessionState; 继承接口IRequiresSessionState）
            //c.前台添加点击事件
            // .绑定事件：Document.on('click', function() { })
            //ii.区别请求URL后缀添加随机数：Math.random()
            //d.生成图片和随机码：CaptchaGenerator工具类
            //e.将该图片按照制定格式存入当前输出流中：bitmap.Save(context.Response.OutputStream, ImageFormat.Jpeg);
            CaptchaGenerator captchaGenerator = new CaptchaGenerator();
            string code = captchaGenerator.Text;
            Bitmap bitmap = captchaGenerator.Image;
            context.Session.Add("yanzhengma", code);

            bitmap.Save(context.Response.OutputStream, ImageFormat.Jpeg);
            context.Response.ContentType = "images/jpeg";
            context.Response.End();
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