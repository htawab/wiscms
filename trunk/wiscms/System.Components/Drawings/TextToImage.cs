//------------------------------------------------------------------------------
// <copyright file="TextToImage.cs" company="Everwis">
//     Copyright (C) Everwis Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

namespace Wis.Toolkit.Drawings
{
    public class TextToImage
    {
        private TextToImage() { }

        /// <summary>
        /// 生成随机的指定长度的字母。
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string CreateRandText(int length)
        {
            string str = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            char[] arr = str.ToCharArray();

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            System.Random random = new System.Random();
            for (int index = 0; index <= length - 1; index++)
            {
                int randNum = random.Next(str.Length);
                sb.Append(arr[randNum]);
            }

            return sb.ToString();
        }
        /// <summary>
        /// 生成随机的指定长度的字母和数子。
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string CreateRandNum(int length)
        {
            string str = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            char[] arr = str.ToCharArray();

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            System.Random random = new System.Random();
            for (int index = 0; index <= length - 1; index++)
            {
                int randNum = random.Next(str.Length);
                sb.Append(arr[randNum]);
            }

            return sb.ToString();
        }
        /// <summary>
        /// 文字转换为图片。
        /// </summary>
        /// <param name="text">文本</param>
        public static void WriteImage(string text, System.Web.HttpContext context)
        {
            // http://www.chinaz.com/Program/.NET/0430O252007.html
#warning TODO:需要支持更多的验证码，比如扭曲，汉字，验证码出现算法
            System.Drawing.Font font = new System.Drawing.Font("Charlemagne Std", 12, System.Drawing.FontStyle.Bold);
            System.Drawing.Bitmap bitmap = new System.Drawing.Bitmap(1, 1);
            System.Drawing.Graphics graphics = System.Drawing.Graphics.FromImage(bitmap);
            System.Drawing.SizeF sizeF = graphics.MeasureString(text, font);
            bitmap = new System.Drawing.Bitmap(System.Convert.ToInt32(sizeF.Width), System.Convert.ToInt32(sizeF.Height));
            graphics = System.Drawing.Graphics.FromImage(bitmap);
            graphics.Clear(System.Drawing.Color.WhiteSmoke);
            graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
            graphics.DrawString(text, font, new System.Drawing.SolidBrush(System.Drawing.Color.Red), 0, 0);
            graphics.Flush();
            bitmap.MakeTransparent(System.Drawing.Color.LightBlue);
            context.Response.ContentType = "image/GIF";
            bitmap.Save(context.Response.OutputStream, System.Drawing.Imaging.ImageFormat.Gif);
        }
    }
}
