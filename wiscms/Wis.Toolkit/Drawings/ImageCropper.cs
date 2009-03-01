using System;
using System.Collections.Generic;
using System.Text;

namespace Wis.Toolkit.Drawings
{
    public class ImageCropper
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
    }
}
