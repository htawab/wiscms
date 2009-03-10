//------------------------------------------------------------------------------
// <copyright file="ReleaseManager.cs" company="Everwis">
//     Copyright (C) Everwis Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text.RegularExpressions;

namespace Wis.Website.DataManager
{
    public class ReleaseManager
    {
        public ReleaseManager()
        {
            DbProviderHelper.GetConnection();
        }

        private string _ApplicationPath;
        /// <summary>
        /// 获取服务器上 ASP.NET 应用程序的虚拟应用程序根路径，前后都有 /。
        /// </summary>
        public string ApplicationPath
        {
            get
            {
                if (string.IsNullOrEmpty(_ApplicationPath))
                {
                    _ApplicationPath = System.Web.HttpContext.Current.Request.ApplicationPath;
                    if (!_ApplicationPath.EndsWith("/")) _ApplicationPath += "/"; // 前后导都有 /
                }
                return _ApplicationPath;
            }
        }

        private string _TemplateDirectory;
        /// <summary>
        /// 存放模版的目录，没有后导 /
        /// </summary>
        public string TemplateDirectory
        {
            get
            {
                if (string.IsNullOrEmpty(_TemplateDirectory))
                {
                    _TemplateDirectory = System.Configuration.ConfigurationManager.AppSettings["TemplateRootDirectory"].Trim('/');
                    _TemplateDirectory = string.Format("{0}{1}", this.ApplicationPath, _TemplateDirectory);
                }
                return _TemplateDirectory;
            }
        }

        private string _ReleaseDirectory;
        /// <summary>
        /// 存储静态页的路径，没有后导 /
        /// </summary>
        public string ReleaseDirectory
        {
            get
            {
                if (string.IsNullOrEmpty(_ReleaseDirectory))
                {
                    _ReleaseDirectory = System.Configuration.ConfigurationManager.AppSettings["ReleaseRootDirectory"].Trim('/'); // 不要有前后导的间隔符号
                    _ReleaseDirectory = string.Format("{0}{1}", this.ApplicationPath, _ReleaseDirectory);
                }
                return _ReleaseDirectory;
            }
        }

        public string ReleasePager(int pageIndex, int recordCount, int pageCount)
        {
            // 列表页："/{RootPath}/{CategoryId}/{PageIndex}.htm"
            // 第2页 / 共2页 首页 上一页 [1] [2] 下一页 尾页
            if (pageIndex < 1) return string.Empty; // 当前页数必须>0
            if (pageCount <= 1) return string.Empty;
            int prevPage = pageIndex - 1;
            int nextPage = pageIndex + 1;
            StringBuilder sb = new StringBuilder();
            sb.Append("<div id='Pager'>");
            sb.Append(string.Format("<span>共{0}条记录&nbsp;第{1}页/共{2}页</span>", recordCount, pageIndex, pageCount));

            if (prevPage < 1)
            {
                sb.Append("<span class='noLink'>首页</span>");
                sb.Append("<span class='noLink'>上一页</span>");
            }
            else
            {
                sb.Append("<a href='1.htm'>首页</a>");
                sb.Append(string.Format("<a href='{0}.htm'>上一页</a>", prevPage));
            }

            int startPage;
            if (pageIndex % 10 == 0)
                startPage = pageIndex - 9;
            else
                startPage = pageIndex - pageIndex % 10 + 1;

            if (startPage > 10)
                sb.Append(string.Format("<a href='{0}.htm' title='前10页'>...</a>", (startPage - 1)));

            for (int index = startPage; index < startPage + 10; index++)
            {
                if (index > pageCount) break;
                if (index == pageIndex)
                    sb.Append(string.Format("<span title='第{0}页' class='currentPager'>{0}</span>", index));
                else
                    sb.Append(string.Format("<a href='{0}.htm' title='第{0}页'>{0}</a>", index));
            }
            if (pageCount >= startPage + 10)
                sb.Append(string.Format("<a href='{0}.htm' title='下{0}页'>...</a>", (startPage + 10)));
            if (nextPage > pageCount)
            {
                sb.Append("<span class='noLink'>下一页</span>");
                sb.Append("<span class='noLink'>尾页</span>");
            }
            else
            {
                sb.Append(string.Format("<a href='{0}.htm'>下一页</a>", nextPage));
                sb.Append(string.Format("<a href='{0}.htm'>尾页</a>", pageCount));
            }

            sb.Append("</div>");
            return sb.ToString();
        }


