//------------------------------------------------------------------------------
// <copyright file="ArticleManager.cs" company="Everwis">
//     Copyright (C) Everwis Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;

namespace Wis.Website.DataManager
{
    public class ArticleManager
    {
        public ArticleManager()
        {
            DbProviderHelper.GetConnection();
        }

        public List<Article> GetArticles()
        {
            List<Article> articles = new List<Article>();
            DbCommand command = DbProviderHelper.CreateCommand("SELECTArticles", CommandType.StoredProcedure);
            DbDataReader dataReader = DbProviderHelper.ExecuteReader(command);
            return GetArticles(dataReader);
        }


        public List<Article> GetArticlesByCategoryId(int categoryId)
        {
            List<Article> articles = new List<Article>();
            DbCommand command = DbProviderHelper.CreateCommand("SelectArticlesByCategoryId", CommandType.StoredProcedure);
            command.Parameters.Add(DbProviderHelper.CreateParameter("@CategoryId", DbType.Int32, categoryId));
            DbDataReader dataReader = DbProviderHelper.ExecuteReader(command);
            return GetArticles(dataReader);
        }


        /// <summary>
        /// 获取内容集合。
        /// </summary>
        /// <param name="dataReader"></param>
        /// <returns></returns>
        public List<Article> GetArticles(DbDataReader dataReader)
        {
            // 集合类
            List<Article> articles = new List<Article>();
            while (dataReader.Read())
            {
                Article article = new Article();
                article.ArticleId = Convert.ToInt32(dataReader["ArticleId"]);
                article.ArticleGuid = (Guid)dataReader["ArticleGuid"];

                // 获取 Category 对象
                article.Category.CategoryId = Convert.ToInt32(dataReader["CategoryId"]);
                article.Category.CategoryGuid = (Guid)dataReader["CategoryGuid"];
                article.Category.CategoryName = Convert.ToString(dataReader["CategoryName"]);
                article.Category.ParentGuid = (Guid)dataReader["ParentGuid"];
                article.Category.Rank = Convert.ToInt32(dataReader["Rank"]);

                if (dataReader["TemplatePath"] != DBNull.Value)
                    article.Category.TemplatePath = Convert.ToString(dataReader["TemplatePath"]);

                if (dataReader["ReleasePath"] != DBNull.Value)
                    article.Category.ReleasePath = Convert.ToString(dataReader["ReleasePath"]);

                article.ArticleType = (ArticleType)System.Enum.Parse(typeof(ArticleType), dataReader["ArticleType"].ToString(), true);

                if (dataReader["ImagePath"] != DBNull.Value)
                    article.ImagePath = Convert.ToString(dataReader["ImagePath"]);

                if (dataReader["ImageWidth"] != DBNull.Value)
                    article.ImageWidth = Convert.ToInt32(dataReader["ImageWidth"]);

                if (dataReader["ImageHeight"] != DBNull.Value)
                    article.ImageHeight = Convert.ToInt32(dataReader["ImageHeight"]);

                if (dataReader["MetaKeywords"] != DBNull.Value)
                    article.MetaKeywords = Convert.ToString(dataReader["MetaKeywords"]);

                if (dataReader["MetaDesc"] != DBNull.Value)
                    article.MetaDesc = Convert.ToString(dataReader["MetaDesc"]);

                article.Title = Convert.ToString(dataReader["Title"]);

                if (dataReader["TitleColor"] != DBNull.Value)
                    article.TitleColor = Convert.ToString(dataReader["TitleColor"]);

                if (dataReader["SubTitle"] != DBNull.Value)
                    article.SubTitle = Convert.ToString(dataReader["SubTitle"]);

                if (dataReader["Summary"] != DBNull.Value)
                    article.Summary = Convert.ToString(dataReader["Summary"]);

                if (dataReader["ContentHtml"] != DBNull.Value)
                    article.ContentHtml = Convert.ToString(dataReader["ContentHtml"]);

                if (dataReader["Editor"] != DBNull.Value)
                    article.Editor = (Guid)dataReader["Editor"];

                if (dataReader["Author"] != DBNull.Value)
                    article.Author = Convert.ToString(dataReader["Author"]);

                if (dataReader["Original"] != DBNull.Value)
                    article.Original = Convert.ToString(dataReader["Original"]);

                article.Rank = Convert.ToInt32(dataReader["Rank"]);

                if (dataReader["SpecialGuid"] != DBNull.Value)
                    article.SpecialGuid = (Guid)dataReader["SpecialGuid"];

                article.TemplatePath = Convert.ToString(dataReader["TemplatePath"]);
                article.ReleasePath = Convert.ToString(dataReader["ReleasePath"]);
                article.Hits = Convert.ToInt32(dataReader["Hits"]);
                article.Comments = Convert.ToInt32(dataReader["Comments"]);
                article.Votes = Convert.ToInt32(dataReader["Votes"]);
                article.DateCreated = Convert.ToDateTime(dataReader["DateCreated"]);
                articles.Add(article);
            }
            dataReader.Close();
            return articles;
        }


