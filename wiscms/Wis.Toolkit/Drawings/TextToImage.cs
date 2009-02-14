//------------------------------------------------------------------------------
// <copyright file="TextToImage.cs" company="Everwis">
//     Copyright (C) Everwis Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using System.Web;
using System.Drawing;
using System;

namespace Wis.Toolkit.Drawings
{
    public class TextToImage
    {
        /// <summary>
        /// ���������ָ�����ȵ���ĸ��
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string CreateRandChars(int length)
        {
            string str = "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
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


        /*
        /// <summary>
        /// ����ת��ΪͼƬ��
        /// </summary>
        /// <param name="text">�ı�</param>
        public static void WriteImage(string text, System.Web.HttpContext context)
        {
            // http://www.chinaz.com/Program/.NET/0430O252007.html
#warning TODO:��Ҫ֧�ָ������֤�룬����Ť�������֣���֤������㷨
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
        */

        #region ��֤�볤��(Ĭ��4����֤��ĳ���)
        int length = 4;
        public int Length
        {
            get { return length; }
            set { length = value; }
        }
        #endregion

        #region ��֤�������С(Ϊ����ʾŤ��Ч����Ĭ��28���أ����������޸�)
        int fontSize = 28;
        public int FontSize
        {
            get { return fontSize; }
            set { fontSize = value; }
        }
        #endregion

        #region �߿�(Ĭ��1����)
        int padding = 5;
        public int Padding
        {
            get { return padding; }
            set { padding = value; }
        }
        #endregion

        #region �Ƿ�������(Ĭ�ϲ����)
        bool chaos = true;
        public bool Chaos
        {
            get { return chaos; }
            set { chaos = value; }
        }
        #endregion

        #region ���������ɫ(Ĭ�ϻ�ɫ)
        Color chaosColor = Color.LightGray;
        public Color ChaosColor
        {
            get { return chaosColor; }
            set { chaosColor = value; }
        }
        #endregion

        #region �Զ��屳��ɫ(Ĭ�ϰ�ɫ)
        Color backgroundColor = Color.White;
        public Color BackgroundColor
        {
            get { return backgroundColor; }
            set { backgroundColor = value; }
        }
        #endregion

        #region �Զ��������ɫ����
        Color[] colors = { Color.Black, Color.Red, Color.DarkBlue, Color.Green, Color.Orange, Color.Brown, Color.DarkCyan, Color.Purple };
        public Color[] Colors
        {
            get { return colors; }
            set { colors = value; }
        }
        #endregion

        #region �Զ�����������
        string[] fonts = { "Arial", "Georgia" };
        public string[] Fonts
        {
            get { return fonts; }
            set { fonts = value; }
        }
        #endregion

        #region �Զ���������ַ�������
        string codeSerial = "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
        public string CodeSerial
        {
            get { return codeSerial; }
            set { codeSerial = value; }
        }
        #endregion

        #region ���������˾�Ч��

        private const double PI = 3.1415926535897932384626433832795;
        private const double PI2 = 6.283185307179586476925286766559;

        /// <summary>
        /// ��������WaveŤ��ͼƬ��Edit By 51aspx.com��
        /// </summary>
        /// <param name="srcBmp">ͼƬ·��</param>
        /// <param name="bXDir">���Ť����ѡ��ΪTrue</param>
        /// <param name="nMultValue">���εķ��ȱ�����Խ��Ť���ĳ̶�Խ�ߣ�һ��Ϊ3</param>
        /// <param name="dPhase">���ε���ʼ��λ��ȡֵ����[0-2*PI)</param>
        /// <returns></returns>
        public System.Drawing.Bitmap TwistImage(Bitmap srcBmp, bool bXDir, double dMultValue, double dPhase)
        {
            System.Drawing.Bitmap destBmp = new Bitmap(srcBmp.Width, srcBmp.Height);

            // ��λͼ�������Ϊ��ɫ
            System.Drawing.Graphics graph = System.Drawing.Graphics.FromImage(destBmp);
            graph.FillRectangle(new SolidBrush(System.Drawing.Color.White), 0, 0, destBmp.Width, destBmp.Height);
            graph.Dispose();

            double dBaseAxisLen = bXDir ? (double)destBmp.Height : (double)destBmp.Width;

            for (int i = 0; i < destBmp.Width; i++)
            {
                for (int j = 0; j < destBmp.Height; j++)
                {
                    double dx = 0;
                    dx = bXDir ? (PI2 * (double)j) / dBaseAxisLen : (PI2 * (double)i) / dBaseAxisLen;
                    dx += dPhase;
                    double dy = Math.Sin(dx);

                    // ȡ�õ�ǰ�����ɫ
                    int nOldX = 0, nOldY = 0;
                    nOldX = bXDir ? i + (int)(dy * dMultValue) : i;
                    nOldY = bXDir ? j : j + (int)(dy * dMultValue);

                    System.Drawing.Color color = srcBmp.GetPixel(i, j);
                    if (nOldX >= 0 && nOldX < destBmp.Width
                     && nOldY >= 0 && nOldY < destBmp.Height)
                    {
                        destBmp.SetPixel(nOldX, nOldY, color);
                    }
                }
            }

            return destBmp;
        }



        #endregion

        #region ����У����ͼƬ
        public Bitmap CreateImage(string code)
        {
            int fSize = FontSize;
            int fWidth = fSize + Padding;

            int imageWidth = (int)(code.Length * fWidth) + 4 + Padding * 2;
            int imageHeight = fSize * 2 + Padding;

            System.Drawing.Bitmap image = new System.Drawing.Bitmap(imageWidth, imageHeight);

            Graphics g = Graphics.FromImage(image);

            g.Clear(BackgroundColor);

            Random rand = new Random();

            //���������������ɵ����
            if (this.Chaos)
            {

                Pen pen = new Pen(ChaosColor, 0);
                int c = Length * 10;

                for (int i = 0; i < c; i++)
                {
                    int x = rand.Next(image.Width);
                    int y = rand.Next(image.Height);

                    g.DrawRectangle(pen, x, y, 1, 1);
                }
            }

            int left = 0, top = 0, top1 = 1, top2 = 1;

            int n1 = (imageHeight - FontSize - Padding * 2);
            int n2 = n1 / 4;
            top1 = n2;
            top2 = n2 * 2;

            Font f;
            Brush b;

            int cindex, findex;

            //����������ɫ����֤���ַ�
            for (int i = 0; i < code.Length; i++)
            {
                cindex = rand.Next(Colors.Length - 1);
                findex = rand.Next(Fonts.Length - 1);

                f = new System.Drawing.Font(Fonts[findex], fSize, System.Drawing.FontStyle.Bold);
                b = new System.Drawing.SolidBrush(Colors[cindex]);

                if (i % 2 == 1)
                {
                    top = top2;
                }
                else
                {
                    top = top1;
                }

                left = i * fWidth;

                g.DrawString(code.Substring(i, 1), f, b, left, top);
            }

            //��һ���߿� �߿���ɫΪColor.Gainsboro
            g.DrawRectangle(new Pen(Color.Gainsboro, 0), 0, 0, image.Width + 2, image.Height + 2);
            g.Dispose();

            //��������
            image = TwistImage(image, true, 8, 4);

            return image;
        }
        #endregion

        #region �������õ�ͼƬ�����ҳ��
        public void Write(string code, HttpContext context)
        {
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            Bitmap image = this.CreateImage(code);

            image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);

            context.Response.ClearContent();
            context.Response.ContentType = "image/Jpeg";
            context.Response.BinaryWrite(ms.GetBuffer());

            ms.Close();
            ms = null;
            image.Dispose();
            image = null;
        }
        #endregion

        #region ��������ַ���

        /// <summary>
        /// ��������ı�
        /// </summary>
        /// <returns>��������ı�</returns>
        public string CreateRandText(int length)
        {
            if (length == 0) length = Length;

            char[] arr = CodeSerial.ToCharArray();
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            System.Random random = new System.Random();
            for (int index = 0; index <= length - 1; index++)
            {
                int randNum = random.Next(CodeSerial.Length);
                sb.Append(arr[randNum]);
            }

            return sb.ToString();

        }

        /// <summary>
        /// ��������ı�
        /// </summary>
        /// <returns>��������ı�</returns>
        public string CreateRandText()
        {
            return CreateRandText(0);
        }

        #endregion
    }
}
