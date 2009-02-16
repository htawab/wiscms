﻿using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.IO;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace Wis.Website.Web.Backend.dialog
{
    public partial class Cutimg : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
          
            if (!Page.IsPostBack)
            {
                string sh = Request.QueryString["heights"];
                string imagePath = Request["ImagePath"];
                string ToWidth = "360";//从数据库中读取需要切图的大小
                string ToHeight = "270";
                this.w.Text = this.tow.Value = ToWidth;
                this.h.Text = this.toh.Value = ToHeight;
                this.PhotoUrl.Value = imagePath;
                select_iframe.InnerHtml = "<iframe src=\"Cutimg_view.aspx?ImagePath=" + imagePath + "&ToWidth=" + ToWidth + "&ToHeight=" + ToHeight + "\" frameborder=\"0\" id=\"select_main\" scrolling=\"yes\" name=\"select_main\" width=\"100%\" height=\"" + sh + "px\" />";

            }    
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            int tow, toh, x, y, w, h;
            string file;
            tow = Convert.ToInt16(this.tow.Value.ToString());
            toh = Convert.ToInt16(this.toh.Value.ToString());
            x = Convert.ToInt16(this.x.Text);
            y = Convert.ToInt16(this.y.Text);
            w = Convert.ToInt16(this.w.Text);
            h = Convert.ToInt16(this.h.Text);

            file = Server.MapPath(this.PhotoUrl.Value.ToString());
            MakeMyThumbPhoto(file, tow, toh, x, y, w, h);
        }
        /// <summary>
        /// 生成缩略图
        /// </summary>
        /// <param name="originalImagePath">源图路径（物理路径）</param>
        /// <param name="thumbnailPath">缩略图路径（物理路径）</param>
        /// <param name="width">缩略图宽度</param>
        /// <param name="height">缩略图高度</param>
        /// <param name="mode">生成缩略图的方式</param>    
        protected void MakeMyThumbPhoto(string originalImagePath, int toW, int toH, int X, int Y, int W, int H)
        {
            System.Drawing.Image originalImage = System.Drawing.Image.FromFile(originalImagePath);
            int towidth = toW;
            int toheight = toH;
            int x = X;
            int y = Y;
            int ow = W;
            int oh = H;


            //新建一个bmp图片
            System.Drawing.Image bitmap = new System.Drawing.Bitmap(towidth, toheight);

            //新建一个画板
            System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(bitmap);

            //设置高质量插值法
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;

            //设置高质量,低速度呈现平滑程度
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

            //清空画布并以透明背景色填充
            g.Clear(System.Drawing.Color.Transparent);

            //在指定位置并且按指定大小绘制原图片的指定部分
            g.DrawImage(originalImage, new System.Drawing.Rectangle(0, 0, towidth, toheight),
                new System.Drawing.Rectangle(x, y, ow, oh),
                System.Drawing.GraphicsUnit.Pixel);
            try
            {
                string fileExtension = ".jpg"; //缩略图后缀名
                string sUserUploadPath = "/files/images";
                string DirectoryPath;



                DirectoryPath = sUserUploadPath + "/" + DateTime.Now.ToString("yyyy-MM");
                if (!System.IO.Directory.Exists(Server.MapPath(DirectoryPath)))
                {
                    System.IO.Directory.CreateDirectory(Server.MapPath(DirectoryPath));
                }
                string sFileName = DateTime.Now.ToString("yyyyMMddHHmmssffff") + fileExtension;  // 文件名称
                string thumbnailPath =Server.MapPath(DirectoryPath + "/" + sFileName );        // 服务器端文件路径

                //以jpg格式保存缩略图
                bitmap.Save(thumbnailPath, System.Drawing.Imaging.ImageFormat.Jpeg);
                //WriteJs("-1", "parent.opener.document.getElementById('txtImg').value='" + DirectoryPath + "/" + sFileName + "';parent.close();");
                ViewState["javescript"] = "ReturnValue('" + DirectoryPath + "/" + sFileName + "');closefDiv();";
                return;
            }
            catch (System.Exception e)
            {
                //throw e;
            }
            finally
            {
                originalImage.Dispose();
                bitmap.Dispose();
                g.Dispose();
            }
        }
   
    }
}