        public int CountArticlesByTitle(string title)
        {
            DbCommand command = DbProviderHelper.CreateCommand("CountArticlesByTitle", CommandType.StoredProcedure);
            command.Parameters.Add(DbProviderHelper.CreateParameter("@Title", DbType.String, title));
            object o = DbProviderHelper.ExecuteScalar(command);
            command.Dispose();
            return (int)o;
        }


        public Article GetArticle(int ArticleId)
        {
            Article article = new Article();
            DbCommand command = DbProviderHelper.CreateCommand("SELECTArticle", CommandType.StoredProcedure);
            command.Parameters.Add(DbProviderHelper.CreateParameter("@ArticleId", DbType.Int32, ArticleId));
            DbDataReader dataReader = DbProviderHelper.ExecuteReader(command);
            while (dataReader.Read())
            {
                article.ArticleId = Convert.ToInt32(dataReader["ArticleId"]);
                article.ArticleGuid = (Guid)dataReader["ArticleGuid"];

                // 获取 Category 对象
                article.Category.CategoryId = Convert.ToInt32(dataReader[CategoryField.CategoryId]);
                article.Category.CategoryGuid = (Guid)dataReader[CategoryField.CategoryGuid];
                article.Category.CategoryName = Convert.ToString(dataReader[CategoryField.CategoryName]);
                article.Category.ParentGuid = (Guid) dataReader[CategoryField.ParentGuid];
                article.Category.Rank = Convert.ToInt32(dataReader[CategoryField.Rank]);
                if (dataReader["TemplatePath"] != DBNull.Value)
                    article.Category.TemplatePath = Convert.ToString(dataReader[CategoryField.TemplatePath]);
                if (dataReader["ReleasePath"] != DBNull.Value)
                    article.Category.ReleasePath = Convert.ToString(dataReader[CategoryField.ReleasePath]);

                article.ArticleType = (ArticleType)System.Enum.Parse(typeof(ArticleType), dataReader["ArticleType"].ToString(), true);

                if (dataReader["ImagePath"] != DBNull.Value)
                    article.ImagePath = Convert.ToString(dataReader["ImagePath"]);

                if (dataReader["ImageWidth"] != DBNull.Value)
                    article.ImageWidth = Convert.ToInt32(dataReader["ImageWidth"]);

                if (dataReader["ImageHeight"] != DBNull.Value)
                    article.ImageHeight = Convert.ToInt32(dataReader["ImageHeight"]);

                if (dataReader["MetaKeywords"] != DBNull.Value)
                    article.MetaKeywords = Convert.ToString(dataReader["MetaKeywords"]);

                if (dataReader["MetaDesc"] != DBNull.Value)
                    article.MetaDesc = Convert.ToString(dataReader["MetaDesc"]);
                article.Title = Convert.ToString(dataReader["Title"]);

                if (dataReader["TitleColor"] != DBNull.Value)
                    article.TitleColor = Convert.ToString(dataReader["TitleColor"]);

                if (dataReader["SubTitle"] != DBNull.Value)
                    article.SubTitle = Convert.ToString(dataReader["SubTitle"]);

                if (dataReader["Summary"] != DBNull.Value)
                    article.Summary = Convert.ToString(dataReader["Summary"]);

                if (dataReader["ContentHtml"] != DBNull.Value)
                    article.ContentHtml = Convert.ToString(dataReader["ContentHtml"]);

                if (dataReader["Editor"] != DBNull.Value)
                    article.Editor = (Guid)dataReader["Editor"];

                if (dataReader["Author"] != DBNull.Value)
                    article.Author = Convert.ToString(dataReader["Author"]);

                if (dataReader["Original"] != DBNull.Value)
                    article.Original = Convert.ToString(dataReader["Original"]);
                article.Rank = Convert.ToInt32(dataReader["Rank"]);

                if (dataReader["SpecialGuid"] != DBNull.Value)
                    article.SpecialGuid = (Guid)dataReader["SpecialGuid"];
                article.TemplatePath = Convert.ToString(dataReader["TemplatePath"]);
                article.ReleasePath = Convert.ToString(dataReader["ReleasePath"]);
                article.Hits = Convert.ToInt32(dataReader["Hits"]);
                article.Comments = Convert.ToInt32(dataReader["Comments"]);
                article.Votes = Convert.ToInt32(dataReader["Votes"]);
                article.DateCreated = Convert.ToDateTime(dataReader["DateCreated"]);
            }
            dataReader.Close();
            return article;
        }

