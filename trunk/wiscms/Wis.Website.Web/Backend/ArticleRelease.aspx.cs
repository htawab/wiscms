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
        }

        protected void ImageButtonNext_Click(object sender, ImageClickEventArgs e)
        {
            // 生成静态页面和关联页面
            DataManager.ReleaseManager releaseManager = new DataManager.ReleaseManager();
            releaseManager.ReleaseRelation(article);

            // 提示操作成功
            Warning.InnerHtml = string.Format("发布静态页成功，<a href='ArticleAddNew.aspx?CategoryGuid={0}'>继续添加新闻</a>，还是<a href='ArticleList.aspx?CategoryGuid={0}'>返回新闻列表页</a>？", article.Category.CategoryGuid);
#warning TODO:继续添加新闻，返回新闻列表页？在页面上放两个按钮，完成的按钮应隐藏
            //Response.Redirect("ArticleAddNew.aspx?CategoryGuid=" + article.Category.CategoryGuid);
        }
    }
}