        public string ReleaseTinyPager(int pageIndex, int recordCount, int pageCount)
        {
            // 列表页："/{RootPath}/{CategoryId}/{PageIndex}.htm"
            // 首页 上一页 下一页 尾页
            if (pageIndex < 1) return string.Empty; // 当前页数必须>0
            if (pageCount <= 1) return string.Empty;
            int prevPage = pageIndex - 1;
            int nextPage = pageIndex + 1;
            StringBuilder sb = new StringBuilder();
            sb.Append("<div id='Pager'>");

            if (prevPage < 1)
            {
                sb.Append("<span class='noLink'>首页</span>");
                sb.Append("<span class='noLink'>上一页</span>");
            }
            else
            {
                sb.Append("<a href='1.htm'>首页</a>");
                sb.Append(string.Format("<a href='{0}.htm'>上一页</a>", prevPage));
            }

            if (nextPage > pageCount)
            {
                sb.Append("<span class='noLink'>下一页</span>");
                sb.Append("<span class='noLink'>尾页</span>");
            }
            else
            {
                sb.Append(string.Format("<a href='{0}.htm'>下一页</a>", nextPage));
                sb.Append(string.Format("<a href='{0}.htm'>尾页</a>", pageCount));
            }

            sb.Append("</div>");
            return sb.ToString();
        }


        // Item
        //public void ReleaseArticle(Article article)
        //{

        //}

        //public void ReleaseArticlePhoto(ArticlePhoto articlePhoto)
        //{

        //}

        //public void ReleaseArticleVideo(ArticlePhoto articlePhoto)
        //{

        //}

        //public void ReleaseArticleSoft(ArticlePhoto articlePhoto)
        //{

        //}

        // List 需要循环输出的

        // Index

        // Special

        // Page

        /// <summary>
        /// 根据分类编号获取关联的发布对象
        /// </summary>
        /// <param name="categoryGuid"></param>
        /// <returns></returns>
        public List<Release> GetReleasesByCategory(Guid categoryGuid)
        {
            List<Release> releases = new List<Release>();
            DbCommand command = DbProviderHelper.CreateCommand("SelectReleaseRelations", CommandType.StoredProcedure);
            command.Parameters.Add(DbProviderHelper.CreateParameter("@CategoryGuid", DbType.Guid, categoryGuid));
            DbDataReader dataReader = DbProviderHelper.ExecuteReader(command);
            while (dataReader.Read())
            {
                Release release = new Release();

                // 发布信息
                release.ReleaseId = Convert.ToInt32(dataReader[ViewReleaseTemplateField.ReleaseId]);
                release.ReleaseGuid = (Guid)dataReader[ViewReleaseTemplateField.ReleaseGuid];
                release.Title = (string)dataReader[ViewReleaseTemplateField.ReleaseTitle];
                //release.TemplatePath = (string)dataReader[ViewReleaseTemplateField.TemplatePath];
                release.ReleasePath = (string)dataReader[ViewReleaseTemplateField.ReleasePath];
                release.DateReleased = (DateTime)dataReader[ViewReleaseTemplateField.DateReleased];
                release.ParentGuid = (Guid)dataReader[ViewReleaseTemplateField.ParentGuid];

                // 模版信息
                release.Template.TemplateId = Convert.ToInt32(dataReader[ViewReleaseTemplateField.TemplateId]);
                release.Template.TemplateGuid = (Guid)dataReader[ViewReleaseTemplateField.TemplateGuid];
                release.Template.Title = Convert.ToString(dataReader[ViewReleaseTemplateField.TemplateTitle]);
                release.Template.TemplatePath = Convert.ToString(dataReader[ViewReleaseTemplateField.TemplatePath]);
                // 枚举 TemplateType
                release.Template.TemplateType = (TemplateType)System.Enum.Parse(typeof(TemplateType), dataReader[ViewReleaseTemplateField.TemplateType].ToString(), true);

                if (dataReader[ViewReleaseTemplateField.PageSize] != DBNull.Value)
                    release.PageSize = (int)dataReader[ViewReleaseTemplateField.PageSize];

                if (dataReader[ViewReleaseTemplateField.PagerStyle] == DBNull.Value)
                    release.PagerStyle = PagerStyle.None;
                else
                    release.PagerStyle = (PagerStyle)System.Enum.Parse(typeof(PagerStyle), dataReader[ViewReleaseTemplateField.PagerStyle].ToString(), true);

                releases.Add(release);
            }
            dataReader.Close();
            return releases;
        }


