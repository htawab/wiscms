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
using Wis.Toolkit;
using Wis.Website.DataManager;

namespace Wis.Website.Web.Backend
{
    public partial class ArticleAddNew : System.Web.UI.Page
    {
        Wis.Website.DataManager.Category category = null;
        Wis.Website.DataManager.CategoryManager categoryManager = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            // 1 获取分类的信息
            string requestCategoryGuid = Request.QueryString["CategoryGuid"];
            if (string.IsNullOrEmpty(requestCategoryGuid) || !Wis.Toolkit.Validator.IsGuid(requestCategoryGuid))
            {
                Warning.InnerHtml = "不正确的分类编号，请<a href='ArticleSelectCategory.aspx'>返回</a>选择分类";
                return;
            }

            if (categoryManager == null) categoryManager = new Wis.Website.DataManager.CategoryManager();
            Guid categoryGuid = new Guid(requestCategoryGuid);
            category = categoryManager.GetCategoryByCategoryGuid(categoryGuid);
            if (string.IsNullOrEmpty(category.CategoryName))
            {
                Warning.InnerHtml = "未读取到分类信息，请<a href='ArticleSelectCategory.aspx'>返回</a>选择分类";
                return;
            }

            HyperLinkCategory.Text = category.CategoryName;
            HyperLinkCategory.NavigateUrl = string.Format("ArticleList.aspx?CategoryGuid={0}", category.CategoryGuid);
        }

        protected void ImageButtonNext_Click(object sender, ImageClickEventArgs e)
        {
            // 添加内容，下一步
            if (string.IsNullOrEmpty(category.CategoryName))
            {
                Warning.InnerHtml = "未读取到分类信息，请<a href='ArticleSelectCategory.aspx'>返回</a>选择分类";
                return;
            }

            // 2 判断并校验表单中各录入项的值
            Wis.Website.DataManager.Article article = new Wis.Website.DataManager.Article();

            // TODO:事务处理
            //articleManager.BeginTransaction();

            // 2.1 标题
            // TODO:注入式处理
            article.Title = RequestManager.Request("Title");
            // 2.1.1 判断标题为空
            if (string.IsNullOrEmpty(article.Title))
            {
                Warning.InnerHtml = "标题不能为空";
                return;
            }
            // 2.1.3 判断标题是否包含全角空格符
            if (article.Title.IndexOf("　") != -1)
            {
                Warning.InnerHtml = "标题不能包含全角空格符";
                return;
            }
            // 2.1.3 判断标题长度
            // TODO:验表单中各录入项的值的长度判断，如果内容过长，数据库会截断或抛异常
            // 可以参考：http://www.china-aspx.com/ShowArticle.aspx?ArticleID=181
            if (article.Title.Length > 128)
            {
                Warning.InnerHtml = string.Format("标题最大长度为128个字符,当前为 {0} 个字符", article.Title.Length);
                return;
            }
            // 2.1.4 判断标题是否重复
            Wis.Website.DataManager.ArticleManager articleManager = new Wis.Website.DataManager.ArticleManager();
            int count = articleManager.CountArticlesByTitle(article.Title);
            if (count > 0)
            {
                Warning.InnerHtml = "标题不能重复";
                return;
            }

            // TODO:过滤非法字符和脏字词语
            //if (ArticleManager.HasBannedWord(articlePhoto.Title) || ArticleManager.HasBannedWord(articlePhoto.ContentHtml))
            //{
            //    Warning.InnerHtml = "对不起, 您提交的内容包含不良信息, 因此无法提交, 请返回修改!";
            //    return;
            //}

            // 3 录入内容
            article.ArticleGuid = Guid.NewGuid();
            // TODO:Author 注入式处理
            article.Author = Author.Value.Replace("'", "\"");
            article.Category = category;
            article.Comments = 0;
            // TODO:ContentHtml 注入式处理
            article.ContentHtml = ContentHtml.Text;
            article.DateCreated = System.DateTime.Now;
            article.Editor = Guid.Empty; // TODO: 当前登录用户
            article.Hits = 0;
            // TODO:MetaDesc 注入式处理
            article.MetaDesc = MetaDesc.Value.Replace("'", "\"");
            // TODO:MetaKeywords 注入式处理
            article.MetaKeywords = MetaKeywords.Value.Replace("'", "\"");
            // TODO:Original 注入式处理
            article.Original = Original.Value.Replace("'", "\"");
            article.Rank = 0;
            // TODO:SubTitle 注入式处理
            article.SubTitle = SubTitle.Value;
            // TODO:Summary 注入式处理
            article.Summary = Summary.Value;
            // TODO:TitleColor 注入式处理
            article.TitleColor = TitleColor.Value;
            article.Votes = 0;

            // articlePhoto 入库
            article.ArticleId = articleManager.AddNew(article);

            // 4 添加主题
            string requestTags = RequestManager.Request("Tags"); // 获取主题
            TagManager tagManager = new TagManager();
            tagManager.AddNew(article.ArticleGuid, requestTags);

            // 5 添加操作日志
            // TODO:完善，保存操作的对象
            DataManager.LogManager logManager = new DataManager.LogManager();
            logManager.AddNew(Guid.NewGuid(), Guid.Empty, "添加新闻", article.ArticleGuid, article.Title, System.DateTime.Now);

            // 6 下一步
            // TODO:创建 ArticleType 表，扩展文章类型 
            switch (category.ArticleType)
            {
                case 1: // 普通新闻
                    Response.Redirect("ArticleRelease.aspx?ArticleGuid=" + article.ArticleGuid);
                    break;
                case 2:// 图片新闻
                    Response.Redirect("ArticleAddPhoto.aspx?ArticleGuid=" + article.ArticleGuid);
                    break;
                case 3:// 视频新闻
                    Response.Redirect("ArticleAddVideo.aspx?ArticleGuid=" + article.ArticleGuid);
                    break;
                case 4:// 软件
                    Response.Redirect("ArticleAddSoft.aspx?ArticleGuid=" + article.ArticleGuid);
                    break;
                default:
                    Response.Redirect("ArticleRelease.aspx?ArticleGuid=" + article.ArticleGuid);
                    break;
            }
        }
    }
}
