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
        public void ReleasingArticleItem(Article article, Release articleRelease)
        {

        }

        public void ReleasingArticleList(Category articleCategory, Release articleCategoryRelease)
        {

        }

        public void ReleasingArticleIndex(Category articleCategory, Release articleCategoryRelease)
        {

        }
        #endregion

#warning 发布图片文章，完善功能
        #region 发布图片文章

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

        public void ReleasingPhotoArticleItem(Article photoArticle, Release photoArticleRelease)
        {

        }

        public void ReleasingPhotoArticleList(Category photoArticleCategory, Release photoArticleCategoryRelease)
        {

        }

        public void ReleasingPhotoArticleIndex(Category photoArticleCategory, Release photoArticleCategoryRelease)
        {

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

            templateManager.SetVariable("VideoArticleCategory", videoArticle.Article.Category); // 最近更新的分类

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
                    videoArticleRelease.ReleasePath = Regex.Replace(videoArticleRelease.ReleasePath, @"\{ArticleId\}", replacedArticleId, RegexOptions.IgnoreCase);

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
            if (release.PageSize.HasValue == false)
                throw new System.ArgumentNullException(string.Format("发布编号为 {0} 的 PageSize 参数未配置", videoArticleCategoryRelease.ReleaseId));

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

                    videoArticleCategoryRelease.ReleasePath = Regex.Replace(videoArticleCategoryRelease.ReleasePath, @"\{PageIndex\}", pageIndex.ToString(), RegexOptions.IgnoreCase);
                    ReleasingStaticPage(videoArticleCategoryRelease, templateManager);
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


        /// <summary>
        /// 发布图片新闻的关联页面。
        /// </summary>
        /// <param name="articlePhoto"></param>
        public void ReleasingRelatedReleasesByArticlePhoto(ArticlePhoto articlePhoto)
        {
            List<Release> releases = GetRelatedReleases(articlePhoto.Category.CategoryGuid);
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

                        templateManager.SetVariable("Pager", ReleaseTinyPager(pageIndex, pageCount));
                        release.ReleasePath = Regex.Replace(release.ReleasePath, pattern, pageIndex.ToString(), RegexOptions.IgnoreCase);

                        ReleasingStaticPage(release, templateManager);
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

                    ReleasingStaticPage(release, templateManager);
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
        public void ReleasingRelatedReleasesByArticle(Article article)
        {
            // 根据分类编号获取发布编号 
            List<Release> releases = GetRelatedReleases(article.Category.CategoryGuid);
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

                        ReleasingStaticPage(release, templateManager);
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

                    ReleasingStaticPage(release, templateManager);
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
            // 1、读取发布分类关联表 RelatedRelease，读取本篇新闻对应的发布编号ReleaseGuid；
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
                ReleasingStaticPage(release, templateManager);
            }
        }

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
