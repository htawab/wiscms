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
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Wis.Website.DataManager;


namespace Wis.Website.Web.Backend
{
    public partial class ArticleList : BackendPage
    {
        private string releaseDirectory;
        /// <summary>
        /// 发布路径
        /// </summary>
        protected string ReleaseDirectory
        {
            get
            {
                if (string.IsNullOrEmpty(releaseDirectory))
                {
                    string applicationPath = System.Web.HttpContext.Current.Request.ApplicationPath.Trim('/');
                    if (!applicationPath.EndsWith("/")) applicationPath += "/"; // 前后导都有 /
                    releaseDirectory = string.Format("{0}{1}", applicationPath, System.Configuration.ConfigurationManager.AppSettings["ReleaseRootDirectory"].Trim('/'));
                }
             
                return releaseDirectory;
            }
        }

        protected int PageSize
        {
            get
            {
                return 18;
            }
        }

        private Guid categoryGuid;
        private Wis.Website.DataManager.Category category = null;
        private Wis.Website.DataManager.CategoryManager categoryManager = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            // 获取分类编号
            string requestCategoryGuid = Request.QueryString["CategoryGuid"];
            if (Wis.Toolkit.Validator.IsGuid(requestCategoryGuid))
                categoryGuid = new Guid(requestCategoryGuid);
            else
                categoryGuid = Guid.Empty;

            if (!Page.IsPostBack)
            {
                BindRepeater();
            }

            if (!categoryGuid.Equals(Guid.Empty))
            {
                if (categoryManager == null) categoryManager = new Wis.Website.DataManager.CategoryManager();
                category = categoryManager.GetCategoryByCategoryGuid(categoryGuid);
            }
            // 管理所在位置 MySiteMapPath
            List<KeyValuePair<string, Uri>> nodes = new List<KeyValuePair<string, Uri>>();
            if (category != null)
            {
                nodes.Add(new KeyValuePair<string, Uri>(category.CategoryName, new Uri(Request.Url, string.Format("ArticleList.aspx?CategoryGuid={0}", category.CategoryGuid))));
            }
            nodes.Add(new KeyValuePair<string, Uri>("内容管理", Request.Url));
            ((Wis.Toolkit.SiteMapDataProvider)SiteMap.Provider).Stack(nodes);
        }

        /// <summary>
        /// 绑定新闻列表。
        /// </summary>
        private void BindRepeater()
        {
            // 获取 PageIndex
            int pageIndex;
            string requestPageIndex = Request.QueryString["PageIndex"];
            if (int.TryParse(requestPageIndex, out pageIndex) == false)
                pageIndex = 1;

            string keywords = (Request.QueryString["Keywords"] == null) ? string.Empty : Request.QueryString["Keywords"].Trim();

            // 获取新闻
            Wis.Website.DataManager.ArticleManager articleManager = new Wis.Website.DataManager.ArticleManager();
            List<Wis.Website.DataManager.Article> articles = Wis.Website.DataManager.ArticleManager.GetArticlesByKeywords(keywords, categoryGuid, pageIndex, this.PageSize);

            MiniPager1.RecordCount = articleManager.CountArticlesByKeywords(keywords, categoryGuid);
            MiniPager1.PageIndex = pageIndex;
            MiniPager1.PageSize = this.PageSize;
            string pattern = @"PageIndex=\d+$";
            if (Regex.IsMatch(Request.RawUrl, pattern))
            {
                MiniPager1.UrlPattern = Regex.Replace(Request.RawUrl, pattern, "PageIndex={0}");
            }
            else
            {
                if (Request.RawUrl.IndexOf("?") == -1)
                    MiniPager1.UrlPattern = Request.RawUrl + "?PageIndex={0}";
                else
                    MiniPager1.UrlPattern = Request.RawUrl + "&PageIndex={0}";
            }

            RepeaterArticleList.DataSource = articles;
            RepeaterArticleList.DataBind();
        }


        /// <summary>
        /// 删除文章。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void LinkButtonDelete_Click(object sender, CommandEventArgs e)
        {
#warning 使用 ArticleGuid
            int articleId;
            if(int.TryParse(e.CommandName, out articleId) == false)
            {
                MessageBox("错误提示", "文章编号必须为整数");
                return;
            }
            Wis.Website.DataManager.ArticleManager articleManager = new Wis.Website.DataManager.ArticleManager();
            Wis.Website.DataManager.Article article = articleManager.GetArticle(articleId);

            // 重新生成静态页面和关联页面
            DataManager.ReleaseManager releaseManager = new DataManager.ReleaseManager();
            switch (article.Category.ArticleType)
            {
                case 1://ArticleType.Normal:
                    releaseManager.ReleasingRemovedArticle(article);
                    break;
                case 2://ArticleType.Photo:
                    ArticlePhotoManager articlePhotoManager = new ArticlePhotoManager();
                    ArticlePhoto articlePhoto = articlePhotoManager.GetArticlePhoto(article.ArticleGuid);
                    releaseManager.ReleasingRemovedPhotoArticle(articlePhoto);
                    break;
                case 3://ArticleType.Video:
                    VideoArticleManager videoArticleManager = new VideoArticleManager();
                    VideoArticle videoArticle = videoArticleManager.GetVideoArticle(article.ArticleGuid);
                    releaseManager.ReleasingRemovedVideoArticle(videoArticle);
                    break;
                //case ArticleType.Soft:
                //    releaseManager.ReleasingRemovedSoftArticle(article);
                //    break;
                //case ArticleType.Link:
                //    releaseManager.ReleasingRemovedLinkArticle(article);
                //    break;
            }

#warning 删除与该文章相关的图片信息、视频信息、软件信息、
            int iExecuteNonQuery = articleManager.Remove(article.ArticleGuid);
            if (iExecuteNonQuery > 0)
            {
                MessageBox("操作提示", "删除成功！");
            }
            else
            {
                MessageBox("操作提示", "删除失败！");
            }

            // 添加操作日志
            DataManager.LogManager logManager = new DataManager.LogManager();
            logManager.AddNew(Guid.NewGuid(), Guid.Empty, "删除新闻", article.ArticleGuid, article.Title, System.DateTime.Now);

            // 重新绑定新闻列表
            BindRepeater();
        }
    }
}
