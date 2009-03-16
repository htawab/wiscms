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
        /// ��ȡ�������� ASP.NET Ӧ�ó��������Ӧ�ó����·����ǰ���� /��
        /// </summary>
        public string ApplicationPath
        {
            get
            {
                if (string.IsNullOrEmpty(_ApplicationPath))
                {
                    _ApplicationPath = System.Web.HttpContext.Current.Request.ApplicationPath;
                    if (!_ApplicationPath.EndsWith("/")) _ApplicationPath += "/"; // ǰ�󵼶��� /
                }
                return _ApplicationPath;
            }
        }

        private string _TemplateDirectory;
        /// <summary>
        /// ���ģ���Ŀ¼��û�к� /
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
        /// �洢��̬ҳ��·����û�к� /
        /// </summary>
        public string ReleaseDirectory
        {
            get
            {
                if (string.IsNullOrEmpty(_ReleaseDirectory))
                {
                    _ReleaseDirectory = System.Configuration.ConfigurationManager.AppSettings["ReleaseRootDirectory"].Trim('/'); // ��Ҫ��ǰ�󵼵ļ������
                    _ReleaseDirectory = string.Format("{0}{1}", this.ApplicationPath, _ReleaseDirectory);
                }
                return _ReleaseDirectory;
            }
        }

        public string ReleasePager(int pageIndex, int recordCount, int pageCount)
        {
            // �б�ҳ��"/{RootPath}/{CategoryId}/{PageIndex}.htm"
            // ��2ҳ / ��2ҳ ��ҳ ��һҳ [1] [2] ��һҳ βҳ
            if (pageIndex < 1) return string.Empty; // ��ǰҳ������>0
            if (pageCount <= 1) return string.Empty;
            int prevPage = pageIndex - 1;
            int nextPage = pageIndex + 1;
            StringBuilder sb = new StringBuilder();
            sb.Append("<div id='Pager'>");
            sb.Append(string.Format("<span>��{0}����¼&nbsp;��{1}ҳ/��{2}ҳ</span>", recordCount, pageIndex, pageCount));

            if (prevPage < 1)
            {
                sb.Append("<span class='noLink'>��ҳ</span>");
                sb.Append("<span class='noLink'>��һҳ</span>");
            }
            else
            {
                sb.Append("<a href='1.htm'>��ҳ</a>");
                sb.Append(string.Format("<a href='{0}.htm'>��һҳ</a>", prevPage));
            }

            int startPage;
            if (pageIndex % 10 == 0)
                startPage = pageIndex - 9;
            else
                startPage = pageIndex - pageIndex % 10 + 1;

            if (startPage > 10)
                sb.Append(string.Format("<a href='{0}.htm' title='ǰ10ҳ'>...</a>", (startPage - 1)));

            for (int index = startPage; index < startPage + 10; index++)
            {
                if (index > pageCount) break;
                if (index == pageIndex)
                    sb.Append(string.Format("<span title='��{0}ҳ' class='currentPager'>{0}</span>", index));
                else
                    sb.Append(string.Format("<a href='{0}.htm' title='��{0}ҳ'>{0}</a>", index));
            }
            if (pageCount >= startPage + 10)
                sb.Append(string.Format("<a href='{0}.htm' title='��{0}ҳ'>...</a>", (startPage + 10)));
            if (nextPage > pageCount)
            {
                sb.Append("<span class='noLink'>��һҳ</span>");
                sb.Append("<span class='noLink'>βҳ</span>");
            }
            else
            {
                sb.Append(string.Format("<a href='{0}.htm'>��һҳ</a>", nextPage));
                sb.Append(string.Format("<a href='{0}.htm'>βҳ</a>", pageCount));
            }

            sb.Append("</div>");
            return sb.ToString();
        }


        public string ReleaseTinyPager(int pageIndex, int pageCount)
        {
            // �б�ҳ��"/{RootPath}/{CategoryId}/{PageIndex}.htm"
            // ��ҳ ��һҳ ��һҳ βҳ
            if (pageIndex < 1) return string.Empty; // ��ǰҳ������>0
            if (pageCount <= 1) return string.Empty;
            int prevPage = pageIndex - 1;
            int nextPage = pageIndex + 1;
            StringBuilder sb = new StringBuilder();
            sb.Append("<div id='Pager'>");

            if (prevPage < 1)
            {
                sb.Append("<span class='noLink'>��ҳ</span>");
                sb.Append("<span class='noLink'>��һҳ</span>");
            }
            else
            {
                sb.Append("<a href='1.htm'>��ҳ</a>");
                sb.Append(string.Format("<a href='{0}.htm'>��һҳ</a>", prevPage));
            }

            if (nextPage > pageCount)
            {
                sb.Append("<span class='noLink'>��һҳ</span>");
                sb.Append("<span class='noLink'>βҳ</span>");
            }
            else
            {
                sb.Append(string.Format("<a href='{0}.htm'>��һҳ</a>", nextPage));
                sb.Append(string.Format("<a href='{0}.htm'>βҳ</a>", pageCount));
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

        // List ��Ҫѭ�������

        // Index

        // Special

        // Page

        #region ��ȡ��������
        /// <summary>
        /// ���ݹ�����Ż�ȡ�����ķ������󣬹�����ſ����Ƿ����š���ҳ��š�ר����
        /// </summary>
        /// <param name="relationGuid">�������</param>
        /// <returns>���ع����ķ�����</returns>
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
        /// ��ȡ�������󼯺ϡ�
        /// </summary>
        /// <param name="dataReader">������Դ��ȡ�е�һ��ֻ������</param>
        /// <returns>���ط������󼯺�</returns>
        public List<Release> GetReleases(DbDataReader dataReader)
        {
            List<Release> releases = new List<Release>();
            while (dataReader.Read())
            {
                Release release = new Release();

                // ������Ϣ
                release.ReleaseId = Convert.ToInt32(dataReader[ViewReleaseTemplateField.ReleaseId]);
                release.ReleaseGuid = (Guid)dataReader[ViewReleaseTemplateField.ReleaseGuid];
                release.Title = (string)dataReader[ViewReleaseTemplateField.ReleaseTitle];
                //release.TemplatePath = (string)dataReader[ViewReleaseTemplateField.TemplatePath];
                release.ReleasePath = (string)dataReader[ViewReleaseTemplateField.ReleasePath];
                release.DateReleased = (DateTime)dataReader[ViewReleaseTemplateField.DateReleased];
                release.ParentGuid = (Guid)dataReader[ViewReleaseTemplateField.ParentGuid];

                // ģ����Ϣ
                release.Template.TemplateId = Convert.ToInt32(dataReader[ViewReleaseTemplateField.TemplateId]);
                release.Template.TemplateGuid = (Guid)dataReader[ViewReleaseTemplateField.TemplateGuid];
                release.Template.Title = Convert.ToString(dataReader[ViewReleaseTemplateField.TemplateTitle]);
                release.Template.TemplatePath = Convert.ToString(dataReader[ViewReleaseTemplateField.TemplatePath]);
                // ö�� TemplateType
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

        #region ��������ҳ
        /// <summary>
        /// ��������ҳ
        /// </summary>
        /// <param name="release">��������</param>
        public void ReleasingIndex(Release release)
        {
            // ��ҳ��{TemplateDirectory}/default.htm
            if (release.Template.TemplateType != TemplateType.Index) return;

            // װ��ģ��
            release.Template.TemplatePath = Regex.Replace(release.Template.TemplatePath, @"\{TemplateDirectory\}", this.TemplateDirectory, RegexOptions.IgnoreCase);
            string templatePath = System.Web.HttpContext.Current.Server.MapPath(release.Template.TemplatePath);
            Wis.Toolkit.Templates.TemplateManager templateManager = Wis.Toolkit.Templates.TemplateManager.LoadFile(templatePath, Encoding.UTF8);
            templateManager.SetVariable("ApplicationPath", this.ApplicationPath);
            templateManager.SetVariable("TemplateDirectory", this.TemplateDirectory);
            templateManager.SetVariable("ReleaseDirectory", this.ReleaseDirectory);

            // ���� Release
            release.ReleasePath = Regex.Replace(release.ReleasePath, @"\{ReleaseDirectory\}", this.ReleaseDirectory, RegexOptions.IgnoreCase);

            // ������̬ҳ
            ReleasingStaticPage(release, templateManager);
        }
        #endregion

#warning �������£����ƹ���
        #region ������ͨ����
        public void ReleasingArticle(Article article)
        {
            // 1 ���� Article ҳ�棬���ݷ������� Item ��ģ��
            // 2 ���� Category ҳ�棬���ݷ�������List��Index��ģ��
            List<Release> releases = GetReleasesByCategoryGuid(article.Category.CategoryGuid);
            foreach (Release release in releases)
            {
                switch (release.Template.TemplateType)
                {
                    case TemplateType.ArticleIndex: // ��Ƶ��������ҳ
                        ReleasingArticleIndex(article.Category, release);
                        break;
                    case TemplateType.ArticleList: // ��Ƶ�����б�ҳ������ҳ
                        ReleasingArticleList(article.Category, release);
                        break;
                    case TemplateType.ArticleItem:// ��Ƶ������ϸҳ
                        ReleasingArticleItem(article, release);
                        break;
                    default:
                        throw new System.ArgumentException("TemplateType");
                }
            }

            // 3 ���������õ�ҳ��
            ReleasingRelatedReleases(article.Category.CategoryGuid);
        }

        public void ReleasingRemovedArticle(Article article)
        {
            // 1 ���� Article ҳ�棬���ݷ������� Item ��ģ��
            // 2 ���� Category ҳ�棬���ݷ�������List��Index��ģ��
            List<Release> releases = GetReleasesByCategoryGuid(article.Category.CategoryGuid);
            foreach (Release release in releases)
            {
                switch (release.Template.TemplateType)
                {
                    case TemplateType.ArticleIndex: // ��Ƶ��������ҳ
                        ReleasingArticleIndex(article.Category, release);
                        break;
                    case TemplateType.ArticleList: // ��Ƶ�����б�ҳ������ҳ
                        ReleasingArticleList(article.Category, release);
                        break;
                    case TemplateType.ArticleItem:// �Ƴ�������ϸҳ
                        // ���� Release
                        release.ReleasePath = Regex.Replace(release.ReleasePath, @"\{ReleaseDirectory\}", this.ReleaseDirectory, RegexOptions.IgnoreCase);
                        release.ReleasePath = Regex.Replace(release.ReleasePath, @"\{CategoryId\}", article.Category.CategoryId.ToString(), RegexOptions.IgnoreCase);
                        release.ReleasePath = Regex.Replace(release.ReleasePath, @"\{Year\}", article.DateCreated.Year.ToString(), RegexOptions.IgnoreCase);
                        release.ReleasePath = Regex.Replace(release.ReleasePath, @"\{Month\}", article.DateCreated.Month.ToString(), RegexOptions.IgnoreCase);
                        release.ReleasePath = Regex.Replace(release.ReleasePath, @"\{Day\}", article.DateCreated.Day.ToString(), RegexOptions.IgnoreCase);

                        // ���ɾ�̬ҳ
#warning ���� ContentHtml ���ȣ�ɾ����ҳ����Ҫ����
                        string pagerSeparator = "<!--Article.ContentHtml.Pager-->";
                        if (article.ContentHtml.IndexOf(pagerSeparator) > -1) // ���з�ҳ
                        {
                            string[] contentHtmls = article.ContentHtml.Split(pagerSeparator.ToCharArray());
                            for (int index = 0; index < contentHtmls.Length; index++)
                            {
                                string replacedArticleId = string.Format("{0}/{1}", article.ArticleId, (index + 1));
                                release.ReleasePath = Regex.Replace(release.ReleasePath, @"\{ArticleId\}", replacedArticleId, RegexOptions.IgnoreCase);

                                // ɾ�����¾�̬ҳ
                                release.ReleasePath = System.Web.HttpContext.Current.Server.MapPath(release.ReleasePath);
                                if (System.IO.File.Exists(release.ReleasePath))
                                    System.IO.File.Delete(release.ReleasePath);
                            }
                        }
                        else
                        {
                            release.ReleasePath = Regex.Replace(release.ReleasePath, @"\{ArticleId\}", article.ArticleId.ToString(), RegexOptions.IgnoreCase);

                            // ɾ�����¾�̬ҳ
                            release.ReleasePath = System.Web.HttpContext.Current.Server.MapPath(release.ReleasePath);
                            if (System.IO.File.Exists(release.ReleasePath))
                                System.IO.File.Delete(release.ReleasePath);
                        }
                        
                        break;
                    default:
                        throw new System.ArgumentException("TemplateType");
                }
            }

            // 3 ���������õ�ҳ��
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
            // ��ϸҳ��{ReleaseDirectory}/{CategoryId}/{Year}-{Month}-{Day}/{ArticleId}.htm
            if (articleRelease.Template.TemplateType != TemplateType.ArticleItem) return;

            // װ��ģ��
            articleRelease.Template.TemplatePath = Regex.Replace(articleRelease.Template.TemplatePath, @"\{TemplateDirectory\}", this.TemplateDirectory, RegexOptions.IgnoreCase);
            string templatePath = System.Web.HttpContext.Current.Server.MapPath(articleRelease.Template.TemplatePath);
            Wis.Toolkit.Templates.TemplateManager templateManager = Wis.Toolkit.Templates.TemplateManager.LoadFile(templatePath, Encoding.UTF8);

            templateManager.SetVariable("ApplicationPath", this.ApplicationPath);
            templateManager.SetVariable("TemplateDirectory", this.TemplateDirectory);
            templateManager.SetVariable("ReleaseDirectory", this.ReleaseDirectory);

            // ���� Release
            articleRelease.ReleasePath = Regex.Replace(articleRelease.ReleasePath, @"\{ReleaseDirectory\}", this.ReleaseDirectory, RegexOptions.IgnoreCase);
            articleRelease.ReleasePath = Regex.Replace(articleRelease.ReleasePath, @"\{CategoryId\}", article.Category.CategoryId.ToString(), RegexOptions.IgnoreCase);
            articleRelease.ReleasePath = Regex.Replace(articleRelease.ReleasePath, @"\{Year\}", article.DateCreated.Year.ToString(), RegexOptions.IgnoreCase);
            articleRelease.ReleasePath = Regex.Replace(articleRelease.ReleasePath, @"\{Month\}", article.DateCreated.Month.ToString(), RegexOptions.IgnoreCase);
            articleRelease.ReleasePath = Regex.Replace(articleRelease.ReleasePath, @"\{Day\}", article.DateCreated.Day.ToString(), RegexOptions.IgnoreCase);

            // ���ɾ�̬ҳ
#warning ���� ContentHtml ���ȣ����ɶ�ҳ����Ҫ����
            string pagerSeparator = "<!--Article.ContentHtml.Pager-->";
            if (article.ContentHtml.IndexOf(pagerSeparator) > -1) // ���з�ҳ
            {
                string[] contentHtmls = article.ContentHtml.Split(pagerSeparator.ToCharArray());
                for (int index = 0; index < contentHtmls.Length; index++)
                {
                    article.ContentHtml = contentHtmls[index] + ReleaseTinyPager((index + 1), contentHtmls.Length);
                    templateManager.SetVariable("Article", article); // ������µ�����
                    string replacedArticleId = string.Format("{0}/{1}", article.ArticleId, (index + 1));

                    string rawReleasePath = articleRelease.ReleasePath; // ��¼{ArticleId}
                    articleRelease.ReleasePath = Regex.Replace(rawReleasePath, @"\{ArticleId\}", replacedArticleId, RegexOptions.IgnoreCase);
                    ReleasingStaticPage(articleRelease, templateManager);
                    articleRelease.ReleasePath = rawReleasePath;

                    ReleasingStaticPage(articleRelease, templateManager);
                }
            }
            else
            {
                templateManager.SetVariable("Article", article); // ������µ�����
                articleRelease.ReleasePath = Regex.Replace(articleRelease.ReleasePath, @"\{ArticleId\}", article.ArticleId.ToString(), RegexOptions.IgnoreCase);

                ReleasingStaticPage(articleRelease, templateManager);
            }
        }

        public void ReleasingArticleList(Category articleCategory, Release articleCategoryRelease)
        {
            // �б�ҳ��{ReleaseDirectory}/{CategoryId}/{PageIndex}.htm
            if (articleCategoryRelease.Template.TemplateType != TemplateType.ArticleList) return;

            // װ��ģ��
            articleCategoryRelease.Template.TemplatePath = Regex.Replace(articleCategoryRelease.Template.TemplatePath, @"\{TemplateDirectory\}", this.TemplateDirectory, RegexOptions.IgnoreCase);
            string templatePath = System.Web.HttpContext.Current.Server.MapPath(articleCategoryRelease.Template.TemplatePath);
            Wis.Toolkit.Templates.TemplateManager templateManager = Wis.Toolkit.Templates.TemplateManager.LoadFile(templatePath, Encoding.UTF8);
            templateManager.SetVariable("ArticleCategory", articleCategory);
            templateManager.SetVariable("ApplicationPath", this.ApplicationPath);
            templateManager.SetVariable("TemplateDirectory", this.TemplateDirectory);
            templateManager.SetVariable("ReleaseDirectory", this.ReleaseDirectory);

            // ���� Release
            articleCategoryRelease.ReleasePath = Regex.Replace(articleCategoryRelease.ReleasePath, @"\{ReleaseDirectory\}", this.ReleaseDirectory, RegexOptions.IgnoreCase);
            articleCategoryRelease.ReleasePath = Regex.Replace(articleCategoryRelease.ReleasePath, @"\{CategoryId\}", articleCategory.CategoryId.ToString(), RegexOptions.IgnoreCase);

            if (articleCategoryRelease.PageSize.HasValue == false)
                throw new System.ArgumentNullException(string.Format("�������Ϊ {0} �� PageSize ����δ����", articleCategoryRelease.ReleaseId));

            // ����һ���ж���ҳ��Ȼ����ҳ����
            int pageSize = articleCategoryRelease.PageSize.Value;
            templateManager.SetVariable("PageSize", pageSize);
            templateManager.SetVariable("RecordCount", articleCategory.RecordCount);

            // ��ҳ���� pageCount
            int pageCount;
            pageCount = articleCategory.RecordCount / pageSize;
            if (articleCategory.RecordCount % pageSize != 0)
                pageCount += 1;
            templateManager.SetVariable("PageCount", pageCount);

            // ��ҳ����
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

                    string rawReleasePath = articleCategoryRelease.ReleasePath; // ��¼{PageIndex}
                    articleCategoryRelease.ReleasePath = Regex.Replace(rawReleasePath, @"\{PageIndex\}", pageIndex.ToString(), RegexOptions.IgnoreCase);
                    ReleasingStaticPage(articleCategoryRelease, templateManager);
                    articleCategoryRelease.ReleasePath = rawReleasePath;
                }
            }
        }

        public void ReleasingArticleIndex(Category articleCategory, Release articleCategoryRelease)
        {
            // ��ͨ��������ҳ��{ReleaseDirectory}/{CategoryId}/1.htm
            if (articleCategoryRelease.Template.TemplateType != TemplateType.ArticleIndex) return;

            // װ��ģ��
            articleCategoryRelease.Template.TemplatePath = Regex.Replace(articleCategoryRelease.Template.TemplatePath, @"\{TemplateDirectory\}", this.TemplateDirectory, RegexOptions.IgnoreCase);
            string templatePath = System.Web.HttpContext.Current.Server.MapPath(articleCategoryRelease.Template.TemplatePath);
            Wis.Toolkit.Templates.TemplateManager templateManager = Wis.Toolkit.Templates.TemplateManager.LoadFile(templatePath, Encoding.UTF8);
            templateManager.SetVariable("ArticleCategory", articleCategory);
            templateManager.SetVariable("ApplicationPath", this.ApplicationPath);
            templateManager.SetVariable("TemplateDirectory", this.TemplateDirectory);
            templateManager.SetVariable("ReleaseDirectory", this.ReleaseDirectory);

            // ���� Release
            articleCategoryRelease.ReleasePath = Regex.Replace(articleCategoryRelease.ReleasePath, @"\{ReleaseDirectory\}", this.ReleaseDirectory, RegexOptions.IgnoreCase);
            articleCategoryRelease.ReleasePath = Regex.Replace(articleCategoryRelease.ReleasePath, @"\{CategoryId\}", articleCategory.CategoryId.ToString(), RegexOptions.IgnoreCase);

            ReleasingStaticPage(articleCategoryRelease, templateManager);
        }
        #endregion

#warning ����ͼƬ���£����ƹ���
        #region ����ͼƬ����

        /// <summary>
        /// 
        /// </summary>
        /// <param name="photoArticle"></param>
        public void ReleasingPhotoArticle(ArticlePhoto photoArticle)
        {
            // 1 ���� VideoArticle ҳ�棬���ݷ�������Item��ģ��
            // 2 ���� Category ҳ�棬���ݷ�������List��Index��ģ��
            List<Release> releases = GetReleasesByCategoryGuid(photoArticle.Category.CategoryGuid);
            foreach (Release release in releases)
            {
                switch (release.Template.TemplateType)
                {
                    case TemplateType.PhotoArticleIndex: // ��Ƶ��������ҳ
                        ReleasingPhotoArticleIndex(photoArticle.Category, release);
                        break;
                    case TemplateType.PhotoArticleList: // ��Ƶ�����б�ҳ������ҳ
                        ReleasingPhotoArticleList(photoArticle.Category, release);
                        break;
                    case TemplateType.PhotoArticleItem:// ��Ƶ������ϸҳ
                        ReleasingPhotoArticleItem(photoArticle, release);
                        break;
                    default:
                        throw new System.ArgumentException("TemplateType");
                }
            }

            // 3 ���������õ�ҳ��
            ReleasingRelatedReleases(photoArticle.Category.CategoryGuid);
        }

        public void ReleasingRemovedPhotoArticle(ArticlePhoto photoArticle)
        {
            // 1 ���� PhotoArticle ҳ�棬���ݷ������� Item ��ģ��
            // 2 ���� Category ҳ�棬���ݷ�������List��Index��ģ��
            List<Release> releases = GetReleasesByCategoryGuid(photoArticle.Category.CategoryGuid);
            foreach (Release release in releases)
            {
                switch (release.Template.TemplateType)
                {
                    case TemplateType.PhotoArticleIndex: // ��Ƶ��������ҳ
                        ReleasingPhotoArticleIndex(photoArticle.Category, release);
                        break;
                    case TemplateType.PhotoArticleList: // ��Ƶ�����б�ҳ������ҳ
                        ReleasingPhotoArticleList(photoArticle.Category, release);
                        break;
                    case TemplateType.PhotoArticleItem:// �Ƴ�������ϸҳ
                        // ���� Release
                        release.ReleasePath = Regex.Replace(release.ReleasePath, @"\{ReleaseDirectory\}", this.ReleaseDirectory, RegexOptions.IgnoreCase);
                        release.ReleasePath = Regex.Replace(release.ReleasePath, @"\{CategoryId\}", photoArticle.Category.CategoryId.ToString(), RegexOptions.IgnoreCase);
                        release.ReleasePath = Regex.Replace(release.ReleasePath, @"\{Year\}", photoArticle.DateCreated.Year.ToString(), RegexOptions.IgnoreCase);
                        release.ReleasePath = Regex.Replace(release.ReleasePath, @"\{Month\}", photoArticle.DateCreated.Month.ToString(), RegexOptions.IgnoreCase);
                        release.ReleasePath = Regex.Replace(release.ReleasePath, @"\{Day\}", photoArticle.DateCreated.Day.ToString(), RegexOptions.IgnoreCase);

                        // ���ɾ�̬ҳ
#warning ���� ContentHtml ���ȣ�ɾ����ҳ����Ҫ����
                        string pagerSeparator = "<!--Article.ContentHtml.Pager-->";
                        if (photoArticle.ContentHtml.IndexOf(pagerSeparator) > -1) // ���з�ҳ
                        {
                            string[] contentHtmls = photoArticle.ContentHtml.Split(pagerSeparator.ToCharArray());
                            for (int index = 0; index < contentHtmls.Length; index++)
                            {
                                string replacedArticleId = string.Format("{0}/{1}", photoArticle.ArticleId, (index + 1));
                                release.ReleasePath = Regex.Replace(release.ReleasePath, @"\{ArticleId\}", replacedArticleId, RegexOptions.IgnoreCase);

                                // ɾ�����¾�̬ҳ
                                release.ReleasePath = System.Web.HttpContext.Current.Server.MapPath(release.ReleasePath);
                                if (System.IO.File.Exists(release.ReleasePath))
                                    System.IO.File.Delete(release.ReleasePath);
                            }
                        }
                        else
                        {
                            release.ReleasePath = Regex.Replace(release.ReleasePath, @"\{ArticleId\}", photoArticle.ArticleId.ToString(), RegexOptions.IgnoreCase);

                            // ɾ�����¾�̬ҳ
                            release.ReleasePath = System.Web.HttpContext.Current.Server.MapPath(release.ReleasePath);
                            if (System.IO.File.Exists(release.ReleasePath))
                                System.IO.File.Delete(release.ReleasePath);
                        }

                        break;
                    default:
                        throw new System.ArgumentException("TemplateType");
                }
            }

            // 3 ���������õ�ҳ��
            ReleasingRelatedReleases(photoArticle.Category.CategoryGuid);
        }

        /// <summary>
        /// ����ͼƬ��������ҳ��
        /// </summary>
        /// <param name="photoArticle">ͼƬ����</param>
        public void ReleasingPhotoArticleItem(ArticlePhoto photoArticle)
        {
            List<Release> releases = GetReleasesByCategoryGuid(photoArticle.Category.CategoryGuid, TemplateType.PhotoArticleItem);
            foreach (Release release in releases)
            {
                ReleasingPhotoArticleItem(photoArticle, release);
            }
        }

        /// <summary>
        /// ����ͼƬ��������ҳ��
        /// </summary>
        /// <param name="photoArticle">ͼƬ����</param>
        /// <param name="photoArticleRelease">ͼƬ���·�����</param>
        public void ReleasingPhotoArticleItem(ArticlePhoto photoArticle, Release photoArticleRelease)
        {
            // ��ϸҳ��{ReleaseDirectory}/{CategoryId}/{Year}-{Month}-{Day}/{ArticleId}.htm
            if (photoArticleRelease.Template.TemplateType != TemplateType.PhotoArticleItem) return;

            // װ��ģ��
            photoArticleRelease.Template.TemplatePath = Regex.Replace(photoArticleRelease.Template.TemplatePath, @"\{TemplateDirectory\}", this.TemplateDirectory, RegexOptions.IgnoreCase);
            string templatePath = System.Web.HttpContext.Current.Server.MapPath(photoArticleRelease.Template.TemplatePath);
            Wis.Toolkit.Templates.TemplateManager templateManager = Wis.Toolkit.Templates.TemplateManager.LoadFile(templatePath, Encoding.UTF8);

            templateManager.SetVariable("ApplicationPath", this.ApplicationPath);
            templateManager.SetVariable("TemplateDirectory", this.TemplateDirectory);
            templateManager.SetVariable("ReleaseDirectory", this.ReleaseDirectory);

            // ���� Release
            photoArticleRelease.ReleasePath = Regex.Replace(photoArticleRelease.ReleasePath, @"\{ReleaseDirectory\}", this.ReleaseDirectory, RegexOptions.IgnoreCase);
            photoArticleRelease.ReleasePath = Regex.Replace(photoArticleRelease.ReleasePath, @"\{CategoryId\}", photoArticle.Category.CategoryId.ToString(), RegexOptions.IgnoreCase);
            photoArticleRelease.ReleasePath = Regex.Replace(photoArticleRelease.ReleasePath, @"\{Year\}", photoArticle.DateCreated.Year.ToString(), RegexOptions.IgnoreCase);
            photoArticleRelease.ReleasePath = Regex.Replace(photoArticleRelease.ReleasePath, @"\{Month\}", photoArticle.DateCreated.Month.ToString(), RegexOptions.IgnoreCase);
            photoArticleRelease.ReleasePath = Regex.Replace(photoArticleRelease.ReleasePath, @"\{Day\}", photoArticle.DateCreated.Day.ToString(), RegexOptions.IgnoreCase);

            // ���ɾ�̬ҳ
#warning ���� ContentHtml ���ȣ����ɶ�ҳ����Ҫ����
            string pagerSeparator = "<!--Article.ContentHtml.Pager-->";
            if (photoArticle.ContentHtml.IndexOf(pagerSeparator) > -1) // ���з�ҳ
            {
                string[] contentHtmls = photoArticle.ContentHtml.Split(pagerSeparator.ToCharArray());
                for (int index = 0; index < contentHtmls.Length; index++)
                {
                    photoArticle.ContentHtml = contentHtmls[index] + ReleaseTinyPager((index + 1), contentHtmls.Length);
                    templateManager.SetVariable("PhotoArticle", photoArticle); // ������µ�����
                    string replacedArticleId = string.Format("{0}/{1}", photoArticle.ArticleId, (index + 1));
                    //photoArticleRelease.ReleasePath = Regex.Replace(photoArticleRelease.ReleasePath, @"\{ArticleId\}", replacedArticleId, RegexOptions.IgnoreCase);
                    string rawReleasePath = photoArticleRelease.ReleasePath; // ��¼{ArticleId}
                    photoArticleRelease.ReleasePath = Regex.Replace(rawReleasePath, @"\{ArticleId\}", replacedArticleId, RegexOptions.IgnoreCase);
                    ReleasingStaticPage(photoArticleRelease, templateManager);
                    photoArticleRelease.ReleasePath = rawReleasePath;

                    ReleasingStaticPage(photoArticleRelease, templateManager);
                }
            }
            else
            {
                templateManager.SetVariable("PhotoArticle", photoArticle); // ������µ�����
                photoArticleRelease.ReleasePath = Regex.Replace(photoArticleRelease.ReleasePath, @"\{ArticleId\}", photoArticle.ArticleId.ToString(), RegexOptions.IgnoreCase);

                ReleasingStaticPage(photoArticleRelease, templateManager);
            }
        }

        /// <summary>
        /// ����ͼƬ�����б�ҳ��
        /// </summary>
        /// <param name="photoArticleCategory">ͼƬ���µķ���</param>
        /// <param name="photoArticleCategoryRelease">ͼƬ���µķ����Ӧ�ķ�������</param>
        public void ReleasingPhotoArticleList(Category photoArticleCategory, Release photoArticleCategoryRelease)
        {
            // �б�ҳ��{ReleaseDirectory}/{CategoryId}/{PageIndex}.htm
            if (photoArticleCategoryRelease.Template.TemplateType != TemplateType.PhotoArticleList) return;

            // װ��ģ��
            photoArticleCategoryRelease.Template.TemplatePath = Regex.Replace(photoArticleCategoryRelease.Template.TemplatePath, @"\{TemplateDirectory\}", this.TemplateDirectory, RegexOptions.IgnoreCase);
            string templatePath = System.Web.HttpContext.Current.Server.MapPath(photoArticleCategoryRelease.Template.TemplatePath);
            Wis.Toolkit.Templates.TemplateManager templateManager = Wis.Toolkit.Templates.TemplateManager.LoadFile(templatePath, Encoding.UTF8);
            templateManager.SetVariable("PhotoArticleCategory", photoArticleCategory);
            templateManager.SetVariable("ApplicationPath", this.ApplicationPath);
            templateManager.SetVariable("TemplateDirectory", this.TemplateDirectory);
            templateManager.SetVariable("ReleaseDirectory", this.ReleaseDirectory);

            // ���� Release
            photoArticleCategoryRelease.ReleasePath = Regex.Replace(photoArticleCategoryRelease.ReleasePath, @"\{ReleaseDirectory\}", this.ReleaseDirectory, RegexOptions.IgnoreCase);
            photoArticleCategoryRelease.ReleasePath = Regex.Replace(photoArticleCategoryRelease.ReleasePath, @"\{CategoryId\}", photoArticleCategory.CategoryId.ToString(), RegexOptions.IgnoreCase);

            if (photoArticleCategoryRelease.PageSize.HasValue == false)
                throw new System.ArgumentNullException(string.Format("�������Ϊ {0} �� PageSize ����δ����", photoArticleCategoryRelease.ReleaseId));

            // ����һ���ж���ҳ��Ȼ����ҳ����
            int pageSize = photoArticleCategoryRelease.PageSize.Value;
            templateManager.SetVariable("PageSize", pageSize);
            templateManager.SetVariable("RecordCount", photoArticleCategory.RecordCount);

            // ��ҳ���� pageCount
            int pageCount;
            pageCount = photoArticleCategory.RecordCount / pageSize;
            if (photoArticleCategory.RecordCount % pageSize != 0)
                pageCount += 1;
            templateManager.SetVariable("PageCount", pageCount);

            // ��ҳ����
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

                    string rawReleasePath = photoArticleCategoryRelease.ReleasePath; // ��¼{PageIndex}
                    photoArticleCategoryRelease.ReleasePath = Regex.Replace(rawReleasePath, @"\{PageIndex\}", pageIndex.ToString(), RegexOptions.IgnoreCase);
                    ReleasingStaticPage(photoArticleCategoryRelease, templateManager);
                    photoArticleCategoryRelease.ReleasePath = rawReleasePath;
                }
            }
        }

        /// <summary>
        /// ����ͼƬ��������ҳ��
        /// </summary>
        /// <param name="photoArticleCategory">ͼƬ���µķ���</param>
        /// <param name="photoArticleCategoryRelease">ͼƬ���µķ����Ӧ�ķ�������</param>
        public void ReleasingPhotoArticleIndex(Category photoArticleCategory, Release photoArticleCategoryRelease)
        {
            // ͼƬ��������ҳ��{ReleaseDirectory}/{CategoryId}/1.htm
            if (photoArticleCategoryRelease.Template.TemplateType != TemplateType.PhotoArticleIndex) return;

            // װ��ģ��
            photoArticleCategoryRelease.Template.TemplatePath = Regex.Replace(photoArticleCategoryRelease.Template.TemplatePath, @"\{TemplateDirectory\}", this.TemplateDirectory, RegexOptions.IgnoreCase);
            string templatePath = System.Web.HttpContext.Current.Server.MapPath(photoArticleCategoryRelease.Template.TemplatePath);
            Wis.Toolkit.Templates.TemplateManager templateManager = Wis.Toolkit.Templates.TemplateManager.LoadFile(templatePath, Encoding.UTF8);
            templateManager.SetVariable("PhotoArticleCategory", photoArticleCategory);
            templateManager.SetVariable("ApplicationPath", this.ApplicationPath);
            templateManager.SetVariable("TemplateDirectory", this.TemplateDirectory);
            templateManager.SetVariable("ReleaseDirectory", this.ReleaseDirectory);

            // ���� Release
            photoArticleCategoryRelease.ReleasePath = Regex.Replace(photoArticleCategoryRelease.ReleasePath, @"\{ReleaseDirectory\}", this.ReleaseDirectory, RegexOptions.IgnoreCase);
            photoArticleCategoryRelease.ReleasePath = Regex.Replace(photoArticleCategoryRelease.ReleasePath, @"\{CategoryId\}", photoArticleCategory.CategoryId.ToString(), RegexOptions.IgnoreCase);

            ReleasingStaticPage(photoArticleCategoryRelease, templateManager);
        }
        #endregion

        #region ������Ƶ����

        /// <summary>
        /// ������Ƶ���¾�̬ҳ�͹����ľ�̬ҳ��
        /// </summary>
        /// <param name="videoArticle"></param>
        public void ReleasingVideoArticle(VideoArticle videoArticle)
        {
            // 1 ���� VideoArticle ҳ�棬���ݷ�������Item��ģ��
            // 2 ���� Category ҳ�棬���ݷ�������List��Index��ģ��
            List<Release> releases = GetReleasesByCategoryGuid(videoArticle.Article.Category.CategoryGuid);
            foreach (Release release in releases)
            {
                switch (release.Template.TemplateType)
                {
                    case TemplateType.VideoArticleIndex: // ��Ƶ��������ҳ
                        ReleasingVideoArticleIndex(videoArticle.Article.Category, release);
                        break;
                    case TemplateType.VideoArticleList: // ��Ƶ�����б�ҳ������ҳ
                        ReleasingVideoArticleList(videoArticle.Article.Category, release);
                        break;
                    case TemplateType.VideoArticleItem:// ��Ƶ������ϸҳ
                        ReleasingVideoArticleItem(videoArticle, release);
                        break;
                    default:
                        throw new System.ArgumentException("TemplateType");
                }          
            }

            // 3 ���������õ�ҳ��
            ReleasingRelatedReleases(videoArticle.Article.Category.CategoryGuid);
        }

        public void ReleasingRemovedVideoArticle(VideoArticle videoArticle)
        {
            // 1 ���� VideoArticle ҳ�棬���ݷ������� Item ��ģ��
            // 2 ���� Category ҳ�棬���ݷ�������List��Index��ģ��
            List<Release> releases = GetReleasesByCategoryGuid(videoArticle.Article.Category.CategoryGuid);
            foreach (Release release in releases)
            {
                switch (release.Template.TemplateType)
                {
                    case TemplateType.VideoArticleIndex: // ��Ƶ��������ҳ
                        ReleasingVideoArticleIndex(videoArticle.Article.Category, release);
                        break;
                    case TemplateType.VideoArticleList: // ��Ƶ�����б�ҳ������ҳ
                        ReleasingVideoArticleList(videoArticle.Article.Category, release);
                        break;
                    case TemplateType.VideoArticleItem:// �Ƴ�������ϸҳ
                        // ���� Release
                        release.ReleasePath = Regex.Replace(release.ReleasePath, @"\{ReleaseDirectory\}", this.ReleaseDirectory, RegexOptions.IgnoreCase);
                        release.ReleasePath = Regex.Replace(release.ReleasePath, @"\{CategoryId\}", videoArticle.Article.Category.CategoryId.ToString(), RegexOptions.IgnoreCase);
                        release.ReleasePath = Regex.Replace(release.ReleasePath, @"\{Year\}", videoArticle.Article.DateCreated.Year.ToString(), RegexOptions.IgnoreCase);
                        release.ReleasePath = Regex.Replace(release.ReleasePath, @"\{Month\}", videoArticle.Article.DateCreated.Month.ToString(), RegexOptions.IgnoreCase);
                        release.ReleasePath = Regex.Replace(release.ReleasePath, @"\{Day\}", videoArticle.Article.DateCreated.Day.ToString(), RegexOptions.IgnoreCase);

                        // ���ɾ�̬ҳ
#warning ���� ContentHtml ���ȣ�ɾ����ҳ����Ҫ����
                        string pagerSeparator = "<!--Article.ContentHtml.Pager-->";
                        if (videoArticle.Article.ContentHtml.IndexOf(pagerSeparator) > -1) // ���з�ҳ
                        {
                            string[] contentHtmls = videoArticle.Article.ContentHtml.Split(pagerSeparator.ToCharArray());
                            for (int index = 0; index < contentHtmls.Length; index++)
                            {
                                string replacedArticleId = string.Format("{0}/{1}", videoArticle.Article.ArticleId, (index + 1));
                                release.ReleasePath = Regex.Replace(release.ReleasePath, @"\{ArticleId\}", replacedArticleId, RegexOptions.IgnoreCase);

                                // ɾ�����¾�̬ҳ
                                release.ReleasePath = System.Web.HttpContext.Current.Server.MapPath(release.ReleasePath);
                                if (System.IO.File.Exists(release.ReleasePath))
                                    System.IO.File.Delete(release.ReleasePath);
                            }
                        }
                        else
                        {
                            release.ReleasePath = Regex.Replace(release.ReleasePath, @"\{ArticleId\}", videoArticle.Article.ArticleId.ToString(), RegexOptions.IgnoreCase);

                            // ɾ�����¾�̬ҳ
                            release.ReleasePath = System.Web.HttpContext.Current.Server.MapPath(release.ReleasePath);
                            if (System.IO.File.Exists(release.ReleasePath))
                                System.IO.File.Delete(release.ReleasePath);
                        }

                        break;
                    default:
                        throw new System.ArgumentException("TemplateType");
                }
            }

            // 3 ���������õ�ҳ��
            ReleasingRelatedReleases(videoArticle.Article.Category.CategoryGuid);
        }


        /// <summary>
        /// ������Ƶ��������ҳ��
        /// </summary>
        /// <param name="videoArticle">��Ƶ����</param>
        public void ReleasingVideoArticleItem(VideoArticle videoArticle)
        {
            List<Release> releases = GetReleasesByCategoryGuid(videoArticle.Article.Category.CategoryGuid, TemplateType.VideoArticleItem);
            foreach (Release release in releases)
            {
                ReleasingVideoArticleItem(videoArticle, release);
            }
        }

        /// <summary>
        /// ������Ƶ��������ҳ��
        /// </summary>
        /// <param name="videoArticle">��Ƶ����</param>
        /// <param name="videoArticleRelease">��Ƶ���·�������</param>
        public void ReleasingVideoArticleItem(VideoArticle videoArticle, Release videoArticleRelease)
        {
            // ��ϸҳ��{ReleaseDirectory}/{CategoryId}/{Year}-{Month}-{Day}/{ArticleId}.htm
            if (videoArticleRelease.Template.TemplateType != TemplateType.VideoArticleItem) return;

            // װ��ģ��
            videoArticleRelease.Template.TemplatePath = Regex.Replace(videoArticleRelease.Template.TemplatePath, @"\{TemplateDirectory\}", this.TemplateDirectory, RegexOptions.IgnoreCase);
            string templatePath = System.Web.HttpContext.Current.Server.MapPath(videoArticleRelease.Template.TemplatePath);
            Wis.Toolkit.Templates.TemplateManager templateManager = Wis.Toolkit.Templates.TemplateManager.LoadFile(templatePath, Encoding.UTF8);

            templateManager.SetVariable("ApplicationPath", this.ApplicationPath);
            templateManager.SetVariable("TemplateDirectory", this.TemplateDirectory);
            templateManager.SetVariable("ReleaseDirectory", this.ReleaseDirectory);

            // ���� Release
            videoArticleRelease.ReleasePath = Regex.Replace(videoArticleRelease.ReleasePath, @"\{ReleaseDirectory\}", this.ReleaseDirectory, RegexOptions.IgnoreCase);
            videoArticleRelease.ReleasePath = Regex.Replace(videoArticleRelease.ReleasePath, @"\{CategoryId\}", videoArticle.Article.Category.CategoryId.ToString(), RegexOptions.IgnoreCase);
            videoArticleRelease.ReleasePath = Regex.Replace(videoArticleRelease.ReleasePath, @"\{Year\}", videoArticle.Article.DateCreated.Year.ToString(), RegexOptions.IgnoreCase);
            videoArticleRelease.ReleasePath = Regex.Replace(videoArticleRelease.ReleasePath, @"\{Month\}", videoArticle.Article.DateCreated.Month.ToString(), RegexOptions.IgnoreCase);
            videoArticleRelease.ReleasePath = Regex.Replace(videoArticleRelease.ReleasePath, @"\{Day\}", videoArticle.Article.DateCreated.Day.ToString(), RegexOptions.IgnoreCase);

            // ���ɾ�̬ҳ
#warning ���� ContentHtml ���ȣ����ɶ�ҳ����Ҫ����
            string pagerSeparator = "<!--Article.ContentHtml.Pager-->";
            if (videoArticle.Article.ContentHtml.IndexOf(pagerSeparator) > -1) // ���з�ҳ
            {
                string[] contentHtmls = videoArticle.Article.ContentHtml.Split(pagerSeparator.ToCharArray());
                for (int index = 0; index < contentHtmls.Length; index++)
                {
                    videoArticle.Article.ContentHtml = contentHtmls[index] + ReleaseTinyPager((index + 1), contentHtmls.Length);
                    templateManager.SetVariable("VideoArticle", videoArticle); // ������µ�����
                    string replacedArticleId = string.Format("{0}/{1}", videoArticle.Article.ArticleId, (index + 1));
                    //videoArticleRelease.ReleasePath = Regex.Replace(videoArticleRelease.ReleasePath, @"\{ArticleId\}", replacedArticleId, RegexOptions.IgnoreCase);
                    string rawReleasePath = videoArticleRelease.ReleasePath; // ��¼{ArticleId}
                    videoArticleRelease.ReleasePath = Regex.Replace(rawReleasePath, @"\{ArticleId\}", replacedArticleId, RegexOptions.IgnoreCase);
                    ReleasingStaticPage(videoArticleRelease, templateManager);
                    videoArticleRelease.ReleasePath = rawReleasePath;

                    ReleasingStaticPage(videoArticleRelease, templateManager);
                }
            }
            else
            {
                templateManager.SetVariable("VideoArticle", videoArticle); // ������µ�����
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
            // �б�ҳ��{ReleaseDirectory}/{CategoryId}/{PageIndex}.htm
            if (videoArticleCategoryRelease.Template.TemplateType != TemplateType.VideoArticleList) return;

            // װ��ģ��
            videoArticleCategoryRelease.Template.TemplatePath = Regex.Replace(videoArticleCategoryRelease.Template.TemplatePath, @"\{TemplateDirectory\}", this.TemplateDirectory, RegexOptions.IgnoreCase);
            string templatePath = System.Web.HttpContext.Current.Server.MapPath(videoArticleCategoryRelease.Template.TemplatePath);
            Wis.Toolkit.Templates.TemplateManager templateManager = Wis.Toolkit.Templates.TemplateManager.LoadFile(templatePath, Encoding.UTF8);
            templateManager.SetVariable("VideoArticleCategory", videoArticleCategory);
            templateManager.SetVariable("ApplicationPath", this.ApplicationPath);
            templateManager.SetVariable("TemplateDirectory", this.TemplateDirectory);
            templateManager.SetVariable("ReleaseDirectory", this.ReleaseDirectory);

            // ���� Release
            videoArticleCategoryRelease.ReleasePath = Regex.Replace(videoArticleCategoryRelease.ReleasePath, @"\{ReleaseDirectory\}", this.ReleaseDirectory, RegexOptions.IgnoreCase);
            videoArticleCategoryRelease.ReleasePath = Regex.Replace(videoArticleCategoryRelease.ReleasePath, @"\{CategoryId\}", videoArticleCategory.CategoryId.ToString(), RegexOptions.IgnoreCase);

            if (videoArticleCategoryRelease.PageSize.HasValue == false)
                throw new System.ArgumentNullException(string.Format("�������Ϊ {0} �� PageSize ����δ����", videoArticleCategoryRelease.ReleaseId));

            // ����һ���ж���ҳ��Ȼ����ҳ����
            int pageSize = videoArticleCategoryRelease.PageSize.Value;
            templateManager.SetVariable("PageSize", pageSize);
            templateManager.SetVariable("RecordCount", videoArticleCategory.RecordCount);

            // ��ҳ���� pageCount
            int pageCount;
            pageCount = videoArticleCategory.RecordCount / pageSize;
            if (videoArticleCategory.RecordCount % pageSize != 0)
                pageCount += 1;
            templateManager.SetVariable("PageCount", pageCount);

            // ��ҳ����
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

                    string rawReleasePath = videoArticleCategoryRelease.ReleasePath; // ��¼{PageIndex}
                    videoArticleCategoryRelease.ReleasePath = Regex.Replace(rawReleasePath, @"\{PageIndex\}", pageIndex.ToString(), RegexOptions.IgnoreCase);
                    ReleasingStaticPage(videoArticleCategoryRelease, templateManager);
                    videoArticleCategoryRelease.ReleasePath = rawReleasePath;
                }
            }
        }

        /// <summary>
        /// ������Ƶ��������ҳ��
        /// </summary>
        /// <param name="videoArticleCategory">��Ƶ���·���</param>
        /// <param name="videoArticleCategoryRelease">��Ƶ���·����Ӧ�ķ�����</param>
        public void ReleasingVideoArticleIndex(Category videoArticleCategory, Release videoArticleCategoryRelease)
        {
            // ��Ƶ��������ҳ��{ReleaseDirectory}/{CategoryId}/1.htm
            if (videoArticleCategoryRelease.Template.TemplateType != TemplateType.VideoArticleIndex) return;

            // װ��ģ��
            videoArticleCategoryRelease.Template.TemplatePath = Regex.Replace(videoArticleCategoryRelease.Template.TemplatePath, @"\{TemplateDirectory\}", this.TemplateDirectory, RegexOptions.IgnoreCase);
            string templatePath = System.Web.HttpContext.Current.Server.MapPath(videoArticleCategoryRelease.Template.TemplatePath);
            Wis.Toolkit.Templates.TemplateManager templateManager = Wis.Toolkit.Templates.TemplateManager.LoadFile(templatePath, Encoding.UTF8);
            templateManager.SetVariable("VideoArticleCategory", videoArticleCategory);
            templateManager.SetVariable("ApplicationPath", this.ApplicationPath);
            templateManager.SetVariable("TemplateDirectory", this.TemplateDirectory);
            templateManager.SetVariable("ReleaseDirectory", this.ReleaseDirectory);

            // ���� Release
            videoArticleCategoryRelease.ReleasePath = Regex.Replace(videoArticleCategoryRelease.ReleasePath, @"\{ReleaseDirectory\}", this.ReleaseDirectory, RegexOptions.IgnoreCase);
            videoArticleCategoryRelease.ReleasePath = Regex.Replace(videoArticleCategoryRelease.ReleasePath, @"\{CategoryId\}", videoArticleCategory.CategoryId.ToString(), RegexOptions.IgnoreCase);

            ReleasingStaticPage(videoArticleCategoryRelease, templateManager);
        }
        #endregion

        #region ����������ҳ��
        /// <summary>
        /// ���������õ�ҳ�棬������ҳĳ����Ŀ�����˸÷��࣬ר�������˸ķ��࣬�ѹ����ķ���ҳ����������
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
                    case TemplateType.ArticleItem: // ������Ӧ�����µ���������
                        ArticleManager articleManager = new ArticleManager();
                        List<Article> relatedArticles = articleManager.GetArticlesByReleaseGuid(relatedRelease.ReleaseGuid);
                        foreach (Article relatedArticle in relatedArticles)
                        {
                            ReleasingArticleItem(relatedArticle, relatedRelease);
                        }
                        break;
                    case TemplateType.ArticleList: // ������Ӧ�����µ������б�ҳ
                        List<Category> relatedArticleListCategories = categoryManager.GetCategorysByReleaseGuid(relatedRelease.ReleaseGuid);
                        foreach (Category relatedCategory in relatedArticleListCategories)
                        {
                            ReleasingArticleList(relatedCategory, relatedRelease);
                        }
                        break;
                    case TemplateType.ArticleIndex: // ������Ӧ�����µ���������ҳ
                        List<Category> relatedArticleIndexCategories = categoryManager.GetCategorysByReleaseGuid(relatedRelease.ReleaseGuid);
                        foreach (Category relatedCategory in relatedArticleIndexCategories)
                        {
                            ReleasingArticleIndex(relatedCategory, relatedRelease);
                        }
                        break;
                    case TemplateType.Page: // ������Ӧ�ĵ�ҳ
                        PageManager pageManager = new PageManager();
                        List<Page> relatedPages = pageManager.GetPagesByReleaseGuid(relatedRelease.ReleaseGuid);
                        foreach (Page relatedPage in relatedPages)
                        {
                            ReleasingPage(relatedPage, relatedRelease);
                        }
                        break;
                    case TemplateType.PhotoArticleItem: // ������Ӧ�����µ�ͼƬ����
                        ArticlePhotoManager photoArticleManager = new ArticlePhotoManager();
                        List<ArticlePhoto> photoArticles = photoArticleManager.GetPhotoArticlesByReleaseGuid(relatedRelease.ReleaseGuid);
                        foreach (ArticlePhoto photoArticle in photoArticles)
                        {
                            ReleasingPhotoArticleItem(photoArticle, relatedRelease);
                        }
                        break;
                    case TemplateType.PhotoArticleList: // ������Ӧ�����µ��б�ҳ
                        List<Category> relatedPhotoArticleListCategories = categoryManager.GetCategorysByReleaseGuid(relatedRelease.ReleaseGuid);
                        foreach (Category relatedCategory in relatedPhotoArticleListCategories)
                        {
                            ReleasingPhotoArticleList(relatedCategory, relatedRelease);
                        }
                        break;
                    case TemplateType.SpecialList: // ����ר���б�ҳ
                        SpecialManager specialManager = new SpecialManager();
                        List<Special> relatedSpecials = specialManager.GetSpecialsByReleaseGuid(relatedRelease.ReleaseGuid);
                        foreach (Special relatedSpecial in relatedSpecials)
                        {
                            ReleasingSpecial(relatedSpecial, relatedRelease);
                        }
                        break;
                    case TemplateType.VideoArticleItem: // ������Ӧ�����µ�������Ƶ������ϸҳ
                        VideoArticleManager videoArticleManager = new VideoArticleManager();
                        List<VideoArticle> relatedVideoArticles = videoArticleManager.GetVideoArticlesByReleaseGuid(relatedRelease.ReleaseGuid);
                        foreach (VideoArticle relatedVideoArticle in relatedVideoArticles)
                        {
                            ReleasingVideoArticleItem(relatedVideoArticle, relatedRelease);
                        }
                        break;
                    case TemplateType.VideoArticleList: // ������Ӧ�����µ�������Ƶ�б�ҳ
                        List<Category> relatedVideoArticleListCategories = categoryManager.GetCategorysByReleaseGuid(relatedRelease.ReleaseGuid);
                        foreach (Category relatedCategory in relatedVideoArticleListCategories)
                        {
                            ReleasingVideoArticleList(relatedCategory, relatedRelease);
                        }
                        break;
                    case TemplateType.VideoArticleIndex: // ������Ӧ�����µ�������Ƶ����ҳ
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

