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
    /// 图片新闻的图片信息的操作类
    /// </summary>
    public class ArticlePhotoManager
    {
        public ArticlePhotoManager()
        {
            DbProviderHelper.GetConnection();
        }

        public List<ArticlePhoto> GetArticlePhotos()
        {
            List<ArticlePhoto> lstArticlePhotos = new List<ArticlePhoto>();
            DbCommand oDbCommand = DbProviderHelper.CreateCommand("SELECTArticlePhotos", CommandType.StoredProcedure);
            DbDataReader oDbDataReader = DbProviderHelper.ExecuteReader(oDbCommand);
            while (oDbDataReader.Read())
            {
                ArticlePhoto oArticlePhoto = new ArticlePhoto();
                oArticlePhoto.ArticlePhotoId = Convert.ToInt32(oDbDataReader["ArticlePhotoId"]);
                oArticlePhoto.ArticlePhotoGuid = (Guid)oDbDataReader["ArticlePhotoGuid"];
                //oArticlePhoto.ArticleGuid = (Guid)oDbDataReader["ArticleGuid"];
                oArticlePhoto.SourcePath = Convert.ToString(oDbDataReader["SourcePath"]);
                oArticlePhoto.ThumbnailPath = Convert.ToString(oDbDataReader["ThumbnailPath"]);

                if (oDbDataReader["PointX"] != DBNull.Value)
                    oArticlePhoto.PointX = Convert.ToInt32(oDbDataReader["PointX"]);

                if (oDbDataReader["PointY"] != DBNull.Value)
                    oArticlePhoto.PointY = Convert.ToInt32(oDbDataReader["PointY"]);

                if (oDbDataReader["Stretch"] != DBNull.Value)
                    oArticlePhoto.Stretch = Convert.ToBoolean(oDbDataReader["Stretch"]);

                if (oDbDataReader["Beveled"] != DBNull.Value)
                    oArticlePhoto.Beveled = Convert.ToBoolean(oDbDataReader["Beveled"]);

                if (oDbDataReader["CreatedBy"] != DBNull.Value)
                    oArticlePhoto.CreatedBy = Convert.ToString(oDbDataReader["CreatedBy"]);
                oArticlePhoto.CreationDate = Convert.ToDateTime(oDbDataReader["CreationDate"]);
                lstArticlePhotos.Add(oArticlePhoto);
            }
            oDbDataReader.Close();
            return lstArticlePhotos;
        }

        public ArticlePhoto GetArticlePhoto(int ArticlePhotoId)
        {
            ArticlePhoto oArticlePhoto = new ArticlePhoto();
            DbCommand oDbCommand = DbProviderHelper.CreateCommand("SELECTArticlePhoto", CommandType.StoredProcedure);
            oDbCommand.Parameters.Add(DbProviderHelper.CreateParameter("@ArticlePhotoId", DbType.Int32, ArticlePhotoId));
            DbDataReader oDbDataReader = DbProviderHelper.ExecuteReader(oDbCommand);
            while (oDbDataReader.Read())
            {
                oArticlePhoto.ArticlePhotoId = Convert.ToInt32(oDbDataReader["ArticlePhotoId"]);
                oArticlePhoto.ArticlePhotoGuid = (Guid)oDbDataReader["ArticlePhotoGuid"];
                //oArticlePhoto.ArticleGuid = (Guid)oDbDataReader["ArticleGuid"];
                oArticlePhoto.SourcePath = Convert.ToString(oDbDataReader["SourcePath"]);
                oArticlePhoto.ThumbnailPath = Convert.ToString(oDbDataReader["ThumbnailPath"]);

                if (oDbDataReader["PointX"] != DBNull.Value)
                    oArticlePhoto.PointX = Convert.ToInt32(oDbDataReader["PointX"]);

                if (oDbDataReader["PointY"] != DBNull.Value)
                    oArticlePhoto.PointY = Convert.ToInt32(oDbDataReader["PointY"]);

                if (oDbDataReader["Stretch"] != DBNull.Value)
                    oArticlePhoto.Stretch = Convert.ToBoolean(oDbDataReader["Stretch"]);

                if (oDbDataReader["Beveled"] != DBNull.Value)
                    oArticlePhoto.Beveled = Convert.ToBoolean(oDbDataReader["Beveled"]);

                if (oDbDataReader["CreatedBy"] != DBNull.Value)
                    oArticlePhoto.CreatedBy = Convert.ToString(oDbDataReader["CreatedBy"]);
                oArticlePhoto.CreationDate = Convert.ToDateTime(oDbDataReader["CreationDate"]);
            }
            oDbDataReader.Close();
            return oArticlePhoto;
        }

        public int AddNew(ArticlePhoto articlePhoto)
        {
            DbCommand command = DbProviderHelper.CreateCommand("INSERTArticlePhoto", CommandType.StoredProcedure);
            command.Parameters.Add(DbProviderHelper.CreateParameter("@ArticlePhotoGuid", DbType.Guid, articlePhoto.ArticlePhotoGuid));
            command.Parameters.Add(DbProviderHelper.CreateParameter("@ArticleGuid", DbType.Guid, articlePhoto.Article.ArticleGuid));
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

            DbCommand oDbCommand = DbProviderHelper.CreateCommand("UPDATEArticlePhoto", CommandType.StoredProcedure);
            oDbCommand.Parameters.Add(DbProviderHelper.CreateParameter("@ArticlePhotoGuid", DbType.Guid, ArticlePhotoGuid));
            oDbCommand.Parameters.Add(DbProviderHelper.CreateParameter("@ArticleGuid", DbType.Guid, ArticleGuid));
            oDbCommand.Parameters.Add(DbProviderHelper.CreateParameter("@SourcePath", DbType.String, SourcePath));
            oDbCommand.Parameters.Add(DbProviderHelper.CreateParameter("@ThumbnailPath", DbType.String, ThumbnailPath));
            if (PointX.HasValue)
                oDbCommand.Parameters.Add(DbProviderHelper.CreateParameter("@PointX", DbType.Int32, PointX));
            else
                oDbCommand.Parameters.Add(DbProviderHelper.CreateParameter("@PointX", DbType.Int32, DBNull.Value));
            if (PointY.HasValue)
                oDbCommand.Parameters.Add(DbProviderHelper.CreateParameter("@PointY", DbType.Int32, PointY));
            else
                oDbCommand.Parameters.Add(DbProviderHelper.CreateParameter("@PointY", DbType.Int32, DBNull.Value));
            if (Stretch.HasValue)
                oDbCommand.Parameters.Add(DbProviderHelper.CreateParameter("@Stretch", DbType.Boolean, Stretch));
            else
                oDbCommand.Parameters.Add(DbProviderHelper.CreateParameter("@Stretch", DbType.Boolean, DBNull.Value));
            if (Beveled.HasValue)
                oDbCommand.Parameters.Add(DbProviderHelper.CreateParameter("@Beveled", DbType.Boolean, Beveled));
            else
                oDbCommand.Parameters.Add(DbProviderHelper.CreateParameter("@Beveled", DbType.Boolean, DBNull.Value));
            if (CreatedBy != null)
                oDbCommand.Parameters.Add(DbProviderHelper.CreateParameter("@CreatedBy", DbType.String, CreatedBy));
            else
                oDbCommand.Parameters.Add(DbProviderHelper.CreateParameter("@CreatedBy", DbType.String, DBNull.Value));
            oDbCommand.Parameters.Add(DbProviderHelper.CreateParameter("@CreationDate", DbType.DateTime, CreationDate));
            oDbCommand.Parameters.Add(DbProviderHelper.CreateParameter("@ArticlePhotoId", DbType.Int32, ArticlePhotoId));
            return DbProviderHelper.ExecuteNonQuery(oDbCommand);
        }

        public int Remove(int ArticlePhotoId)
        {
            DbCommand oDbCommand = DbProviderHelper.CreateCommand("DELETEArticlePhoto", CommandType.StoredProcedure);
            oDbCommand.Parameters.Add(DbProviderHelper.CreateParameter("@ArticlePhotoId", DbType.Int32, ArticlePhotoId));
            return DbProviderHelper.ExecuteNonQuery(oDbCommand);
        }
    }
}
