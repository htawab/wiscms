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
                sb.Append("��ҳ");
                sb.Append("��һҳ");
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
                sb.Append("��һҳ");
                sb.Append("βҳ");
            }
            else
            {
                sb.Append(string.Format("<a href='{0}.htm'>��һҳ</a>", nextPage));
                sb.Append(string.Format("<a href='{0}.htm'>βҳ</a>", pageCount));
            }

            sb.Append("</div>");
            return sb.ToString();
        }

        public void Release(Article article)
        {
            // 1����ȡ������������� ReleaseCategory����ȡ��ƪ���Ŷ�Ӧ�ķ������ReleaseGuid��
            // 2�����ݷ������ReleaseGuid��ȡ��Ҫ���ɾ�̬ҳ��ģ�壬Articleʵ������Ϊ�������룻
            // 3������ҳ���б�ҳ����ϸҳ��ר��ҳ������ɣ�
            // 4��Release ��ӳ��ǰ̨��վ��վ��ṹ�����Ը���Release������վ���ͼGoogle Sitemap��Baidu Sitemap
            // �������ʵ���ˣ�������ģ��û����ϣ�һƪ���µ���ϸҳ�����ö���ģ�����ɣ�ͬʱ������Ӱ��Ĺ���ҳ�档

            // ���ݷ����Ż�ȡ������� 
            List<Release> releases = new List<Release>();
            DbCommand command = DbProviderHelper.CreateCommand("SelectReleasesByCategoryGuid", CommandType.StoredProcedure);
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

            // ģ��·��
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
                // װ��ģ��
                release.TemplatePath = release.TemplatePath.Replace("{RootPath}", templateDirectory);
                string templatePath = System.Web.HttpContext.Current.Server.MapPath(release.TemplatePath);
                Wis.Toolkit.Templates.TemplateManager templateManager = Wis.Toolkit.Templates.TemplateManager.LoadFile(templatePath, Encoding.UTF8);
                templateManager.SetVariable("TemplateDirectory", templateDirectory);

                // ��ȡ���ݶ�Ӧ������ TODO:ǩ��ģ���е���
                //CommentManager commentManager = new CommentManager();
                //List<Comment> comments = commentManager.GetCommentsBySubmissionGuid(article.ArticleGuid);
                //templateManager.SetVariable("Comments", comments);

                // ���ɾ�̬ҳ��֧������ҳ������ҳ���б�ҳ����ϸҳ��ר��ҳ
                // ��ҳ��  "{RootPath}/default.htm"
                // ��ҳ��  "{RootPath}/{PageId}.htm" ���{PageId}����֪��
                // �б�ҳ��"{RootPath}/{CategoryId}/{PageIndex}.htm"
                // ��ϸҳ��"{RootPath}/{CategoryId}/{Month}-{Day}/{ArticleId}.htm"
                // ר��ҳ��"{RootPath}/{SpecialId}.htm" ���{SpecialId}����֪��
                release.ReleasePath = release.ReleasePath.Replace("{RootPath}", releaseDirectory);
                release.ReleasePath = release.ReleasePath.Replace("{CategoryId}", article.Category.CategoryId.ToString());
                release.ReleasePath = release.ReleasePath.Replace("{Month}", article.DateCreated.Month.ToString());
                release.ReleasePath = release.ReleasePath.Replace("{Day}", article.DateCreated.Day.ToString());
                release.ReleasePath = release.ReleasePath.Replace("{ArticleId}", article.ArticleId.ToString());
                
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

                    // ��ҳ���� PageCount
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
                        string releasePath = Regex.Replace(release.ReleasePath, pattern, pageIndex.ToString(), RegexOptions.IgnoreCase);
                        releasePath = System.Web.HttpContext.Current.Server.MapPath(releasePath);
                        lock (this)
                        {
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
                                sw.Write(templateManager.Process()); // ģ�������ģ��ı�ǩ������
                            }
                        }
                    }
                }
                else
                {
                    if (release.ReleasePath.IndexOf("{ArticleId}") > -1) // ��ϸҳ
                        templateManager.SetVariable("Article", article);

                    release.ReleasePath = System.Web.HttpContext.Current.Server.MapPath(release.ReleasePath);

                    // TODO:���� ContentHtml ���ȣ����ɶ�ҳ
                    //if (release.ReleasePath.IndexOf("{ArticleId}") > -1) // ��ϸҳ
                    //{
                    //}

                    lock (this)
                    {
                        // TODO:ʧ������λع�? ���Ѿ�ɾ������ҳ�ָ�
                        if (System.IO.File.Exists(article.ReleasePath))
                        {
                            System.IO.File.Delete(article.ReleasePath);
                            System.Threading.Thread.Sleep(100);
                        }

                        // ���Ŀ¼�����ڣ��򴴽�Ŀ¼
                        System.IO.FileInfo fileInfo = new System.IO.FileInfo(release.ReleasePath);
                        if (!fileInfo.Directory.Exists) fileInfo.Directory.Create();

                        // ���ɷ���
                        using (System.IO.StreamWriter sw = new System.IO.StreamWriter(release.ReleasePath, false, Encoding.UTF8))
                        {
                            sw.Write(templateManager.Process()); // ģ�������ģ��ı�ǩ������
                        }
                    }
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
        /// �������š�
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