#warning �������
#warning ��������

#warning ������ҳ

        /// <summary>
        /// ������ҳ
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageRelease"></param>
        public void ReleasingPage(Page page, Release pageRelease)
        {
            // ��ҳ��  "{RootPath}/{PageId}.htm" ���{PageId}����֪��
        }

        /// <summary>
        /// ����ר��ҳ��
        /// </summary>
        /// <param name="special"></param>
        /// <param name="specialRelease"></param>
        public void ReleasingSpecial(Special special, Release specialRelease)
        {
            // ר��ҳ��{RootPath}/{SpecialId}.htm ���{SpecialId}����֪��
        }

#warning �������ͣ�����Զ����ļ�������ȡ����ƴ����

#warning ReleaseRelation ��ӳ��ǰ̨��վ��վ��ṹ�����Ը���Release������վ���ͼGoogle Sitemap��Baidu Sitemap

        /// <summary>
        /// ������̬ҳ
        /// </summary>
        /// <param name="ripeRelease">Ԥ�������Release����</param>
        /// <param name="templateManager">Ԥ�������ģ�������</param>
        public void ReleasingStaticPage(Release ripeRelease, Wis.Toolkit.Templates.TemplateManager ripeTemplateManager)
        {
            lock (this)
            {
                // ����·��
                string releasePath = System.Web.HttpContext.Current.Server.MapPath(ripeRelease.ReleasePath);

                // TODO:ʧ������λع�? ���Ѿ�ɾ������ҳ�ָ�
                if (System.IO.File.Exists(releasePath))
                {
                    System.IO.File.Delete(releasePath);
                    System.Threading.Thread.Sleep(100);
                }

                // ���Ŀ¼�����ڣ��򴴽�Ŀ¼
                System.IO.FileInfo fileInfo = new System.IO.FileInfo(releasePath);
                if (!fileInfo.Directory.Exists) fileInfo.Directory.Create();

                // ���ɷ���
                using (System.IO.StreamWriter sw = new System.IO.StreamWriter(releasePath, false, Encoding.UTF8))
                {
                    sw.Write(ripeTemplateManager.Process());
                }
            }
        }

        #region ����/�޸�/ɾ��
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
