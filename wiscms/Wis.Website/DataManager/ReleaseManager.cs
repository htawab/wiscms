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

        public void ReleaseRelation(Article article)
        {
            // 1、读取发布分类关联表 ReleaseCategory，读取本篇新闻对应的发布编号ReleaseGuid；
            // 2、根据发布编号ReleaseGuid读取需要生成静态页的模板，Article实体类作为参数传入；
            // 3、索引页、列表页、详细页、专题页逐个生成；
            // 4、ReleaseRelation 表反映了前台网站的站点结构，可以根据Release表生成站点地图Google Sitemap和Baidu Sitemap
            // 以上设计实现了：内容与模板没有耦合，一篇文章的详细页可以用多套模板生成，同时生成受影响的关联页面。

            // 根据分类编号获取发布编号 
            List<Release> releases = new List<Release>();
            DbCommand command = DbProviderHelper.CreateCommand("SelectReleaseRelations", CommandType.StoredProcedure);
            command.Parameters.Add(DbProviderHelper.CreateParameter("@CategoryGuid", DbType.Guid, article.Category.CategoryGuid));
            DbDataReader dataReader = DbProviderHelper.ExecuteReader(command);
            while (dataReader.Read())
            {
                Release release = new Release();
                release.ReleaseId = Convert.ToInt32(dataReader["ReleaseId"]);
                release.ReleaseGuid = (Guid)dataReader["ReleaseGuid"];
                release.Title = (string)dataReader["Title"];
                release.TemplatePath = (string)dataReader["TemplatePath"];
                release.ReleasePath = (string)dataReader["ReleasePath"];
                release.DateReleased = (DateTime)dataReader["DateReleased"];
                release.ParentGuid = (Guid)dataReader["ParentGuid"];
                releases.Add(release);
            }
            dataReader.Close();

            if (releases.Count == 0) return;

            // 模板路径
            string applicationPath = System.Web.HttpContext.Current.Request.ApplicationPath;
            if (!applicationPath.EndsWith("/")) applicationPath += "/";
            string templateDirectory = System.Configuration.ConfigurationManager.AppSettings["TemplateRootDirectory"];
            if (!templateDirectory.EndsWith("/")) templateDirectory += "/";
            templateDirectory = applicationPath + templateDirectory;
            string releaseDirectory = System.Configuration.ConfigurationManager.AppSettings["ReleaseRootDirectory"];
            if (!releaseDirectory.EndsWith("/")) releaseDirectory += "/";
            releaseDirectory = applicationPath + releaseDirectory;

            foreach (Release release in releases)
            {
                // 装载模版
                release.TemplatePath = release.TemplatePath.Replace("{RootPath}", templateDirectory);
                string templatePath = System.Web.HttpContext.Current.Server.MapPath(release.TemplatePath);
                Wis.Toolkit.Templates.TemplateManager templateManager = Wis.Toolkit.Templates.TemplateManager.LoadFile(templatePath, Encoding.UTF8);
                templateManager.SetVariable("TemplateDirectory", templateDirectory);
                templateManager.SetVariable("ReleaseDirectory", releaseDirectory);
                // 读取内容对应的评论 TODO:签入模板中调用
                //CommentManager commentManager = new CommentManager();
                //List<Comment> comments = commentManager.GetCommentsBySubmissionGuid(article.ArticleGuid);
                //templateManager.SetVariable("Comments", comments);

                // 生成静态页，支持索引页比如首页、列表页、详细页、专题页
                // 首页：  "{RootPath}/default.htm"
                // 单页：  "{RootPath}/{PageId}.htm" 这个{PageId}是已知的
                // 列表页："{RootPath}/{CategoryId}/{PageIndex}.htm"
                // 详细页："{RootPath}/{CategoryId}/{Month}-{Day}/{ArticleId}.htm"
                // 专题页："{RootPath}/{SpecialId}.htm" 这个{SpecialId}是已知的
                release.ReleasePath = release.ReleasePath.Replace("{RootPath}", releaseDirectory);
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

                    // 求页总数 PageCount
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
                        string releasePath = Regex.Replace(release.ReleasePath, pattern, pageIndex.ToString(), RegexOptions.IgnoreCase);
                        releasePath = System.Web.HttpContext.Current.Server.MapPath(releasePath);
                        lock (this)
                        {
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
                                sw.Write(templateManager.Process()); // 模板解析由模板的标签所决定
                            }
                        }
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
                    release.ReleasePath = System.Web.HttpContext.Current.Server.MapPath(release.ReleasePath);

                    // TODO:按照 ContentHtml 长度，生成多页
                    //if (release.ReleasePath.IndexOf("{ArticleId}") > -1) // 详细页
                    //{
                    //}

                    lock (this)
                    {
                        // TODO:失败了如何回滚? 将已经删除的网页恢复
                        if (System.IO.File.Exists(article.ReleasePath))
                        {
                            System.IO.File.Delete(article.ReleasePath);
                            System.Threading.Thread.Sleep(100);
                        }

                        // 如果目录不存在，则创建目录
                        System.IO.FileInfo fileInfo = new System.IO.FileInfo(release.ReleasePath);
                        if (!fileInfo.Directory.Exists) fileInfo.Directory.Create();

                        // 生成发布
                        using (System.IO.StreamWriter sw = new System.IO.StreamWriter(release.ReleasePath, false, Encoding.UTF8))
                        {
                            sw.Write(templateManager.Process()); // 模板解析由模板的标签所决定
                        }
                    }
                }
            }
        }

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
                    release.TemplatePath = (string)dataReader["TemplatePath"];
                    release.ReleasePath = (string)dataReader["ReleasePath"];
                    release.DateReleased = (DateTime)dataReader["DateReleased"];
                    release.ParentGuid = (Guid)dataReader["ParentGuid"];
                    releases.Add(release);
                }
            }
            dataReader.Close();

            if (releases.Count == 0) return;

            // 模板路径
            string applicationPath = System.Web.HttpContext.Current.Request.ApplicationPath;
            if (!applicationPath.EndsWith("/")) applicationPath += "/";
            string templateDirectory = System.Configuration.ConfigurationManager.AppSettings["TemplateRootDirectory"];
            if (!templateDirectory.EndsWith("/")) templateDirectory += "/";
            templateDirectory = applicationPath + templateDirectory;
            string releaseDirectory = System.Configuration.ConfigurationManager.AppSettings["ReleaseRootDirectory"];
            if (!releaseDirectory.EndsWith("/")) releaseDirectory += "/";
            releaseDirectory = applicationPath + releaseDirectory;

            foreach (Release release in releases)
            {
                // 装载模版
                release.TemplatePath = release.TemplatePath.Replace("{RootPath}", templateDirectory);
                string templatePath = System.Web.HttpContext.Current.Server.MapPath(release.TemplatePath);
                Wis.Toolkit.Templates.TemplateManager templateManager = Wis.Toolkit.Templates.TemplateManager.LoadFile(templatePath, Encoding.UTF8);
                templateManager.SetVariable("TemplateDirectory", templateDirectory);
                templateManager.SetVariable("ReleaseDirectory", releaseDirectory);

                // 生成详细页的静态页
                // "{RootPath}/{CategoryId}/{Month}-{Day}/{ArticleId}.htm"
                release.ReleasePath = release.ReleasePath.Replace("{RootPath}", releaseDirectory);
                release.ReleasePath = release.ReleasePath.Replace("{CategoryId}", article.Category.CategoryId.ToString());

                 // 详细页
                release.ReleasePath = release.ReleasePath.Replace("{Year}", article.DateCreated.Year.ToString());
                release.ReleasePath = release.ReleasePath.Replace("{Month}", article.DateCreated.Month.ToString());
                release.ReleasePath = release.ReleasePath.Replace("{Day}", article.DateCreated.Day.ToString());
                release.ReleasePath = release.ReleasePath.Replace("{ArticleId}", article.ArticleId.ToString());
                templateManager.SetVariable("Article", article);

                release.ReleasePath = System.Web.HttpContext.Current.Server.MapPath(release.ReleasePath);

                // TODO:按照 ContentHtml 长度，生成多页
                //if (release.ReleasePath.IndexOf("{ArticleId}") > -1) // 详细页
                //{
                //}

                lock (this)
                {
                    // TODO:失败了如何回滚? 将已经删除的网页恢复
                    if (System.IO.File.Exists(article.ReleasePath))
                    {
                        System.IO.File.Delete(article.ReleasePath);
                        System.Threading.Thread.Sleep(100);
                    }

                    // 如果目录不存在，则创建目录
                    System.IO.FileInfo fileInfo = new System.IO.FileInfo(release.ReleasePath);
                    if (!fileInfo.Directory.Exists) fileInfo.Directory.Create();

                    // 生成发布
                    using (System.IO.StreamWriter sw = new System.IO.StreamWriter(release.ReleasePath, false, Encoding.UTF8))
                    {
                        sw.Write(templateManager.Process());
                    }
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
                release.TemplatePath = (string)dataReader["TemplatePath"];
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
                release.TemplatePath = (string)dataReader["TemplatePath"];
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
        /// <param name="article"></param>
        /// <returns></returns>
        public int AddNew(Article article)
        {
            Guid releaseGuid = Guid.NewGuid();
            DbCommand command = DbProviderHelper.CreateCommand("INSERTRelease", CommandType.StoredProcedure);
            command.Parameters.Add(DbProviderHelper.CreateParameter("@ReleaseGuid", DbType.Guid, releaseGuid));
            command.Parameters.Add(DbProviderHelper.CreateParameter("@CategoryGuid", DbType.Guid, article.Category.CategoryGuid));
            command.Parameters.Add(DbProviderHelper.CreateParameter("@TemplatePath", DbType.String, article.TemplatePath));
            command.Parameters.Add(DbProviderHelper.CreateParameter("@ReleasePath", DbType.String, article.ReleasePath));
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
