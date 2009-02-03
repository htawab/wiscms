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

namespace Wis.Website.DataManager
{
	public class ReleaseManager
	{
		public ReleaseManager()
		{
			DbProviderHelper.GetConnection();
		}

		public List<Release> GetReleases()
		{
			List<Release> lstReleases = new List<Release>();
			DbCommand oDbCommand = DbProviderHelper.CreateCommand("SELECTReleases",CommandType.StoredProcedure);
			DbDataReader oDbDataReader = DbProviderHelper.ExecuteReader(oDbCommand);
			while (oDbDataReader.Read())
			{
				Release oRelease = new Release();
				oRelease.ReleaseId = Convert.ToInt32(oDbDataReader["ReleaseId"]);
				oRelease.ReleaseGuid = (Guid) oDbDataReader["ReleaseGuid"];
				oRelease.CategoryGuid = (Guid) oDbDataReader["CategoryGuid"];
				oRelease.TemplatePath = Convert.ToString(oDbDataReader["TemplatePath"]);
				oRelease.ReleasePath = Convert.ToString(oDbDataReader["ReleasePath"]);
				lstReleases.Add(oRelease);
			}
			oDbDataReader.Close();
			return lstReleases;
		}

		public Release GetRelease(int ReleaseId)
		{
			Release oRelease = new Release();
			DbCommand oDbCommand = DbProviderHelper.CreateCommand("SELECTRelease",CommandType.StoredProcedure);
			oDbCommand.Parameters.Add(DbProviderHelper.CreateParameter("@ReleaseId",DbType.Int32,ReleaseId));
			DbDataReader oDbDataReader = DbProviderHelper.ExecuteReader(oDbCommand);
			while (oDbDataReader.Read())
			{
				oRelease.ReleaseId = Convert.ToInt32(oDbDataReader["ReleaseId"]);
				oRelease.ReleaseGuid = (Guid) oDbDataReader["ReleaseGuid"];
				oRelease.CategoryGuid = (Guid) oDbDataReader["CategoryGuid"];
				oRelease.TemplatePath = Convert.ToString(oDbDataReader["TemplatePath"]);
				oRelease.ReleasePath = Convert.ToString(oDbDataReader["ReleasePath"]);
			}
			oDbDataReader.Close();
			return oRelease;
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
			DbCommand command = DbProviderHelper.CreateCommand("INSERTRelease",CommandType.StoredProcedure);
			command.Parameters.Add(DbProviderHelper.CreateParameter("@ReleaseGuid",DbType.Guid,ReleaseGuid));
			command.Parameters.Add(DbProviderHelper.CreateParameter("@CategoryGuid",DbType.Guid,CategoryGuid));
			command.Parameters.Add(DbProviderHelper.CreateParameter("@TemplatePath",DbType.String,TemplatePath));
			command.Parameters.Add(DbProviderHelper.CreateParameter("@ReleasePath",DbType.String,ReleasePath));

			return Convert.ToInt32(DbProviderHelper.ExecuteScalar(command));
		}

		public int Update(int ReleaseId, Guid ReleaseGuid, Guid CategoryGuid, string TemplatePath, string ReleasePath)
		{

			DbCommand oDbCommand = DbProviderHelper.CreateCommand("UPDATERelease",CommandType.StoredProcedure);
			oDbCommand.Parameters.Add(DbProviderHelper.CreateParameter("@ReleaseGuid",DbType.Guid,ReleaseGuid));
			oDbCommand.Parameters.Add(DbProviderHelper.CreateParameter("@CategoryGuid",DbType.Guid,CategoryGuid));
			oDbCommand.Parameters.Add(DbProviderHelper.CreateParameter("@TemplatePath",DbType.String,TemplatePath));
			oDbCommand.Parameters.Add(DbProviderHelper.CreateParameter("@ReleasePath",DbType.String,ReleasePath));
			oDbCommand.Parameters.Add(DbProviderHelper.CreateParameter("@ReleaseId",DbType.Int32,ReleaseId));
			return DbProviderHelper.ExecuteNonQuery(oDbCommand);
		}

		public int Remove(int ReleaseId)
		{
			DbCommand oDbCommand = DbProviderHelper.CreateCommand("DELETERelease",CommandType.StoredProcedure);
			oDbCommand.Parameters.Add(DbProviderHelper.CreateParameter("@ReleaseId",DbType.Int32,ReleaseId));
			return DbProviderHelper.ExecuteNonQuery(oDbCommand);
		}
	}
}
