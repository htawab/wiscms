using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.IO;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Collections.Generic;
using Wis.Toolkit;
using Wis.Website.DataManager;

namespace Wis.Website.Web.Backend.Article
{ 
    public partial class ArticleAdd : System.Web.UI.Page
    {
        private const string CallScriptKey = "CallMessageBox";
        /// <summary>
        /// 输出标题和消息。
        /// </summary>
        /// <param name="title">标题。</param>
        /// <param name="message">消息。</param>
        public void MessageBox(string title, string message)
        {
            if (!this.Page.ClientScript.IsStartupScriptRegistered(CallScriptKey))
            {
                string scriptBlock = string.Format("\n<script language='JavaScript' type='text/javascript'><!--\nMessageBox.init('{0}', '{1}');\n//--></script>\n", title, message);
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), CallScriptKey, scriptBlock);
            }
        }

        private Wis.Website.DataManager.Category category = null;
        private Wis.Website.DataManager.CategoryManager categoryManager = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            string requestCategoryGuid = Request.QueryString["CategoryGuid"];
            if(categoryManager == null) categoryManager = new Wis.Website.DataManager.CategoryManager();
            DropdownMenuCategory.MenuItems = categoryManager.GetCategoryMenuItems();

            if (!Page.IsPostBack)
            {
                // 获取分类的信息
                if (Wis.Toolkit.Validator.IsGuid(requestCategoryGuid))
                {
                    Guid categoryGuid = new Guid(requestCategoryGuid);
                    category = categoryManager.GetCategoryByCategoryGuid(categoryGuid);
                    if (!string.IsNullOrEmpty(category.CategoryName))
                    {
                        DropdownMenuCategory.Text = category.CategoryName;
                        DropdownMenuCategory.Value = category.CategoryGuid.ToString();
                    }
                }

                // 提交表单前检测
                this.btnOK.Attributes.Add("onclick", "javascript:return CheckArticle();");
            }

            // 管理所在位置 MySiteMapPath
            List<KeyValuePair<string, Uri>> nodes = new List<KeyValuePair<string, Uri>>();
            if (category != null)
            {
                nodes.Add(new KeyValuePair<string, Uri>(category.CategoryName, new Uri(Request.Url, string.Format("ArticleList.aspx?CategoryGuid={0}", category.CategoryGuid))));
            }
            nodes.Add(new KeyValuePair<string, Uri>("新增内容", Request.Url));
            ((Wis.Toolkit.SiteMapDataProvider)SiteMap.Provider).Stack(nodes);
        }

        protected void btnOK_Click(object sender, EventArgs e)
        {
            // 获取分类的信息
            if (string.IsNullOrEmpty(DropdownMenuCategory.Value) || !Wis.Toolkit.Validator.IsGuid(DropdownMenuCategory.Value))
            {
                MessageBox("错误提示", "请输入分类信息");
                return;
            }

            Guid categoryGuid = new Guid(DropdownMenuCategory.Value);
            category = categoryManager.GetCategoryByCategoryGuid(categoryGuid);
            if (string.IsNullOrEmpty(category.CategoryName))// 没有读取到分类信息
            {
                MessageBox("错误提示", "未读取到分类信息");
                return;
            }

            // TODO:判断并校验表单中各录入项的值
            Wis.Website.DataManager.Article article = new Wis.Website.DataManager.Article();
            // article.ArticleId 数据库自动生成

            // 内容类型 ArticleType
            // TODO:支持图片、视频和软件
            if (ArticleType0.Checked) article.ArticleType = Wis.Website.DataManager.ArticleType.Normal;
            if (ArticleType1.Checked)
            {
                //article.ImagePath = this.ImagePath.Value;
                article.ArticleType = Wis.Website.DataManager.ArticleType.Image;
            }
            if (ArticleType2.Checked)
            {
                article.ImagePath = this.TabloidPathVideo.Value;
                article.ArticleType = Wis.Website.DataManager.ArticleType.Video;
            }
            if (ArticleType3.Checked)
            {
                // TODO:article.ImagePath = this.TabloidPathVideo.Value;
                article.ArticleType = Wis.Website.DataManager.ArticleType.Soft;
            }

            // 注入式脚本处理
            article.Title = RequestManager.Request("Title");
            if (string.IsNullOrEmpty(article.Title))
            {
                MessageBox("错误提示", "标题不能为空");
                return;
            }
            if (article.Title.Length > 128)
            {
                MessageBox("错误提示", string.Format("标题最大长度为128个字符,当前为 {0} 个字符", article.Title.Length));
                return;
            }
            if (article.Title.IndexOf("　") != -1)
            {
                MessageBox("错误提示", "标题不能包含全角空格符");
                return;
            }
            // 判断标题是否重复
            Wis.Website.DataManager.ArticleManager articleManager = new Wis.Website.DataManager.ArticleManager();
            int count = articleManager.CountArticlesByTitle(article.Title);
            if (count > 0)
            {
                MessageBox("错误提示", "标题不能重复");
                return;
            }
            // TODO:过滤非法字符和脏字词语
            //if (ArticleManager.HasBannedWord(article.Title) || ArticleManager.HasBannedWord(article.ContentHtml))
            //{
            //    MessageBox("错误提示", "对不起, 您提交的内容包含不良信息, 因此无法提交, 请返回修改!");
            //    return;
            //}


            // TODO:事务处理
            //articleManager.BeginTransaction();


            // TODO:验表单中各录入项的值的长度判断，如果内容过长，数据库会截断或抛异常
            // 可以参考：http://www.china-aspx.com/ShowArticle.aspx?ArticleID=181
            article.ArticleGuid = Guid.NewGuid();
            article.Author = Author.Value.Replace("'", "\"");
            article.Category = category;
            article.Comments = 0;
            article.ContentHtml = ContentHtml.Text;
            article.DateCreated = System.DateTime.Now;
            article.Editor = Guid.Empty; // TODO: 当前登录用户
            article.Hits = 0;
            // article.ImageHeight
            // article.ImagePath
            // article.ImageWidth
            article.MetaDesc = MetaDesc.Value.Replace("'", "\"");
            article.MetaKeywords = MetaKeywords.Value.Replace("'", "\"");
            article.Original = Original.Value.Replace("'", "\"");
            article.Rank = 0;
            article.SpecialGuid = Guid.Empty; // TODO:
            article.SubTitle = SubTitle.Value;
            article.Summary = Summary.Value;
            article.TitleColor = TitleColor.Value;
            article.Votes = 0;

            // article 入库，获取 ArticleId ，补全了 article 对象的ArticleId
            article.ArticleId = articleManager.AddNew(article);
            
            // 添加标记
            string requestTags = RequestManager.Request("Tags"); // 获取标记
            TagManager tagManager = new TagManager();
            tagManager.AddNew(article.ArticleGuid, requestTags);

            // TODO:需要事务处理，如果生成页面失败，那新增新闻也失败
            // 生成静态页面和关联页面
            DataManager.ReleaseManager releaseManager = new DataManager.ReleaseManager();
            releaseManager.ReleaseRelation(article);

            // 添加操作日志
            DataManager.LogManager logManager = new DataManager.LogManager();
            logManager.AddNew(Guid.NewGuid(), Guid.Empty, "添加新闻", article.ArticleGuid, article.Title, System.DateTime.Now);

            // 跳转
            Response.Redirect("ArticleAdd.aspx?CategoryGuid=" + DropdownMenuCategory.Value);
        }
    }
}