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
using Wis.Toolkit.WebControls.FileUploads;

namespace Wis.Website.Web.Backend
{
    public partial class ArticleAddPhoto : System.Web.UI.Page
    {
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

        private Guid _ArticleGuid;
        /// <summary>
        /// 文章编号
        /// </summary>
        protected Guid ArticleGuid
        {
            get { return _ArticleGuid; }
            set { _ArticleGuid = value; }
        }

        Wis.Website.DataManager.Article article = null;
        Wis.Website.DataManager.ArticleManager articleManager = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            string outputPath = "~/Uploads/Temp/";
            string physicalOutputPath = Server.MapPath(outputPath);
            if (!System.IO.Directory.Exists(physicalOutputPath)) System.IO.Directory.CreateDirectory(physicalOutputPath);

            FileSystemProcessor fs = new FileSystemProcessor();
            fs.OutputPath = outputPath;
            DJUploadController1.DefaultFileProcessor = fs;
            Photo.FileProcessor = fs;

            // 获取文章信息
            string requestArticleGuid = Request.QueryString["ArticleGuid"];
            if (string.IsNullOrEmpty(requestArticleGuid) || !Wis.Toolkit.Validator.IsGuid(requestArticleGuid))
            {
                Warning.InnerHtml = "不正确的文章编号，请<a href='ArticleSelectCategory.aspx'>点击这里</a>重新操作";
                return;
            }
            this.ArticleGuid = new Guid(requestArticleGuid);

            if (article == null)
            {
                if (articleManager == null) articleManager = new Wis.Website.DataManager.ArticleManager();
                article = articleManager.GetArticleByArticleGuid(this.ArticleGuid);
            }

            HyperLinkCategory.Text = article.Category.CategoryName;
            HyperLinkCategory.NavigateUrl = string.Format("ArticleList.aspx?CategoryGuid={0}", article.Category.CategoryGuid);

            if (!article.Category.ThumbnailWidth.HasValue || !article.Category.ThumbnailHeight.HasValue)
            {
                Warning.InnerHtml = "缩微图的宽度和高度需要指定，请<a href='CategoryUpdate.aspx?CategoryGuid={0}' target='_blank'>点击这里</a>设定";
                return;
            }
            this.ThumbnailWidth = article.Category.ThumbnailWidth.Value;
            this.ThumbnailHeight = article.Category.ThumbnailHeight.Value;
        }

        protected void ImageButtonNext_Click(object sender, ImageClickEventArgs e)
        {
#warning 如何先上传，后录入数据库?

            // 录入图片信息，进入下一步
            if (DJUploadController1.Status == null || DJUploadController1.Status.UploadedFiles.Count != 1)
            {
                return;
            }
            UploadedFile f = DJUploadController1.Status.UploadedFiles[0];
            // f.FileName "E:\\Tools\\visualxpath.zip"
            string fileName = f.FileName;
            int charIndex = fileName.LastIndexOf("\\");
            if (charIndex == -1 || charIndex >= fileName.Length)
            {
                return;
            }
            fileName = fileName.Substring(charIndex + 1);

            string outputPath = "~/Uploads/Temp/";
            string physicalOutputPath = Server.MapPath(outputPath);
            if (!System.IO.Directory.Exists(physicalOutputPath)) System.IO.Directory.CreateDirectory(physicalOutputPath);
            string srcFilename = Server.MapPath(string.Format("{0}{1}", outputPath, fileName));
            System.IO.FileInfo thumbnail = new System.IO.FileInfo(srcFilename);

            Wis.Website.DataManager.FileManager fileManager = new Wis.Website.DataManager.FileManager();
            Wis.Website.DataManager.File file = new Wis.Website.DataManager.File();
            file.FileGuid = Guid.NewGuid();
            string destFilename = Server.MapPath(string.Format("~/Uploads/Photos/{0}/{1}{2}", System.DateTime.Now.ToShortDateString(), file.FileGuid, thumbnail.Extension));

#warning TODO:填写当前登录用户的UserName
            file.CreatedBy = string.Empty;
            file.CreationDate = System.DateTime.Now;
#warning TODO：如何提供描述？
            file.Description = string.Empty;
            file.Hits = 0;
            file.OriginalFileName = fileName;
            file.Rank = 0;
            file.SaveAsFileName = destFilename;
            file.Size = thumbnail.Length;
            file.SubmissionGuid = this.ArticleGuid;
            file.FileId = fileManager.AddNew(file);

            // 缩略图操作
            
            // PointX 和 PointY都不为空，则进行图片裁剪
            string requestPointX = Request["PointX"];
            string requestPointY = Request["PointY"];
            if (!string.IsNullOrEmpty(requestPointX) && !string.IsNullOrEmpty(requestPointY))
            {
                int pointX;
                int pointY;
                if (int.TryParse(requestPointX, out pointX) == false || int.TryParse(requestPointY, out pointY) == false)
                {
                    Warning.InnerHtml = "缩略图的PointX和PointY不为整数";
                    return;
                }

                // 裁剪图片
                Wis.Toolkit.Drawings.Imager.Crop(srcFilename, destFilename, pointX, pointY, this.ThumbnailWidth, this.ThumbnailHeight);
            }
            else
            {
                bool stretch = !string.IsNullOrEmpty(Request["Stretch"]);
                bool beveled = !string.IsNullOrEmpty(Request["Beveled"]);

                Wis.Toolkit.Drawings.Imager.Thumbnail(srcFilename, destFilename, this.ThumbnailWidth, this.ThumbnailHeight, stretch, beveled);
            }

            // 移除临时文件
#warning 移除临时文件
            //if (System.IO.File.Exists(srcFilename)) System.IO.File.Delete(srcFilename);

            // 下一步
            Response.Redirect(string.Format("ArticleRelease.aspx?ArticleGuid={0}", this.ArticleGuid));
        }
    }
}