        public int AddNew(Article article)
        {
            // 写入数据库
            DbCommand command = DbProviderHelper.CreateCommand("INSERTArticle", CommandType.StoredProcedure);
            command.Parameters.Add(DbProviderHelper.CreateParameter("@ArticleGuid", DbType.Guid, article.ArticleGuid));
            command.Parameters.Add(DbProviderHelper.CreateParameter("@CategoryGuid", DbType.Guid, article.Category.CategoryGuid));
            command.Parameters.Add(DbProviderHelper.CreateParameter("@ArticleType", DbType.Int16, article.ArticleType));
            
            if (string.IsNullOrEmpty(article.ImagePath))
                command.Parameters.Add(DbProviderHelper.CreateParameter("@ImagePath", DbType.String, DBNull.Value));
            else
                command.Parameters.Add(DbProviderHelper.CreateParameter("@ImagePath", DbType.String, article.ImagePath));

            if (article.ImageWidth.HasValue)
                command.Parameters.Add(DbProviderHelper.CreateParameter("@ImageWidth", DbType.Int32, article.ImageWidth));
            else
                command.Parameters.Add(DbProviderHelper.CreateParameter("@ImageWidth", DbType.Int32, DBNull.Value));
            
            if (article.ImageHeight.HasValue)
                command.Parameters.Add(DbProviderHelper.CreateParameter("@ImageHeight", DbType.Int32, article.ImageHeight));
            else
                command.Parameters.Add(DbProviderHelper.CreateParameter("@ImageHeight", DbType.Int32, DBNull.Value));
            
            if (string.IsNullOrEmpty(article.MetaKeywords))
                command.Parameters.Add(DbProviderHelper.CreateParameter("@MetaKeywords", DbType.String, DBNull.Value));
            else
                command.Parameters.Add(DbProviderHelper.CreateParameter("@MetaKeywords", DbType.String, article.MetaKeywords));
            
            if (string.IsNullOrEmpty(article.MetaDesc))
                command.Parameters.Add(DbProviderHelper.CreateParameter("@MetaDesc", DbType.String, DBNull.Value));
            else
                command.Parameters.Add(DbProviderHelper.CreateParameter("@MetaDesc", DbType.String, article.MetaDesc));
           
            command.Parameters.Add(DbProviderHelper.CreateParameter("@Title", DbType.String, article.Title));
            
            if (string.IsNullOrEmpty(article.TitleColor))
                command.Parameters.Add(DbProviderHelper.CreateParameter("@TitleColor", DbType.String, DBNull.Value));
            else
                command.Parameters.Add(DbProviderHelper.CreateParameter("@TitleColor", DbType.String, article.TitleColor));
            
            if (string.IsNullOrEmpty(article.SubTitle))
                command.Parameters.Add(DbProviderHelper.CreateParameter("@SubTitle", DbType.String, DBNull.Value));
            else
                command.Parameters.Add(DbProviderHelper.CreateParameter("@SubTitle", DbType.String, article.SubTitle));
            
            if (string.IsNullOrEmpty(article.Summary))
                command.Parameters.Add(DbProviderHelper.CreateParameter("@Summary", DbType.String, DBNull.Value));
            else
                command.Parameters.Add(DbProviderHelper.CreateParameter("@Summary", DbType.String, article.Summary));
            
            if (string.IsNullOrEmpty(article.ContentHtml))
                command.Parameters.Add(DbProviderHelper.CreateParameter("@ContentHtml", DbType.String, DBNull.Value));
            else
                command.Parameters.Add(DbProviderHelper.CreateParameter("@ContentHtml", DbType.String, article.ContentHtml));
           
            if (article.Editor.HasValue && !article.Editor.Equals(Guid.Empty))
                command.Parameters.Add(DbProviderHelper.CreateParameter("@Editor", DbType.Guid, article.Editor));
            else
                command.Parameters.Add(DbProviderHelper.CreateParameter("@Editor", DbType.Guid, DBNull.Value));
            
            if (string.IsNullOrEmpty(article.Author))
                command.Parameters.Add(DbProviderHelper.CreateParameter("@Author", DbType.String, DBNull.Value));
            else
                command.Parameters.Add(DbProviderHelper.CreateParameter("@Author", DbType.String, article.Author));

            if (string.IsNullOrEmpty(article.Original))
                command.Parameters.Add(DbProviderHelper.CreateParameter("@Original", DbType.String, DBNull.Value));
            else
                command.Parameters.Add(DbProviderHelper.CreateParameter("@Original", DbType.String, article.Original));

            command.Parameters.Add(DbProviderHelper.CreateParameter("@Rank", DbType.Int32, article.Rank));

            if (article.SpecialGuid.HasValue && !article.SpecialGuid.Equals(Guid.Empty))
                command.Parameters.Add(DbProviderHelper.CreateParameter("@SpecialGuid", DbType.Guid, article.SpecialGuid));
            else
                command.Parameters.Add(DbProviderHelper.CreateParameter("@SpecialGuid", DbType.Guid, DBNull.Value));

            command.Parameters.Add(DbProviderHelper.CreateParameter("@TemplatePath", DbType.String, article.TemplatePath));
            command.Parameters.Add(DbProviderHelper.CreateParameter("@ReleasePath", DbType.String, article.ReleasePath));
            command.Parameters.Add(DbProviderHelper.CreateParameter("@Hits", DbType.Int32, article.Hits));
            command.Parameters.Add(DbProviderHelper.CreateParameter("@Comments", DbType.Int32, article.Comments));
            command.Parameters.Add(DbProviderHelper.CreateParameter("@Votes", DbType.Int32, article.Votes));
            command.Parameters.Add(DbProviderHelper.CreateParameter("@DateCreated", DbType.DateTime, article.DateCreated));

            article.ArticleId = Convert.ToInt32(DbProviderHelper.ExecuteScalar(command));

            // 装载模版
            string templatePath = System.Web.HttpContext.Current.Server.MapPath(article.TemplatePath);
            Wis.Toolkit.Templates.TemplateManager templateManager = Wis.Toolkit.Templates.TemplateManager.LoadFile(templatePath, Encoding.UTF8);

            // 内容实体类
            templateManager.SetVariable("Article", article);

            // 模板路径
            string templateDirectory = System.Configuration.ConfigurationManager.AppSettings["TemplateDirectory"];
            if (!templateDirectory.EndsWith("/")) templateDirectory += "/";
            string applicationPath = System.Web.HttpContext.Current.Request.ApplicationPath;
            if (!applicationPath.EndsWith("/")) applicationPath += "/";
            templateDirectory = applicationPath + templateDirectory;
            templateManager.SetVariable("TemplateDirectory", templateDirectory);

            // 读取内容对应的评论
            CommentManager commentManager = new CommentManager();
            List<Comment> comments = commentManager.GetCommentsBySubmissionGuid(article.ArticleGuid);
            templateManager.SetVariable("Comments", comments);

            // 生成静态页
            string releasePath = System.Web.HttpContext.Current.Server.MapPath(article.ReleasePath);
            if (!System.IO.Directory.Exists(releasePath))
            {
                System.IO.Directory.CreateDirectory(releasePath);
            }
            releasePath = releasePath + "\\" + article.ArticleId.ToString() + ".htm";
            using (System.IO.StreamWriter sw = new System.IO.StreamWriter(releasePath, false, Encoding.UTF8))
            {
                sw.Write(templateManager.Process());
            }

            // 生成分类页

            // 生成相关页

            return article.ArticleId;
        }


