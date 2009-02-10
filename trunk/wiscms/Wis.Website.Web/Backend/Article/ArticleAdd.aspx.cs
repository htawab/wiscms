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

namespace Wis.Website.Web.Backend.Article
{ 
    public partial class ArticleAdd : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Wis.Website.DataManager.CategoryManager categoryManager = new Wis.Website.DataManager.CategoryManager();
                Category.MenuItems = categoryManager.GetCategoryMenuItems();

                // 获取分类的信息
                string requestCategoryGuid = Request.QueryString["CategoryGuid"];
                if (Wis.Toolkit.Validator.IsGuid(requestCategoryGuid))
                {
                    Guid categoryGuid = new Guid(requestCategoryGuid);
                    Wis.Website.DataManager.Category category = categoryManager.GetCategoryByCategoryGuid(categoryGuid);
                    if (!string.IsNullOrEmpty(category.CategoryName))
                    {
                        Category.Text = category.CategoryName;
                        Category.Value = category.CategoryGuid.ToString();
                        daohang.InnerText = category.CategoryName;
                        jiantou.InnerText = " » ";
                    }
                }

                // 读取 Template 表中 “TemplateType 为 Item ; ArticleType为选定内容类型”的模版
                Wis.Website.DataManager.ArticleType articleType = Wis.Website.DataManager.ArticleType.Normal;
                if (ArticleType0.Checked) articleType = Wis.Website.DataManager.ArticleType.Normal;
                if (ArticleType1.Checked) articleType = Wis.Website.DataManager.ArticleType.Image;
                if (ArticleType2.Checked) articleType = Wis.Website.DataManager.ArticleType.Video;
                if (ArticleType3.Checked) articleType = Wis.Website.DataManager.ArticleType.Soft;
                Wis.Website.DataManager.TemplateType templateType = Wis.Website.DataManager.TemplateType.Item;
                Wis.Website.DataManager.TemplateManager templateManager = new Wis.Website.DataManager.TemplateManager();
                List<Wis.Website.DataManager.Template> templates = templateManager.GetTemplates(articleType, templateType);
                TemplatePaths.DataSource = templates;
                TemplatePaths.DataBind();

                // 提交表单前检测
                this.btnOK.Attributes.Add("onclick", "javascript:return checkNews();");
            }            
        }

        protected void btnOK_Click(object sender, EventArgs e)
        {
            // 获取分类的信息
            if (string.IsNullOrEmpty(Category.Value))
            {
                Response.Write("<script language='javascript'>alert ('请输入分类信息!');window.close();</script>");
                return;
            }
            if (!Wis.Toolkit.Validator.IsGuid(Category.Value))
            {
                Response.Write("<script language='javascript'>alert ('请输入分类信息!');window.close();</script>");
                return;
            }

            Wis.Website.DataManager.CategoryManager categoryManager = new Wis.Website.DataManager.CategoryManager();
            Guid categoryGuid = new Guid(Category.Value);
            Wis.Website.DataManager.Category category = categoryManager.GetCategoryByCategoryGuid(categoryGuid);
            if (!string.IsNullOrEmpty(category.CategoryName))// 没有读取到分类信息
            {
                Response.Write("<script language='javascript'>alert ('未读取到分类信息!');window.close();</script>");
                return;
                // 报错
            }

            // TODO:判断并校验表单中各录入项的值

            Wis.Website.DataManager.Article article = new Wis.Website.DataManager.Article();
            // article.ArticleId 数据库自动生成


            // 内容类型 ArticleType
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
            
            article.Title = title.Value.Replace("'","\"");
            // TODO:注入式脚本处理，过滤非法字符

            // 判断标题是否重复
            // TODO:事务处理
            Wis.Website.DataManager.ArticleManager articleManager = new Wis.Website.DataManager.ArticleManager();
            int count = articleManager.CountArticlesByTitle(article.Title);
            if (count > 0)
            {
                // TODO:更友好的提示
                ViewState["javescript"] = string.Format("alert('标题重复!');");
                return;
            }

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
            article.TemplatePath = TemplatePaths.SelectedItem.Value;
            article.TitleColor = TitleColor.Value;
            article.Votes = 0;

            // 发布路径，静态页会在该发布路径中生成
            // 首页：  "/" + {RootPath} + "/"
            // default.htm
            // 单页：  "/" + {RootPath} + "/"
            // {PageId} + ".htm"
            // 列表页："/" + {RootPath} + "/" + {CategoryId} + "/"
            // {PageIndex} + ".htm"
            // 详细页："/" + {RootPath} + "/" + {CategoryId} + "/" + {Month} + "-" + {Day} + "/"
            // {ArticleId} + ".htm"
            article.ReleasePath = string.Format("/{0}/{1}/{2}-{3}/", System.Configuration.ConfigurationManager.AppSettings["ReleaseDirectory"],
                article.Category.CategoryId, article.DateCreated.Month, article.DateCreated.Day);
            // TODO: ReleaseDirectory 为常量

            // 获取附件信息
            List<Wis.Website.DataManager.File> files = new List<Wis.Website.DataManager.File>();
            string gFileURL = Request.Form["FileUrl"];
            string requestRanks = Request.Form["Rank"];
            string gURLName = Request.Form["URLName"];
            if (!string.IsNullOrEmpty(gFileURL))
            {
                string[] FileURL = gFileURL.Split(',');
                string[] ranks = requestRanks.Split(',');
                string[] URLName = gURLName.Split(',');
                for (int index = 0; index < FileURL.Length; index++)
                {
                    if (!string.IsNullOrEmpty(FileURL[index]))
                    {
                        Wis.Website.DataManager.File file = new Wis.Website.DataManager.File();

                        long size = 0;
                        // 判断当前路径所指向的是否为文件
                        if (File.Exists(Server.MapPath(FileURL[index])))
                        {
                            FileInfo fileInfo = new FileInfo(Server.MapPath(FileURL[index]));
                            file.Size = (int)(fileInfo.Length / 1024); // KB
                            if (!string.IsNullOrEmpty(URLName[index]))
                                URLName[index] = fileInfo.Name;
                        }

                        file.CreatedBy = string.Empty; // TODO:跟用户模块集成

                        file.CreationDate = System.DateTime.Now;
                        file.Description = string.Empty; // TODO:描述，增加附件可以参考QQ邮箱
                        file.SubmissionGuid = article.ArticleGuid;
                        file.FileGuid = Guid.NewGuid();
                        file.OriginalFileName = URLName[index].Replace("'", "\"");
                        file.SaveAsFileName = FileURL[index];
                        file.Rank = Convert.ToInt32(ranks[index]); // TODO:
                        file.Hits = 0;
                        files.Add(file); // 添加 附件
                    }
                }
            }

            article.ArticleId = articleManager.AddNew(article); // 获取 ArticleId ，补全了 article 对象的Id
            
            // 上传附件
            // Wis.Website.DataManager.FileManager fileManager = new Wis.Website.DataManager.FileManager();

            // TODO:需要事务处理，如果生成页面失败，那新增新闻也失败


            // 生成静态页面和关联页面
            //Website.WriteHtml.Build(article);
            //DataManager.ReleaseManager releaseManager = new DataManager.ReleaseManager();
            //releaseManager.AddNew(article);

            // 添加操作日志。

            Wis.Website.DataManager.LogManager logManager = new Wis.Website.DataManager.LogManager();
            logManager.AddNew(Guid.NewGuid(), Guid.Empty, "添加新闻", article.ArticleGuid, article.Title, System.DateTime.Now);

            Response.Redirect("ArticleList.aspx?CategoryGuid=" + Category.Value);
        }
    }
}