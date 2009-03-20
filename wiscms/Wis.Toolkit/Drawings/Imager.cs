using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing.Imaging;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Wis.Toolkit.Drawings
{
    public class Imager
    {
        /// <summary>
        /// 裁剪缩略图
        /// </summary>
        /// <param name="srcFilename">源图物理路径</param>
        /// <param name="destFilename">缩略图物理路径</param>
        /// <param name="pointX">X坐标</param>
        /// <param name="pointY">Y坐标</param>
        /// <param name="cropperWidth">缩略图宽度</param>
        /// <param name="cropperHeight">缩略图高度</param>
        public static void Crop(string srcFilename, string destFilename, int pointX, int pointY, int cropperWidth, int cropperHeight)
        {
            // 要绘制的 System.Drawing.Image
            System.Drawing.Image image = System.Drawing.Image.FromFile(srcFilename);
            // 指定所绘制图像的位置和大小。将图像进行缩放以适合该矩形。
            System.Drawing.Rectangle destRect = new System.Drawing.Rectangle(0, 0, cropperWidth, cropperHeight);
            // 指定 image 对象中要绘制的部分。
            System.Drawing.Rectangle srcRect = new System.Drawing.Rectangle(pointX, pointY, cropperWidth, cropperHeight);
            // 指定 srcRect 参数所用的度量单位。
            System.Drawing.GraphicsUnit srcUnit = System.Drawing.GraphicsUnit.Pixel;

            // 新建一个bmp图片
            System.Drawing.Image bitmap = new System.Drawing.Bitmap(cropperWidth, cropperHeight);
            // 新建一个画板
            System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(bitmap);
            // 设置高质量插值法
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;
            // 设置高质量,低速度呈现平滑程度
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            // 清空画布并以透明背景色填充
            g.Clear(System.Drawing.Color.Transparent);
            // 在指定位置并且按指定大小绘制指定的 image 的指定部分。
            g.DrawImage(image, destRect, srcRect, srcUnit);

            // 目标图片的路径
            System.IO.FileInfo destFileInfo = new System.IO.FileInfo(destFilename);
            if (!destFileInfo.Directory.Exists) destFileInfo.Directory.Create();

            // 以Jpeg格式保存缩略图
            bitmap.Save(destFilename);

            image.Dispose();
            bitmap.Dispose();
            g.Dispose();
        }

        /// <summary>
        /// 缩略图。
        /// </summary>
        /// <param name="srcFilename">源图路径</param>
        /// <param name="destFilename">目标图路径</param>
        /// <param name="thumbWidth">请求的缩略图的宽度（以像素为单位）</param>
        /// <param name="thumbHeight">请求的缩略图的高度（以像素为单位）</param>
        /// <param name="stretch">拉伸</param>
        /// <param name="beveled">斜面</param>
        public static void Thumbnail(string srcFilename, string destFilename, int thumbWidth, int thumbHeight, bool stretch, bool beveled)
        {
            float fx, fy, f;
            int destWidth, destHeight; float widthOrig, heightOrig;

            // create thumbnail using .net function GetThumbnailImage
            Bitmap srcBitmap = new Bitmap(srcFilename); // load original image
            if (!stretch)
            {   // retain aspect ratio
                widthOrig = srcBitmap.Width;
                heightOrig = srcBitmap.Height;
                fx = widthOrig / thumbWidth;
                fy = heightOrig / thumbHeight; // subsample factors
                // must fit in thumbnail size
                f = Math.Max(fx, fy); if (f < 1) f = 1;
                destWidth = (int)(widthOrig / f);
                destHeight = (int)(heightOrig / f);
            }
            else
            {
                destWidth = thumbWidth;
                destHeight = thumbHeight;
            }

            // create the new bitmap with the specified size
            Bitmap destBitmap = new Bitmap(srcBitmap, destWidth, destHeight);
            Graphics g = Graphics.FromImage(destBitmap);
            g.Clear(Color.White);
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            g.CompositingMode = System.Drawing.Drawing2D.CompositingMode.SourceCopy;
            g.DrawImage(srcBitmap, 0, 0, destWidth, destHeight);
            g.Save();

            // according to type of the file we will save the new image
            ImageFormat imageFormat = ImageFormat.Jpeg;
            System.IO.FileInfo destFileInfo = new System.IO.FileInfo(destFilename);
            switch (destFileInfo.Extension.ToLower())
            {
                case ".bmp":
                    imageFormat = ImageFormat.Bmp;
                    break;
                case ".png":
                    imageFormat = ImageFormat.Png;
                    break;
                case ".gif":
                    imageFormat = ImageFormat.Gif;
                    break;
            }

            if (!destFileInfo.Directory.Exists) destFileInfo.Directory.Create();
            if (!beveled)
            {
                destBitmap.Save(destFilename, imageFormat); // ImageFormat.Jpeg
                destBitmap.Dispose();
                srcBitmap.Dispose();
                g.Dispose();
                return;
            }
            // ---- apply bevel
            int widTh, heTh;
            widTh = srcBitmap.Width;
            heTh = srcBitmap.Height;
            int BevW = 10, LowA = 0, HighA = 180, Dark = 80, Light = 255;
            // hilight color, low and high
            Color clrHi1 = Color.FromArgb(LowA, Light, Light, Light);
            Color clrHi2 = Color.FromArgb(HighA, Light, Light, Light);
            Color clrDark1 = Color.FromArgb(LowA, Dark, Dark, Dark);
            Color clrDark2 = Color.FromArgb(HighA, Dark, Dark, Dark);
            LinearGradientBrush br; Rectangle rectSide;
            Graphics newG = Graphics.FromImage(srcBitmap);
            Size szHorz = new Size(widTh, BevW);
            Size szVert = new Size(BevW, heTh);
            // ---- draw dark (shadow) sides first
            // draw bottom-side of bevel
            szHorz += new Size(0, 2); szVert += new Size(2, 0);
            rectSide = new Rectangle(new Point(0, heTh - BevW), szHorz);
            br = new LinearGradientBrush(rectSide, clrDark1, clrDark2, LinearGradientMode.Vertical);
            rectSide.Inflate(0, -1);
            newG.FillRectangle(br, rectSide);
            // draw right-side of bevel
            rectSide = new Rectangle(new Point(widTh - BevW, 0), szVert);
            br = new LinearGradientBrush(rectSide, clrDark1, clrDark2, LinearGradientMode.Horizontal);
            rectSide.Inflate(-1, 0);
            newG.FillRectangle(br, rectSide);
            // ---- draw bright (hilight) sides next
            szHorz -= new Size(0, 2); szVert -= new Size(2, 0);
            // draw top-side of bevel
            rectSide = new Rectangle(new Point(0, 0), szHorz);
            br = new LinearGradientBrush(rectSide, clrHi2, clrHi1, LinearGradientMode.Vertical);
            newG.FillRectangle(br, rectSide);
            // draw left-side of bevel
            rectSide = new Rectangle(new Point(0, 0), szVert);
            br = new LinearGradientBrush(rectSide, clrHi2, clrHi1, LinearGradientMode.Horizontal);
            newG.FillRectangle(br, rectSide);
            // dispose graphics objects and return bitmap
            br.Dispose();
            newG.Dispose();

            destBitmap.Save(destFilename, imageFormat); // ImageFormat.Jpeg
            destBitmap.Dispose();
            srcBitmap.Dispose();
            g.Dispose();
        }
        private static bool ThumbnailCallback() { return false; }
    }
}