        /// <summary>
        /// 发布视频新闻相关的页面
        /// </summary>
        /// <param name="videoArticle">视频新闻实体类</param>
        public void ReleaseVideoArticleRelation(VideoArticle videoArticle)
        {
            // 1 发布 Category 页面
            List<Release> releases = GetReleasesByCategory(videoArticle.Article.Category.CategoryGuid);
            if (releases.Count > 0)
            {
                foreach (Release release in releases)
                {
                    // 装载模版
                    release.Template.TemplatePath = Regex.Replace(release.Template.TemplatePath, @"\{RootPath\}", this.TemplateDirectory, RegexOptions.IgnoreCase);
                    string templatePath = System.Web.HttpContext.Current.Server.MapPath(release.Template.TemplatePath);
                    Wis.Toolkit.Templates.TemplateManager templateManager = Wis.Toolkit.Templates.TemplateManager.LoadFile(templatePath, Encoding.UTF8);
                    templateManager.SetVariable("VideoArticle", videoArticle); // 最近更新的文章
                    templateManager.SetVariable("Category", videoArticle.Article.Category); // 最近更新的分类
                    templateManager.SetVariable("ApplicationPath", this.ApplicationPath);
                    templateManager.SetVariable("TemplateDirectory", this.TemplateDirectory);
                    templateManager.SetVariable("ReleaseDirectory", this.ReleaseDirectory);

                    // 处理 Release
                    release.ReleasePath = Regex.Replace(release.ReleasePath, @"\{RootPath\}", this.ReleaseDirectory, RegexOptions.IgnoreCase);
                    release.ReleasePath = Regex.Replace(release.ReleasePath, @"\{CategoryId\}", videoArticle.Article.Category.CategoryId.ToString(), RegexOptions.IgnoreCase);

                    switch (release.Template.TemplateType)
                    {
                        case TemplateType.Index: // 1.1 发布 Index 页面，不带分页
                            // 首页：{RootPath}/default.htm
                            ReleaseStaticPage(release, templateManager);
                            break;
                        case TemplateType.VideoArticleList: // 1.2 发布 List 页面，带分页
#warning 重点测试：视频新闻-新闻列表页 工作会议-新闻列表页 教育教学-新闻列表页 科研活动-新闻列表页

                            // 列表页：{RootPath}/{CategoryId}/{PageIndex}.htm
                            if (release.PageSize.HasValue == false)
                                throw new System.ArgumentNullException(string.Format("发布编号为 {0} 的 PageSize 参数未配置", release.ReleaseId));

                            // 计算一共有多少页，然后逐页生成
                            int pageSize = release.PageSize.Value;
                            templateManager.SetVariable("PageSize", pageSize);

                            // 计算总数
                            ArticleManager articleManager = new ArticleManager();
                            int recordCount = (int)articleManager.CountArticlesByCategoryGuid(videoArticle.Article.Category.CategoryGuid);
#warning 给Category增加RecordCount字段，缓存用
                            templateManager.SetVariable("RecordCount", recordCount);

                            // 求页总数 pageCount
                            int pageCount;
                            pageCount = recordCount / pageSize;
                            if (recordCount % pageSize != 0)
                                pageCount += 1;
                            templateManager.SetVariable("PageCount", pageCount);

                            // 逐页生成
                            if (release.PagerStyle != PagerStyle.None)
                            {
                                for (int pageIndex = 1; pageIndex <= pageCount; pageIndex++)
                                {
                                    // {RootPath}/{CategoryId}/{PageIndex}.htm
                                    templateManager.SetVariable("PageIndex", pageIndex);
                                    if(release.PagerStyle == PagerStyle.Normal)
                                        templateManager.SetVariable("Pager", ReleasePager(pageIndex, recordCount, pageCount));
                                    else if(release.PagerStyle == PagerStyle.Tiny)
                                        templateManager.SetVariable("Pager", ReleaseTinyPager(pageIndex, recordCount, pageCount));
                                   
                                    release.ReleasePath = Regex.Replace(release.ReleasePath, @"\{PageIndex\}", pageIndex.ToString(), RegexOptions.IgnoreCase);
                                    ReleaseStaticPage(release, templateManager);
                                }
                            }

                            break;
                        case TemplateType.VideoArticleItem:// 1.3 发布详细页
                            // 详细页：{RootPath}/{CategoryId}/{Month}-{Day}/{ArticleId}.htm
                            release.ReleasePath = Regex.Replace(release.ReleasePath, @"\{Year\}", videoArticle.Article.DateCreated.Year.ToString(), RegexOptions.IgnoreCase);
                            release.ReleasePath = Regex.Replace(release.ReleasePath, @"\{Month\}", videoArticle.Article.DateCreated.Month.ToString(), RegexOptions.IgnoreCase);
                            release.ReleasePath = Regex.Replace(release.ReleasePath, @"\{Day\}", videoArticle.Article.DateCreated.Day.ToString(), RegexOptions.IgnoreCase);
                            release.ReleasePath = Regex.Replace(release.ReleasePath, @"\{ArticleId\}", videoArticle.Article.ArticleId.ToString(), RegexOptions.IgnoreCase);
                            // TODO:按照 ContentHtml 长度，生成多页
                            //if (release.ReleasePath.IndexOf("{ArticleId}") > -1) // 详细页
                            //{
                            //  
                            //}
                            ReleaseStaticPage(release, templateManager);
                            break;
                        default:
                            throw new System.ArgumentException("articlePhoto");
                    }
                }
            }
            // 发布 Page 页面
            // 单页：  "{RootPath}/{PageId}.htm" 这个{PageId}是已知的

            // 发布 专题 页面
            // 专题页：{RootPath}/{SpecialId}.htm 这个{SpecialId}是已知的
        }

