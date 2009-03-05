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
            // 获取文章信息
            string requestArticleGuid = Request.QueryString["ArticleGuid"];
            requestArticleGuid = "f6f21612-7973-451a-be29-26f15700b36a";
            if (string.IsNullOrEmpty(requestArticleGuid) || !Wis.Toolkit.Validator.IsGuid(requestArticleGuid))
            {
                Warning.InnerHtml = "不正确的文章编号，请<a href='ArticleSelectCategory.aspx'>返回</a>重新操作";
                return;
            }

            if (article == null)
            {
                if (articleManager == null) articleManager = new Wis.Website.DataManager.ArticleManager();
                Guid articleGuid = new Guid(requestArticleGuid);
                article = articleManager.GetArticleByArticleGuid(articleGuid);
            }

            if (!article.Category.ImageWidth.HasValue || !article.Category.ImageHeight.HasValue)
            {
                Warning.InnerHtml = "缩微图的宽度和高度需要指定，请<a href='CategoryUpdate.aspx?CategoryGuid={0}' target='_blank'>点击这里</a>设定";
                return;
            }
            this.ThumbnailWidth = article.Category.ImageWidth.Value;
            this.ThumbnailHeight = article.Category.ImageHeight.Value;
        }

        protected void ImageButtonNext_Click(object sender, ImageClickEventArgs e)
        {
            // 录入图片信息，进入下一步

        }
    }
}
