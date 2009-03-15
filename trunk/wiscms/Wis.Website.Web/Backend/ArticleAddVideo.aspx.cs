using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Wis.Toolkit.WebControls.FileUploads;
using Wis.Toolkit;
using System.Diagnostics;
using System.Text.RegularExpressions;

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
            // 临时目录不存在，则创建
            string outputPath = "~/Uploads/Temp/";
            string physicalOutputPath = Server.MapPath(outputPath);
            if (!System.IO.Directory.Exists(physicalOutputPath))
                System.IO.Directory.CreateDirectory(physicalOutputPath);

#warning 在aspx中配置临时目录
            FileSystemProcessor fs = new FileSystemProcessor();
            fs.OutputPath = outputPath;
            DJUploadController1.DefaultFileProcessor = fs;
            VideoFile.FileProcessor = fs;

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
#warning 最大允许上传的文件尺寸
            // DJUploadController1.Status.LengthExceeded
            // The maximum upload size was exceeded

            // Uploads were not processed via the module
            if (!UploadManager.Instance.ModuleInstalled)
            {
                Warning.InnerHtml = "请配置文件上传的 Module";
                return;
            }

            // Files uploaded
            if (DJUploadController1.Status == null ||
                DJUploadController1.Status.ErrorFiles.Count > 0 ||  // Files with errors
                DJUploadController1.Status.UploadedFiles.Count != 1)
            {
                Warning.InnerHtml = "上传文件错误";
                return;
            }

            // 获得文件名
            UploadedFile videoUploadedFile = DJUploadController1.Status.UploadedFiles[0];
            //// Exception
            //if (f.Exception != null)
            //{
            //    Warning.InnerHtml = string.Format("文件上传发生异常：{0}", f.Exception.Message);
            //    return;
            //}
            if (videoUploadedFile == null)
            {
                Warning.InnerHtml = "未正确获取上传的控件对象";
                return;
            }

#warning TODO:Uploads 作为配置项
            string uploadsDirectory = "Uploads";
            uploadsDirectory.Trim('/'); // 去除前后的 /

            // 获得视频临时路径
            string inFile = Server.MapPath(string.Format("/{0}/Temp/{1}", uploadsDirectory, System.IO.Path.GetFileName(videoUploadedFile.FileName)));
            System.IO.FileInfo tempFileInfo = new System.IO.FileInfo(inFile);
            if (!tempFileInfo.Exists)
            {
                Warning.InnerHtml = "上传文件错误，请重新上传";
                return;
            }

            // 视频信息
            Wis.Website.DataManager.VideoArticle videoArticle = null;
            Wis.Website.DataManager.VideoArticleManager videoArticleManager = new Wis.Website.DataManager.VideoArticleManager();
            int videoArticleCount = videoArticleManager.Count(this.ArticleGuid);
            if (videoArticleCount == 0)
            {
                videoArticle = new Wis.Website.DataManager.VideoArticle();
                videoArticle.VideoArticleGuid = Guid.NewGuid();
                videoArticle.Article.ArticleGuid = article.ArticleGuid;
            }
            else
            {
                videoArticle = videoArticleManager.GetVideoArticle(this.ArticleGuid);

                // 删除已存在的文件
                if(System.IO.File.Exists(Page.MapPath(videoArticle.VideoPath)))
                    System.IO.File.Delete(Page.MapPath(videoArticle.VideoPath));
                if(System.IO.File.Exists(Page.MapPath(videoArticle.FlvVideoPath)))
                    System.IO.File.Delete(Page.MapPath(videoArticle.FlvVideoPath));
                if(System.IO.File.Exists(Page.MapPath(videoArticle.PreviewFramePath)))
                    System.IO.File.Delete(Page.MapPath(videoArticle.PreviewFramePath));
            }

            // 视频星级 Star
            byte star;
            if (byte.TryParse(RequestManager.Request("Star"), out star))
                if (star != 0) videoArticle.Star = star;

            // Videos 目录
            string path = Server.MapPath(string.Format("/{0}/Videos/{1}/", uploadsDirectory, article.DateCreated.ToShortDateString()));
            if (!System.IO.Directory.Exists(path)) System.IO.Directory.CreateDirectory(path);

            // 视频，Flv视频，预览帧 路径
            videoArticle.VideoPath = string.Format("/{0}/Videos/{1}/{2}{3}", uploadsDirectory, article.DateCreated.ToShortDateString(), this.ArticleGuid, tempFileInfo.Extension);
            videoArticle.FlvVideoPath = Regex.Replace(videoArticle.VideoPath, tempFileInfo.Extension, ".swf", RegexOptions.IgnoreCase);
            videoArticle.PreviewFramePath = Regex.Replace(videoArticle.VideoPath, tempFileInfo.Extension, ".jpg", RegexOptions.IgnoreCase);

            // 3 创建缩微图 88x66
            string ffmpegFile = Page.MapPath("Tools/ffmpeg.exe");
            MediaHandler mediaHandler = new MediaHandler();
            this.TotalSeconds = mediaHandler.GetTotalSeconds(ffmpegFile, inFile);
            int timeOffset = (this.TotalSeconds % 2 == 0) ? (int)(this.TotalSeconds / 2) : ((int)(this.TotalSeconds / 2) + 1);
            string outFile = Page.MapPath(videoArticle.PreviewFramePath);
            mediaHandler.CreatePreviewFrame(ffmpegFile, timeOffset, inFile, outFile, this.ThumbnailWidth, this.ThumbnailHeight, new DataReceivedEventHandler(MediaHandler_DataReceived));

            // 视频文件转移到 Videos 目录
            tempFileInfo.MoveTo(Server.MapPath(videoArticle.VideoPath));

            // 转换视频
            if (tempFileInfo.Extension.ToLower() == ".flv" || tempFileInfo.Extension.ToLower() == ".swf")
            {
                outFile = Page.MapPath(videoArticle.FlvVideoPath);
                if (tempFileInfo.Extension.ToLower() == ".flv")
                {
                    tempFileInfo.MoveTo(outFile);
                    videoArticle.VideoPath = videoArticle.FlvVideoPath;
                }
                // 添加 Meta Data信息 InjectMetadata
                string flvtool2File = Page.MapPath("Tools/flvtool2.exe");
                mediaHandler.InjectMetadata(flvtool2File, outFile, new DataReceivedEventHandler(MediaHandler_DataReceived));

                // 视频信息入库
                if (videoArticleCount == 0)
                    videoArticle.VideoArticleId = videoArticleManager.AddNew(videoArticle);
                else
                    videoArticle.VideoArticleId = videoArticleManager.Update(videoArticle);

                // 下一步
                //Wis.Toolkit.ClientScript.Window.Redirect(string.Format("ArticleRelease.aspx?ArticleGuid={0}", this.ArticleGuid));
                Response.Redirect(string.Format("ArticleRelease.aspx?ArticleGuid={0}", this.ArticleGuid));
            }
            else
            {
                System.Web.HttpContext.Current.Items.Add("VideoArticle", videoArticle);
                Server.Transfer(string.Format("ArticleConvertingVideo.aspx?ArticleGuid={0}", this.ArticleGuid));
                return;
            }
        }

        /// <summary>
        /// 视频总时长
        /// </summary>
        private double TotalSeconds;

        private void MediaHandler_DataReceived(object sender, System.Diagnostics.DataReceivedEventArgs e)
        {
            if (string.IsNullOrEmpty(e.Data)) return;
            Warning.InnerHtml = e.Data;
        }
    }
}