        /// <summary>
        /// 发布图片新闻的关联页面。
        /// </summary>
        /// <param name="articlePhoto"></param>
        public void ReleaseArticlePhotoRelation(ArticlePhoto articlePhoto)
        {
            List<Release> releases = GetReleasesByCategory(articlePhoto.Category.CategoryGuid);
            if (releases.Count == 0) return;

            foreach (Release release in releases)
            {
                // 装载模版
                release.Template.TemplatePath = release.Template.TemplatePath.Replace("{RootPath}", this.TemplateDirectory);
                string templatePath = System.Web.HttpContext.Current.Server.MapPath(release.Template.TemplatePath);
                Wis.Toolkit.Templates.TemplateManager templateManager = Wis.Toolkit.Templates.TemplateManager.LoadFile(templatePath, Encoding.UTF8);
                templateManager.SetVariable("ApplicationPath", this.ApplicationPath);
                templateManager.SetVariable("TemplateDirectory", this.TemplateDirectory);
                templateManager.SetVariable("ReleaseDirectory", this.ReleaseDirectory);

                // 生成静态页，支持索引页比如首页、列表页、详细页、专题页
                // 首页：  "{RootPath}/default.htm"
                // 单页：  "{RootPath}/{PageId}.htm" 这个{PageId}是已知的
                // 列表页："{RootPath}/{CategoryId}/{PageIndex}.htm"
                // 图片新闻-详细页："{RootPath}/{CategoryId}/{Month}-{Day}/{ArticleId}.htm"
                // 专题页："{RootPath}/{SpecialId}.htm" 这个{SpecialId}是已知的
                release.ReleasePath = release.ReleasePath.Replace("{RootPath}", this.ReleaseDirectory);
                release.ReleasePath = release.ReleasePath.Replace("{CategoryId}", articlePhoto.Category.CategoryId.ToString());

                // 分类页
                // 处理 {PageIndex:20}
                string pattern = @"\{PageIndex\:(?<PageSize>\d+)\}";
                Regex reg = new Regex(pattern, RegexOptions.IgnoreCase);
                Match match = reg.Match(release.ReleasePath);
                if (match.Success)
                {
                    // 哪个分类
                    templateManager.SetVariable("Category", articlePhoto.Category);

                    // 计算一共有多少页，然后逐页生成
                    int pageSize = 20;
                    string groupPageSize = match.Groups[1].Value;
                    int.TryParse(groupPageSize, out pageSize);
                    templateManager.SetVariable("PageSize", pageSize);

                    // 计算总数
                    ArticleManager articleManager = new ArticleManager();
                    int recordCount = (int)articleManager.CountArticlesByCategoryGuid(articlePhoto.Category.CategoryGuid);
                    templateManager.SetVariable("RecordCount", recordCount);

                    // 求页总数 pageCount
                    int pageCount;
                    pageCount = recordCount / pageSize;
                    if (recordCount % pageSize != 0)
                        pageCount += 1;
                    templateManager.SetVariable("PageCount", pageCount);

                    // 逐页生成
                    for (int pageIndex = 1; pageIndex <= pageCount; pageIndex++)
                    {
                        // {RootPath}/{CategoryId}/{PageIndex}.htm
                        templateManager.SetVariable("PageIndex", pageIndex);
                        // 分页
#warning ReleaseTinyPager 可以配置使用那种分页，PagerMode?

                        templateManager.SetVariable("Pager", ReleaseTinyPager(pageIndex, recordCount, pageCount));
                        release.ReleasePath = Regex.Replace(release.ReleasePath, pattern, pageIndex.ToString(), RegexOptions.IgnoreCase);
                        
                        ReleaseStaticPage(release, templateManager);
                    }
                }
                else
                {
                    if (release.ReleasePath.IndexOf("{ArticleId}") > -1) // 详细页
                    {
                        release.ReleasePath = release.ReleasePath.Replace("{Year}", articlePhoto.DateCreated.Year.ToString());
                        release.ReleasePath = release.ReleasePath.Replace("{Month}", articlePhoto.DateCreated.Month.ToString());
                        release.ReleasePath = release.ReleasePath.Replace("{Day}", articlePhoto.DateCreated.Day.ToString());
                        release.ReleasePath = release.ReleasePath.Replace("{ArticleId}", articlePhoto.ArticleId.ToString());
                        templateManager.SetVariable("ArticlePhoto", articlePhoto);
                    }

                    // TODO:按照 ContentHtml 长度，生成多页
                    //if (release.ReleasePath.IndexOf("{ArticleId}") > -1) // 详细页
                    //{
                    //}

                    ReleaseStaticPage(release, templateManager);
                }
            }
        }


#warning ReleaseRelation 表反映了前台网站的站点结构，可以根据Release表生成站点地图Google Sitemap和Baidu Sitemap

