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

        //public List<Article> GetArticles()
        //{
        //    List<Article> videoArticles = new List<Article>();
        //    DbCommand command = DbProviderHelper.CreateCommand("SELECTArticles", CommandType.StoredProcedure);
        //    DbDataReader dataReader = DbProviderHelper.ExecuteReader(command);
        //    return GetArticles(dataReader);
        //}


        public static List<Article> GetArticlesByCategoryName(string categoryName, string parentCategoryName, int pageIndex, int pageSize)
        {
            DbProviderHelper.GetConnection();

            if (string.IsNullOrEmpty(categoryName))
                throw new System.ArgumentNullException("categoryName");
            if (string.IsNullOrEmpty(parentCategoryName))
                parentCategoryName = string.Empty;

            // TODO:对 categoryName 进行注入式攻击防范

            List<Article> articles = new List<Article>();
            DbCommand command = DbProviderHelper.CreateCommand("SelectObjects", CommandType.StoredProcedure);
            command.Parameters.Add(DbProviderHelper.CreateParameter("@TableName", DbType.String, "View_Article"));
            command.Parameters.Add(DbProviderHelper.CreateParameter("@ColumnList", DbType.String, "*"));
            command.Parameters.Add(DbProviderHelper.CreateParameter("@SearchCondition", DbType.String, string.Format("CategoryName='{0}' and ParentCategoryName='{1}'", categoryName, parentCategoryName)));
            command.Parameters.Add(DbProviderHelper.CreateParameter("@OrderList", DbType.String, "DateCreated DESC"));
            command.Parameters.Add(DbProviderHelper.CreateParameter("@PageSize", DbType.Int32, pageSize));
            command.Parameters.Add(DbProviderHelper.CreateParameter("@PageIndex", DbType.Int32, pageIndex));
            DbDataReader dataReader = DbProviderHelper.ExecuteReader(command);
            return GetArticles(dataReader);
        }


        public static List<Article> GetArticlesByParentCategoryName(string parentCategoryName, int pageIndex, int pageSize)
        {
            DbProviderHelper.GetConnection();

            if (string.IsNullOrEmpty(parentCategoryName))
                parentCategoryName = string.Empty;

            // TODO:对 categoryName 进行注入式攻击防范

            List<Article> articles = new List<Article>();
            DbCommand command = DbProviderHelper.CreateCommand("SelectObjects", CommandType.StoredProcedure);
            command.Parameters.Add(DbProviderHelper.CreateParameter("@TableName", DbType.String, "View_Article"));
            command.Parameters.Add(DbProviderHelper.CreateParameter("@ColumnList", DbType.String, "*"));
            command.Parameters.Add(DbProviderHelper.CreateParameter("@SearchCondition", DbType.String, string.Format("(CategoryName='{0}' OR ParentCategoryName='{0}')", parentCategoryName)));
            command.Parameters.Add(DbProviderHelper.CreateParameter("@OrderList", DbType.String, "DateCreated DESC"));
            command.Parameters.Add(DbProviderHelper.CreateParameter("@PageSize", DbType.Int32, pageSize));
            command.Parameters.Add(DbProviderHelper.CreateParameter("@PageIndex", DbType.Int32, pageIndex));
            DbDataReader dataReader = DbProviderHelper.ExecuteReader(command);
            return GetArticles(dataReader);
        }


        public static List<Article> GetArticlesByCategoryGuid(Guid categoryGuid, int pageIndex, int pageSize)
        {
            DbProviderHelper.GetConnection();

            List<Article> articles = new List<Article>();
            DbCommand command = DbProviderHelper.CreateCommand("SelectObjects", CommandType.StoredProcedure);
            command.Parameters.Add(DbProviderHelper.CreateParameter("@TableName", DbType.String, "View_Article"));
            command.Parameters.Add(DbProviderHelper.CreateParameter("@ColumnList", DbType.String, "*"));
            if (categoryGuid == null || categoryGuid.Equals(Guid.Empty))
                command.Parameters.Add(DbProviderHelper.CreateParameter("@SearchCondition", DbType.String, "1=1"));
            else
                command.Parameters.Add(DbProviderHelper.CreateParameter("@SearchCondition", DbType.String, string.Format("CategoryGuid='{0}'", categoryGuid)));
            command.Parameters.Add(DbProviderHelper.CreateParameter("@OrderList", DbType.String, "DateCreated DESC"));
            command.Parameters.Add(DbProviderHelper.CreateParameter("@PageSize", DbType.Int32, pageSize));
            command.Parameters.Add(DbProviderHelper.CreateParameter("@PageIndex", DbType.Int32, pageIndex));
            DbDataReader dataReader = DbProviderHelper.ExecuteReader(command);
            return GetArticles(dataReader);
        }


        public static List<Article> GetArticlesByKeywords(string keywords, Guid categoryGuid, int pageIndex, int pageSize)
        {
            if (string.IsNullOrEmpty(keywords))
                return GetArticlesByCategoryGuid(categoryGuid, pageIndex, pageSize);

            keywords = keywords.Replace("\"", "").Replace("'", ""); // 做进一步的注入式攻击防范

            // 获取分类的字典集合
            SortedList<Guid, Category> categoryDictionaries = new SortedList<Guid, Category>();
            CategoryManager categoryManager = new CategoryManager();
            categoryDictionaries = categoryManager.GetCategoryDictionaries();

            DbProviderHelper.GetConnection();

            DbCommand command = DbProviderHelper.CreateCommand("SelectObjects", CommandType.StoredProcedure);
            command.Parameters.Add(DbProviderHelper.CreateParameter("@TableName", DbType.String, "Article"));
            command.Parameters.Add(DbProviderHelper.CreateParameter("@ColumnList", DbType.String, "*"));
            if (categoryGuid == null || categoryGuid.Equals(Guid.Empty))
                command.Parameters.Add(DbProviderHelper.CreateParameter("@SearchCondition", DbType.String, string.Format("CONTAINS(*, N'\" * {0} * \"') ", keywords)));
            else
                command.Parameters.Add(DbProviderHelper.CreateParameter("@SearchCondition", DbType.String, string.Format("CONTAINS(*, N'\" * {0} * \"') and CategoryGuid='{1}'", keywords, categoryGuid)));
            command.Parameters.Add(DbProviderHelper.CreateParameter("@OrderList", DbType.String, "DateCreated DESC"));
            command.Parameters.Add(DbProviderHelper.CreateParameter("@PageSize", DbType.Int32, pageSize));
            command.Parameters.Add(DbProviderHelper.CreateParameter("@PageIndex", DbType.Int32, pageIndex));
            DbDataReader dataReader = DbProviderHelper.ExecuteReader(command);
            List<Article> articles = new List<Article>();
            while (dataReader.Read())
            {
                Article article = new Article();
                article.ArticleId = Convert.ToInt32(dataReader["ArticleId"]);
                article.ArticleGuid = (Guid)dataReader["ArticleGuid"];

                // 获取 Category 对象
                article.Category = categoryDictionaries[(Guid)dataReader["CategoryGuid"]];

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
                article.Hits = Convert.ToInt32(dataReader["Hits"]);
                article.Comments = Convert.ToInt32(dataReader["Comments"]);
                article.Votes = Convert.ToInt32(dataReader["Votes"]);
                article.DateCreated = Convert.ToDateTime(dataReader["DateCreated"]);
                articles.Add(article);
            }
            dataReader.Close();
            return articles;
        }

        public static List<Article> GetArticlesByTag(Guid articleGuid, int size)
        {
            DbProviderHelper.GetConnection();

            DbCommand command = DbProviderHelper.CreateCommand("SelectArticlesByTag", CommandType.StoredProcedure);
            command.Parameters.Add(DbProviderHelper.CreateParameter("@ArticleGuid", DbType.Guid, articleGuid));
            command.Parameters.Add(DbProviderHelper.CreateParameter("@Size", DbType.Int32, size));
            DbDataReader dataReader = DbProviderHelper.ExecuteReader(command);
            return GetArticles(dataReader);
        }

        public List<Article> GetArticlesByReleaseGuid(Guid releaseGuid)
        {
            DbCommand command = DbProviderHelper.CreateCommand("SelectArticlesByReleaseGuid", CommandType.StoredProcedure);
            command.Parameters.Add(DbProviderHelper.CreateParameter("@ReleaseGuid", DbType.Guid, releaseGuid));
            DbDataReader dataReader = DbProviderHelper.ExecuteReader(command);
            return GetArticles(dataReader);
        }

        /// <summary>
        /// 获取内容集合。
        /// </summary>
        /// <param name="dataReader"></param>
        /// <returns></returns>
        public static List<Article> GetArticles(DbDataReader dataReader)
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
                article.Category.RecordCount = (int)dataReader[CategoryField.RecordCount];

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



        public int CountArticlesByCategoryGuid(Guid categoryGuid)
        {
            DbCommand command;
            if (categoryGuid.Equals(Guid.Empty))
                command = DbProviderHelper.CreateCommand("COUNTArticles", CommandType.StoredProcedure);
            else
            {
                command = DbProviderHelper.CreateCommand("CountArticlesByCategoryGuid", CommandType.StoredProcedure);
                command.Parameters.Add(DbProviderHelper.CreateParameter("@CategoryGuid", DbType.Guid, categoryGuid));
            }
            object o = DbProviderHelper.ExecuteScalar(command);
            command.Dispose();
            return (int)o;
        }


        public int CountArticlesByKeywords(string keywords, Guid categoryGuid)
        {
            if (string.IsNullOrEmpty(keywords))
                return CountArticlesByCategoryGuid(categoryGuid);

            DbCommand command;
            command = DbProviderHelper.CreateCommand("CountArticlesByKeywords", CommandType.StoredProcedure);
            command.Parameters.Add(DbProviderHelper.CreateParameter("@Keywords", DbType.String, keywords));
            command.Parameters.Add(DbProviderHelper.CreateParameter("@CategoryGuid", DbType.Guid, categoryGuid)); // Guid.Empty在存储过程中判断了
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
                article.Category.ParentGuid = (Guid)dataReader[CategoryField.ParentGuid];
                article.Category.ParentCategoryName = (string)dataReader[CategoryField.ParentCategoryName];
                article.Category.Rank = Convert.ToInt32(dataReader[CategoryField.Rank]);
                article.Category.RecordCount = (int)dataReader[CategoryField.RecordCount];

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
                article.Hits = Convert.ToInt32(dataReader["Hits"]);
                article.Comments = Convert.ToInt32(dataReader["Comments"]);
                article.Votes = Convert.ToInt32(dataReader["Votes"]);
                article.DateCreated = Convert.ToDateTime(dataReader["DateCreated"]);
            }
            dataReader.Close();
            return article;
        }

        /// <summary>
        /// 根据文章编号获取文章实体类
        /// </summary>
        /// <param name="articleGuid">文章分类</param>
        /// <returns>返回文章实体类</returns>
        public Article GetArticleByArticleGuid(Guid articleGuid)
        {
            Article article = new Article();
            article.ArticleGuid = articleGuid;

            DbCommand command = DbProviderHelper.CreateCommand("SelectArticleByArticleGuid", CommandType.StoredProcedure);
            command.Parameters.Add(DbProviderHelper.CreateParameter("@ArticleGuid", DbType.Guid, articleGuid));
            DbDataReader dataReader = DbProviderHelper.ExecuteReader(command);
            while (dataReader.Read())
            {
                article.ArticleId = Convert.ToInt32(dataReader["ArticleId"]);

                // 获取 Category 对象
                article.Category.CategoryId = Convert.ToInt32(dataReader[CategoryField.CategoryId]);
                article.Category.CategoryGuid = (Guid)dataReader[CategoryField.CategoryGuid];
                article.Category.CategoryName = Convert.ToString(dataReader[CategoryField.CategoryName]);
                article.Category.ParentGuid = (Guid)dataReader[CategoryField.ParentGuid];
                article.Category.ParentCategoryName = (string)dataReader[CategoryField.ParentCategoryName];
                article.Category.Rank = Convert.ToInt32(dataReader[CategoryField.Rank]);
                article.Category.RecordCount = (int)dataReader[CategoryField.RecordCount];

                article.Category.ArticleType = (byte)dataReader[CategoryField.ArticleType];
                if (dataReader[CategoryField.ThumbnailWidth] != DBNull.Value)
                    article.Category.ThumbnailWidth = Convert.ToInt32(dataReader[CategoryField.ThumbnailWidth]);
                if (dataReader[CategoryField.ThumbnailHeight] != DBNull.Value)
                    article.Category.ThumbnailHeight = Convert.ToInt32(dataReader[CategoryField.ThumbnailHeight]);
                
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
            command.Parameters.Add(DbProviderHelper.CreateParameter("@Hits", DbType.Int32, article.Hits));
            command.Parameters.Add(DbProviderHelper.CreateParameter("@Comments", DbType.Int32, article.Comments));
            command.Parameters.Add(DbProviderHelper.CreateParameter("@Votes", DbType.Int32, article.Votes));
            command.Parameters.Add(DbProviderHelper.CreateParameter("@DateCreated", DbType.DateTime, article.DateCreated));

            article.ArticleId = Convert.ToInt32(DbProviderHelper.ExecuteScalar(command));
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

        /// <summary>
        /// 更新浏览数
        /// </summary>
        /// <param name="articleGuid">文章编号</param>
        /// <returns>返回受影响的记录数，1表示成功</returns>
        public int UpdateArticleHits(Guid articleGuid)
        {
            DbCommand command = DbProviderHelper.CreateCommand("UpdateArticleHits", CommandType.StoredProcedure);
            command.Parameters.Add(DbProviderHelper.CreateParameter("@ArticleGuid", DbType.Guid, articleGuid));
            return DbProviderHelper.ExecuteNonQuery(command);
        }

        /// <summary>
        /// 更新评论数
        /// </summary>
        /// <param name="articleGuid">文章编号</param>
        /// <returns>返回受影响的记录数，1表示成功</returns>
        public int UpdateArticleComments(Guid articleGuid)
        {
            DbCommand command = DbProviderHelper.CreateCommand("UpdateArticleComments", CommandType.StoredProcedure);
            command.Parameters.Add(DbProviderHelper.CreateParameter("@ArticleGuid", DbType.Guid, articleGuid));
            return DbProviderHelper.ExecuteNonQuery(command);
        }

        public int Update(Article article)
        {
            DbCommand command = DbProviderHelper.CreateCommand("UPDATEArticle", CommandType.StoredProcedure);
            command.Parameters.Add(DbProviderHelper.CreateParameter("@ArticleGuid", DbType.Guid, article.ArticleGuid));
            command.Parameters.Add(DbProviderHelper.CreateParameter("@CategoryGuid", DbType.Guid, article.Category.CategoryGuid));

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
            command.Parameters.Add(DbProviderHelper.CreateParameter("@Hits", DbType.Int32, article.Hits));
            command.Parameters.Add(DbProviderHelper.CreateParameter("@Comments", DbType.Int32, article.Comments));
            command.Parameters.Add(DbProviderHelper.CreateParameter("@Votes", DbType.Int32, article.Votes));
            command.Parameters.Add(DbProviderHelper.CreateParameter("@DateCreated", DbType.DateTime, article.DateCreated));
            command.Parameters.Add(DbProviderHelper.CreateParameter("@ArticleId", DbType.Int32, article.ArticleId));
            return DbProviderHelper.ExecuteNonQuery(command);
        }

        /// <summary>
        /// 移除文章以及文章的附属信息。
        /// </summary>
        /// <param name="articleGuid">文章编号</param>
        /// <returns>返回受影响的记录数</returns>
        public int Remove(Guid articleGuid)
        {
            DbCommand command = DbProviderHelper.CreateCommand("DeleteArticle", CommandType.StoredProcedure);
            command.Parameters.Add(DbProviderHelper.CreateParameter("@ArticleGuid", DbType.Guid, articleGuid));
            return DbProviderHelper.ExecuteNonQuery(command);
        }
    }
}
