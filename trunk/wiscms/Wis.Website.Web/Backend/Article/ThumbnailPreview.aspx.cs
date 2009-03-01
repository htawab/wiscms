using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace Wis.Website.Web.Backend.dialog
{
    public partial class Cutimg_view : System.Web.UI.Page
    {
        private int _TopX = 100;
        /// <summary>
        /// X。
        /// </summary>
        protected int TopX
        {
            get { return _TopX; }
            set { _TopX = value; }
        }

        private int _TopY = 100;
        /// <summary>
        /// Y。
        /// </summary>
        protected int TopY
        {
            get { return _TopY; }
            set { _TopY = value; }
        }

        private int _ThumbnailWidth = 100;
        /// <summary>
        /// 缩略图宽度。
        /// </summary>
        protected int ThumbnailWidth
        {
            get { return _ThumbnailWidth; }
            set { _ThumbnailWidth = value; }
        }

        private int _ThumbnailHeight = 100;
        /// <summary>
        /// 缩略图高度。
        /// </summary>
        protected int ThumbnailHeight
        {
            get { return _ThumbnailHeight; }
            set { _ThumbnailHeight = value; }
        }

        private int _ImageWidth = 500;
        /// <summary>
        /// 原图宽度。
        /// </summary>
        protected int ImageWidth
        {
            get { return _ImageWidth; }
            set { _ImageWidth = value; }
        }

        private int _ImageHeight = 500;
        /// <summary>
        /// 原图高度。
        /// </summary>
        protected int ImageHeight
        {
            get { return _ImageHeight; }
            set { _ImageHeight = value; }
        }

        private string _ImagePath = "/Uploads/Photos/2009-02-21/1708138d.jpg";
        /// <summary>
        /// 图片路径。
        /// </summary>
        protected string ImagePath
        {
            get { return _ImagePath; }
            set { _ImagePath = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            System.Drawing.Image originalImage = System.Drawing.Image.FromFile(Server.MapPath(this.ImagePath));
            this.ImageWidth = originalImage.Width;
            this.ImageHeight = originalImage.Height;
            originalImage.Dispose();

            if (!Page.IsPostBack)
            {
                this.TopX = (ImageWidth - ThumbnailWidth) / 2;
                this.TopY = (ImageHeight - ThumbnailHeight) / 2;
            }
        }

        /// <summary>
        /// 裁剪图片。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void LinkButtonCut_Click(object sender, EventArgs e)
        {
            int tow, toh, x, y, w, h;
            string file;
            tow = this.ThumbnailWidth;
            toh = this.ThumbnailHeight;
            x = Convert.ToInt16(this.x.Text);
            y = Convert.ToInt16(this.y.Text);

            string imagePath = Request["ImagePath"];
            file = Server.MapPath(imagePath);
            MakeMyThumbPhoto(file, tow, toh, x, y, this.ThumbnailWidth, this.ThumbnailHeight); 
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

            // 新建一个bmp图片
            System.Drawing.Image bitmap = new System.Drawing.Bitmap(towidth, toheight);

            // 新建一个画板
            System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(bitmap);

            // 设置高质量插值法
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;

            // 设置高质量,低速度呈现平滑程度
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

            // 清空画布并以透明背景色填充
            g.Clear(System.Drawing.Color.Transparent);

            // 在指定位置并且按指定大小绘制原图片的指定部分
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
                string fileExtension = ".jpg"; //缩略图后缀名

                string sUserUploadPath = "/Uploads/Photos";
                string DirectoryPath = sUserUploadPath + "/" + DateTime.Now.ToShortDateString();
                if (!System.IO.Directory.Exists(Server.MapPath(DirectoryPath)))
                {
                    System.IO.Directory.CreateDirectory(Server.MapPath(DirectoryPath));
                }
                string sFileName = DateTime.Now.ToString("yyyyMMddHHmmssffff") + fileExtension;  // 文件名称
                string thumbnailPath = Server.MapPath(DirectoryPath + "/" + sFileName);        // 服务器端文件路径

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