        /// <summary>
        /// 内容与模板没有耦合，一篇文章的详细页可以用多套模板生成，同时生成受影响的关联页面。
        /// 1、读取分类关联的Release对象集合；
        /// 2、是否有分页即列表页，有分页则逐页生成，没有分页即索引页、详细页、专题页，逐个生成；
        /// </summary>
        /// <param name="article"></param>
        public void ReleaseRelation(Article article)
        {
            // 根据分类编号获取发布编号 
            List<Release> releases = GetReleasesByCategory(article.Category.CategoryGuid);
            if (releases.Count == 0) return;

            foreach (Release release in releases)
            {
                // 装载模版
                release.Template.TemplatePath = release.Template.TemplatePath.Replace("{RootPath}", this.TemplateDirectory);
                string templatePath = System.Web.HttpContext.Current.Server.MapPath(release.Template.TemplatePath);
                Wis.Toolkit.Templates.TemplateManager templateManager = Wis.Toolkit.Templates.TemplateManager.LoadFile(templatePath, Encoding.UTF8);
                templateManager.SetVariable("ApplicationPath", this.ApplicationPath);
                templateManager.SetVariable("TemplateDirectory", this.TemplateDirectory);
                templateManager.SetVariable("ReleaseDirectory", this.ReleaseDirectory);

                // 生成静态页，支持索引页比如首页、列表页、详细页、专题页
                // 首页：  "{RootPath}/default.htm"
                // 单页：  "{RootPath}/{PageId}.htm" 这个{PageId}是已知的
                // 列表页："{RootPath}/{CategoryId}/{PageIndex}.htm"
                // 详细页："{RootPath}/{CategoryId}/{Month}-{Day}/{ArticleId}.htm"
                // 专题页："{RootPath}/{SpecialId}.htm" 这个{SpecialId}是已知的
                release.ReleasePath = release.ReleasePath.Replace("{RootPath}", this.ReleaseDirectory);
                release.ReleasePath = release.ReleasePath.Replace("{CategoryId}", article.Category.CategoryId.ToString());

                // 分类页
                // 处理 {PageIndex:20}
                string pattern = @"\{PageIndex\:(?<PageSize>\d+)\}";
                Regex reg = new Regex(pattern, RegexOptions.IgnoreCase);
                Match match = reg.Match(release.ReleasePath);
                if (match.Success)
                {
                    // 哪个分类
                    templateManager.SetVariable("Category", article.Category);

                    // 计算一共有多少页，然后逐页生成
                    int pageSize = 20;
                    string groupPageSize = match.Groups[1].Value;
                    int.TryParse(groupPageSize, out pageSize);
                    templateManager.SetVariable("PageSize", pageSize);

                    // 计算总数
                    ArticleManager articleManager = new ArticleManager();
                    int recordCount = (int)articleManager.CountArticlesByCategoryGuid(article.Category.CategoryGuid);
                    templateManager.SetVariable("RecordCount", recordCount);

                    // 求页总数 pageCount
                    int pageCount;
                    pageCount = recordCount / pageSize;
                    if (recordCount % pageSize != 0)
                        pageCount += 1;
                    templateManager.SetVariable("PageCount", pageCount);

                    // 逐页生成
                    for (int pageIndex = 1; pageIndex <= pageCount; pageIndex++)
                    {
                        // {RootPath}/{CategoryId}/{PageIndex}.htm
                        templateManager.SetVariable("PageIndex", pageIndex);
                        // 分页
                        templateManager.SetVariable("Pager", ReleasePager(pageIndex, recordCount, pageCount));
                        release.ReleasePath = Regex.Replace(release.ReleasePath, pattern, pageIndex.ToString(), RegexOptions.IgnoreCase);
                        
                        ReleaseStaticPage(release, templateManager);
                    }
                }
                else
                {
                    if (release.ReleasePath.IndexOf("{ArticleId}") > -1) // 详细页
                    {
                        release.ReleasePath = release.ReleasePath.Replace("{Year}", article.DateCreated.Year.ToString());
                        release.ReleasePath = release.ReleasePath.Replace("{Month}", article.DateCreated.Month.ToString());
                        release.ReleasePath = release.ReleasePath.Replace("{Day}", article.DateCreated.Day.ToString());
                        release.ReleasePath = release.ReleasePath.Replace("{ArticleId}", article.ArticleId.ToString());
                        templateManager.SetVariable("Article", article);
                    }

                    // TODO:按照 ContentHtml 长度，生成多页
                    //if (release.ReleasePath.IndexOf("{ArticleId}") > -1) // 详细页
                    //{
                    //}

                    ReleaseStaticPage(release, templateManager);
                }
            }
        }

