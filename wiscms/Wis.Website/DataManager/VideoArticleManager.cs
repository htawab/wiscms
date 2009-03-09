//------------------------------------------------------------------------------
// <copyright file="VideoArticleManager.cs" company="Everwis">
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
    /// ͼƬ���ŵĲ�����
    /// </summary>
    public class VideoArticleManager
    {
        public VideoArticleManager()
        {
            DbProviderHelper.GetConnection();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="categoryName"></param>
        /// <param name="parentCategoryName"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public static List<VideoArticle> GetVideoArticles(string categoryName, string parentCategoryName, int pageIndex, int pageSize)
        {
            DbProviderHelper.GetConnection();

            if (string.IsNullOrEmpty(categoryName))
                throw new System.ArgumentNullException("categoryName");
            if (string.IsNullOrEmpty(parentCategoryName))
                parentCategoryName = string.Empty;

            // TODO:�� categoryName ����ע��ʽ��������
            DbCommand command = DbProviderHelper.CreateCommand("SelectObjects", CommandType.StoredProcedure);
            command.Parameters.Add(DbProviderHelper.CreateParameter("@TableName", DbType.String, "View_VideoArticle"));
            command.Parameters.Add(DbProviderHelper.CreateParameter("@ColumnList", DbType.String, "*"));
            command.Parameters.Add(DbProviderHelper.CreateParameter("@SearchCondition", DbType.String, string.Format("CategoryName='{0}' and ParentCategoryName='{1}'", categoryName, parentCategoryName)));
            command.Parameters.Add(DbProviderHelper.CreateParameter("@OrderList", DbType.String, "DateCreated DESC"));
            command.Parameters.Add(DbProviderHelper.CreateParameter("@PageSize", DbType.Int32, pageSize));
            command.Parameters.Add(DbProviderHelper.CreateParameter("@PageIndex", DbType.Int32, pageIndex));
            DbDataReader dataReader = DbProviderHelper.ExecuteReader(command);
            return GetVideoArticles(dataReader);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="categoryGuid"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public static List<VideoArticle> GetVideoArticles(Guid categoryGuid, int pageIndex, int pageSize)
        {
            DbProviderHelper.GetConnection();

            if (categoryGuid.Equals(Guid.Empty))
                throw new System.ArgumentNullException("categoryGuid");

            // TODO:�� categoryName ����ע��ʽ��������
            DbCommand command = DbProviderHelper.CreateCommand("SelectObjects", CommandType.StoredProcedure);
            command.Parameters.Add(DbProviderHelper.CreateParameter("@TableName", DbType.String, "View_VideoArticle"));
            command.Parameters.Add(DbProviderHelper.CreateParameter("@ColumnList", DbType.String, "*"));
            command.Parameters.Add(DbProviderHelper.CreateParameter("@SearchCondition", DbType.String, string.Format("CategoryGuid='{0}'", categoryGuid)));
            command.Parameters.Add(DbProviderHelper.CreateParameter("@OrderList", DbType.String, "DateCreated DESC"));
            command.Parameters.Add(DbProviderHelper.CreateParameter("@PageSize", DbType.Int32, pageSize));
            command.Parameters.Add(DbProviderHelper.CreateParameter("@PageIndex", DbType.Int32, pageIndex));
            DbDataReader dataReader = DbProviderHelper.ExecuteReader(command);
            return GetVideoArticles(dataReader);
        }


        /// <summary>
        /// ��ȡ��Ƶ���ż��ϡ�
        /// </summary>
        /// <param name="dataReader"></param>
        /// <returns></returns>
        public static List<VideoArticle> GetVideoArticles(DbDataReader dataReader)
        {
            // ������
            List<VideoArticle> videoArticles = new List<VideoArticle>();
            while (dataReader.Read())
            {
                VideoArticle videoArticle = new VideoArticle();

                videoArticle.Article.ArticleId = (int)dataReader[ViewVideoArticleField.ArticleId];
                videoArticle.Article.ArticleGuid = (Guid)dataReader[ViewVideoArticleField.ArticleGuid];

                // ��ȡ Category ����
                videoArticle.Article.Category.CategoryId = Convert.ToInt32(dataReader[CategoryField.CategoryId]);
                videoArticle.Article.Category.CategoryGuid = (Guid)dataReader[CategoryField.CategoryGuid];
                videoArticle.Article.Category.CategoryName = Convert.ToString(dataReader[CategoryField.CategoryName]);
                videoArticle.Article.Category.ParentGuid = (Guid)dataReader[CategoryField.ParentGuid];
                videoArticle.Article.Category.Rank = Convert.ToInt32(dataReader[CategoryField.Rank]);

                if (dataReader[ViewVideoArticleField.MetaKeywords] != DBNull.Value)
                    videoArticle.Article.MetaKeywords = Convert.ToString(dataReader[ViewVideoArticleField.MetaKeywords]);

                if (dataReader[ViewVideoArticleField.MetaDesc] != DBNull.Value)
                    videoArticle.Article.MetaDesc = Convert.ToString(dataReader[ViewVideoArticleField.MetaDesc]);

                videoArticle.Article.Title = Convert.ToString(dataReader[ViewVideoArticleField.Title]);

                if (dataReader[ViewVideoArticleField.TitleColor] != DBNull.Value)
                    videoArticle.Article.TitleColor = Convert.ToString(dataReader[ViewVideoArticleField.TitleColor]);

                if (dataReader[ViewVideoArticleField.SubTitle] != DBNull.Value)
                    videoArticle.Article.SubTitle = Convert.ToString(dataReader[ViewVideoArticleField.SubTitle]);

                if (dataReader[ViewVideoArticleField.Summary] != DBNull.Value)
                    videoArticle.Article.Summary = Convert.ToString(dataReader[ViewVideoArticleField.Summary]);

                if (dataReader[ViewVideoArticleField.ContentHtml] != DBNull.Value)
                    videoArticle.Article.ContentHtml = Convert.ToString(dataReader[ViewVideoArticleField.ContentHtml]);

                if (dataReader[ViewVideoArticleField.Editor] != DBNull.Value)
                    videoArticle.Article.Editor = (Guid)dataReader[ViewVideoArticleField.Editor];

                if (dataReader[ViewVideoArticleField.Author] != DBNull.Value)
                    videoArticle.Article.Author = Convert.ToString(dataReader[ViewVideoArticleField.Author]);

                if (dataReader[ViewVideoArticleField.Original] != DBNull.Value)
                    videoArticle.Article.Original = Convert.ToString(dataReader[ViewVideoArticleField.Original]);

                videoArticle.Article.Rank = Convert.ToInt32(dataReader[ViewVideoArticleField.VideoArticleRank]);
                videoArticle.Article.Hits = Convert.ToInt32(dataReader[ViewVideoArticleField.Hits]);
                videoArticle.Article.Comments = Convert.ToInt32(dataReader[ViewVideoArticleField.Comments]);
                videoArticle.Article.Votes = Convert.ToInt32(dataReader[ViewVideoArticleField.Votes]);
                videoArticle.Article.DateCreated = Convert.ToDateTime(dataReader[ViewVideoArticleField.DateCreated]);

                // ��Ƶ��Ϣ
                videoArticle.VideoArticleId = Convert.ToInt32(dataReader[ViewVideoArticleField.VideoArticleId]);
                videoArticle.VideoArticleGuid = (Guid)dataReader[ViewVideoArticleField.VideoArticleGuid];
                //videoArticle.ArticleGuid = (Guid)dataReader[ViewVideoArticleField.ArticleGuid];
                videoArticle.VideoPath = Convert.ToString(dataReader[ViewVideoArticleField.VideoPath]);
                videoArticle.Size = Convert.ToInt64(dataReader[ViewVideoArticleField.Size]);

                if (dataReader[ViewVideoArticleField.VideoArticleRank] != DBNull.Value)
                    videoArticle.Rank = (byte)dataReader[ViewVideoArticleField.VideoArticleRank];
                videoArticle.SourceImagePath = Convert.ToString(dataReader[ViewVideoArticleField.SourceImagePath]);
                videoArticle.ThumbnailPath = Convert.ToString(dataReader[ViewVideoArticleField.ThumbnailPath]);

                if (dataReader[ViewVideoArticleField.PointX] != DBNull.Value)
                    videoArticle.PointX = Convert.ToInt32(dataReader[ViewVideoArticleField.PointX]);

                if (dataReader[ViewVideoArticleField.PointY] != DBNull.Value)
                    videoArticle.PointY = Convert.ToInt32(dataReader[ViewVideoArticleField.PointY]);

                if (dataReader[ViewVideoArticleField.Stretch] != DBNull.Value)
                    videoArticle.Stretch = Convert.ToBoolean(dataReader[ViewVideoArticleField.Stretch]);

                if (dataReader[ViewVideoArticleField.Beveled] != DBNull.Value)
                    videoArticle.Beveled = Convert.ToBoolean(dataReader[ViewVideoArticleField.Beveled]);

                if (dataReader[ViewVideoArticleField.CreatedBy] != DBNull.Value)
                    videoArticle.CreatedBy = Convert.ToString(dataReader[ViewVideoArticleField.CreatedBy]);
                videoArticle.CreationDate = Convert.ToDateTime(dataReader[ViewVideoArticleField.CreationDate]);

                videoArticles.Add(videoArticle);
            }
            dataReader.Close();
            return videoArticles;
        }


        public VideoArticle GetVideoArticle(Guid articleGuid)
        {
            VideoArticle videoArticle = new VideoArticle();
            videoArticle.Article.ArticleGuid = articleGuid;

            DbCommand command = DbProviderHelper.CreateCommand("SelectVideoArticle", CommandType.StoredProcedure);
            command.Parameters.Add(DbProviderHelper.CreateParameter("@ArticleGuid", DbType.Guid, articleGuid));
            DbDataReader dataReader = DbProviderHelper.ExecuteReader(command);
            while (dataReader.Read())
            {
                videoArticle.Article.ArticleId = (int)dataReader[ViewVideoArticleField.ArticleId];
                //videoArticle.Article.ArticleGuid = (Guid)dataReader[ViewVideoArticleField.ArticleGuid];

                // ��ȡ Category ����
                videoArticle.Article.Category.CategoryId = Convert.ToInt32(dataReader[ViewVideoArticleField.CategoryId]);
                videoArticle.Article.Category.CategoryGuid = (Guid)dataReader[ViewVideoArticleField.CategoryGuid];
                videoArticle.Article.Category.CategoryName = Convert.ToString(dataReader[ViewVideoArticleField.CategoryName]);
                videoArticle.Article.Category.ParentGuid = (Guid)dataReader[ViewVideoArticleField.ParentGuid];
                videoArticle.Article.Category.Rank = Convert.ToInt32(dataReader[ViewVideoArticleField.CategoryRank]);

                if (dataReader[ViewVideoArticleField.MetaKeywords] != DBNull.Value)
                    videoArticle.Article.MetaKeywords = Convert.ToString(dataReader[ViewVideoArticleField.MetaKeywords]);

                if (dataReader[ViewVideoArticleField.MetaDesc] != DBNull.Value)
                    videoArticle.Article.MetaDesc = Convert.ToString(dataReader[ViewVideoArticleField.MetaDesc]);

                videoArticle.Article.Title = Convert.ToString(dataReader[ViewVideoArticleField.Title]);

                if (dataReader[ViewVideoArticleField.TitleColor] != DBNull.Value)
                    videoArticle.Article.TitleColor = Convert.ToString(dataReader[ViewVideoArticleField.TitleColor]);

                if (dataReader[ViewVideoArticleField.SubTitle] != DBNull.Value)
                    videoArticle.Article.SubTitle = Convert.ToString(dataReader[ViewVideoArticleField.SubTitle]);

                if (dataReader[ViewVideoArticleField.Summary] != DBNull.Value)
                    videoArticle.Article.Summary = Convert.ToString(dataReader[ViewVideoArticleField.Summary]);

                if (dataReader[ViewVideoArticleField.ContentHtml] != DBNull.Value)
                    videoArticle.Article.ContentHtml = Convert.ToString(dataReader[ViewVideoArticleField.ContentHtml]);

                if (dataReader[ViewVideoArticleField.Editor] != DBNull.Value)
                    videoArticle.Article.Editor = (Guid)dataReader[ViewVideoArticleField.Editor];

                if (dataReader[ViewVideoArticleField.Author] != DBNull.Value)
                    videoArticle.Article.Author = Convert.ToString(dataReader[ViewVideoArticleField.Author]);

                if (dataReader[ViewVideoArticleField.Original] != DBNull.Value)
                    videoArticle.Article.Original = Convert.ToString(dataReader[ViewVideoArticleField.Original]);

                videoArticle.Article.Rank = Convert.ToInt32(dataReader[ViewVideoArticleField.ArticleRank]);
                videoArticle.Article.Hits = Convert.ToInt32(dataReader[ViewVideoArticleField.Hits]);
                videoArticle.Article.Comments = Convert.ToInt32(dataReader[ViewVideoArticleField.Comments]);
                videoArticle.Article.Votes = Convert.ToInt32(dataReader[ViewVideoArticleField.Votes]);
                videoArticle.Article.DateCreated = Convert.ToDateTime(dataReader[ViewVideoArticleField.DateCreated]);

                // ��Ƶ��Ϣ
                videoArticle.VideoArticleId = Convert.ToInt32(dataReader[ViewVideoArticleField.VideoArticleId]);
                videoArticle.VideoArticleGuid = (Guid)dataReader[ViewVideoArticleField.VideoArticleGuid];
                //videoArticle.ArticleGuid = (Guid)dataReader[ViewVideoArticleField.ArticleGuid"];
                videoArticle.VideoPath = Convert.ToString(dataReader[ViewVideoArticleField.VideoPath]);
                videoArticle.Size = Convert.ToInt64(dataReader[ViewVideoArticleField.Size]);

                if (dataReader[ViewVideoArticleField.VideoArticleRank] != DBNull.Value)
                    videoArticle.Rank = (byte)(dataReader[ViewVideoArticleField.VideoArticleRank]);
                videoArticle.SourceImagePath = Convert.ToString(dataReader[ViewVideoArticleField.SourceImagePath]);
                videoArticle.ThumbnailPath = Convert.ToString(dataReader[ViewVideoArticleField.ThumbnailPath]);

                if (dataReader[ViewVideoArticleField.PointX] != DBNull.Value)
                    videoArticle.PointX = Convert.ToInt32(dataReader[ViewVideoArticleField.PointX]);

                if (dataReader[ViewVideoArticleField.PointY] != DBNull.Value)
                    videoArticle.PointY = Convert.ToInt32(dataReader[ViewVideoArticleField.PointY]);

                if (dataReader[ViewVideoArticleField.Stretch] != DBNull.Value)
                    videoArticle.Stretch = Convert.ToBoolean(dataReader[ViewVideoArticleField.Stretch]);

                if (dataReader[ViewVideoArticleField.Beveled] != DBNull.Value)
                    videoArticle.Beveled = Convert.ToBoolean(dataReader[ViewVideoArticleField.Beveled]);

                if (dataReader[ViewVideoArticleField.CreatedBy] != DBNull.Value)
                    videoArticle.CreatedBy = Convert.ToString(dataReader[ViewVideoArticleField.CreatedBy]);
                videoArticle.CreationDate = Convert.ToDateTime(dataReader[ViewVideoArticleField.CreationDate]);
            }
            dataReader.Close();
            return videoArticle;
        }

        public int AddNew(VideoArticle videoArticle)
        {
            DbCommand command = DbProviderHelper.CreateCommand("INSERTVideoArticle", CommandType.StoredProcedure);
            command.Parameters.Add(DbProviderHelper.CreateParameter("@VideoArticleGuid", DbType.Guid, videoArticle.VideoArticleGuid));
            command.Parameters.Add(DbProviderHelper.CreateParameter("@ArticleGuid", DbType.Guid, videoArticle.Article.ArticleGuid));
            command.Parameters.Add(DbProviderHelper.CreateParameter("@VideoPath", DbType.String, videoArticle.VideoPath));
            command.Parameters.Add(DbProviderHelper.CreateParameter("@Size", DbType.Int64, videoArticle.Size));
            if (videoArticle.Rank.HasValue)
                command.Parameters.Add(DbProviderHelper.CreateParameter("@Rank", DbType.Byte, videoArticle.Rank));
            else
                command.Parameters.Add(DbProviderHelper.CreateParameter("@Rank", DbType.Byte, DBNull.Value));
            command.Parameters.Add(DbProviderHelper.CreateParameter("@SourceImagePath", DbType.String, videoArticle.SourceImagePath));
            command.Parameters.Add(DbProviderHelper.CreateParameter("@ThumbnailPath", DbType.String, videoArticle.ThumbnailPath));
            if (videoArticle.PointX.HasValue)
                command.Parameters.Add(DbProviderHelper.CreateParameter("@PointX", DbType.Int32, videoArticle.PointX));
            else
                command.Parameters.Add(DbProviderHelper.CreateParameter("@PointX", DbType.Int32, DBNull.Value));
            if (videoArticle.PointY.HasValue)
                command.Parameters.Add(DbProviderHelper.CreateParameter("@PointY", DbType.Int32, videoArticle.PointY));
            else
                command.Parameters.Add(DbProviderHelper.CreateParameter("@PointY", DbType.Int32, DBNull.Value));
            if (videoArticle.Stretch.HasValue)
                command.Parameters.Add(DbProviderHelper.CreateParameter("@Stretch", DbType.Boolean, videoArticle.Stretch));
            else
                command.Parameters.Add(DbProviderHelper.CreateParameter("@Stretch", DbType.Boolean, DBNull.Value));
            if (videoArticle.Beveled.HasValue)
                command.Parameters.Add(DbProviderHelper.CreateParameter("@Beveled", DbType.Boolean, videoArticle.Beveled));
            else
                command.Parameters.Add(DbProviderHelper.CreateParameter("@Beveled", DbType.Boolean, DBNull.Value));
            if (videoArticle.CreatedBy != null)
                command.Parameters.Add(DbProviderHelper.CreateParameter("@CreatedBy", DbType.String, videoArticle.CreatedBy));
            else
                command.Parameters.Add(DbProviderHelper.CreateParameter("@CreatedBy", DbType.String, DBNull.Value));
            command.Parameters.Add(DbProviderHelper.CreateParameter("@CreationDate", DbType.DateTime, videoArticle.CreationDate));

            return Convert.ToInt32(DbProviderHelper.ExecuteScalar(command));
        }

        public int Update(VideoArticle videoArticle)
        {
            DbCommand command = DbProviderHelper.CreateCommand("UPDATEVideoArticle", CommandType.StoredProcedure);
            command.Parameters.Add(DbProviderHelper.CreateParameter("@VideoArticleGuid", DbType.Guid, videoArticle.VideoArticleGuid));
            command.Parameters.Add(DbProviderHelper.CreateParameter("@ArticleGuid", DbType.Guid, videoArticle.Article.ArticleGuid));
            command.Parameters.Add(DbProviderHelper.CreateParameter("@VideoPath", DbType.String, videoArticle.VideoPath));
            command.Parameters.Add(DbProviderHelper.CreateParameter("@Size", DbType.Int64, videoArticle.Size));
            if (videoArticle.Rank.HasValue)
                command.Parameters.Add(DbProviderHelper.CreateParameter("@Rank", DbType.SByte, videoArticle.Rank));
            else
                command.Parameters.Add(DbProviderHelper.CreateParameter("@Rank", DbType.SByte, DBNull.Value));
            command.Parameters.Add(DbProviderHelper.CreateParameter("@SourceImagePath", DbType.String, videoArticle.SourceImagePath));
            command.Parameters.Add(DbProviderHelper.CreateParameter("@ThumbnailPath", DbType.String, videoArticle.ThumbnailPath));
            if (videoArticle.PointX.HasValue)
                command.Parameters.Add(DbProviderHelper.CreateParameter("@PointX", DbType.Int32, videoArticle.PointX));
            else
                command.Parameters.Add(DbProviderHelper.CreateParameter("@PointX", DbType.Int32, DBNull.Value));
            if (videoArticle.PointY.HasValue)
                command.Parameters.Add(DbProviderHelper.CreateParameter("@PointY", DbType.Int32, videoArticle.PointY));
            else
                command.Parameters.Add(DbProviderHelper.CreateParameter("@PointY", DbType.Int32, DBNull.Value));
            if (videoArticle.Stretch.HasValue)
                command.Parameters.Add(DbProviderHelper.CreateParameter("@Stretch", DbType.Boolean, videoArticle.Stretch));
            else
                command.Parameters.Add(DbProviderHelper.CreateParameter("@Stretch", DbType.Boolean, DBNull.Value));
            if (videoArticle.Beveled.HasValue)
                command.Parameters.Add(DbProviderHelper.CreateParameter("@Beveled", DbType.Boolean, videoArticle.Beveled));
            else
                command.Parameters.Add(DbProviderHelper.CreateParameter("@Beveled", DbType.Boolean, DBNull.Value));
            if (videoArticle.CreatedBy != null)
                command.Parameters.Add(DbProviderHelper.CreateParameter("@CreatedBy", DbType.String, videoArticle.CreatedBy));
            else
                command.Parameters.Add(DbProviderHelper.CreateParameter("@CreatedBy", DbType.String, DBNull.Value));
            command.Parameters.Add(DbProviderHelper.CreateParameter("@CreationDate", DbType.DateTime, videoArticle.CreationDate));
            command.Parameters.Add(DbProviderHelper.CreateParameter("@VideoArticleId", DbType.Int32, videoArticle.VideoArticleId));
            return DbProviderHelper.ExecuteNonQuery(command);
        }

        public int Remove(int videoArticleId)
        {
            DbCommand command = DbProviderHelper.CreateCommand("DELETEVideoArticle", CommandType.StoredProcedure);
            command.Parameters.Add(DbProviderHelper.CreateParameter("@VideoArticleId", DbType.Int32, videoArticleId));
            return DbProviderHelper.ExecuteNonQuery(command);
        }
    }
}
