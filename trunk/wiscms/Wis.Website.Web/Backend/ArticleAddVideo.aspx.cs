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
using Wis.Toolkit;

namespace Wis.Website.Web.Backend
{
    public partial class ArticleAddVideo : System.Web.UI.Page
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
            Video.FileProcessor = fs;
            SourceImage.FileProcessor = fs;

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

            //if (article.Category.ArticleType != 3)
            //{
            //    Wis.Toolkit.ClientScript.Window.Alert("当前内容不为视频内容，请先设定好该新闻分类的类别");
            //    Wis.Toolkit.ClientScript.Window.Redirect(string.Format("CategoryUpdate.aspx?CategoryGuid={0}", article.Category.CategoryGuid));
            //    return;
            //}

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

            // DJUploadController1.Status.LengthExceeded
            // The maximum upload size was exceeded

            // Uploads were not processed via the module
            if (!UploadManager.Instance.ModuleInstalled)
            {
                Warning.InnerHtml = "请配置文件上传的 Module";
                return;
            }

            // Files uploaded
            // DJUploadController1.Status.UploadedFiles
            // 录入图片信息，进入下一步
            if (DJUploadController1.Status == null ||
                DJUploadController1.Status.ErrorFiles.Count > 0 ||  // Files with errors
                DJUploadController1.Status.UploadedFiles.Count != 2)
            {
                Warning.InnerHtml = "上传文件错误";
                return;
            }
            
            // 获得文件名
            UploadedFile videoUploadedFile = DJUploadController1.Status.UploadedFiles[0];
            UploadedFile sourceImageUploadedFile = DJUploadController1.Status.UploadedFiles[1];
            //// Exception
            //if (f.Exception != null)
            //{
            //    Warning.InnerHtml = string.Format("文件上传发生异常：{0}", f.Exception.Message);
            //    return;
            //}
            if (videoUploadedFile == null || sourceImageUploadedFile == null)
            {
                Warning.InnerHtml = "未正确获取上传的控件，请检查页面上传控件是否正确命名，视频文件上传控件的ID得为Video，图片文件上传控件的ID得为SourceImage";
                return;
            }

            //string fileName = f.FileName;// f.FileName "E:\\Tools\\visualxpath.zip"
            //int charIndex = fileName.LastIndexOf("\\");
            //if (charIndex == -1 || charIndex >= fileName.Length)
            //{
            //    return;
            //}
            //fileName = fileName.Substring(charIndex + 1);

            Wis.Website.DataManager.VideoArticle videoArticle = new Wis.Website.DataManager.VideoArticle();
            videoArticle.VideoArticleGuid = Guid.NewGuid();
            videoArticle.Article.ArticleGuid = article.ArticleGuid;

#warning TODO:填写当前登录用户的UserName
            // 获得 CreatedBy
            videoArticle.CreatedBy = string.Empty;

            // 获得 CreationDate
            DateTime creationDate = System.DateTime.Now;
            string requestCreationDate = Request["CreationDate"];
            if(!string.IsNullOrEmpty(requestCreationDate)) DateTime.TryParse(requestCreationDate, out creationDate);
            videoArticle.CreationDate = creationDate;

#warning TODO:Uploads 作为配置项

            string path = Server.MapPath(string.Format("/Uploads/Videos/{0}/", videoArticle.CreationDate.ToShortDateString()));
            if (!System.IO.Directory.Exists(path)) System.IO.Directory.CreateDirectory(path);

            // 1 获得视频路径
            string videoFilename = System.IO.Path.GetFileName(videoUploadedFile.FileName);
            string videoExtension = System.IO.Path.GetExtension(videoUploadedFile.FileName);
            videoFilename = Server.MapPath(string.Format("/Uploads/Temp/{0}", videoFilename));
            System.IO.FileInfo videoFileInfo = new System.IO.FileInfo(videoFilename);
            if (!videoFileInfo.Directory.Exists) videoFileInfo.Directory.Create();
            videoArticle.VideoPath = string.Format("/Uploads/Videos/{0}/{1}video{2}", videoArticle.CreationDate.ToShortDateString(), videoArticle.VideoArticleGuid, videoExtension);
            videoFilename = Server.MapPath(videoArticle.VideoPath);
            videoFileInfo.MoveTo(videoFilename);

            // 2 文件大小
            videoArticle.Size = videoFileInfo.Length; 

            // 4 获得 Rank
            byte rank;
            if (byte.TryParse(RequestManager.Request("Rank"), out rank))
            {
                if(rank != 0) videoArticle.Rank = rank;
            }

            // 3 获得源图路径
            string srcFilename = System.IO.Path.GetFileName(sourceImageUploadedFile.FileName);
            string srcExtension = System.IO.Path.GetExtension(sourceImageUploadedFile.FileName);
            srcFilename = Server.MapPath(string.Format("/Uploads/Temp/{0}", srcFilename));
            System.IO.FileInfo srcFileInfo = new System.IO.FileInfo(srcFilename);
            if (!srcFileInfo.Directory.Exists) srcFileInfo.Directory.Create();
            videoArticle.SourceImagePath = string.Format("/Uploads/Videos/{0}/{1}src{2}", videoArticle.CreationDate.ToShortDateString(), videoArticle.VideoArticleGuid, srcExtension);
            srcFilename = Server.MapPath(videoArticle.SourceImagePath);
            srcFileInfo.MoveTo(srcFilename);

            // 3 获得缩微图路径
            videoArticle.ThumbnailPath = string.Format("/Uploads/Videos/{0}/{1}{2}", videoArticle.CreationDate.ToShortDateString(), videoArticle.VideoArticleGuid, srcExtension);
            string destFilename = Server.MapPath(videoArticle.ThumbnailPath);
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

                videoArticle.PointX = pointX;
                videoArticle.PointY = pointY;

                // 裁剪图片
                Wis.Toolkit.Drawings.Imager.Crop(srcFilename, destFilename, pointX, pointY, this.ThumbnailWidth, this.ThumbnailHeight);
            }
            else
            {
                videoArticle.Stretch = !string.IsNullOrEmpty(Request["Stretch"]); // 拉伸
                videoArticle.Beveled = !string.IsNullOrEmpty(Request["Beveled"]); // 斜角
                Wis.Toolkit.Drawings.Imager.Thumbnail(srcFilename, destFilename, this.ThumbnailWidth, this.ThumbnailHeight, videoArticle.Stretch.Value, videoArticle.Beveled.Value);
            }
            
            // 图片信息入库
            Wis.Website.DataManager.VideoArticleManager videoArticleManager = new Wis.Website.DataManager.VideoArticleManager();
            videoArticle.VideoArticleId = videoArticleManager.AddNew(videoArticle);

            // 移除临时文件
#warning 移除临时文件
            //if (System.IO.File.Exists(srcFilename)) System.IO.File.Delete(srcFilename);

            // 下一步
            Response.Redirect(string.Format("ArticleRelease.aspx?ArticleGuid={0}", this.ArticleGuid));
        }
    }
}