        /// <summary>
        /// 发布详细页，比如评论后需要重新生成页面。
        /// </summary>
        /// <param name="article">文章实体类</param>
        public void ReleaseArticle(Article article)
        {
            // {RootPath}/{CategoryId}/{Year}-{Month}-{Day}/{ArticleId}.htm
            // 1、读取发布分类关联表 ReleaseCategory，读取本篇新闻对应的发布编号ReleaseGuid；
            // 2、根据发布编号ReleaseGuid读取需要生成静态页的模板，Article实体类作为参数传入；
            // 3、根据 {ArticleId} 读取详细页；

            // 根据分类编号获取发布编号 
            List<Release> releases = new List<Release>();
            DbCommand command = DbProviderHelper.CreateCommand("SelectReleaseArticles", CommandType.StoredProcedure);
            command.Parameters.Add(DbProviderHelper.CreateParameter("@CategoryGuid", DbType.Guid, article.Category.CategoryGuid));
            DbDataReader dataReader = DbProviderHelper.ExecuteReader(command);
            while (dataReader.Read())
            {
                if (dataReader["ReleasePath"].ToString().IndexOf("{ArticleId}") > -1)
                {
                    Release release = new Release();
                    release.ReleaseId = Convert.ToInt32(dataReader["ReleaseId"]);
                    release.ReleaseGuid = (Guid)dataReader["ReleaseGuid"];
                    release.Title = (string)dataReader["Title"];
                    release.Template.TemplatePath = (string)dataReader[ViewReleaseTemplateField.TemplatePath];
                    release.ReleasePath = (string)dataReader["ReleasePath"];
                    release.DateReleased = (DateTime)dataReader["DateReleased"];
                    release.ParentGuid = (Guid)dataReader["ParentGuid"];
                    releases.Add(release);
                }
            }
            dataReader.Close();

            if (releases.Count == 0) return;
            foreach (Release release in releases)
            {
                // 装载模版
                release.Template.TemplatePath = release.Template.TemplatePath.Replace("{RootPath}", this.TemplateDirectory);
                string templatePath = System.Web.HttpContext.Current.Server.MapPath(release.Template.TemplatePath);
                Wis.Toolkit.Templates.TemplateManager templateManager = Wis.Toolkit.Templates.TemplateManager.LoadFile(templatePath, Encoding.UTF8);
                templateManager.SetVariable("ApplicationPath", this.ApplicationPath);
                templateManager.SetVariable("TemplateDirectory", this.TemplateDirectory);
                templateManager.SetVariable("ReleaseDirectory", this.ReleaseDirectory);
                templateManager.SetVariable("Article", article);

                // 预处理发布路径
                // "{RootPath}/{CategoryId}/{Month}-{Day}/{ArticleId}.htm"
                release.ReleasePath = release.ReleasePath.Replace("{RootPath}", this.ReleaseDirectory);
                release.ReleasePath = release.ReleasePath.Replace("{CategoryId}", article.Category.CategoryId.ToString());
                release.ReleasePath = release.ReleasePath.Replace("{Year}", article.DateCreated.Year.ToString());
                release.ReleasePath = release.ReleasePath.Replace("{Month}", article.DateCreated.Month.ToString());
                release.ReleasePath = release.ReleasePath.Replace("{Day}", article.DateCreated.Day.ToString());
                release.ReleasePath = release.ReleasePath.Replace("{ArticleId}", article.ArticleId.ToString());

                // TODO:按照 ContentHtml 长度，生成多页
                //if (release.ReleasePath.IndexOf("{ArticleId}") > -1) // 详细页
                //{
                //}

                // 生成详细页的静态页
                ReleaseStaticPage(release, templateManager);
            }
        }

