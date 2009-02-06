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
			DbDataReader oDbDataReader = DbProviderHelper.ExecuteReader(oDbCommand);
			while (oDbDataReader.Read())
			{
				Template oTemplate = new Template();
				oTemplate.TemplateId = Convert.ToInt32(oDbDataReader["TemplateId"]);
				oTemplate.TemplateGuid = (Guid) oDbDataReader["TemplateGuid"];
				oTemplate.Title = Convert.ToString(oDbDataReader["Title"]);
				oTemplate.TemplatePath = Convert.ToString(oDbDataReader["TemplatePath"]);
				oTemplate.TemplateType = Convert.ToSByte(oDbDataReader["TemplateType"]);
				oTemplate.ArticleType = Convert.ToSByte(oDbDataReader["ArticleType"]);
				lstTemplates.Add(oTemplate);
			}
			oDbDataReader.Close();
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
                template.TemplateType = Convert.ToSByte(dataReader["TemplateType"]);
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
			DbDataReader oDbDataReader = DbProviderHelper.ExecuteReader(oDbCommand);
			while (oDbDataReader.Read())
			{
				oTemplate.TemplateId = Convert.ToInt32(oDbDataReader["TemplateId"]);
				oTemplate.TemplateGuid = (Guid) oDbDataReader["TemplateGuid"];
				oTemplate.Title = Convert.ToString(oDbDataReader["Title"]);
				oTemplate.TemplatePath = Convert.ToString(oDbDataReader["TemplatePath"]);
				oTemplate.TemplateType = Convert.ToSByte(oDbDataReader["TemplateType"]);
				oTemplate.ArticleType = Convert.ToSByte(oDbDataReader["ArticleType"]);
			}
			oDbDataReader.Close();
			return oTemplate;
		}

		public int AddNew(Guid TemplateGuid, string Title, string TemplatePath, SByte TemplateType, SByte ArticleType)
		{
			DbCommand oDbCommand = DbProviderHelper.CreateCommand("INSERTTemplate",CommandType.StoredProcedure);
			oDbCommand.Parameters.Add(DbProviderHelper.CreateParameter("@TemplateGuid",DbType.Guid,TemplateGuid));
			oDbCommand.Parameters.Add(DbProviderHelper.CreateParameter("@Title",DbType.String,Title));
			oDbCommand.Parameters.Add(DbProviderHelper.CreateParameter("@TemplatePath",DbType.String,TemplatePath));
			oDbCommand.Parameters.Add(DbProviderHelper.CreateParameter("@TemplateType",DbType.SByte,TemplateType));
			oDbCommand.Parameters.Add(DbProviderHelper.CreateParameter("@ArticleType",DbType.SByte,ArticleType));

			return Convert.ToInt32(DbProviderHelper.ExecuteScalar(oDbCommand));
		}

		public int Update(int TemplateId, Guid TemplateGuid, string Title, string TemplatePath, SByte TemplateType, SByte ArticleType)
		{

			DbCommand oDbCommand = DbProviderHelper.CreateCommand("UPDATETemplate",CommandType.StoredProcedure);
			oDbCommand.Parameters.Add(DbProviderHelper.CreateParameter("@TemplateGuid",DbType.Guid,TemplateGuid));
			oDbCommand.Parameters.Add(DbProviderHelper.CreateParameter("@Title",DbType.String,Title));
			oDbCommand.Parameters.Add(DbProviderHelper.CreateParameter("@TemplatePath",DbType.String,TemplatePath));
			oDbCommand.Parameters.Add(DbProviderHelper.CreateParameter("@TemplateType",DbType.SByte,TemplateType));
			oDbCommand.Parameters.Add(DbProviderHelper.CreateParameter("@ArticleType",DbType.SByte,ArticleType));
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
