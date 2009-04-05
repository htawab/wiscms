using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace Wis.Website.Web.Backend
{
    /// <summary>
    /// 更新内容。
    /// </summary>
    public partial class ArticleUpdate : System.Web.UI.Page
    {
        /// <summary>
        /// 文章编号。
        /// </summary>
        protected Guid articleGuid;

        Wis.Website.DataManager.Article article = null;
        Wis.Website.DataManager.ArticleManager articleManager = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            // 1 获取分类的信息
            string requestArticleGuid = Request.QueryString["ArticleGuid"];
            if (string.IsNullOrEmpty(requestArticleGuid) || !Wis.Toolkit.Validator.IsGuid(requestArticleGuid))
            {
                Warning.InnerHtml = "不正确的文章编号";
                return;
            }

            if (articleManager == null) articleManager = new Wis.Website.DataManager.ArticleManager();
            Guid articleGuid = new Guid(requestArticleGuid);
            article = articleManager.GetArticleByArticleGuid(articleGuid);
            if (string.IsNullOrEmpty(article.Title))
            {
                Warning.InnerHtml = "未读取到文章信息";
                return;
            }

            HyperLinkCategory.Text = article.Category.CategoryName;
            HyperLinkCategory.NavigateUrl = string.Format("ArticleList.aspx?CategoryGuid={0}", article.Category.CategoryGuid);
        }

        /// <summary>
        /// 修改文章。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ImageButtonNext_Click(object sender, ImageClickEventArgs e)
        {
            // TODO:事务处理
            //articleManager.BeginTransaction();

            // 1 判断文章是否存在

            // 2.1 标题
            // TODO:注入式处理
            article.Title = Wis.Toolkit.RequestManager.Request("Title");
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

            // TODO:过滤非法字符和脏字词语
            //if (ArticleManager.HasBannedWord(articlePhoto.Title) || ArticleManager.HasBannedWord(articlePhoto.ContentHtml))
            //{
            //    Warning.InnerHtml = "对不起, 您提交的内容包含不良信息, 因此无法提交, 请返回修改!";
            //    return;
            //}

            // 3 录入内容
            article.ArticleGuid = Guid.NewGuid();
            // TODO:Author 注入式处理
            article.Author = TextBoxAuthor.Text.Replace("'", "\"");
            //article.Category = category;
            //article.Comments = 0;
            // TODO:ContentHtml 注入式处理
            article.ContentHtml = ContentHtml.Text;
#warning 更新日期
            //article.DateCreated = System.DateTime.Now;
            article.Editor = Guid.Empty; // TODO: 当前登录用户
            article.Hits = 0;
            // TODO:MetaDesc 注入式处理
            article.MetaDesc = TextBoxMetaDesc.Text.Replace("'", "\"");
            // TODO:MetaKeywords 注入式处理
            article.MetaKeywords = TextBoxMetaKeywords.Text.Replace("'", "\"");
            // TODO:Original 注入式处理
            article.Original = TextBoxOriginal.Text.Replace("'", "\"");
            article.Rank = 0;
            // TODO:SubTitle 注入式处理
            article.SubTitle = TextBoxSubTitle.Text;
            // TODO:Summary 注入式处理
            article.Summary = TextBoxSummary.Text;
            // TODO:TitleColor 注入式处理
            article.TitleColor = TitleColor.Value;
            article.Votes = 0;

            // article 入库
            article.ArticleId = articleManager.Update(article);

            // 4 修改主题
#warning 修改主题，测试
            string requestTags = Wis.Toolkit.RequestManager.Request("Tags"); // 获取主题
            Wis.Website.DataManager.TagManager tagManager = new Wis.Website.DataManager.TagManager();
            //tagManager.Update(article.ArticleGuid, requestTags);

            // 5 添加操作日志
            // TODO:完善，保存操作的对象
            DataManager.LogManager logManager = new DataManager.LogManager();
            logManager.AddNew(Guid.NewGuid(), Guid.Empty, "添加新闻", article.ArticleGuid, article.Title, System.DateTime.Now);

            // 6 下一步
            // TODO:创建 ArticleType 表，扩展文章类型
#warning 如果标题、时间、作者没有变化，则不用生成索引页
            switch (article.Category.ArticleType)
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