        /// <summary>
        /// 发布静态页
        /// </summary>
        /// <param name="ripeRelease">预处理过的Release对象</param>
        /// <param name="templateManager">预处理过的模版管理器</param>
        public void ReleaseStaticPage(Release ripeRelease, Wis.Toolkit.Templates.TemplateManager ripeTemplateManager)
        {
            lock (this)
            {
                // 绝对路径
                ripeRelease.ReleasePath = System.Web.HttpContext.Current.Server.MapPath(ripeRelease.ReleasePath);

                // TODO:失败了如何回滚? 将已经删除的网页恢复
                if (System.IO.File.Exists(ripeRelease.ReleasePath))
                {
                    System.IO.File.Delete(ripeRelease.ReleasePath);
                    System.Threading.Thread.Sleep(100);
                }

                // 如果目录不存在，则创建目录
                System.IO.FileInfo fileInfo = new System.IO.FileInfo(ripeRelease.ReleasePath);
                if (!fileInfo.Directory.Exists) fileInfo.Directory.Create();

                // 生成发布
                using (System.IO.StreamWriter sw = new System.IO.StreamWriter(ripeRelease.ReleasePath, false, Encoding.UTF8))
                {
                    sw.Write(ripeTemplateManager.Process());
                }
            }
        }

        /// <summary>
        /// 生成单页关联的模板。
        /// </summary>
        /// <param name="page"></param>
        public void Release(Page page)
        {
            //
        }

        public List<Release> GetReleases()
        {
            List<Release> releases = new List<Release>();
            DbCommand command = DbProviderHelper.CreateCommand("SELECTReleases", CommandType.StoredProcedure);
            DbDataReader dataReader = DbProviderHelper.ExecuteReader(command);
            while (dataReader.Read())
            {
                Release release = new Release();
                release.ReleaseId = Convert.ToInt32(dataReader["ReleaseId"]);
                release.ReleaseGuid = (Guid)dataReader["ReleaseGuid"];
                release.Title = (string)dataReader["Title"];
                release.Template.TemplatePath = (string)dataReader[ViewReleaseTemplateField.TemplatePath];
                release.ReleasePath = (string)dataReader["ReleasePath"];
                release.DateReleased = (DateTime)dataReader["DateReleased"];
                release.ParentGuid = (Guid)dataReader["ParentGuid"];
                releases.Add(release);
            }
            dataReader.Close();
            return releases;
        }

