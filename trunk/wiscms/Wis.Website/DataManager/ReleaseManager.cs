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


        public string ReleaseTinyPager(int pageIndex, int recordCount, int pageCount)
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

        /// <summary>
        /// ���ݷ����Ż�ȡ�����ķ�������
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


        /// <summary>
        /// ������Ƶ������ص�ҳ��
        /// </summary>
        /// <param name="videoArticle">��Ƶ����ʵ����</param>
        public void ReleaseVideoArticleRelation(VideoArticle videoArticle)
        {
            // 1 ���� Category ҳ��
            List<Release> releases = GetReleasesByCategory(videoArticle.Article.Category.CategoryGuid);
            if (releases.Count > 0)
            {
                foreach (Release release in releases)
                {
                    // װ��ģ��
                    release.Template.TemplatePath = Regex.Replace(release.Template.TemplatePath, @"\{RootPath\}", this.TemplateDirectory, RegexOptions.IgnoreCase);
                    string templatePath = System.Web.HttpContext.Current.Server.MapPath(release.Template.TemplatePath);
                    Wis.Toolkit.Templates.TemplateManager templateManager = Wis.Toolkit.Templates.TemplateManager.LoadFile(templatePath, Encoding.UTF8);
                    templateManager.SetVariable("VideoArticle", videoArticle); // ������µ�����
                    templateManager.SetVariable("Category", videoArticle.Article.Category); // ������µķ���
                    templateManager.SetVariable("ApplicationPath", this.ApplicationPath);
                    templateManager.SetVariable("TemplateDirectory", this.TemplateDirectory);
                    templateManager.SetVariable("ReleaseDirectory", this.ReleaseDirectory);

                    // ���� Release
                    release.ReleasePath = Regex.Replace(release.ReleasePath, @"\{RootPath\}", this.ReleaseDirectory, RegexOptions.IgnoreCase);
                    release.ReleasePath = Regex.Replace(release.ReleasePath, @"\{CategoryId\}", videoArticle.Article.Category.CategoryId.ToString(), RegexOptions.IgnoreCase);

                    switch (release.Template.TemplateType)
                    {
                        case TemplateType.Index: // 1.1 ���� Index ҳ�棬������ҳ
                            // ��ҳ��{RootPath}/default.htm
                            ReleaseStaticPage(release, templateManager);
                            break;
                        case TemplateType.VideoArticleList: // 1.2 ���� List ҳ�棬����ҳ
#warning �ص���ԣ���Ƶ����-�����б�ҳ ��������-�����б�ҳ ������ѧ-�����б�ҳ ���л-�����б�ҳ

                            // �б�ҳ��{RootPath}/{CategoryId}/{PageIndex}.htm
                            if (release.PageSize.HasValue == false)
                                throw new System.ArgumentNullException(string.Format("�������Ϊ {0} �� PageSize ����δ����", release.ReleaseId));

                            // ����һ���ж���ҳ��Ȼ����ҳ����
                            int pageSize = release.PageSize.Value;
                            templateManager.SetVariable("PageSize", pageSize);

                            // ��������
                            ArticleManager articleManager = new ArticleManager();
                            int recordCount = (int)articleManager.CountArticlesByCategoryGuid(videoArticle.Article.Category.CategoryGuid);
#warning ��Category����RecordCount�ֶΣ�������
                            templateManager.SetVariable("RecordCount", recordCount);

                            // ��ҳ���� pageCount
                            int pageCount;
                            pageCount = recordCount / pageSize;
                            if (recordCount % pageSize != 0)
                                pageCount += 1;
                            templateManager.SetVariable("PageCount", pageCount);

                            // ��ҳ����
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
                        case TemplateType.VideoArticleItem:// 1.3 ������ϸҳ
                            // ��ϸҳ��{RootPath}/{CategoryId}/{Month}-{Day}/{ArticleId}.htm
                            release.ReleasePath = Regex.Replace(release.ReleasePath, @"\{Year\}", videoArticle.Article.DateCreated.Year.ToString(), RegexOptions.IgnoreCase);
                            release.ReleasePath = Regex.Replace(release.ReleasePath, @"\{Month\}", videoArticle.Article.DateCreated.Month.ToString(), RegexOptions.IgnoreCase);
                            release.ReleasePath = Regex.Replace(release.ReleasePath, @"\{Day\}", videoArticle.Article.DateCreated.Day.ToString(), RegexOptions.IgnoreCase);
                            release.ReleasePath = Regex.Replace(release.ReleasePath, @"\{ArticleId\}", videoArticle.Article.ArticleId.ToString(), RegexOptions.IgnoreCase);
                            // TODO:���� ContentHtml ���ȣ����ɶ�ҳ
                            //if (release.ReleasePath.IndexOf("{ArticleId}") > -1) // ��ϸҳ
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
            // ���� Page ҳ��
            // ��ҳ��  "{RootPath}/{PageId}.htm" ���{PageId}����֪��

            // ���� ר�� ҳ��
            // ר��ҳ��{RootPath}/{SpecialId}.htm ���{SpecialId}����֪��
        }

        /// <summary>
        /// ����ͼƬ���ŵĹ���ҳ�档
        /// </summary>
        /// <param name="articlePhoto"></param>
        public void ReleaseArticlePhotoRelation(ArticlePhoto articlePhoto)
        {
            List<Release> releases = GetReleasesByCategory(articlePhoto.Category.CategoryGuid);
            if (releases.Count == 0) return;

            foreach (Release release in releases)
            {
                // װ��ģ��
                release.Template.TemplatePath = release.Template.TemplatePath.Replace("{RootPath}", this.TemplateDirectory);
                string templatePath = System.Web.HttpContext.Current.Server.MapPath(release.Template.TemplatePath);
                Wis.Toolkit.Templates.TemplateManager templateManager = Wis.Toolkit.Templates.TemplateManager.LoadFile(templatePath, Encoding.UTF8);
                templateManager.SetVariable("ApplicationPath", this.ApplicationPath);
                templateManager.SetVariable("TemplateDirectory", this.TemplateDirectory);
                templateManager.SetVariable("ReleaseDirectory", this.ReleaseDirectory);

                // ���ɾ�̬ҳ��֧������ҳ������ҳ���б�ҳ����ϸҳ��ר��ҳ
                // ��ҳ��  "{RootPath}/default.htm"
                // ��ҳ��  "{RootPath}/{PageId}.htm" ���{PageId}����֪��
                // �б�ҳ��"{RootPath}/{CategoryId}/{PageIndex}.htm"
                // ͼƬ����-��ϸҳ��"{RootPath}/{CategoryId}/{Month}-{Day}/{ArticleId}.htm"
                // ר��ҳ��"{RootPath}/{SpecialId}.htm" ���{SpecialId}����֪��
                release.ReleasePath = release.ReleasePath.Replace("{RootPath}", this.ReleaseDirectory);
                release.ReleasePath = release.ReleasePath.Replace("{CategoryId}", articlePhoto.Category.CategoryId.ToString());

                // ����ҳ
                // ���� {PageIndex:20}
                string pattern = @"\{PageIndex\:(?<PageSize>\d+)\}";
                Regex reg = new Regex(pattern, RegexOptions.IgnoreCase);
                Match match = reg.Match(release.ReleasePath);
                if (match.Success)
                {
                    // �ĸ�����
                    templateManager.SetVariable("Category", articlePhoto.Category);

                    // ����һ���ж���ҳ��Ȼ����ҳ����
                    int pageSize = 20;
                    string groupPageSize = match.Groups[1].Value;
                    int.TryParse(groupPageSize, out pageSize);
                    templateManager.SetVariable("PageSize", pageSize);

                    // ��������
                    ArticleManager articleManager = new ArticleManager();
                    int recordCount = (int)articleManager.CountArticlesByCategoryGuid(articlePhoto.Category.CategoryGuid);
                    templateManager.SetVariable("RecordCount", recordCount);

                    // ��ҳ���� pageCount
                    int pageCount;
                    pageCount = recordCount / pageSize;
                    if (recordCount % pageSize != 0)
                        pageCount += 1;
                    templateManager.SetVariable("PageCount", pageCount);

                    // ��ҳ����
                    for (int pageIndex = 1; pageIndex <= pageCount; pageIndex++)
                    {
                        // {RootPath}/{CategoryId}/{PageIndex}.htm
                        templateManager.SetVariable("PageIndex", pageIndex);
                        // ��ҳ
#warning ReleaseTinyPager ��������ʹ�����ַ�ҳ��PagerMode?

                        templateManager.SetVariable("Pager", ReleaseTinyPager(pageIndex, recordCount, pageCount));
                        release.ReleasePath = Regex.Replace(release.ReleasePath, pattern, pageIndex.ToString(), RegexOptions.IgnoreCase);
                        
                        ReleaseStaticPage(release, templateManager);
                    }
                }
                else
                {
                    if (release.ReleasePath.IndexOf("{ArticleId}") > -1) // ��ϸҳ
                    {
                        release.ReleasePath = release.ReleasePath.Replace("{Year}", articlePhoto.DateCreated.Year.ToString());
                        release.ReleasePath = release.ReleasePath.Replace("{Month}", articlePhoto.DateCreated.Month.ToString());
                        release.ReleasePath = release.ReleasePath.Replace("{Day}", articlePhoto.DateCreated.Day.ToString());
                        release.ReleasePath = release.ReleasePath.Replace("{ArticleId}", articlePhoto.ArticleId.ToString());
                        templateManager.SetVariable("ArticlePhoto", articlePhoto);
                    }

                    // TODO:���� ContentHtml ���ȣ����ɶ�ҳ
                    //if (release.ReleasePath.IndexOf("{ArticleId}") > -1) // ��ϸҳ
                    //{
                    //}

                    ReleaseStaticPage(release, templateManager);
                }
            }
        }


#warning ReleaseRelation ��ӳ��ǰ̨��վ��վ��ṹ�����Ը���Release������վ���ͼGoogle Sitemap��Baidu Sitemap

        /// <summary>
        /// ������ģ��û����ϣ�һƪ���µ���ϸҳ�����ö���ģ�����ɣ�ͬʱ������Ӱ��Ĺ���ҳ�档
        /// 1����ȡ���������Release���󼯺ϣ�
        /// 2���Ƿ��з�ҳ���б�ҳ���з�ҳ����ҳ���ɣ�û�з�ҳ������ҳ����ϸҳ��ר��ҳ��������ɣ�
        /// </summary>
        /// <param name="article"></param>
        public void ReleaseRelation(Article article)
        {
            // ���ݷ����Ż�ȡ������� 
            List<Release> releases = GetReleasesByCategory(article.Category.CategoryGuid);
            if (releases.Count == 0) return;

            foreach (Release release in releases)
            {
                // װ��ģ��
                release.Template.TemplatePath = release.Template.TemplatePath.Replace("{RootPath}", this.TemplateDirectory);
                string templatePath = System.Web.HttpContext.Current.Server.MapPath(release.Template.TemplatePath);
                Wis.Toolkit.Templates.TemplateManager templateManager = Wis.Toolkit.Templates.TemplateManager.LoadFile(templatePath, Encoding.UTF8);
                templateManager.SetVariable("ApplicationPath", this.ApplicationPath);
                templateManager.SetVariable("TemplateDirectory", this.TemplateDirectory);
                templateManager.SetVariable("ReleaseDirectory", this.ReleaseDirectory);

                // ���ɾ�̬ҳ��֧������ҳ������ҳ���б�ҳ����ϸҳ��ר��ҳ
                // ��ҳ��  "{RootPath}/default.htm"
                // ��ҳ��  "{RootPath}/{PageId}.htm" ���{PageId}����֪��
                // �б�ҳ��"{RootPath}/{CategoryId}/{PageIndex}.htm"
                // ��ϸҳ��"{RootPath}/{CategoryId}/{Month}-{Day}/{ArticleId}.htm"
                // ר��ҳ��"{RootPath}/{SpecialId}.htm" ���{SpecialId}����֪��
                release.ReleasePath = release.ReleasePath.Replace("{RootPath}", this.ReleaseDirectory);
                release.ReleasePath = release.ReleasePath.Replace("{CategoryId}", article.Category.CategoryId.ToString());

                // ����ҳ
                // ���� {PageIndex:20}
                string pattern = @"\{PageIndex\:(?<PageSize>\d+)\}";
                Regex reg = new Regex(pattern, RegexOptions.IgnoreCase);
                Match match = reg.Match(release.ReleasePath);
                if (match.Success)
                {
                    // �ĸ�����
                    templateManager.SetVariable("Category", article.Category);

                    // ����һ���ж���ҳ��Ȼ����ҳ����
                    int pageSize = 20;
                    string groupPageSize = match.Groups[1].Value;
                    int.TryParse(groupPageSize, out pageSize);
                    templateManager.SetVariable("PageSize", pageSize);

                    // ��������
                    ArticleManager articleManager = new ArticleManager();
                    int recordCount = (int)articleManager.CountArticlesByCategoryGuid(article.Category.CategoryGuid);
                    templateManager.SetVariable("RecordCount", recordCount);

                    // ��ҳ���� pageCount
                    int pageCount;
                    pageCount = recordCount / pageSize;
                    if (recordCount % pageSize != 0)
                        pageCount += 1;
                    templateManager.SetVariable("PageCount", pageCount);

                    // ��ҳ����
                    for (int pageIndex = 1; pageIndex <= pageCount; pageIndex++)
                    {
                        // {RootPath}/{CategoryId}/{PageIndex}.htm
                        templateManager.SetVariable("PageIndex", pageIndex);
                        // ��ҳ
                        templateManager.SetVariable("Pager", ReleasePager(pageIndex, recordCount, pageCount));
                        release.ReleasePath = Regex.Replace(release.ReleasePath, pattern, pageIndex.ToString(), RegexOptions.IgnoreCase);
                        
                        ReleaseStaticPage(release, templateManager);
                    }
                }
                else
                {
                    if (release.ReleasePath.IndexOf("{ArticleId}") > -1) // ��ϸҳ
                    {
                        release.ReleasePath = release.ReleasePath.Replace("{Year}", article.DateCreated.Year.ToString());
                        release.ReleasePath = release.ReleasePath.Replace("{Month}", article.DateCreated.Month.ToString());
                        release.ReleasePath = release.ReleasePath.Replace("{Day}", article.DateCreated.Day.ToString());
                        release.ReleasePath = release.ReleasePath.Replace("{ArticleId}", article.ArticleId.ToString());
                        templateManager.SetVariable("Article", article);
                    }

                    // TODO:���� ContentHtml ���ȣ����ɶ�ҳ
                    //if (release.ReleasePath.IndexOf("{ArticleId}") > -1) // ��ϸҳ
                    //{
                    //}

                    ReleaseStaticPage(release, templateManager);
                }
            }
        }

        /// <summary>
        /// ������ϸҳ���������ۺ���Ҫ��������ҳ�档
        /// </summary>
        /// <param name="article">����ʵ����</param>
        public void ReleaseArticle(Article article)
        {
            // {RootPath}/{CategoryId}/{Year}-{Month}-{Day}/{ArticleId}.htm
            // 1����ȡ������������� ReleaseCategory����ȡ��ƪ���Ŷ�Ӧ�ķ������ReleaseGuid��
            // 2�����ݷ������ReleaseGuid��ȡ��Ҫ���ɾ�̬ҳ��ģ�壬Articleʵ������Ϊ�������룻
            // 3������ {ArticleId} ��ȡ��ϸҳ��

            // ���ݷ����Ż�ȡ������� 
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
                // װ��ģ��
                release.Template.TemplatePath = release.Template.TemplatePath.Replace("{RootPath}", this.TemplateDirectory);
                string templatePath = System.Web.HttpContext.Current.Server.MapPath(release.Template.TemplatePath);
                Wis.Toolkit.Templates.TemplateManager templateManager = Wis.Toolkit.Templates.TemplateManager.LoadFile(templatePath, Encoding.UTF8);
                templateManager.SetVariable("ApplicationPath", this.ApplicationPath);
                templateManager.SetVariable("TemplateDirectory", this.TemplateDirectory);
                templateManager.SetVariable("ReleaseDirectory", this.ReleaseDirectory);
                templateManager.SetVariable("Article", article);

                // Ԥ������·��
                // "{RootPath}/{CategoryId}/{Month}-{Day}/{ArticleId}.htm"
                release.ReleasePath = release.ReleasePath.Replace("{RootPath}", this.ReleaseDirectory);
                release.ReleasePath = release.ReleasePath.Replace("{CategoryId}", article.Category.CategoryId.ToString());
                release.ReleasePath = release.ReleasePath.Replace("{Year}", article.DateCreated.Year.ToString());
                release.ReleasePath = release.ReleasePath.Replace("{Month}", article.DateCreated.Month.ToString());
                release.ReleasePath = release.ReleasePath.Replace("{Day}", article.DateCreated.Day.ToString());
                release.ReleasePath = release.ReleasePath.Replace("{ArticleId}", article.ArticleId.ToString());

                // TODO:���� ContentHtml ���ȣ����ɶ�ҳ
                //if (release.ReleasePath.IndexOf("{ArticleId}") > -1) // ��ϸҳ
                //{
                //}

                // ������ϸҳ�ľ�̬ҳ
                ReleaseStaticPage(release, templateManager);
            }
        }

        /// <summary>
        /// ������̬ҳ
        /// </summary>
        /// <param name="ripeRelease">Ԥ�������Release����</param>
        /// <param name="templateManager">Ԥ�������ģ�������</param>
        public void ReleaseStaticPage(Release ripeRelease, Wis.Toolkit.Templates.TemplateManager ripeTemplateManager)
        {
            lock (this)
            {
                // ����·��
                ripeRelease.ReleasePath = System.Web.HttpContext.Current.Server.MapPath(ripeRelease.ReleasePath);

                // TODO:ʧ������λع�? ���Ѿ�ɾ������ҳ�ָ�
                if (System.IO.File.Exists(ripeRelease.ReleasePath))
                {
                    System.IO.File.Delete(ripeRelease.ReleasePath);
                    System.Threading.Thread.Sleep(100);
                }

                // ���Ŀ¼�����ڣ��򴴽�Ŀ¼
                System.IO.FileInfo fileInfo = new System.IO.FileInfo(ripeRelease.ReleasePath);
                if (!fileInfo.Directory.Exists) fileInfo.Directory.Create();

                // ���ɷ���
                using (System.IO.StreamWriter sw = new System.IO.StreamWriter(ripeRelease.ReleasePath, false, Encoding.UTF8))
                {
                    sw.Write(ripeTemplateManager.Process());
                }
            }
        }

        /// <summary>
        /// ���ɵ�ҳ������ģ�塣
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
        /// �������š�
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