        /// <summary>
        /// 更新内容的发布路径。
        /// </summary>
        /// <param name="ArticleId">内容编号。</param>
        /// <param name="ReleasePath">发布路径。</param>
        /// <returns>返回受影响的记录数，1表示成功。</returns>
        public int UpdateArticleReleasePath(int ArticleId, string ReleasePath)
        {
            DbCommand command = DbProviderHelper.CreateCommand("UpdateArticleReleasePath", CommandType.StoredProcedure);
            command.Parameters.Add(DbProviderHelper.CreateParameter("@ArticleId", DbType.Guid, ArticleId));
            command.Parameters.Add(DbProviderHelper.CreateParameter("@ReleasePath", DbType.Guid, ReleasePath));
            return DbProviderHelper.ExecuteNonQuery(command);
        }


        public int Update(Article article)
        {
            DbCommand command = DbProviderHelper.CreateCommand("UPDATEArticle", CommandType.StoredProcedure);
            command.Parameters.Add(DbProviderHelper.CreateParameter("@ArticleGuid", DbType.Guid, article.ArticleGuid));
            command.Parameters.Add(DbProviderHelper.CreateParameter("@CategoryGuid", DbType.Guid, article.Category.CategoryGuid));
            command.Parameters.Add(DbProviderHelper.CreateParameter("@ArticleType", DbType.Int16, (Int16)article.ArticleType));
            if (article.ImagePath != null)
                command.Parameters.Add(DbProviderHelper.CreateParameter("@ImagePath", DbType.String, article.ImagePath));
            else
                command.Parameters.Add(DbProviderHelper.CreateParameter("@ImagePath", DbType.String, DBNull.Value));
            if (article.ImageWidth.HasValue)
                command.Parameters.Add(DbProviderHelper.CreateParameter("@ImageWidth", DbType.Int32, article.ImageWidth));
            else
                command.Parameters.Add(DbProviderHelper.CreateParameter("@ImageWidth", DbType.Int32, DBNull.Value));
            if (article.ImageHeight.HasValue)
                command.Parameters.Add(DbProviderHelper.CreateParameter("@ImageHeight", DbType.Int32, article.ImageHeight));
            else
                command.Parameters.Add(DbProviderHelper.CreateParameter("@ImageHeight", DbType.Int32, DBNull.Value));
            if (article.MetaKeywords != null)
                command.Parameters.Add(DbProviderHelper.CreateParameter("@MetaKeywords", DbType.String, article.MetaKeywords));
            else
                command.Parameters.Add(DbProviderHelper.CreateParameter("@MetaKeywords", DbType.String, DBNull.Value));
            if (article.MetaDesc != null)
                command.Parameters.Add(DbProviderHelper.CreateParameter("@MetaDesc", DbType.String, article.MetaDesc));
            else
                command.Parameters.Add(DbProviderHelper.CreateParameter("@MetaDesc", DbType.String, DBNull.Value));
            command.Parameters.Add(DbProviderHelper.CreateParameter("@Title", DbType.String, article.Title));
            if (article.TitleColor != null)
                command.Parameters.Add(DbProviderHelper.CreateParameter("@TitleColor", DbType.String, article.TitleColor));
            else
                command.Parameters.Add(DbProviderHelper.CreateParameter("@TitleColor", DbType.String, DBNull.Value));
            if (article.SubTitle != null)
                command.Parameters.Add(DbProviderHelper.CreateParameter("@SubTitle", DbType.String, article.SubTitle));
            else
                command.Parameters.Add(DbProviderHelper.CreateParameter("@SubTitle", DbType.String, DBNull.Value));
            if (article.Summary != null)
                command.Parameters.Add(DbProviderHelper.CreateParameter("@Summary", DbType.String, article.Summary));
            else
                command.Parameters.Add(DbProviderHelper.CreateParameter("@Summary", DbType.String, DBNull.Value));
            if (article.ContentHtml != null)
                command.Parameters.Add(DbProviderHelper.CreateParameter("@ContentHtml", DbType.String, article.ContentHtml));
            else
                command.Parameters.Add(DbProviderHelper.CreateParameter("@ContentHtml", DbType.String, DBNull.Value));
            if (article.Editor.HasValue)
                command.Parameters.Add(DbProviderHelper.CreateParameter("@Editor", DbType.Guid, article.Editor));
            else
                command.Parameters.Add(DbProviderHelper.CreateParameter("@Editor", DbType.Guid, DBNull.Value));
            if (article.Author != null)
                command.Parameters.Add(DbProviderHelper.CreateParameter("@Author", DbType.String, article.Author));
            else
                command.Parameters.Add(DbProviderHelper.CreateParameter("@Author", DbType.String, DBNull.Value));
            if (article.Original != null)
                command.Parameters.Add(DbProviderHelper.CreateParameter("@Original", DbType.String, article.Original));
            else
                command.Parameters.Add(DbProviderHelper.CreateParameter("@Original", DbType.String, DBNull.Value));
            command.Parameters.Add(DbProviderHelper.CreateParameter("@Rank", DbType.Int32, article.Rank));
            if (article.SpecialGuid.HasValue)
                command.Parameters.Add(DbProviderHelper.CreateParameter("@SpecialGuid", DbType.Guid, article.SpecialGuid));
            else
                command.Parameters.Add(DbProviderHelper.CreateParameter("@SpecialGuid", DbType.Guid, DBNull.Value));
            command.Parameters.Add(DbProviderHelper.CreateParameter("@TemplatePath", DbType.String, article.TemplatePath));
            command.Parameters.Add(DbProviderHelper.CreateParameter("@ReleasePath", DbType.String, article.ReleasePath));
            command.Parameters.Add(DbProviderHelper.CreateParameter("@Hits", DbType.Int32, article.Hits));
            command.Parameters.Add(DbProviderHelper.CreateParameter("@Comments", DbType.Int32, article.Comments));
            command.Parameters.Add(DbProviderHelper.CreateParameter("@Votes", DbType.Int32, article.Votes));
            command.Parameters.Add(DbProviderHelper.CreateParameter("@DateCreated", DbType.DateTime, article.DateCreated));
            command.Parameters.Add(DbProviderHelper.CreateParameter("@ArticleId", DbType.Int32, article.ArticleId));
            return DbProviderHelper.ExecuteNonQuery(command);
        }

        public int Remove(int ArticleId)
        {
            DbCommand oDbCommand = DbProviderHelper.CreateCommand("DELETEArticle", CommandType.StoredProcedure);
            oDbCommand.Parameters.Add(DbProviderHelper.CreateParameter("@ArticleId", DbType.Int32, ArticleId));
            return DbProviderHelper.ExecuteNonQuery(oDbCommand);
        }
    }
}
