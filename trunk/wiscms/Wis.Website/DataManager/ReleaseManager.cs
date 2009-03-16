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


        public string ReleaseTinyPager(int pageIndex, int pageCount)
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

        #region 获取发布对象
        /// <summary>
        /// 根据关联编号获取关联的发布对象，关联编号可以是分类编号、单页编号、专题编号
        /// </summary>
        /// <param name="relationGuid">关联编号</param>
        /// <returns>返回关联的发布类</returns>
        public List<Release> GetRelatedReleases(Guid relationGuid)
        {
            DbCommand command = DbProviderHelper.CreateCommand("SelectRelatedReleases", CommandType.StoredProcedure);
            command.Parameters.Add(DbProviderHelper.CreateParameter("@RelationGuid", DbType.Guid, relationGuid));
            DbDataReader dataReader = DbProviderHelper.ExecuteReader(command);
            return GetReleases(dataReader);
        }

        public List<Release> GetReleasesByCategoryGuid(Guid categoryGuid)
        {
            DbCommand command = DbProviderHelper.CreateCommand("SelectReleasesByCategoryGuid", CommandType.StoredProcedure);
            command.Parameters.Add(DbProviderHelper.CreateParameter("@CategoryGuid", DbType.Guid, categoryGuid));
            DbDataReader dataReader = DbProviderHelper.ExecuteReader(command);
            return GetReleases(dataReader);
        }

        public List<Release> GetReleasesByCategoryGuid(Guid categoryGuid, TemplateType templateType)
        {
            DbCommand command = DbProviderHelper.CreateCommand("SelectReleasesByCategoryGuidAndTemplateType", CommandType.StoredProcedure);
            command.Parameters.Add(DbProviderHelper.CreateParameter("@CategoryGuid", DbType.Guid, categoryGuid));
            command.Parameters.Add(DbProviderHelper.CreateParameter("@TemplateType", DbType.String, templateType.ToString()));
            DbDataReader dataReader = DbProviderHelper.ExecuteReader(command);
            return GetReleases(dataReader);
        }

        /// <summary>
        /// 获取发布对象集合。
        /// </summary>
        /// <param name="dataReader">从数据源读取行的一个只进流。</param>
        /// <returns>返回发布对象集合</returns>
        public List<Release> GetReleases(DbDataReader dataReader)
        {
            List<Release> releases = new List<Release>();
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
        #endregion

        #region 发布索引页
        /// <summary>
        /// 发布索引页
        /// </summary>
        /// <param name="release">发布对象</param>
        public void ReleasingIndex(Release release)
        {
            // 首页：{TemplateDirectory}/default.htm
            if (release.Template.TemplateType != TemplateType.Index) return;

            // 装载模版
            release.Template.TemplatePath = Regex.Replace(release.Template.TemplatePath, @"\{TemplateDirectory\}", this.TemplateDirectory, RegexOptions.IgnoreCase);
            string templatePath = System.Web.HttpContext.Current.Server.MapPath(release.Template.TemplatePath);
            Wis.Toolkit.Templates.TemplateManager templateManager = Wis.Toolkit.Templates.TemplateManager.LoadFile(templatePath, Encoding.UTF8);
            templateManager.SetVariable("ApplicationPath", this.ApplicationPath);
            templateManager.SetVariable("TemplateDirectory", this.TemplateDirectory);
            templateManager.SetVariable("ReleaseDirectory", this.ReleaseDirectory);

            // 处理 Release
            release.ReleasePath = Regex.Replace(release.ReleasePath, @"\{ReleaseDirectory\}", this.ReleaseDirectory, RegexOptions.IgnoreCase);

            // 发布静态页
            ReleasingStaticPage(release, templateManager);
        }
        #endregion

#warning 发布文章，完善功能
        #region 发布普通文章
        public void ReleasingArticle(Article article)
        {
            // 1 发布 Article 页面，根据分类编号找 Item 的模版
            // 2 发布 Category 页面，根据分类编号找List或Index的模版
            List<Release> releases = GetReleasesByCategoryGuid(article.Category.CategoryGuid);
            foreach (Release release in releases)
            {
                switch (release.Template.TemplateType)
                {
                    case TemplateType.ArticleIndex: // 视频文章索引页
                        ReleasingArticleIndex(article.Category, release);
                        break;
                    case TemplateType.ArticleList: // 视频文章列表页，带分页
                        ReleasingArticleList(article.Category, release);
                        break;
                    case TemplateType.ArticleItem:// 视频文章详细页
                        ReleasingArticleItem(article, release);
                        break;
                    default:
                        throw new System.ArgumentException("TemplateType");
                }
            }

            // 3 发布被引用的页面
            ReleasingRelatedReleases(article.Category.CategoryGuid);
        }

        public void ReleasingRemovedArticle(Article article)
        {
            // 1 发布 Article 页面，根据分类编号找 Item 的模版
            // 2 发布 Category 页面，根据分类编号找List或Index的模版
            List<Release> releases = GetReleasesByCategoryGuid(article.Category.CategoryGuid);
            foreach (Release release in releases)
            {
                switch (release.Template.TemplateType)
                {
                    case TemplateType.ArticleIndex: // 视频文章索引页
                        ReleasingArticleIndex(article.Category, release);
                        break;
                    case TemplateType.ArticleList: // 视频文章列表页，带分页
                        ReleasingArticleList(article.Category, release);
                        break;
                    case TemplateType.ArticleItem:// 移除文章详细页
                        // 处理 Release
                        release.ReleasePath = Regex.Replace(release.ReleasePath, @"\{ReleaseDirectory\}", this.ReleaseDirectory, RegexOptions.IgnoreCase);
                        release.ReleasePath = Regex.Replace(release.ReleasePath, @"\{CategoryId\}", article.Category.CategoryId.ToString(), RegexOptions.IgnoreCase);
                        release.ReleasePath = Regex.Replace(release.ReleasePath, @"\{Year\}", article.DateCreated.Year.ToString(), RegexOptions.IgnoreCase);
                        release.ReleasePath = Regex.Replace(release.ReleasePath, @"\{Month\}", article.DateCreated.Month.ToString(), RegexOptions.IgnoreCase);
                        release.ReleasePath = Regex.Replace(release.ReleasePath, @"\{Day\}", article.DateCreated.Day.ToString(), RegexOptions.IgnoreCase);

                        // 生成静态页
#warning 按照 ContentHtml 长度，删除多页，需要测试
                        string pagerSeparator = "<!--Article.ContentHtml.Pager-->";
                        if (article.ContentHtml.IndexOf(pagerSeparator) > -1) // 含有分页
                        {
                            string[] contentHtmls = article.ContentHtml.Split(pagerSeparator.ToCharArray());
                            for (int index = 0; index < contentHtmls.Length; index++)
                            {
                                string replacedArticleId = string.Format("{0}/{1}", article.ArticleId, (index + 1));
                                release.ReleasePath = Regex.Replace(release.ReleasePath, @"\{ArticleId\}", replacedArticleId, RegexOptions.IgnoreCase);

                                // 删除文章静态页
                                release.ReleasePath = System.Web.HttpContext.Current.Server.MapPath(release.ReleasePath);
                                if (System.IO.File.Exists(release.ReleasePath))
                                    System.IO.File.Delete(release.ReleasePath);
                            }
                        }
                        else
                        {
                            release.ReleasePath = Regex.Replace(release.ReleasePath, @"\{ArticleId\}", article.ArticleId.ToString(), RegexOptions.IgnoreCase);

                            // 删除文章静态页
                            release.ReleasePath = System.Web.HttpContext.Current.Server.MapPath(release.ReleasePath);
                            if (System.IO.File.Exists(release.ReleasePath))
                                System.IO.File.Delete(release.ReleasePath);
                        }
                        
                        break;
                    default:
                        throw new System.ArgumentException("TemplateType");
                }
            }

            // 3 发布被引用的页面
            ReleasingRelatedReleases(article.Category.CategoryGuid);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="article"></param>
        public void ReleasingArticleItem(Article article)
        {
            List<Release> releases = GetReleasesByCategoryGuid(article.Category.CategoryGuid, TemplateType.ArticleItem);
            foreach (Release release in releases)
            {
                ReleasingArticleItem(article, release);
            }
        }

        public void ReleasingArticleItem(Article article, Release articleRelease)
        {
            // 详细页：{ReleaseDirectory}/{CategoryId}/{Year}-{Month}-{Day}/{ArticleId}.htm
            if (articleRelease.Template.TemplateType != TemplateType.ArticleItem) return;

            // 装载模版
            articleRelease.Template.TemplatePath = Regex.Replace(articleRelease.Template.TemplatePath, @"\{TemplateDirectory\}", this.TemplateDirectory, RegexOptions.IgnoreCase);
            string templatePath = System.Web.HttpContext.Current.Server.MapPath(articleRelease.Template.TemplatePath);
            Wis.Toolkit.Templates.TemplateManager templateManager = Wis.Toolkit.Templates.TemplateManager.LoadFile(templatePath, Encoding.UTF8);

            templateManager.SetVariable("ApplicationPath", this.ApplicationPath);
            templateManager.SetVariable("TemplateDirectory", this.TemplateDirectory);
            templateManager.SetVariable("ReleaseDirectory", this.ReleaseDirectory);

            // 处理 Release
            articleRelease.ReleasePath = Regex.Replace(articleRelease.ReleasePath, @"\{ReleaseDirectory\}", this.ReleaseDirectory, RegexOptions.IgnoreCase);
            articleRelease.ReleasePath = Regex.Replace(articleRelease.ReleasePath, @"\{CategoryId\}", article.Category.CategoryId.ToString(), RegexOptions.IgnoreCase);
            articleRelease.ReleasePath = Regex.Replace(articleRelease.ReleasePath, @"\{Year\}", article.DateCreated.Year.ToString(), RegexOptions.IgnoreCase);
            articleRelease.ReleasePath = Regex.Replace(articleRelease.ReleasePath, @"\{Month\}", article.DateCreated.Month.ToString(), RegexOptions.IgnoreCase);
            articleRelease.ReleasePath = Regex.Replace(articleRelease.ReleasePath, @"\{Day\}", article.DateCreated.Day.ToString(), RegexOptions.IgnoreCase);

            // 生成静态页
#warning 按照 ContentHtml 长度，生成多页，需要测试
            string pagerSeparator = "<!--Article.ContentHtml.Pager-->";
            if (article.ContentHtml.IndexOf(pagerSeparator) > -1) // 含有分页
            {
                string[] contentHtmls = article.ContentHtml.Split(pagerSeparator.ToCharArray());
                for (int index = 0; index < contentHtmls.Length; index++)
                {
                    article.ContentHtml = contentHtmls[index] + ReleaseTinyPager((index + 1), contentHtmls.Length);
                    templateManager.SetVariable("Article", article); // 最近更新的文章
                    string replacedArticleId = string.Format("{0}/{1}", article.ArticleId, (index + 1));

                    string rawReleasePath = articleRelease.ReleasePath; // 记录{ArticleId}
                    articleRelease.ReleasePath = Regex.Replace(rawReleasePath, @"\{ArticleId\}", replacedArticleId, RegexOptions.IgnoreCase);
                    ReleasingStaticPage(articleRelease, templateManager);
                    articleRelease.ReleasePath = rawReleasePath;

                    ReleasingStaticPage(articleRelease, templateManager);
                }
            }
            else
            {
                templateManager.SetVariable("Article", article); // 最近更新的文章
                articleRelease.ReleasePath = Regex.Replace(articleRelease.ReleasePath, @"\{ArticleId\}", article.ArticleId.ToString(), RegexOptions.IgnoreCase);

                ReleasingStaticPage(articleRelease, templateManager);
            }
        }

        public void ReleasingArticleList(Category articleCategory, Release articleCategoryRelease)
        {
            // 列表页：{ReleaseDirectory}/{CategoryId}/{PageIndex}.htm
            if (articleCategoryRelease.Template.TemplateType != TemplateType.ArticleList) return;

            // 装载模版
            articleCategoryRelease.Template.TemplatePath = Regex.Replace(articleCategoryRelease.Template.TemplatePath, @"\{TemplateDirectory\}", this.TemplateDirectory, RegexOptions.IgnoreCase);
            string templatePath = System.Web.HttpContext.Current.Server.MapPath(articleCategoryRelease.Template.TemplatePath);
            Wis.Toolkit.Templates.TemplateManager templateManager = Wis.Toolkit.Templates.TemplateManager.LoadFile(templatePath, Encoding.UTF8);
            templateManager.SetVariable("ArticleCategory", articleCategory);
            templateManager.SetVariable("ApplicationPath", this.ApplicationPath);
            templateManager.SetVariable("TemplateDirectory", this.TemplateDirectory);
            templateManager.SetVariable("ReleaseDirectory", this.ReleaseDirectory);

            // 处理 Release
            articleCategoryRelease.ReleasePath = Regex.Replace(articleCategoryRelease.ReleasePath, @"\{ReleaseDirectory\}", this.ReleaseDirectory, RegexOptions.IgnoreCase);
            articleCategoryRelease.ReleasePath = Regex.Replace(articleCategoryRelease.ReleasePath, @"\{CategoryId\}", articleCategory.CategoryId.ToString(), RegexOptions.IgnoreCase);

            if (articleCategoryRelease.PageSize.HasValue == false)
                throw new System.ArgumentNullException(string.Format("发布编号为 {0} 的 PageSize 参数未配置", articleCategoryRelease.ReleaseId));

            // 计算一共有多少页，然后逐页生成
            int pageSize = articleCategoryRelease.PageSize.Value;
            templateManager.SetVariable("PageSize", pageSize);
            templateManager.SetVariable("RecordCount", articleCategory.RecordCount);

            // 求页总数 pageCount
            int pageCount;
            pageCount = articleCategory.RecordCount / pageSize;
            if (articleCategory.RecordCount % pageSize != 0)
                pageCount += 1;
            templateManager.SetVariable("PageCount", pageCount);

            // 逐页生成
            if (articleCategoryRelease.PagerStyle != PagerStyle.None)
            {
                for (int pageIndex = 1; pageIndex <= pageCount; pageIndex++)
                {
                    // {RootPath}/{CategoryId}/{PageIndex}.htm
                    templateManager.SetVariable("PageIndex", pageIndex);
                    if (articleCategoryRelease.PagerStyle == PagerStyle.Normal)
                        templateManager.SetVariable("Pager", ReleasePager(pageIndex, articleCategory.RecordCount, pageCount));
                    else if (articleCategoryRelease.PagerStyle == PagerStyle.Tiny)
                        templateManager.SetVariable("Pager", ReleaseTinyPager(pageIndex, pageCount));
                    else
                        throw new System.ArgumentException("PagerStyle");

                    string rawReleasePath = articleCategoryRelease.ReleasePath; // 记录{PageIndex}
                    articleCategoryRelease.ReleasePath = Regex.Replace(rawReleasePath, @"\{PageIndex\}", pageIndex.ToString(), RegexOptions.IgnoreCase);
                    ReleasingStaticPage(articleCategoryRelease, templateManager);
                    articleCategoryRelease.ReleasePath = rawReleasePath;
                }
            }
        }

        public void ReleasingArticleIndex(Category articleCategory, Release articleCategoryRelease)
        {
            // 普通文章索引页：{ReleaseDirectory}/{CategoryId}/1.htm
            if (articleCategoryRelease.Template.TemplateType != TemplateType.ArticleIndex) return;

            // 装载模版
            articleCategoryRelease.Template.TemplatePath = Regex.Replace(articleCategoryRelease.Template.TemplatePath, @"\{TemplateDirectory\}", this.TemplateDirectory, RegexOptions.IgnoreCase);
            string templatePath = System.Web.HttpContext.Current.Server.MapPath(articleCategoryRelease.Template.TemplatePath);
            Wis.Toolkit.Templates.TemplateManager templateManager = Wis.Toolkit.Templates.TemplateManager.LoadFile(templatePath, Encoding.UTF8);
            templateManager.SetVariable("ArticleCategory", articleCategory);
            templateManager.SetVariable("ApplicationPath", this.ApplicationPath);
            templateManager.SetVariable("TemplateDirectory", this.TemplateDirectory);
            templateManager.SetVariable("ReleaseDirectory", this.ReleaseDirectory);

            // 处理 Release
            articleCategoryRelease.ReleasePath = Regex.Replace(articleCategoryRelease.ReleasePath, @"\{ReleaseDirectory\}", this.ReleaseDirectory, RegexOptions.IgnoreCase);
            articleCategoryRelease.ReleasePath = Regex.Replace(articleCategoryRelease.ReleasePath, @"\{CategoryId\}", articleCategory.CategoryId.ToString(), RegexOptions.IgnoreCase);

            ReleasingStaticPage(articleCategoryRelease, templateManager);
        }
        #endregion

#warning 发布图片文章，完善功能
        #region 发布图片文章

        /// <summary>
        /// 
        /// </summary>
        /// <param name="photoArticle"></param>
        public void ReleasingPhotoArticle(ArticlePhoto photoArticle)
        {
            // 1 发布 VideoArticle 页面，根据分类编号找Item的模版
            // 2 发布 Category 页面，根据分类编号找List或Index的模版
            List<Release> releases = GetReleasesByCategoryGuid(photoArticle.Category.CategoryGuid);
            foreach (Release release in releases)
            {
                switch (release.Template.TemplateType)
                {
                    case TemplateType.PhotoArticleIndex: // 视频文章索引页
                        ReleasingPhotoArticleIndex(photoArticle.Category, release);
                        break;
                    case TemplateType.PhotoArticleList: // 视频文章列表页，带分页
                        ReleasingPhotoArticleList(photoArticle.Category, release);
                        break;
                    case TemplateType.PhotoArticleItem:// 视频文章详细页
                        ReleasingPhotoArticleItem(photoArticle, release);
                        break;
                    default:
                        throw new System.ArgumentException("TemplateType");
                }
            }

            // 3 发布被引用的页面
            ReleasingRelatedReleases(photoArticle.Category.CategoryGuid);
        }

        public void ReleasingRemovedPhotoArticle(ArticlePhoto photoArticle)
        {
            // 1 发布 PhotoArticle 页面，根据分类编号找 Item 的模版
            // 2 发布 Category 页面，根据分类编号找List或Index的模版
            List<Release> releases = GetReleasesByCategoryGuid(photoArticle.Category.CategoryGuid);
            foreach (Release release in releases)
            {
                switch (release.Template.TemplateType)
                {
                    case TemplateType.PhotoArticleIndex: // 视频文章索引页
                        ReleasingPhotoArticleIndex(photoArticle.Category, release);
                        break;
                    case TemplateType.PhotoArticleList: // 视频文章列表页，带分页
                        ReleasingPhotoArticleList(photoArticle.Category, release);
                        break;
                    case TemplateType.PhotoArticleItem:// 移除文章详细页
                        // 处理 Release
                        release.ReleasePath = Regex.Replace(release.ReleasePath, @"\{ReleaseDirectory\}", this.ReleaseDirectory, RegexOptions.IgnoreCase);
                        release.ReleasePath = Regex.Replace(release.ReleasePath, @"\{CategoryId\}", photoArticle.Category.CategoryId.ToString(), RegexOptions.IgnoreCase);
                        release.ReleasePath = Regex.Replace(release.ReleasePath, @"\{Year\}", photoArticle.DateCreated.Year.ToString(), RegexOptions.IgnoreCase);
                        release.ReleasePath = Regex.Replace(release.ReleasePath, @"\{Month\}", photoArticle.DateCreated.Month.ToString(), RegexOptions.IgnoreCase);
                        release.ReleasePath = Regex.Replace(release.ReleasePath, @"\{Day\}", photoArticle.DateCreated.Day.ToString(), RegexOptions.IgnoreCase);

                        // 生成静态页
#warning 按照 ContentHtml 长度，删除多页，需要测试
                        string pagerSeparator = "<!--Article.ContentHtml.Pager-->";
                        if (photoArticle.ContentHtml.IndexOf(pagerSeparator) > -1) // 含有分页
                        {
                            string[] contentHtmls = photoArticle.ContentHtml.Split(pagerSeparator.ToCharArray());
                            for (int index = 0; index < contentHtmls.Length; index++)
                            {
                                string replacedArticleId = string.Format("{0}/{1}", photoArticle.ArticleId, (index + 1));
                                release.ReleasePath = Regex.Replace(release.ReleasePath, @"\{ArticleId\}", replacedArticleId, RegexOptions.IgnoreCase);

                                // 删除文章静态页
                                release.ReleasePath = System.Web.HttpContext.Current.Server.MapPath(release.ReleasePath);
                                if (System.IO.File.Exists(release.ReleasePath))
                                    System.IO.File.Delete(release.ReleasePath);
                            }
                        }
                        else
                        {
                            release.ReleasePath = Regex.Replace(release.ReleasePath, @"\{ArticleId\}", photoArticle.ArticleId.ToString(), RegexOptions.IgnoreCase);

                            // 删除文章静态页
                            release.ReleasePath = System.Web.HttpContext.Current.Server.MapPath(release.ReleasePath);
                            if (System.IO.File.Exists(release.ReleasePath))
                                System.IO.File.Delete(release.ReleasePath);
                        }

                        break;
                    default:
                        throw new System.ArgumentException("TemplateType");
                }
            }

            // 3 发布被引用的页面
            ReleasingRelatedReleases(photoArticle.Category.CategoryGuid);
        }

        /// <summary>
        /// 发布图片文章内容页。
        /// </summary>
        /// <param name="photoArticle">图片文章</param>
        public void ReleasingPhotoArticleItem(ArticlePhoto photoArticle)
        {
            List<Release> releases = GetReleasesByCategoryGuid(photoArticle.Category.CategoryGuid, TemplateType.PhotoArticleItem);
            foreach (Release release in releases)
            {
                ReleasingPhotoArticleItem(photoArticle, release);
            }
        }

        /// <summary>
        /// 发布图片文章内容页。
        /// </summary>
        /// <param name="photoArticle">图片文章</param>
        /// <param name="photoArticleRelease">图片文章发布类</param>
        public void ReleasingPhotoArticleItem(ArticlePhoto photoArticle, Release photoArticleRelease)
        {
            // 详细页：{ReleaseDirectory}/{CategoryId}/{Year}-{Month}-{Day}/{ArticleId}.htm
            if (photoArticleRelease.Template.TemplateType != TemplateType.PhotoArticleItem) return;

            // 装载模版
            photoArticleRelease.Template.TemplatePath = Regex.Replace(photoArticleRelease.Template.TemplatePath, @"\{TemplateDirectory\}", this.TemplateDirectory, RegexOptions.IgnoreCase);
            string templatePath = System.Web.HttpContext.Current.Server.MapPath(photoArticleRelease.Template.TemplatePath);
            Wis.Toolkit.Templates.TemplateManager templateManager = Wis.Toolkit.Templates.TemplateManager.LoadFile(templatePath, Encoding.UTF8);

            templateManager.SetVariable("ApplicationPath", this.ApplicationPath);
            templateManager.SetVariable("TemplateDirectory", this.TemplateDirectory);
            templateManager.SetVariable("ReleaseDirectory", this.ReleaseDirectory);

            // 处理 Release
            photoArticleRelease.ReleasePath = Regex.Replace(photoArticleRelease.ReleasePath, @"\{ReleaseDirectory\}", this.ReleaseDirectory, RegexOptions.IgnoreCase);
            photoArticleRelease.ReleasePath = Regex.Replace(photoArticleRelease.ReleasePath, @"\{CategoryId\}", photoArticle.Category.CategoryId.ToString(), RegexOptions.IgnoreCase);
            photoArticleRelease.ReleasePath = Regex.Replace(photoArticleRelease.ReleasePath, @"\{Year\}", photoArticle.DateCreated.Year.ToString(), RegexOptions.IgnoreCase);
            photoArticleRelease.ReleasePath = Regex.Replace(photoArticleRelease.ReleasePath, @"\{Month\}", photoArticle.DateCreated.Month.ToString(), RegexOptions.IgnoreCase);
            photoArticleRelease.ReleasePath = Regex.Replace(photoArticleRelease.ReleasePath, @"\{Day\}", photoArticle.DateCreated.Day.ToString(), RegexOptions.IgnoreCase);

            // 生成静态页
#warning 按照 ContentHtml 长度，生成多页，需要测试
            string pagerSeparator = "<!--Article.ContentHtml.Pager-->";
            if (photoArticle.ContentHtml.IndexOf(pagerSeparator) > -1) // 含有分页
            {
                string[] contentHtmls = photoArticle.ContentHtml.Split(pagerSeparator.ToCharArray());
                for (int index = 0; index < contentHtmls.Length; index++)
                {
                    photoArticle.ContentHtml = contentHtmls[index] + ReleaseTinyPager((index + 1), contentHtmls.Length);
                    templateManager.SetVariable("PhotoArticle", photoArticle); // 最近更新的文章
                    string replacedArticleId = string.Format("{0}/{1}", photoArticle.ArticleId, (index + 1));
                    //photoArticleRelease.ReleasePath = Regex.Replace(photoArticleRelease.ReleasePath, @"\{ArticleId\}", replacedArticleId, RegexOptions.IgnoreCase);
                    string rawReleasePath = photoArticleRelease.ReleasePath; // 记录{ArticleId}
                    photoArticleRelease.ReleasePath = Regex.Replace(rawReleasePath, @"\{ArticleId\}", replacedArticleId, RegexOptions.IgnoreCase);
                    ReleasingStaticPage(photoArticleRelease, templateManager);
                    photoArticleRelease.ReleasePath = rawReleasePath;

                    ReleasingStaticPage(photoArticleRelease, templateManager);
                }
            }
            else
            {
                templateManager.SetVariable("PhotoArticle", photoArticle); // 最近更新的文章
                photoArticleRelease.ReleasePath = Regex.Replace(photoArticleRelease.ReleasePath, @"\{ArticleId\}", photoArticle.ArticleId.ToString(), RegexOptions.IgnoreCase);

                ReleasingStaticPage(photoArticleRelease, templateManager);
            }
        }

        /// <summary>
        /// 发布图片文章列表页。
        /// </summary>
        /// <param name="photoArticleCategory">图片文章的分类</param>
        /// <param name="photoArticleCategoryRelease">图片文章的分类对应的发布对象</param>
        public void ReleasingPhotoArticleList(Category photoArticleCategory, Release photoArticleCategoryRelease)
        {
            // 列表页：{ReleaseDirectory}/{CategoryId}/{PageIndex}.htm
            if (photoArticleCategoryRelease.Template.TemplateType != TemplateType.PhotoArticleList) return;

            // 装载模版
            photoArticleCategoryRelease.Template.TemplatePath = Regex.Replace(photoArticleCategoryRelease.Template.TemplatePath, @"\{TemplateDirectory\}", this.TemplateDirectory, RegexOptions.IgnoreCase);
            string templatePath = System.Web.HttpContext.Current.Server.MapPath(photoArticleCategoryRelease.Template.TemplatePath);
            Wis.Toolkit.Templates.TemplateManager templateManager = Wis.Toolkit.Templates.TemplateManager.LoadFile(templatePath, Encoding.UTF8);
            templateManager.SetVariable("PhotoArticleCategory", photoArticleCategory);
            templateManager.SetVariable("ApplicationPath", this.ApplicationPath);
            templateManager.SetVariable("TemplateDirectory", this.TemplateDirectory);
            templateManager.SetVariable("ReleaseDirectory", this.ReleaseDirectory);

            // 处理 Release
            photoArticleCategoryRelease.ReleasePath = Regex.Replace(photoArticleCategoryRelease.ReleasePath, @"\{ReleaseDirectory\}", this.ReleaseDirectory, RegexOptions.IgnoreCase);
            photoArticleCategoryRelease.ReleasePath = Regex.Replace(photoArticleCategoryRelease.ReleasePath, @"\{CategoryId\}", photoArticleCategory.CategoryId.ToString(), RegexOptions.IgnoreCase);

            if (photoArticleCategoryRelease.PageSize.HasValue == false)
                throw new System.ArgumentNullException(string.Format("发布编号为 {0} 的 PageSize 参数未配置", photoArticleCategoryRelease.ReleaseId));

            // 计算一共有多少页，然后逐页生成
            int pageSize = photoArticleCategoryRelease.PageSize.Value;
            templateManager.SetVariable("PageSize", pageSize);
            templateManager.SetVariable("RecordCount", photoArticleCategory.RecordCount);

            // 求页总数 pageCount
            int pageCount;
            pageCount = photoArticleCategory.RecordCount / pageSize;
            if (photoArticleCategory.RecordCount % pageSize != 0)
                pageCount += 1;
            templateManager.SetVariable("PageCount", pageCount);

            // 逐页生成
            if (photoArticleCategoryRelease.PagerStyle != PagerStyle.None)
            {
                for (int pageIndex = 1; pageIndex <= pageCount; pageIndex++)
                {
                    // {RootPath}/{CategoryId}/{PageIndex}.htm
                    templateManager.SetVariable("PageIndex", pageIndex);
                    if (photoArticleCategoryRelease.PagerStyle == PagerStyle.Normal)
                        templateManager.SetVariable("Pager", ReleasePager(pageIndex, photoArticleCategory.RecordCount, pageCount));
                    else if (photoArticleCategoryRelease.PagerStyle == PagerStyle.Tiny)
                        templateManager.SetVariable("Pager", ReleaseTinyPager(pageIndex, pageCount));
                    else
                        throw new System.ArgumentException("PagerStyle");

                    string rawReleasePath = photoArticleCategoryRelease.ReleasePath; // 记录{PageIndex}
                    photoArticleCategoryRelease.ReleasePath = Regex.Replace(rawReleasePath, @"\{PageIndex\}", pageIndex.ToString(), RegexOptions.IgnoreCase);
                    ReleasingStaticPage(photoArticleCategoryRelease, templateManager);
                    photoArticleCategoryRelease.ReleasePath = rawReleasePath;
                }
            }
        }

        /// <summary>
        /// 发布图片文章索引页。
        /// </summary>
        /// <param name="photoArticleCategory">图片文章的分类</param>
        /// <param name="photoArticleCategoryRelease">图片文章的分类对应的发布对象</param>
        public void ReleasingPhotoArticleIndex(Category photoArticleCategory, Release photoArticleCategoryRelease)
        {
            // 图片文章索引页：{ReleaseDirectory}/{CategoryId}/1.htm
            if (photoArticleCategoryRelease.Template.TemplateType != TemplateType.PhotoArticleIndex) return;

            // 装载模版
            photoArticleCategoryRelease.Template.TemplatePath = Regex.Replace(photoArticleCategoryRelease.Template.TemplatePath, @"\{TemplateDirectory\}", this.TemplateDirectory, RegexOptions.IgnoreCase);
            string templatePath = System.Web.HttpContext.Current.Server.MapPath(photoArticleCategoryRelease.Template.TemplatePath);
            Wis.Toolkit.Templates.TemplateManager templateManager = Wis.Toolkit.Templates.TemplateManager.LoadFile(templatePath, Encoding.UTF8);
            templateManager.SetVariable("PhotoArticleCategory", photoArticleCategory);
            templateManager.SetVariable("ApplicationPath", this.ApplicationPath);
            templateManager.SetVariable("TemplateDirectory", this.TemplateDirectory);
            templateManager.SetVariable("ReleaseDirectory", this.ReleaseDirectory);

            // 处理 Release
            photoArticleCategoryRelease.ReleasePath = Regex.Replace(photoArticleCategoryRelease.ReleasePath, @"\{ReleaseDirectory\}", this.ReleaseDirectory, RegexOptions.IgnoreCase);
            photoArticleCategoryRelease.ReleasePath = Regex.Replace(photoArticleCategoryRelease.ReleasePath, @"\{CategoryId\}", photoArticleCategory.CategoryId.ToString(), RegexOptions.IgnoreCase);

            ReleasingStaticPage(photoArticleCategoryRelease, templateManager);
        }
        #endregion

        #region 发布视频文章

        /// <summary>
        /// 发布视频文章静态页和关联的静态页。
        /// </summary>
        /// <param name="videoArticle"></param>
        public void ReleasingVideoArticle(VideoArticle videoArticle)
        {
            // 1 发布 VideoArticle 页面，根据分类编号找Item的模版
            // 2 发布 Category 页面，根据分类编号找List或Index的模版
            List<Release> releases = GetReleasesByCategoryGuid(videoArticle.Article.Category.CategoryGuid);
            foreach (Release release in releases)
            {
                switch (release.Template.TemplateType)
                {
                    case TemplateType.VideoArticleIndex: // 视频文章索引页
                        ReleasingVideoArticleIndex(videoArticle.Article.Category, release);
                        break;
                    case TemplateType.VideoArticleList: // 视频文章列表页，带分页
                        ReleasingVideoArticleList(videoArticle.Article.Category, release);
                        break;
                    case TemplateType.VideoArticleItem:// 视频文章详细页
                        ReleasingVideoArticleItem(videoArticle, release);
                        break;
                    default:
                        throw new System.ArgumentException("TemplateType");
                }          
            }

            // 3 发布被引用的页面
            ReleasingRelatedReleases(videoArticle.Article.Category.CategoryGuid);
        }

        public void ReleasingRemovedVideoArticle(VideoArticle videoArticle)
        {
            // 1 发布 VideoArticle 页面，根据分类编号找 Item 的模版
            // 2 发布 Category 页面，根据分类编号找List或Index的模版
            List<Release> releases = GetReleasesByCategoryGuid(videoArticle.Article.Category.CategoryGuid);
            foreach (Release release in releases)
            {
                switch (release.Template.TemplateType)
                {
                    case TemplateType.VideoArticleIndex: // 视频文章索引页
                        ReleasingVideoArticleIndex(videoArticle.Article.Category, release);
                        break;
                    case TemplateType.VideoArticleList: // 视频文章列表页，带分页
                        ReleasingVideoArticleList(videoArticle.Article.Category, release);
                        break;
                    case TemplateType.VideoArticleItem:// 移除文章详细页
                        // 处理 Release
                        release.ReleasePath = Regex.Replace(release.ReleasePath, @"\{ReleaseDirectory\}", this.ReleaseDirectory, RegexOptions.IgnoreCase);
                        release.ReleasePath = Regex.Replace(release.ReleasePath, @"\{CategoryId\}", videoArticle.Article.Category.CategoryId.ToString(), RegexOptions.IgnoreCase);
                        release.ReleasePath = Regex.Replace(release.ReleasePath, @"\{Year\}", videoArticle.Article.DateCreated.Year.ToString(), RegexOptions.IgnoreCase);
                        release.ReleasePath = Regex.Replace(release.ReleasePath, @"\{Month\}", videoArticle.Article.DateCreated.Month.ToString(), RegexOptions.IgnoreCase);
                        release.ReleasePath = Regex.Replace(release.ReleasePath, @"\{Day\}", videoArticle.Article.DateCreated.Day.ToString(), RegexOptions.IgnoreCase);

                        // 生成静态页
#warning 按照 ContentHtml 长度，删除多页，需要测试
                        string pagerSeparator = "<!--Article.ContentHtml.Pager-->";
                        if (videoArticle.Article.ContentHtml.IndexOf(pagerSeparator) > -1) // 含有分页
                        {
                            string[] contentHtmls = videoArticle.Article.ContentHtml.Split(pagerSeparator.ToCharArray());
                            for (int index = 0; index < contentHtmls.Length; index++)
                            {
                                string replacedArticleId = string.Format("{0}/{1}", videoArticle.Article.ArticleId, (index + 1));
                                release.ReleasePath = Regex.Replace(release.ReleasePath, @"\{ArticleId\}", replacedArticleId, RegexOptions.IgnoreCase);

                                // 删除文章静态页
                                release.ReleasePath = System.Web.HttpContext.Current.Server.MapPath(release.ReleasePath);
                                if (System.IO.File.Exists(release.ReleasePath))
                                    System.IO.File.Delete(release.ReleasePath);
                            }
                        }
                        else
                        {
                            release.ReleasePath = Regex.Replace(release.ReleasePath, @"\{ArticleId\}", videoArticle.Article.ArticleId.ToString(), RegexOptions.IgnoreCase);

                            // 删除文章静态页
                            release.ReleasePath = System.Web.HttpContext.Current.Server.MapPath(release.ReleasePath);
                            if (System.IO.File.Exists(release.ReleasePath))
                                System.IO.File.Delete(release.ReleasePath);
                        }

                        break;
                    default:
                        throw new System.ArgumentException("TemplateType");
                }
            }

            // 3 发布被引用的页面
            ReleasingRelatedReleases(videoArticle.Article.Category.CategoryGuid);
        }


        /// <summary>
        /// 发布视频文章内容页。
        /// </summary>
        /// <param name="videoArticle">视频文章</param>
        public void ReleasingVideoArticleItem(VideoArticle videoArticle)
        {
            List<Release> releases = GetReleasesByCategoryGuid(videoArticle.Article.Category.CategoryGuid, TemplateType.VideoArticleItem);
            foreach (Release release in releases)
            {
                ReleasingVideoArticleItem(videoArticle, release);
            }
        }

        /// <summary>
        /// 发布视频文章内容页。
        /// </summary>
        /// <param name="videoArticle">视频文章</param>
        /// <param name="videoArticleRelease">视频文章发布对象</param>
        public void ReleasingVideoArticleItem(VideoArticle videoArticle, Release videoArticleRelease)
        {
            // 详细页：{ReleaseDirectory}/{CategoryId}/{Year}-{Month}-{Day}/{ArticleId}.htm
            if (videoArticleRelease.Template.TemplateType != TemplateType.VideoArticleItem) return;

            // 装载模版
            videoArticleRelease.Template.TemplatePath = Regex.Replace(videoArticleRelease.Template.TemplatePath, @"\{TemplateDirectory\}", this.TemplateDirectory, RegexOptions.IgnoreCase);
            string templatePath = System.Web.HttpContext.Current.Server.MapPath(videoArticleRelease.Template.TemplatePath);
            Wis.Toolkit.Templates.TemplateManager templateManager = Wis.Toolkit.Templates.TemplateManager.LoadFile(templatePath, Encoding.UTF8);

            templateManager.SetVariable("ApplicationPath", this.ApplicationPath);
            templateManager.SetVariable("TemplateDirectory", this.TemplateDirectory);
            templateManager.SetVariable("ReleaseDirectory", this.ReleaseDirectory);

            // 处理 Release
            videoArticleRelease.ReleasePath = Regex.Replace(videoArticleRelease.ReleasePath, @"\{ReleaseDirectory\}", this.ReleaseDirectory, RegexOptions.IgnoreCase);
            videoArticleRelease.ReleasePath = Regex.Replace(videoArticleRelease.ReleasePath, @"\{CategoryId\}", videoArticle.Article.Category.CategoryId.ToString(), RegexOptions.IgnoreCase);
            videoArticleRelease.ReleasePath = Regex.Replace(videoArticleRelease.ReleasePath, @"\{Year\}", videoArticle.Article.DateCreated.Year.ToString(), RegexOptions.IgnoreCase);
            videoArticleRelease.ReleasePath = Regex.Replace(videoArticleRelease.ReleasePath, @"\{Month\}", videoArticle.Article.DateCreated.Month.ToString(), RegexOptions.IgnoreCase);
            videoArticleRelease.ReleasePath = Regex.Replace(videoArticleRelease.ReleasePath, @"\{Day\}", videoArticle.Article.DateCreated.Day.ToString(), RegexOptions.IgnoreCase);

            // 生成静态页
#warning 按照 ContentHtml 长度，生成多页，需要测试
            string pagerSeparator = "<!--Article.ContentHtml.Pager-->";
            if (videoArticle.Article.ContentHtml.IndexOf(pagerSeparator) > -1) // 含有分页
            {
                string[] contentHtmls = videoArticle.Article.ContentHtml.Split(pagerSeparator.ToCharArray());
                for (int index = 0; index < contentHtmls.Length; index++)
                {
                    videoArticle.Article.ContentHtml = contentHtmls[index] + ReleaseTinyPager((index + 1), contentHtmls.Length);
                    templateManager.SetVariable("VideoArticle", videoArticle); // 最近更新的文章
                    string replacedArticleId = string.Format("{0}/{1}", videoArticle.Article.ArticleId, (index + 1));
                    //videoArticleRelease.ReleasePath = Regex.Replace(videoArticleRelease.ReleasePath, @"\{ArticleId\}", replacedArticleId, RegexOptions.IgnoreCase);
                    string rawReleasePath = videoArticleRelease.ReleasePath; // 记录{ArticleId}
                    videoArticleRelease.ReleasePath = Regex.Replace(rawReleasePath, @"\{ArticleId\}", replacedArticleId, RegexOptions.IgnoreCase);
                    ReleasingStaticPage(videoArticleRelease, templateManager);
                    videoArticleRelease.ReleasePath = rawReleasePath;

                    ReleasingStaticPage(videoArticleRelease, templateManager);
                }
            }
            else
            {
                templateManager.SetVariable("VideoArticle", videoArticle); // 最近更新的文章
                videoArticleRelease.ReleasePath = Regex.Replace(videoArticleRelease.ReleasePath, @"\{ArticleId\}", videoArticle.Article.ArticleId.ToString(), RegexOptions.IgnoreCase);

                ReleasingStaticPage(videoArticleRelease, templateManager);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="videoArticleCategory"></param>
        /// <param name="videoArticleCategoryRelease"></param>
        public void ReleasingVideoArticleList(Category videoArticleCategory, Release videoArticleCategoryRelease)
        {
            // 列表页：{ReleaseDirectory}/{CategoryId}/{PageIndex}.htm
            if (videoArticleCategoryRelease.Template.TemplateType != TemplateType.VideoArticleList) return;

            // 装载模版
            videoArticleCategoryRelease.Template.TemplatePath = Regex.Replace(videoArticleCategoryRelease.Template.TemplatePath, @"\{TemplateDirectory\}", this.TemplateDirectory, RegexOptions.IgnoreCase);
            string templatePath = System.Web.HttpContext.Current.Server.MapPath(videoArticleCategoryRelease.Template.TemplatePath);
            Wis.Toolkit.Templates.TemplateManager templateManager = Wis.Toolkit.Templates.TemplateManager.LoadFile(templatePath, Encoding.UTF8);
            templateManager.SetVariable("VideoArticleCategory", videoArticleCategory);
            templateManager.SetVariable("ApplicationPath", this.ApplicationPath);
            templateManager.SetVariable("TemplateDirectory", this.TemplateDirectory);
            templateManager.SetVariable("ReleaseDirectory", this.ReleaseDirectory);

            // 处理 Release
            videoArticleCategoryRelease.ReleasePath = Regex.Replace(videoArticleCategoryRelease.ReleasePath, @"\{ReleaseDirectory\}", this.ReleaseDirectory, RegexOptions.IgnoreCase);
            videoArticleCategoryRelease.ReleasePath = Regex.Replace(videoArticleCategoryRelease.ReleasePath, @"\{CategoryId\}", videoArticleCategory.CategoryId.ToString(), RegexOptions.IgnoreCase);

            if (videoArticleCategoryRelease.PageSize.HasValue == false)
                throw new System.ArgumentNullException(string.Format("发布编号为 {0} 的 PageSize 参数未配置", videoArticleCategoryRelease.ReleaseId));

            // 计算一共有多少页，然后逐页生成
            int pageSize = videoArticleCategoryRelease.PageSize.Value;
            templateManager.SetVariable("PageSize", pageSize);
            templateManager.SetVariable("RecordCount", videoArticleCategory.RecordCount);

            // 求页总数 pageCount
            int pageCount;
            pageCount = videoArticleCategory.RecordCount / pageSize;
            if (videoArticleCategory.RecordCount % pageSize != 0)
                pageCount += 1;
            templateManager.SetVariable("PageCount", pageCount);

            // 逐页生成
            if (videoArticleCategoryRelease.PagerStyle != PagerStyle.None)
            {
                for (int pageIndex = 1; pageIndex <= pageCount; pageIndex++)
                {
                    // {RootPath}/{CategoryId}/{PageIndex}.htm
                    templateManager.SetVariable("PageIndex", pageIndex);
                    if (videoArticleCategoryRelease.PagerStyle == PagerStyle.Normal)
                        templateManager.SetVariable("Pager", ReleasePager(pageIndex, videoArticleCategory.RecordCount, pageCount));
                    else if (videoArticleCategoryRelease.PagerStyle == PagerStyle.Tiny)
                        templateManager.SetVariable("Pager", ReleaseTinyPager(pageIndex, pageCount));
                    else
                        throw new System.ArgumentException("PagerStyle");

                    string rawReleasePath = videoArticleCategoryRelease.ReleasePath; // 记录{PageIndex}
                    videoArticleCategoryRelease.ReleasePath = Regex.Replace(rawReleasePath, @"\{PageIndex\}", pageIndex.ToString(), RegexOptions.IgnoreCase);
                    ReleasingStaticPage(videoArticleCategoryRelease, templateManager);
                    videoArticleCategoryRelease.ReleasePath = rawReleasePath;
                }
            }
        }

        /// <summary>
        /// 发布视频文章索引页。
        /// </summary>
        /// <param name="videoArticleCategory">视频文章分类</param>
        /// <param name="videoArticleCategoryRelease">视频文章分类对应的发布类</param>
        public void ReleasingVideoArticleIndex(Category videoArticleCategory, Release videoArticleCategoryRelease)
        {
            // 视频文章索引页：{ReleaseDirectory}/{CategoryId}/1.htm
            if (videoArticleCategoryRelease.Template.TemplateType != TemplateType.VideoArticleIndex) return;

            // 装载模版
            videoArticleCategoryRelease.Template.TemplatePath = Regex.Replace(videoArticleCategoryRelease.Template.TemplatePath, @"\{TemplateDirectory\}", this.TemplateDirectory, RegexOptions.IgnoreCase);
            string templatePath = System.Web.HttpContext.Current.Server.MapPath(videoArticleCategoryRelease.Template.TemplatePath);
            Wis.Toolkit.Templates.TemplateManager templateManager = Wis.Toolkit.Templates.TemplateManager.LoadFile(templatePath, Encoding.UTF8);
            templateManager.SetVariable("VideoArticleCategory", videoArticleCategory);
            templateManager.SetVariable("ApplicationPath", this.ApplicationPath);
            templateManager.SetVariable("TemplateDirectory", this.TemplateDirectory);
            templateManager.SetVariable("ReleaseDirectory", this.ReleaseDirectory);

            // 处理 Release
            videoArticleCategoryRelease.ReleasePath = Regex.Replace(videoArticleCategoryRelease.ReleasePath, @"\{ReleaseDirectory\}", this.ReleaseDirectory, RegexOptions.IgnoreCase);
            videoArticleCategoryRelease.ReleasePath = Regex.Replace(videoArticleCategoryRelease.ReleasePath, @"\{CategoryId\}", videoArticleCategory.CategoryId.ToString(), RegexOptions.IgnoreCase);

            ReleasingStaticPage(videoArticleCategoryRelease, templateManager);
        }
        #endregion

        #region 发布关联的页面
        /// <summary>
        /// 发布被引用的页面，比如首页某个栏目引用了该分类，专题引用了改分类，把关联的发布页面重新生成
        /// </summary>
        /// <param name="categoryGuid"></param>
        public void ReleasingRelatedReleases(Guid relatedGuid)
        {
            CategoryManager categoryManager = new CategoryManager();
            List<Release> relatedReleases = GetRelatedReleases(relatedGuid);
            foreach (Release relatedRelease in relatedReleases)
            {
                switch (relatedRelease.Template.TemplateType)
                {
                    case TemplateType.Index:
                        ReleasingIndex(relatedRelease);
                        break;
                    case TemplateType.ArticleItem: // 发布对应分类下的所有文章
                        ArticleManager articleManager = new ArticleManager();
                        List<Article> relatedArticles = articleManager.GetArticlesByReleaseGuid(relatedRelease.ReleaseGuid);
                        foreach (Article relatedArticle in relatedArticles)
                        {
                            ReleasingArticleItem(relatedArticle, relatedRelease);
                        }
                        break;
                    case TemplateType.ArticleList: // 发布对应分类下的所有列表页
                        List<Category> relatedArticleListCategories = categoryManager.GetCategorysByReleaseGuid(relatedRelease.ReleaseGuid);
                        foreach (Category relatedCategory in relatedArticleListCategories)
                        {
                            ReleasingArticleList(relatedCategory, relatedRelease);
                        }
                        break;
                    case TemplateType.ArticleIndex: // 发布对应分类下的所有索引页
                        List<Category> relatedArticleIndexCategories = categoryManager.GetCategorysByReleaseGuid(relatedRelease.ReleaseGuid);
                        foreach (Category relatedCategory in relatedArticleIndexCategories)
                        {
                            ReleasingArticleIndex(relatedCategory, relatedRelease);
                        }
                        break;
                    case TemplateType.Page: // 发布对应的单页
                        PageManager pageManager = new PageManager();
                        List<Page> relatedPages = pageManager.GetPagesByReleaseGuid(relatedRelease.ReleaseGuid);
                        foreach (Page relatedPage in relatedPages)
                        {
                            ReleasingPage(relatedPage, relatedRelease);
                        }
                        break;
                    case TemplateType.PhotoArticleItem: // 发布对应分类下的图片新闻
                        ArticlePhotoManager photoArticleManager = new ArticlePhotoManager();
                        List<ArticlePhoto> photoArticles = photoArticleManager.GetPhotoArticlesByReleaseGuid(relatedRelease.ReleaseGuid);
                        foreach (ArticlePhoto photoArticle in photoArticles)
                        {
                            ReleasingPhotoArticleItem(photoArticle, relatedRelease);
                        }
                        break;
                    case TemplateType.PhotoArticleList: // 发布对应分类下的列表页
                        List<Category> relatedPhotoArticleListCategories = categoryManager.GetCategorysByReleaseGuid(relatedRelease.ReleaseGuid);
                        foreach (Category relatedCategory in relatedPhotoArticleListCategories)
                        {
                            ReleasingPhotoArticleList(relatedCategory, relatedRelease);
                        }
                        break;
                    case TemplateType.SpecialList: // 发布专题列表页
                        SpecialManager specialManager = new SpecialManager();
                        List<Special> relatedSpecials = specialManager.GetSpecialsByReleaseGuid(relatedRelease.ReleaseGuid);
                        foreach (Special relatedSpecial in relatedSpecials)
                        {
                            ReleasingSpecial(relatedSpecial, relatedRelease);
                        }
                        break;
                    case TemplateType.VideoArticleItem: // 发布对应分类下的所有视频文章详细页
                        VideoArticleManager videoArticleManager = new VideoArticleManager();
                        List<VideoArticle> relatedVideoArticles = videoArticleManager.GetVideoArticlesByReleaseGuid(relatedRelease.ReleaseGuid);
                        foreach (VideoArticle relatedVideoArticle in relatedVideoArticles)
                        {
                            ReleasingVideoArticleItem(relatedVideoArticle, relatedRelease);
                        }
                        break;
                    case TemplateType.VideoArticleList: // 发布对应分类下的所有视频列表页
                        List<Category> relatedVideoArticleListCategories = categoryManager.GetCategorysByReleaseGuid(relatedRelease.ReleaseGuid);
                        foreach (Category relatedCategory in relatedVideoArticleListCategories)
                        {
                            ReleasingVideoArticleList(relatedCategory, relatedRelease);
                        }
                        break;
                    case TemplateType.VideoArticleIndex: // 发布对应分类下的所有视频索引页
                        List<Category> relatedVideoArticleIndexCategories = categoryManager.GetCategorysByReleaseGuid(relatedRelease.ReleaseGuid);
                        foreach (Category relatedCategory in relatedVideoArticleIndexCategories)
                        {
                            ReleasingVideoArticleIndex(relatedCategory, relatedRelease);
                        }
                        break;
                }
            }
        }
        #endregion

#warning 发布软件
#warning 发布链接

#warning 发布单页

        /// <summary>
        /// 发布单页
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageRelease"></param>
        public void ReleasingPage(Page page, Release pageRelease)
        {
            // 单页：  "{RootPath}/{PageId}.htm" 这个{PageId}是已知的
        }

        /// <summary>
        /// 发布专题页。
        /// </summary>
        /// <param name="special"></param>
        /// <param name="specialRelease"></param>
        public void ReleasingSpecial(Special special, Release specialRelease)
        {
            // 专题页：{RootPath}/{SpecialId}.htm 这个{SpecialId}是已知的
        }

#warning 发布博客，如何自定义文件名，获取完整拼音？

#warning ReleaseRelation 表反映了前台网站的站点结构，可以根据Release表生成站点地图Google Sitemap和Baidu Sitemap

        /// <summary>
        /// 发布静态页
        /// </summary>
        /// <param name="ripeRelease">预处理过的Release对象</param>
        /// <param name="templateManager">预处理过的模版管理器</param>
        public void ReleasingStaticPage(Release ripeRelease, Wis.Toolkit.Templates.TemplateManager ripeTemplateManager)
        {
            lock (this)
            {
                // 绝对路径
                string releasePath = System.Web.HttpContext.Current.Server.MapPath(ripeRelease.ReleasePath);

                // TODO:失败了如何回滚? 将已经删除的网页恢复
                if (System.IO.File.Exists(releasePath))
                {
                    System.IO.File.Delete(releasePath);
                    System.Threading.Thread.Sleep(100);
                }

                // 如果目录不存在，则创建目录
                System.IO.FileInfo fileInfo = new System.IO.FileInfo(releasePath);
                if (!fileInfo.Directory.Exists) fileInfo.Directory.Create();

                // 生成发布
                using (System.IO.StreamWriter sw = new System.IO.StreamWriter(releasePath, false, Encoding.UTF8))
                {
                    sw.Write(ripeTemplateManager.Process());
                }
            }
        }

        #region 新增/修改/删除
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
        #endregion
    }
}
