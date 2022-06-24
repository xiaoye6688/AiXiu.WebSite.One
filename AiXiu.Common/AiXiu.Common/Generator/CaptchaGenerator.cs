using System;
using System.Drawing;
using System.Threading;
using System.Web;

namespace AiXiu.Common
{
    /// <summary>
    /// 图片验证码生成器
    /// </summary>
    public class CaptchaGenerator
    {
        #region 私有字段

        public string Text { get; private set; }
        public Bitmap Image { get; private set; }
        private int LetterCount { set; get; }  // 验证码位数
        private int Type { set; get; }
        private int letterWidth = 16;  // 单个字体的宽度范围
        private int letterHeight = 20; // 单个字体的高度范围
        private static Random Random = new Random(~unchecked((int)DateTime.Now.Ticks));
        private Font[] fonts =
        {
            new Font(new FontFamily("Times New Roman"),10 + Random.Next(1),FontStyle.Regular),
            new Font(new FontFamily("Georgia"), 10 + Random.Next(1),FontStyle.Regular),
            new Font(new FontFamily("Arial"), 10 + Random.Next(1),FontStyle.Regular),
            new Font(new FontFamily("Comic Sans MS"), 10 + Random.Next(1),FontStyle.Regular)
        };

        #endregion

        #region 构造器

        /// <summary>
        /// 实例化 <see cref="T:AiXiu.Common.CaptchaGenerator"/> 类的一个新实例，默认为4个数字长度
        /// </summary>
        public CaptchaGenerator()
        {
            HttpContext.Current.Response.Expires = 0;
            HttpContext.Current.Response.Buffer = true;
            HttpContext.Current.Response.ExpiresAbsolute = DateTime.Now.AddSeconds(-1);
            HttpContext.Current.Response.AddHeader("pragma", "no-cache");
            HttpContext.Current.Response.CacheControl = "no-cache";
            LetterCount = 4;
            Type = 0;
            InitText();
            CreateImage();
        }

        /// <summary>
        /// 指定数字长度实例化 <see cref="T:AiXiu.Common.CaptchaGenerator"/> 类的一个新实例
        /// </summary>
        /// <param name="Length">Length.</param>
        public CaptchaGenerator(int Length)
        {
            HttpContext.Current.Response.Expires = 0;
            HttpContext.Current.Response.Buffer = true;
            HttpContext.Current.Response.ExpiresAbsolute = DateTime.Now.AddSeconds(-1);
            HttpContext.Current.Response.AddHeader("pragma", "no-cache");
            HttpContext.Current.Response.CacheControl = "no-cache";
            LetterCount = Length;
            Text = StringGenerator.Number(LetterCount);
            CreateImage();
        }

        /// <summary>
        /// 实例化 <see cref="T:AiXiu.Common.CaptchaGenerator"/> 类的一个新实例
        /// </summary>
        /// <param name="Length">Length.</param>
        /// <param name="type">Type 0 number , 1 char , 2 mixed.</param>
        public CaptchaGenerator(int Length, int type)
        {
            HttpContext.Current.Response.Expires = 0;
            HttpContext.Current.Response.Buffer = true;
            HttpContext.Current.Response.ExpiresAbsolute = DateTime.Now.AddSeconds(-1);
            HttpContext.Current.Response.AddHeader("pragma", "no-cache");
            HttpContext.Current.Response.CacheControl = "no-cache";
            LetterCount = Length;
            Type = type;
            InitText();
            CreateImage();
        }

        #endregion


        #region 私有方法

        /// <summary>
        /// 初始化文本
        /// </summary>
        private void InitText()
        {
            switch (Type)
            {
                case 0: Text = StringGenerator.Number(LetterCount); break;
                case 1: Text = StringGenerator.Char(LetterCount); break;
                case 2: Text = StringGenerator.Mixed(LetterCount); break;
                default: break;
            }
        }