        public Release GetRelease(int ReleaseId)
        {
            Release release = new Release();
            DbCommand command = DbProviderHelper.CreateCommand("SELECTRelease", CommandType.StoredProcedure);
            command.Parameters.Add(DbProviderHelper.CreateParameter("@ReleaseId", DbType.Int32, ReleaseId));
            DbDataReader dataReader = DbProviderHelper.ExecuteReader(command);
            while (dataReader.Read())
            {
                release.ReleaseId = Convert.ToInt32(dataReader["ReleaseId"]);
                release.ReleaseGuid = (Guid)dataReader["ReleaseGuid"];
                release.Title = (string)dataReader["Title"];
                release.Template.TemplatePath = (string)dataReader[ViewReleaseTemplateField.TemplatePath];
                release.ReleasePath = (string)dataReader["ReleasePath"];
                release.DateReleased = (DateTime)dataReader["DateReleased"];
                release.ParentGuid = (Guid)dataReader["ParentGuid"];
            }
            dataReader.Close();
            return release;
        }


        /// <summary>
        /// 发布新闻。
        /// </summary>
        /// <param name="articlePhoto"></param>
        /// <returns></returns>
        public int AddNew(Article article)
        {
            Guid releaseGuid = Guid.NewGuid();
            DbCommand command = DbProviderHelper.CreateCommand("INSERTRelease", CommandType.StoredProcedure);
            command.Parameters.Add(DbProviderHelper.CreateParameter("@ReleaseGuid", DbType.Guid, releaseGuid));
            command.Parameters.Add(DbProviderHelper.CreateParameter("@CategoryGuid", DbType.Guid, article.Category.CategoryGuid));
            //command.Parameters.Add(DbProviderHelper.CreateParameter("@TemplatePath", DbType.String, articlePhoto.TemplatePath));
            //command.Parameters.Add(DbProviderHelper.CreateParameter("@ReleasePath", DbType.String, articlePhoto.ReleasePath));
            return Convert.ToInt32(DbProviderHelper.ExecuteScalar(command));
        }

        public int AddNew(Guid ReleaseGuid, Guid CategoryGuid, string TemplatePath, string ReleasePath)
        {
            DbCommand command = DbProviderHelper.CreateCommand("INSERTRelease", CommandType.StoredProcedure);
            command.Parameters.Add(DbProviderHelper.CreateParameter("@ReleaseGuid", DbType.Guid, ReleaseGuid));
            command.Parameters.Add(DbProviderHelper.CreateParameter("@CategoryGuid", DbType.Guid, CategoryGuid));
            command.Parameters.Add(DbProviderHelper.CreateParameter("@TemplatePath", DbType.String, TemplatePath));
            command.Parameters.Add(DbProviderHelper.CreateParameter("@ReleasePath", DbType.String, ReleasePath));

            return Convert.ToInt32(DbProviderHelper.ExecuteScalar(command));
        }

        public int Update(int ReleaseId, Guid ReleaseGuid, Guid CategoryGuid, string TemplatePath, string ReleasePath)
        {

            DbCommand oDbCommand = DbProviderHelper.CreateCommand("UPDATERelease", CommandType.StoredProcedure);
            oDbCommand.Parameters.Add(DbProviderHelper.CreateParameter("@ReleaseGuid", DbType.Guid, ReleaseGuid));
            oDbCommand.Parameters.Add(DbProviderHelper.CreateParameter("@CategoryGuid", DbType.Guid, CategoryGuid));
            oDbCommand.Parameters.Add(DbProviderHelper.CreateParameter("@TemplatePath", DbType.String, TemplatePath));
            oDbCommand.Parameters.Add(DbProviderHelper.CreateParameter("@ReleasePath", DbType.String, ReleasePath));
            oDbCommand.Parameters.Add(DbProviderHelper.CreateParameter("@ReleaseId", DbType.Int32, ReleaseId));
            return DbProviderHelper.ExecuteNonQuery(oDbCommand);
        }

        public int Remove(int ReleaseId)
        {
            DbCommand oDbCommand = DbProviderHelper.CreateCommand("DELETERelease", CommandType.StoredProcedure);
            oDbCommand.Parameters.Add(DbProviderHelper.CreateParameter("@ReleaseId", DbType.Int32, ReleaseId));
            return DbProviderHelper.ExecuteNonQuery(oDbCommand);
        }
    }
}
