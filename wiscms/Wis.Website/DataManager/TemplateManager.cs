//------------------------------------------------------------------------------
// <copyright file="TemplateManager.cs" company="Everwis">
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
	public class TemplateManager
	{
		public TemplateManager()
		{
			DbProviderHelper.GetConnection();
		}

		public List<Template> GetTemplates()
		{
			List<Template> lstTemplates = new List<Template>();
			DbCommand oDbCommand = DbProviderHelper.CreateCommand("SELECTTemplates",CommandType.StoredProcedure);
			DbDataReader dataReader = DbProviderHelper.ExecuteReader(oDbCommand);
			while (dataReader.Read())
			{
				Template oTemplate = new Template();
				oTemplate.TemplateId = Convert.ToInt32(dataReader["TemplateId"]);
				oTemplate.TemplateGuid = (Guid) dataReader["TemplateGuid"];
				oTemplate.Title = Convert.ToString(dataReader["Title"]);
				oTemplate.TemplatePath = Convert.ToString(dataReader["TemplatePath"]);
                oTemplate.TemplateType = (TemplateType)System.Enum.Parse(typeof(TemplateType), dataReader[ViewReleaseTemplateField.TemplateType].ToString(), true);
				oTemplate.ArticleType = Convert.ToSByte(dataReader["ArticleType"]);
				lstTemplates.Add(oTemplate);
			}
			dataReader.Close();
			return lstTemplates;
		}


        public List<Template> GetTemplates(ArticleType articleType, TemplateType templateType)
        {
            List<Template> templates = new List<Template>();
            DbCommand command = DbProviderHelper.CreateCommand("SelectTemplatesByArticleTypeAndTemplateType", CommandType.StoredProcedure);
            command.Parameters.Add(DbProviderHelper.CreateParameter("@ArticleType", DbType.Int16, articleType));
            command.Parameters.Add(DbProviderHelper.CreateParameter("@TemplateType", DbType.Int16, templateType));
            DbDataReader dataReader = DbProviderHelper.ExecuteReader(command);
            while (dataReader.Read())
            {
                Template template = new Template();
                template.TemplateId = Convert.ToInt32(dataReader["TemplateId"]);
                template.TemplateGuid = (Guid)dataReader["TemplateGuid"];
                template.Title = Convert.ToString(dataReader["Title"]);
                template.TemplatePath = Convert.ToString(dataReader["TemplatePath"]);
                template.TemplateType = (TemplateType)System.Enum.Parse(typeof(TemplateType), dataReader[ViewReleaseTemplateField.TemplateType].ToString(), true);
                template.ArticleType = Convert.ToSByte(dataReader["ArticleType"]);
                templates.Add(template);
            }
            dataReader.Close();
            return templates;
        }


		public Template GetTemplate(int TemplateId)
		{
			Template oTemplate = new Template();
			DbCommand oDbCommand = DbProviderHelper.CreateCommand("SELECTTemplate",CommandType.StoredProcedure);
			oDbCommand.Parameters.Add(DbProviderHelper.CreateParameter("@TemplateId",DbType.Int32,TemplateId));
			DbDataReader dataReader = DbProviderHelper.ExecuteReader(oDbCommand);
			while (dataReader.Read())
			{
				oTemplate.TemplateId = Convert.ToInt32(dataReader["TemplateId"]);
				oTemplate.TemplateGuid = (Guid) dataReader["TemplateGuid"];
				oTemplate.Title = Convert.ToString(dataReader["Title"]);
				oTemplate.TemplatePath = Convert.ToString(dataReader["TemplatePath"]);
                oTemplate.TemplateType = (TemplateType)System.Enum.Parse(typeof(TemplateType), dataReader[ViewReleaseTemplateField.TemplateType].ToString(), true);
				oTemplate.ArticleType = Convert.ToSByte(dataReader["ArticleType"]);
			}
			dataReader.Close();
			return oTemplate;
		}

		public int AddNew(Guid TemplateGuid, string Title, string TemplatePath, SByte TemplateType, SByte ArticleType)
		{
			DbCommand oDbCommand = DbProviderHelper.CreateCommand("INSERTTemplate",CommandType.StoredProcedure);
			oDbCommand.Parameters.Add(DbProviderHelper.CreateParameter("@TemplateGuid",DbType.Guid,TemplateGuid));
			oDbCommand.Parameters.Add(DbProviderHelper.CreateParameter("@Title",DbType.String,Title));
			oDbCommand.Parameters.Add(DbProviderHelper.CreateParameter("@TemplatePath",DbType.String,TemplatePath));
            oDbCommand.Parameters.Add(DbProviderHelper.CreateParameter("@TemplateType", DbType.Byte, TemplateType));
            oDbCommand.Parameters.Add(DbProviderHelper.CreateParameter("@ArticleType", DbType.Byte, ArticleType));

			return Convert.ToInt32(DbProviderHelper.ExecuteScalar(oDbCommand));
		}

		public int Update(int TemplateId, Guid TemplateGuid, string Title, string TemplatePath, SByte TemplateType, SByte ArticleType)
		{

			DbCommand oDbCommand = DbProviderHelper.CreateCommand("UPDATETemplate",CommandType.StoredProcedure);
			oDbCommand.Parameters.Add(DbProviderHelper.CreateParameter("@TemplateGuid",DbType.Guid,TemplateGuid));
			oDbCommand.Parameters.Add(DbProviderHelper.CreateParameter("@Title",DbType.String,Title));
			oDbCommand.Parameters.Add(DbProviderHelper.CreateParameter("@TemplatePath",DbType.String,TemplatePath));
            oDbCommand.Parameters.Add(DbProviderHelper.CreateParameter("@TemplateType", DbType.Byte, TemplateType));
            oDbCommand.Parameters.Add(DbProviderHelper.CreateParameter("@ArticleType", DbType.Byte, ArticleType));
			oDbCommand.Parameters.Add(DbProviderHelper.CreateParameter("@TemplateId",DbType.Int32,TemplateId));
			return DbProviderHelper.ExecuteNonQuery(oDbCommand);
		}

		public int Remove(int TemplateId)
		{
			DbCommand oDbCommand = DbProviderHelper.CreateCommand("DELETETemplate",CommandType.StoredProcedure);
			oDbCommand.Parameters.Add(DbProviderHelper.CreateParameter("@TemplateId",DbType.Int32,TemplateId));
			return DbProviderHelper.ExecuteNonQuery(oDbCommand);
		}
	}
}