        /// <summary>
        /// 绘制验证码
        /// </summary>
        private void CreateImage()
        {
            int ImageWidth = this.Text.Length * letterWidth;
            Bitmap Img = new Bitmap(ImageWidth, letterHeight);
            Graphics g = Graphics.FromImage(Img);
            g.Clear(Color.White);
            for (int i = 0; i < 2; i++)
            {
                int x1 = Random.Next(Img.Width - 1);
                int x2 = Random.Next(Img.Width - 1);
                int y1 = Random.Next(Img.Height - 1);
                int y2 = Random.Next(Img.Height - 1);
                g.DrawLine(new Pen(Color.Silver), x1, y1, x2, y2);
            }
            int _x = -12, _y;
            for (int int_index = 0; int_index < this.Text.Length; int_index++)
            {
                _x += Random.Next(12, 16);
                _y = Random.Next(-2, 2);
                string str_char = this.Text.Substring(int_index, 1);
                str_char = Random.Next(1) == 1 ? str_char.ToLower() : str_char.ToUpper();
                Brush newBrush = new SolidBrush(GetRandomColor());
                Point thePos = new Point(_x, _y);
                g.DrawString(str_char, fonts[Random.Next(fonts.Length - 1)], newBrush, thePos);
            }
            for (int i = 0; i < 10; i++)
            {
                int x = Random.Next(Img.Width - 1);
                int y = Random.Next(Img.Height - 1);
                Img.SetPixel(x, y, Color.FromArgb(Random.Next(0, 255), Random.Next(0, 255), Random.Next(0, 255)));
            }
            Img = TwistImage(Img, true, Random.Next(1, 3), Random.Next(4, 6));
            g.DrawRectangle(new Pen(Color.LightGray, 1), 0, 0, ImageWidth - 1, (letterHeight - 1));
            Image = Img;
        }


        /// <summary>
        /// 字体随机颜色
        /// </summary>
        private Color GetRandomColor()
        {
            Random RandomNum_First = new Random((int)DateTime.Now.Ticks);
            Thread.Sleep(RandomNum_First.Next(50));
            Random RandomNum_Sencond = new Random((int)DateTime.Now.Ticks);
            int int_Red = RandomNum_First.Next(180);
            int int_Green = RandomNum_Sencond.Next(180);
            int int_Blue = (int_Red + int_Green > 300) ? 0 : 400 - int_Red - int_Green;
            int_Blue = (int_Blue > 255) ? 255 : int_Blue;
            return Color.FromArgb(int_Red, int_Green, int_Blue);
        }

        /// <summary>
        /// 正弦曲线Wave扭曲图片
        /// </summary>
        /// <param name="srcBmp">图片路径</param>
        /// <param name="bXDir">如果扭曲则选择为True</param>
        /// <param name="dMultValue">波形的幅度倍数，越大扭曲的程度越高,一般为3</param>
        /// <param name="dPhase">波形的起始相位,取值区间[0-2*PI)</param>
        private Bitmap TwistImage(Bitmap srcBmp, bool bXDir, double dMultValue, double dPhase)
        {
            double PI = 6.283185307179586476925286766559;
            Bitmap destBmp = new Bitmap(srcBmp.Width, srcBmp.Height);
            Graphics graph = Graphics.FromImage(destBmp);
            graph.FillRectangle(new SolidBrush(Color.White), 0, 0, destBmp.Width, destBmp.Height);
            graph.Dispose();
            double dBaseAxisLen = bXDir ? (double)destBmp.Height : (double)destBmp.Width;
            for (int i = 0; i < destBmp.Width; i++)
            {
                for (int j = 0; j < destBmp.Height; j++)
                {
                    double dx;
                    dx = bXDir ? (PI * (double)j) / dBaseAxisLen : (PI * (double)i) / dBaseAxisLen;
                    dx += dPhase;
                    double dy = Math.Sin(dx);
                    int nOldX = 0, nOldY = 0;
                    nOldX = bXDir ? i + (int)(dy * dMultValue) : i;
                    nOldY = bXDir ? j : j + (int)(dy * dMultValue);

                    Color color = srcBmp.GetPixel(i, j);
                    if (nOldX >= 0 && nOldX < destBmp.Width
                     && nOldY >= 0 && nOldY < destBmp.Height)
                    {
                        destBmp.SetPixel(nOldX, nOldY, color);
                    }
                }
            }
            srcBmp.Dispose();
            return destBmp;
        }

        #endregion
    }
}