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
using Wis.Website.DataManager;
using System.Collections.Generic;

namespace Wis.Website.Web.Backend
{
    public partial class ArticleRelease : System.Web.UI.Page
    {
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
        DataManager.ReleaseManager releaseManager = null;
        protected void Page_Load(object sender, EventArgs e)
        {
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

            if (releaseManager == null)
            {
                releaseManager = new DataManager.ReleaseManager();
                List<Release> releases = releaseManager.GetReleasesByCategory(article.Category.CategoryGuid);
                RepeaterReleaseList.DataSource = releases;
                RepeaterReleaseList.DataBind();
            }
        }

        protected void ImageButtonNext_Click(object sender, ImageClickEventArgs e)
        {
            // 生成静态页面和关联页面
            switch (article.Category.ArticleType)
            {
                case 1: // 普通新闻
                    releaseManager.ReleaseRelation(article);
                    break;
                case 2:// 图片新闻
                    Wis.Website.DataManager.ArticlePhotoManager articlePhotoManager = new Wis.Website.DataManager.ArticlePhotoManager();
                    Wis.Website.DataManager.ArticlePhoto articlePhoto = articlePhotoManager.GetArticlePhoto(this.ArticleGuid);
                    releaseManager.ReleaseArticlePhotoRelation(articlePhoto);
                    break;
                case 3:// 视频新闻
                    Wis.Website.DataManager.VideoArticleManager videoArticleManager = new Wis.Website.DataManager.VideoArticleManager();
                    Wis.Website.DataManager.VideoArticle videoArticle = videoArticleManager.GetVideoArticle(this.ArticleGuid);
                    releaseManager.ReleaseVideoArticleRelation(videoArticle);
                    break;
                case 4:// 软件
                    releaseManager.ReleaseRelation(article);
                    break;
                default:
                    releaseManager.ReleaseRelation(article);
                    break;
            }

            // 提示操作成功
            Warning.InnerHtml = string.Format("发布静态页成功，<a href='ArticleAddNew.aspx?CategoryGuid={0}'>继续添加新闻</a>，还是<a href='ArticleList.aspx?CategoryGuid={0}'>返回新闻列表页</a>？", article.Category.CategoryGuid);
#warning TODO:继续添加新闻，返回新闻列表页？在页面上放两个按钮，完成的按钮应隐藏
            //Response.Redirect("ArticleAddNew.aspx?CategoryGuid=" + articlePhoto.Category.CategoryGuid);
        }
    }
}
