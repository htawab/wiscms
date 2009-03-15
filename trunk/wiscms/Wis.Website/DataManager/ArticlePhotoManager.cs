//------------------------------------------------------------------------------
// <copyright file="ArticlePhotoManager.cs" company="Everwis">
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
    /// <summary>
    /// 图片新闻的操作类
    /// </summary>
    public class ArticlePhotoManager
    {
        public ArticlePhotoManager()
        {
            DbProviderHelper.GetConnection();
        }

        public static List<ArticlePhoto> GetPhotoArticles(string categoryName, string parentCategoryName, int pageIndex, int pageSize)
        {
            DbProviderHelper.GetConnection();

            if (string.IsNullOrEmpty(categoryName))
                throw new System.ArgumentNullException("categoryName");
            if (string.IsNullOrEmpty(parentCategoryName))
                parentCategoryName = string.Empty;

            // TODO:对 categoryName 进行注入式攻击防范
            DbCommand command = DbProviderHelper.CreateCommand("SelectObjects", CommandType.StoredProcedure);
            command.Parameters.Add(DbProviderHelper.CreateParameter("@TableName", DbType.String, "View_PhotoArticle"));
            command.Parameters.Add(DbProviderHelper.CreateParameter("@ColumnList", DbType.String, "*"));
            command.Parameters.Add(DbProviderHelper.CreateParameter("@SearchCondition", DbType.String, string.Format("CategoryName='{0}' and ParentCategoryName='{1}'", categoryName, parentCategoryName)));
            command.Parameters.Add(DbProviderHelper.CreateParameter("@OrderList", DbType.String, "DateCreated DESC"));
            command.Parameters.Add(DbProviderHelper.CreateParameter("@PageSize", DbType.Int32, pageSize));
            command.Parameters.Add(DbProviderHelper.CreateParameter("@PageIndex", DbType.Int32, pageIndex));
            DbDataReader dataReader = DbProviderHelper.ExecuteReader(command);
            return GetArticlePhotos(dataReader);
        }


        public static List<ArticlePhoto> GetPhotoArticles(Guid categoryGuid, int pageIndex, int pageSize)
        {
            DbProviderHelper.GetConnection();

            if (categoryGuid.Equals(Guid.Empty))
                throw new System.ArgumentNullException("categoryGuid");

            // TODO:对 categoryName 进行注入式攻击防范
            DbCommand command = DbProviderHelper.CreateCommand("SelectObjects", CommandType.StoredProcedure);
            command.Parameters.Add(DbProviderHelper.CreateParameter("@TableName", DbType.String, "View_PhotoArticle"));
            command.Parameters.Add(DbProviderHelper.CreateParameter("@ColumnList", DbType.String, "*"));
            command.Parameters.Add(DbProviderHelper.CreateParameter("@SearchCondition", DbType.String, string.Format("CategoryGuid='{0}'", categoryGuid)));
            command.Parameters.Add(DbProviderHelper.CreateParameter("@OrderList", DbType.String, "DateCreated DESC"));
            command.Parameters.Add(DbProviderHelper.CreateParameter("@PageSize", DbType.Int32, pageSize));
            command.Parameters.Add(DbProviderHelper.CreateParameter("@PageIndex", DbType.Int32, pageIndex));
            DbDataReader dataReader = DbProviderHelper.ExecuteReader(command);
            return GetArticlePhotos(dataReader);
        }


        public List<ArticlePhoto> GetPhotoArticlesByReleaseGuid(Guid releaseGuid)
        {
            DbCommand command = DbProviderHelper.CreateCommand("SelectPhotoArticlesByReleaseGuid", CommandType.StoredProcedure);
            command.Parameters.Add(DbProviderHelper.CreateParameter("@ReleaseGuid", DbType.Guid, releaseGuid));
            DbDataReader dataReader = DbProviderHelper.ExecuteReader(command);
            return GetArticlePhotos(dataReader);
        }


        /// <summary>
        /// 获取内容集合。
        /// </summary>
        /// <param name="dataReader"></param>
        /// <returns></returns>
        public static List<ArticlePhoto> GetArticlePhotos(DbDataReader dataReader)
        {
            // 集合类
            List<ArticlePhoto> articles = new List<ArticlePhoto>();
            while (dataReader.Read())
            {
                ArticlePhoto articlePhoto = new ArticlePhoto();

                articlePhoto.ArticleId = (int)dataReader[ArticleField.ArticleId];
                articlePhoto.ArticleGuid = (Guid)dataReader[ArticleField.ArticleGuid];

                // 获取 Category 对象
                articlePhoto.Category.CategoryId = Convert.ToInt32(dataReader[CategoryField.CategoryId]);
                articlePhoto.Category.CategoryGuid = (Guid)dataReader[CategoryField.CategoryGuid];
                articlePhoto.Category.CategoryName = Convert.ToString(dataReader[CategoryField.CategoryName]);
                articlePhoto.Category.ParentGuid = (Guid)dataReader[CategoryField.ParentGuid];
                articlePhoto.Category.Rank = Convert.ToInt32(dataReader[CategoryField.Rank]);

                if (dataReader[ArticleField.MetaKeywords] != DBNull.Value)
                    articlePhoto.MetaKeywords = Convert.ToString(dataReader[ArticleField.MetaKeywords]);

                if (dataReader[ArticleField.MetaDesc] != DBNull.Value)
                    articlePhoto.MetaDesc = Convert.ToString(dataReader[ArticleField.MetaDesc]);

                articlePhoto.Title = Convert.ToString(dataReader[ArticleField.Title]);

                if (dataReader[ArticleField.TitleColor] != DBNull.Value)
                    articlePhoto.TitleColor = Convert.ToString(dataReader[ArticleField.TitleColor]);

                if (dataReader[ArticleField.SubTitle] != DBNull.Value)
                    articlePhoto.SubTitle = Convert.ToString(dataReader[ArticleField.SubTitle]);

                if (dataReader[ArticleField.Summary] != DBNull.Value)
                    articlePhoto.Summary = Convert.ToString(dataReader[ArticleField.Summary]);

                if (dataReader[ArticleField.ContentHtml] != DBNull.Value)
                    articlePhoto.ContentHtml = Convert.ToString(dataReader[ArticleField.ContentHtml]);

                if (dataReader[ArticleField.Editor] != DBNull.Value)
                    articlePhoto.Editor = (Guid)dataReader[ArticleField.Editor];

                if (dataReader[ArticleField.Author] != DBNull.Value)
                    articlePhoto.Author = Convert.ToString(dataReader[ArticleField.Author]);

                if (dataReader[ArticleField.Original] != DBNull.Value)
                    articlePhoto.Original = Convert.ToString(dataReader[ArticleField.Original]);

                articlePhoto.Rank = Convert.ToInt32(dataReader[ArticleField.Rank]);
                articlePhoto.Hits = Convert.ToInt32(dataReader[ArticleField.Hits]);
                articlePhoto.Comments = Convert.ToInt32(dataReader[ArticleField.Comments]);
                articlePhoto.Votes = Convert.ToInt32(dataReader[ArticleField.Votes]);
                articlePhoto.DateCreated = Convert.ToDateTime(dataReader[ArticleField.DateCreated]);

                // 图片信息
                articlePhoto.ArticlePhotoId = Convert.ToInt32(dataReader[ArticlePhotoField.ArticlePhotoId]);
                articlePhoto.ArticlePhotoGuid = (Guid)dataReader[ArticlePhotoField.ArticlePhotoGuid];
                //articlePhoto.ArticleGuid = (Guid)dataReader[ArticlePhotoField.ArticleGuid];
                articlePhoto.SourcePath = Convert.ToString(dataReader[ArticlePhotoField.SourcePath]);
                articlePhoto.ThumbnailPath = Convert.ToString(dataReader[ArticlePhotoField.ThumbnailPath]);

                if (dataReader[ArticlePhotoField.PointX] != DBNull.Value)
                    articlePhoto.PointX = Convert.ToInt32(dataReader[ArticlePhotoField.PointX]);

                if (dataReader[ArticlePhotoField.PointY] != DBNull.Value)
                    articlePhoto.PointY = Convert.ToInt32(dataReader[ArticlePhotoField.PointY]);

                if (dataReader[ArticlePhotoField.Stretch] != DBNull.Value)
                    articlePhoto.Stretch = Convert.ToBoolean(dataReader[ArticlePhotoField.Stretch]);

                if (dataReader[ArticlePhotoField.Beveled] != DBNull.Value)
                    articlePhoto.Beveled = Convert.ToBoolean(dataReader[ArticlePhotoField.Beveled]);

                if (dataReader[ArticlePhotoField.CreatedBy] != DBNull.Value)
                    articlePhoto.CreatedBy = Convert.ToString(dataReader[ArticlePhotoField.CreatedBy]);
                articlePhoto.CreationDate = Convert.ToDateTime(dataReader[ArticlePhotoField.CreationDate]);

                articles.Add(articlePhoto);
            }
            dataReader.Close();
            return articles;
        }


        public ArticlePhoto GetArticlePhoto(Guid articleGuid)
        {
            ArticlePhoto articlePhoto = new ArticlePhoto();
            articlePhoto.ArticleGuid = articleGuid;

            DbCommand command = DbProviderHelper.CreateCommand("SelectArticlePhoto", CommandType.StoredProcedure);
            command.Parameters.Add(DbProviderHelper.CreateParameter("@ArticleGuid", DbType.Guid, articleGuid));
            DbDataReader dataReader = DbProviderHelper.ExecuteReader(command);
            while (dataReader.Read())
            {
                articlePhoto.ArticleId = (int)dataReader[ArticleField.ArticleId];
                articlePhoto.ArticleGuid = (Guid)dataReader[ArticleField.ArticleGuid];

                // 获取 Category 对象
                articlePhoto.Category.CategoryId = Convert.ToInt32(dataReader[CategoryField.CategoryId]);
                articlePhoto.Category.CategoryGuid = (Guid)dataReader[CategoryField.CategoryGuid];
                articlePhoto.Category.CategoryName = Convert.ToString(dataReader[CategoryField.CategoryName]);
                articlePhoto.Category.ParentGuid = (Guid)dataReader[CategoryField.ParentGuid];
                articlePhoto.Category.Rank = Convert.ToInt32(dataReader[CategoryField.Rank]);

                if (dataReader[ArticleField.MetaKeywords] != DBNull.Value)
                    articlePhoto.MetaKeywords = Convert.ToString(dataReader[ArticleField.MetaKeywords]);

                if (dataReader[ArticleField.MetaDesc] != DBNull.Value)
                    articlePhoto.MetaDesc = Convert.ToString(dataReader[ArticleField.MetaDesc]);

                articlePhoto.Title = Convert.ToString(dataReader[ArticleField.Title]);

                if (dataReader[ArticleField.TitleColor] != DBNull.Value)
                    articlePhoto.TitleColor = Convert.ToString(dataReader[ArticleField.TitleColor]);

                if (dataReader[ArticleField.SubTitle] != DBNull.Value)
                    articlePhoto.SubTitle = Convert.ToString(dataReader[ArticleField.SubTitle]);

                if (dataReader[ArticleField.Summary] != DBNull.Value)
                    articlePhoto.Summary = Convert.ToString(dataReader[ArticleField.Summary]);

                if (dataReader[ArticleField.ContentHtml] != DBNull.Value)
                    articlePhoto.ContentHtml = Convert.ToString(dataReader[ArticleField.ContentHtml]);

                if (dataReader[ArticleField.Editor] != DBNull.Value)
                    articlePhoto.Editor = (Guid)dataReader[ArticleField.Editor];

                if (dataReader[ArticleField.Author] != DBNull.Value)
                    articlePhoto.Author = Convert.ToString(dataReader[ArticleField.Author]);

                if (dataReader[ArticleField.Original] != DBNull.Value)
                    articlePhoto.Original = Convert.ToString(dataReader[ArticleField.Original]);

                articlePhoto.Rank = Convert.ToInt32(dataReader[ArticleField.Rank]);
                articlePhoto.Hits = Convert.ToInt32(dataReader[ArticleField.Hits]);
                articlePhoto.Comments = Convert.ToInt32(dataReader[ArticleField.Comments]);
                articlePhoto.Votes = Convert.ToInt32(dataReader[ArticleField.Votes]);
                articlePhoto.DateCreated = Convert.ToDateTime(dataReader[ArticleField.DateCreated]);

                // 图片信息
                articlePhoto.ArticlePhotoId = Convert.ToInt32(dataReader[ArticlePhotoField.ArticlePhotoId]);
                articlePhoto.ArticlePhotoGuid = (Guid)dataReader[ArticlePhotoField.ArticlePhotoGuid];
                //articlePhoto.ArticleGuid = (Guid)dataReader[ArticlePhotoField.ArticleGuid];
                articlePhoto.SourcePath = Convert.ToString(dataReader[ArticlePhotoField.SourcePath]);
                articlePhoto.ThumbnailPath = Convert.ToString(dataReader[ArticlePhotoField.ThumbnailPath]);

                if (dataReader[ArticlePhotoField.PointX] != DBNull.Value)
                    articlePhoto.PointX = Convert.ToInt32(dataReader[ArticlePhotoField.PointX]);

                if (dataReader[ArticlePhotoField.PointY] != DBNull.Value)
                    articlePhoto.PointY = Convert.ToInt32(dataReader[ArticlePhotoField.PointY]);

                if (dataReader[ArticlePhotoField.Stretch] != DBNull.Value)
                    articlePhoto.Stretch = Convert.ToBoolean(dataReader[ArticlePhotoField.Stretch]);

                if (dataReader[ArticlePhotoField.Beveled] != DBNull.Value)
                    articlePhoto.Beveled = Convert.ToBoolean(dataReader[ArticlePhotoField.Beveled]);

                if (dataReader[ArticlePhotoField.CreatedBy] != DBNull.Value)
                    articlePhoto.CreatedBy = Convert.ToString(dataReader[ArticlePhotoField.CreatedBy]);
                articlePhoto.CreationDate = Convert.ToDateTime(dataReader[ArticlePhotoField.CreationDate]);
            }
            dataReader.Close();
            return articlePhoto;
        }

        public int AddNew(ArticlePhoto articlePhoto)
        {
            DbCommand command = DbProviderHelper.CreateCommand("INSERTArticlePhoto", CommandType.StoredProcedure);
            command.Parameters.Add(DbProviderHelper.CreateParameter("@ArticlePhotoGuid", DbType.Guid, articlePhoto.ArticlePhotoGuid));
            command.Parameters.Add(DbProviderHelper.CreateParameter("@ArticleGuid", DbType.Guid, articlePhoto.ArticleGuid));
            command.Parameters.Add(DbProviderHelper.CreateParameter("@SourcePath", DbType.String, articlePhoto.SourcePath));
            command.Parameters.Add(DbProviderHelper.CreateParameter("@ThumbnailPath", DbType.String, articlePhoto.ThumbnailPath));
            if (articlePhoto.PointX.HasValue)
                command.Parameters.Add(DbProviderHelper.CreateParameter("@PointX", DbType.Int32, articlePhoto.PointX));
            else
                command.Parameters.Add(DbProviderHelper.CreateParameter("@PointX", DbType.Int32, DBNull.Value));
            if (articlePhoto.PointY.HasValue)
                command.Parameters.Add(DbProviderHelper.CreateParameter("@PointY", DbType.Int32, articlePhoto.PointY));
            else
                command.Parameters.Add(DbProviderHelper.CreateParameter("@PointY", DbType.Int32, DBNull.Value));
            if (articlePhoto.Stretch.HasValue)
                command.Parameters.Add(DbProviderHelper.CreateParameter("@Stretch", DbType.Boolean, articlePhoto.Stretch));
            else
                command.Parameters.Add(DbProviderHelper.CreateParameter("@Stretch", DbType.Boolean, DBNull.Value));
            if (articlePhoto.Beveled.HasValue)
                command.Parameters.Add(DbProviderHelper.CreateParameter("@Beveled", DbType.Boolean, articlePhoto.Beveled));
            else
                command.Parameters.Add(DbProviderHelper.CreateParameter("@Beveled", DbType.Boolean, DBNull.Value));
            if (articlePhoto.CreatedBy != null)
                command.Parameters.Add(DbProviderHelper.CreateParameter("@CreatedBy", DbType.String, articlePhoto.CreatedBy));
            else
                command.Parameters.Add(DbProviderHelper.CreateParameter("@CreatedBy", DbType.String, DBNull.Value));
            command.Parameters.Add(DbProviderHelper.CreateParameter("@CreationDate", DbType.DateTime, articlePhoto.CreationDate));

            return Convert.ToInt32(DbProviderHelper.ExecuteScalar(command));
        }

        public int Update(int ArticlePhotoId, Guid ArticlePhotoGuid, Guid ArticleGuid, string SourcePath, string ThumbnailPath, Nullable<int> PointX, Nullable<int> PointY, Nullable<bool> Stretch, Nullable<bool> Beveled, string CreatedBy, DateTime CreationDate)
        {

            DbCommand command = DbProviderHelper.CreateCommand("UPDATEArticlePhoto", CommandType.StoredProcedure);
            command.Parameters.Add(DbProviderHelper.CreateParameter("@ArticlePhotoGuid", DbType.Guid, ArticlePhotoGuid));
            command.Parameters.Add(DbProviderHelper.CreateParameter("@ArticleGuid", DbType.Guid, ArticleGuid));
            command.Parameters.Add(DbProviderHelper.CreateParameter("@SourcePath", DbType.String, SourcePath));
            command.Parameters.Add(DbProviderHelper.CreateParameter("@ThumbnailPath", DbType.String, ThumbnailPath));
            if (PointX.HasValue)
                command.Parameters.Add(DbProviderHelper.CreateParameter("@PointX", DbType.Int32, PointX));
            else
                command.Parameters.Add(DbProviderHelper.CreateParameter("@PointX", DbType.Int32, DBNull.Value));
            if (PointY.HasValue)
                command.Parameters.Add(DbProviderHelper.CreateParameter("@PointY", DbType.Int32, PointY));
            else
                command.Parameters.Add(DbProviderHelper.CreateParameter("@PointY", DbType.Int32, DBNull.Value));
            if (Stretch.HasValue)
                command.Parameters.Add(DbProviderHelper.CreateParameter("@Stretch", DbType.Boolean, Stretch));
            else
                command.Parameters.Add(DbProviderHelper.CreateParameter("@Stretch", DbType.Boolean, DBNull.Value));
            if (Beveled.HasValue)
                command.Parameters.Add(DbProviderHelper.CreateParameter("@Beveled", DbType.Boolean, Beveled));
            else
                command.Parameters.Add(DbProviderHelper.CreateParameter("@Beveled", DbType.Boolean, DBNull.Value));
            if (CreatedBy != null)
                command.Parameters.Add(DbProviderHelper.CreateParameter("@CreatedBy", DbType.String, CreatedBy));
            else
                command.Parameters.Add(DbProviderHelper.CreateParameter("@CreatedBy", DbType.String, DBNull.Value));
            command.Parameters.Add(DbProviderHelper.CreateParameter("@CreationDate", DbType.DateTime, CreationDate));
            command.Parameters.Add(DbProviderHelper.CreateParameter("@ArticlePhotoId", DbType.Int32, ArticlePhotoId));
            return DbProviderHelper.ExecuteNonQuery(command);
        }

        public int Remove(int ArticlePhotoId)
        {
            DbCommand command = DbProviderHelper.CreateCommand("DELETEArticlePhoto", CommandType.StoredProcedure);
            command.Parameters.Add(DbProviderHelper.CreateParameter("@ArticlePhotoId", DbType.Int32, ArticlePhotoId));
            return DbProviderHelper.ExecuteNonQuery(command);
        }
    }
}
