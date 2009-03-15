using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Wis.Website.DataManager;
using System.Diagnostics;
using Wis.Toolkit;

namespace Wis.Website.Web.Backend
{
    public partial class ArticleConvertingVideo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string requestArticleGuid = Request.QueryString["ArticleGuid"];
            if (string.IsNullOrEmpty(requestArticleGuid) || !Wis.Toolkit.Validator.IsGuid(requestArticleGuid))
            {
                throw new System.ArgumentNullException("ArticleGuid");
            }
            Guid articleGuid = new Guid(requestArticleGuid);
            if (System.Web.HttpContext.Current.Items["VideoArticle"] == null)
            {
                Response.Redirect(string.Format("ArticleAddVideo.aspx?ArticleGuid={0}", articleGuid));
                return;
            }
            VideoArticle videoArticle = System.Web.HttpContext.Current.Items["VideoArticle"] as VideoArticle;

            // 转换视频格式
            // 输出进度条
            string content;
            using (System.IO.StreamReader reader = new System.IO.StreamReader(Page.MapPath("Progressbar.htm"), System.Text.Encoding.UTF8))
            {
                content = reader.ReadToEnd();
                System.Text.RegularExpressions.Regex.Replace(content, "", this.TotalSeconds.ToString(), System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            }
            Context.Response.Write(content);
            Context.Response.Flush();

            string inFile = Page.MapPath(videoArticle.VideoPath);
            string outFile = Page.MapPath(videoArticle.FlvVideoPath);

            string ffmpegFile = Page.MapPath("Tools/ffmpeg.exe");
            MediaHandler mediaHandler = new MediaHandler();
            this.TotalSeconds = mediaHandler.GetTotalSeconds(ffmpegFile, inFile);

            outFile = Page.MapPath(videoArticle.FlvVideoPath);
            mediaHandler.ConvertingVideo(ffmpegFile, inFile, outFile, new DataReceivedEventHandler(ConvertingVideo_DataReceived));

            System.Threading.Thread.Sleep(1000);
            string flvtool2File = Page.MapPath("Tools/flvtool2.exe");
            mediaHandler.InjectMetadata(flvtool2File, outFile, new DataReceivedEventHandler(ConvertingVideo_DataReceived));

            content = "<script type='text/javascript' language='javascript'>AddLog('转换成功。');SetProgressbar('100');</script>\n";
            Response.Write(content);
            Response.Flush();

            // 视频信息入库
            Wis.Website.DataManager.VideoArticleManager videoArticleManager = new Wis.Website.DataManager.VideoArticleManager();
            int videoArticleCount = videoArticleManager.Count(videoArticle.Article.ArticleGuid);
            if (videoArticleCount == 0)
                videoArticle.VideoArticleId = videoArticleManager.AddNew(videoArticle);
            else
                videoArticle.VideoArticleId = videoArticleManager.Update(videoArticle);

            System.Web.HttpContext.Current.Items.Remove("VideoArticle");

            // 下一步
            Wis.Toolkit.ClientScript.Window.Redirect(string.Format("ArticleRelease.aspx?ArticleGuid={0}", videoArticle.Article.ArticleGuid));
            //Response.Redirect(string.Format("ArticleRelease.aspx?ArticleGuid={0}", this.ArticleGuid));
        }

        /// <summary>
        /// 视频总时长
        /// </summary>
        private double TotalSeconds;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ConvertingVideo_DataReceived(object sender, DataReceivedEventArgs e)
        {
            if (string.IsNullOrEmpty(e.Data)) return;
            string content = string.Format("<script type='text/javascript' language='javascript'>AddLog('{0}');</script>\n", e.Data.Replace("'", "\\'"));
            Response.Write(content);
            Response.Flush();

            if (!e.Data.Contains("time=")) return;
            System.Text.RegularExpressions.Match m = System.Text.RegularExpressions.Regex.Match(e.Data, @"time=(\S+)");
            if (m.Success == false) return;
            double currentTime = double.Parse(m.Groups[1].Value);
            int progress = (int)(currentTime * 100 / this.TotalSeconds);
            if (progress > 100) progress = 100;
            content = string.Format("<script type='text/javascript' language='javascript'>SetProgressbar('{0}');</script>\n", progress);
            Response.Write(content);
            Response.Flush();
        }

    }
}
