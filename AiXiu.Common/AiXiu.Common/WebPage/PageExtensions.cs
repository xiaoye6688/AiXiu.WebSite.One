using System.Web.UI;

namespace AiXiu.Common
{
    /// <summary>
    /// 页面扩展类
    /// </summary>
    public static class PageExtensions
    {
        /// <summary>
        /// 弹出消息框
        /// </summary>
        /// <param name="page">页面</param>
        /// <param name="key">要注册的启动脚本的键</param>
        /// <param name="message">要提示的消息</param>
        public static void Alert(this Page page, string key, string message)
        {
            if (!page.ClientScript.IsStartupScriptRegistered(page.GetType(), key))
            {
                page.ClientScript.RegisterStartupScript(page.GetType(), key, $"alert('{message}')", true);
            }
        }

        /// <summary>
        /// 跳转到新地址
        /// </summary>
        /// <param name="page"></param>
        /// <param name="key"></param>
        /// <param name="redirectUrl"></param>
        public static void Redirect(this Page page, string key, string redirectUrl)
        {
            if(!page.ClientScript.IsStartupScriptRegistered(key))
            {
                page.ClientScript.RegisterStartupScript(page.GetType(), key, $"window.location.href='{redirectUrl}'", true);
            }
        }

        /// <summary>
        /// 弹出消息框并跳转到新地址
        /// </summary>
        /// <param name="page"></param>
        /// <param name="key"></param>
        /// <param name="message"></param>
        /// <param name="redirectUrl"></param>
        public static void AlertAndRedirect(this Page page, string key, string message, string redirectUrl)
        {
            if (!page.ClientScript.IsStartupScriptRegistered(page.GetType(), key))
            { 
                page.ClientScript.RegisterStartupScript(page.GetType(), key, $"alert('{message}');window.location.href='{redirectUrl}'", true);
            }
        }
    }
}