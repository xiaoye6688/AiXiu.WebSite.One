using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

namespace AiXiu.WebSite.Ashx
{
    /// <summary>
    /// ImageUploadHandler 的摘要说明
    /// </summary>
    public class ImageUploadHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
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
            //读取图片base46编码大小
           // if (context.Request.QueryString["length"]!=null)
           // {
                int length = int.Parse(context.Request.QueryString["length"] ?? "0");
                if (length <= 0)
                {
                    context.Response.StatusCode = 400;
                    context.Response.End();
                    return;
                }
            //  }
            int inputLength = (int)context.Request.InputStream?.Length;
            if (length<=0)
            {
                context.Response.StatusCode = 400;
                context.Response.End();
                return;
            }
            string imgName = Guid.NewGuid().ToString("N");
            string imgPath = context.Server.MapPath("~/avatar")+"/" + imgName + ".jpg";
            string imgUrl = "avatar/" + imgName + ".jpg";
            byte[] imgByte = baseByte(context);
            using (MemoryStream memoryStream = new MemoryStream(imgByte))
            {
                Image image = Image.FromStream(memoryStream);
                image.Save(imgPath, ImageFormat.Jpeg);
            }
            context.Response.Write(imgUrl);
        }
        public byte[] baseByte(HttpContext context)
        {
            //3、创建byte[]类并设置长度为当前编码流长度
            //4、调用InputStream.Read方法从当前流中读取编码流长度赋值给byte[]中
            //5、将当前byte[]类型中的字节编码为一个字符串（Encoding.Default.GetString）
            byte[] base64Bytes = new byte[context.Request.InputStream.Length];
            context.Request.InputStream.Seek(0, SeekOrigin.Begin);
            context.Request.InputStream.Read(base64Bytes, 0, base64Bytes.Length);
            string base64String = Encoding.Default.GetString(base64Bytes);
            base64String = base64String.Substring(base64String.IndexOf("base64") + 7);
            byte[] baseByte = Convert.FromBase64String(base64String);
            return baseByte;
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