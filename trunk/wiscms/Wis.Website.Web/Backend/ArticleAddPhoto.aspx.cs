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

        Wis.Website.DataManager.Article article = null;
        Wis.Website.DataManager.ArticleManager articleManager = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            FileSystemProcessor fs = new FileSystemProcessor();
            fs.OutputPath = Server.MapPath(this.OutputPath);
            DJUploadController1.DefaultFileProcessor = fs;

            // 获取文章信息
            string requestArticleGuid = Request.QueryString["ArticleGuid"];
            requestArticleGuid = "f6f21612-7973-451a-be29-26f15700b36a";
            if (string.IsNullOrEmpty(requestArticleGuid) || !Wis.Toolkit.Validator.IsGuid(requestArticleGuid))
            {
                Warning.InnerHtml = "不正确的文章编号，请<a href='ArticleSelectCategory.aspx'>点击这里</a>重新操作";
                return;
            }

            if (article == null)
            {
                if (articleManager == null) articleManager = new Wis.Website.DataManager.ArticleManager();
                Guid articleGuid = new Guid(requestArticleGuid);
                article = articleManager.GetArticleByArticleGuid(articleGuid);
            }

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
            // 录入图片信息，进入下一步

            // TODO:确保上传了缩略图

        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            if (Page.IsPostBack && DJUploadController1.Status != null)
            {
                string applicationPath = string.Format("{0}/{1}",
                    Page.Request.Url.AbsoluteUri.Substring(0, Page.Request.Url.AbsoluteUri.IndexOf(Page.Request.Path)).TrimEnd('/'),
                    this.Page.Request.ApplicationPath.TrimStart('/')).TrimEnd('/');

                foreach (UploadedFile f in DJUploadController1.Status.UploadedFiles)
                {
                    // f.FileName "E:\\Tools\\visualxpath.zip"
                    string fileName = f.FileName;
                    int charIndex = fileName.LastIndexOf("\\");
                    if (charIndex > -1 && charIndex < fileName.Length)
                    {
                        // 切割图片
                        string requestPointX = Request["PointX"];
                        string requestPointY = Request["PointY"];//Photo
                        if (!string.IsNullOrEmpty(requestPointX) && !string.IsNullOrEmpty(requestPointY))
                        {
                            // 上传的文件
                            fileName = fileName.Substring(charIndex + 1);

                            // TODO:文件格式判断

                            string srcFilename = string.Format("{1}{2}", this.OutputPath.TrimStart('~'), fileName);
                            string destFilename = "";

                            // PointX 和 PointY都不为空，则进行图片裁剪
                            int pointX;
                            int pointY;
                            if (int.TryParse(requestPointX, out pointX) == false || int.TryParse(requestPointY, out pointY) == false)
                            {
                                //MessageBox("错误提示", "缩略图的PointX和PointY不为整数");
                                return;
                            }

                            // 裁剪图片
                            int cropperWidth = article.Category.ThumbnailWidth.Value;
                            int cropperHeight = article.Category.ThumbnailHeight.Value;
                            Wis.Toolkit.Drawings.ImageCropper.Crop(srcFilename, destFilename, pointX, pointY, cropperWidth, cropperHeight);

                            Wis.Website.DataManager.FileManager fileManager = new Wis.Website.DataManager.FileManager();
                            Wis.Website.DataManager.File file = new Wis.Website.DataManager.File();
                            file.CreatedBy = string.Empty; // TODO:填写当前登录用户的UserName
                            file.CreationDate = System.DateTime.Now;
                            file.Description = string.Empty; // TODO：如何提供描述？
                            file.FileGuid = Guid.NewGuid();
                            file.Hits = 0;
                            file.OriginalFileName = fileName;
                            file.Rank = 0;
                            file.SaveAsFileName = string.Format("{1}{2}", this.OutputPath.TrimStart('~'), fileName);
                            System.IO.FileInfo saveAsFileInfo = new System.IO.FileInfo(Server.MapPath(file.SaveAsFileName));
                            file.Size = saveAsFileInfo.Length;
                            //file.SubmissionGuid = this.ArticleGuid;
                            fileManager.AddNew(file);
                        }
                    }
                }
            }
        }

        private string OutputPath
        {
            get
            {
                string outputPath = string.Format("~/Uploads/Thumbnail/{0}/", System.DateTime.Now.ToShortDateString());
                string physicalOutputPath = Server.MapPath(outputPath);
                if (!System.IO.Directory.Exists(physicalOutputPath)) System.IO.Directory.CreateDirectory(physicalOutputPath);
                return outputPath;
            }
        }
    }
}
