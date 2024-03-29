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

namespace Wis.Website.Web.Backend.dialog
{
    public partial class Cutimg : BackendPage
    {
        private Wis.Website.DataManager.Category category = null;
        private Wis.Website.DataManager.CategoryManager categoryManager = null;

        private int _ImageWidth;
        /// <summary>
        /// 缩略图高度。
        /// </summary>
        protected int ImageWidth
        {
            get { return _ImageWidth; }
            set { _ImageWidth = value; }
        }

        private int _ImageHeight;
        /// <summary>
        /// 缩略图高度。
        /// </summary>
        protected int ImageHeight
        {
            get { return _ImageHeight; }
            set { _ImageHeight = value; }
        }

        private string _ImagePath;
        protected string ImagePath
        {
            get { return _ImagePath; }
            set { _ImagePath = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            // 图片保存的路径 /Uploads/Files/年-月-日/文件编号.htm
            this.ImagePath = Request["ImagePath"];
            if (System.IO.File.Exists(this.ImagePath) == false)
            {
                Wis.Toolkit.ClientScript.Window.Alert("图片不存在");
                Wis.Toolkit.ClientScript.Window.Close();
                return;
            }

            // 获取分类编号
            string requestCategoryGuid = Request.QueryString["CategoryGuid"];
            if (categoryManager == null) categoryManager = new Wis.Website.DataManager.CategoryManager();
            // 获取分类的信息
            if (Wis.Toolkit.Validator.IsGuid(requestCategoryGuid))
            {
                Guid categoryGuid = new Guid(requestCategoryGuid);
                category = categoryManager.GetCategoryByCategoryGuid(categoryGuid);
                if (string.IsNullOrEmpty(category.CategoryName))
                {
                    // TODO:关闭本层
                    // TODO:弹出消息层
                    this.MessageBox("缺少参数", "未传递分类编号");
                    return;
                }
                if(category.ThumbnailWidth.HasValue)
                    this.w.Text = category.ThumbnailWidth.ToString();
                if (category.ThumbnailHeight.HasValue)
                    this.h.Text = category.ThumbnailHeight.ToString();

                if (this.w.Text.Trim() == "" || this.h.Text.Trim() == "")
                {
                    this.MessageBox("配置不全", "分类编号为 {0} 的分类需要配置缩略图的宽度和高度");
                    return;
                }

                this.ImageWidth = category.ThumbnailWidth.Value;
                this.ImageHeight = category.ThumbnailHeight.Value;
            }


            if (!Page.IsPostBack)
            {
                //string ToWidth = "360";//从数据库中读取需要切图的大小
                //string ToHeight = "270";
                //this.w.Text = this.tow.Value = ToWidth;
                //this.h.Text = this.toh.Value = ToHeight;
                //this.PhotoUrl.Value = imagePath;
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

            string imagePath = Request["ImagePath"];
            file = Server.MapPath(imagePath);
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
                Wis.Website.DataManager.FileManager fileManager = new Wis.Website.DataManager.FileManager();
                Wis.Website.DataManager.File file = new Wis.Website.DataManager.File();
                file.CreatedBy = string.Empty; // TODO:填写当前登录用户的UserName
                file.CreationDate = System.DateTime.Now;
                file.Description = string.Empty; // TODO：如何提供描述？
                file.FileGuid = Guid.NewGuid();
                file.Hits = 0;
                file.OriginalFileName = "";
                file.Rank = 0;
                // file.SaveAsFileName = 
                // file.Size
                // file.SubmissionGuid = // ?
                string fileExtension = ".jpg"; //缩略图后缀名

                string sUserUploadPath = "/Uploads/Photos";
                string DirectoryPath = sUserUploadPath + "/" + DateTime.Now.ToShortDateString();
                if (!System.IO.Directory.Exists(Server.MapPath(DirectoryPath)))
                {
                    System.IO.Directory.CreateDirectory(Server.MapPath(DirectoryPath));
                }
                string sFileName = DateTime.Now.ToString("yyyyMMddHHmmssffff") + fileExtension;  // 文件名称
                string thumbnailPath = Server.MapPath(DirectoryPath + "/" + sFileName );        // 服务器端文件路径

                //以jpg格式保存缩略图
                bitmap.Save(thumbnailPath, System.Drawing.Imaging.ImageFormat.Jpeg);
                ViewState["javescript"] = "ReturnValue('" + DirectoryPath + "/" + sFileName + "');";
                return;
            }
            catch (System.Exception e)
            {
                throw e;
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
