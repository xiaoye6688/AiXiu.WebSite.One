using AiXiu.BLL;
using AiXiu.Common;
using AiXiu.IBLL;
using AiXiu.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AiXiu.WebSite
{
    public partial class AvatarEdit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnAvatar_Click(object sender, EventArgs e)
        {
            //1、读取URL传递的length，若小于等于0则返回400-错误请求，结束响应，返回
            //2、读取编码流长度，若小于等于0则返回400，结束响应，返回
            //3、获取二进制(封装调用)
            //3.1、创建byte[]类并设置长度为当前编码流长度（context.Request.InputStream.Length）
            //3.2、调用InputStream.Read方法从当前流中读取编码流长度赋值给byte[]中
            //3.3、将当前byte[]类型中的字节编码为一个字符串（Encoding.Default.GetString）
            //4、图片命名-Guid.NewGuid().ToString("N")
            //5、设置图片物理路径拼接图片名称
            //6、设置相对路径
            //7、定义并实例化一个内存流，以存放图片的字节数组MemoryStream
            //8、使用指定的数据流中嵌入Image，从该数据流创建(Image.FromStream)
            //8、输出相对路径
            TBUsers tBUsers = IdentityManager.ReadUser();
            IUserManager userManager = new UserManager();
            tBUsers.Avatar = hfAvatar.Value;
            OperResult<TBUsers> operResult = userManager.EditAvatar(tBUsers);
            if (operResult.StatusCode == StatusCode.Succeed)
            {
                IdentityManager.SaveUser(operResult.Result);
                PageExtensions.AlertAndRedirect(this, "editPerSuc", operResult.Message, "Personal.aspx");
            }
            else
            {
                PageExtensions.Alert(this, "editPerError", operResult.Message);
            }

        }
       
    }
}