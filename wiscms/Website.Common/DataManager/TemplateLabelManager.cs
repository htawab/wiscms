using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;

namespace Wis.Website.DataManager
{
	public class TemplateLabelManager
	{
		public TemplateLabelManager()
		{
			DbProviderHelper.GetConnection();
		}

		public List<TemplateLabel> GetTemplateLabels()
		{
			List<TemplateLabel> lstTemplateLabels = new List<TemplateLabel>();
			DbCommand oDbCommand = DbProviderHelper.CreateCommand("SELECTTemplateLabels",CommandType.StoredProcedure);
			DbDataReader oDbDataReader = DbProviderHelper.ExecuteReader(oDbCommand);
			while (oDbDataReader.Read())
			{
				TemplateLabel oTemplateLabel = new TemplateLabel();
				oTemplateLabel.TemplateLabelId = Convert.ToInt32(oDbDataReader["TemplateLabelId"]);
				oTemplateLabel.TemplateLabelGuid = (Guid) oDbDataReader["TemplateLabelGuid"];
				oTemplateLabel.TemplateLabelName = Convert.ToString(oDbDataReader["TemplateLabelName"]);
				oTemplateLabel.TemplateLabelValue = Convert.ToString(oDbDataReader["TemplateLabelValue"]);

				if(oDbDataReader["Description"] != DBNull.Value)
					oTemplateLabel.Description = Convert.ToString(oDbDataReader["Description"]);
				oTemplateLabel.DateCreated = Convert.ToDateTime(oDbDataReader["DateCreated"]);
				lstTemplateLabels.Add(oTemplateLabel);
			}
			oDbDataReader.Close();
			return lstTemplateLabels;
		}

		public int AddNew(int TemplateLabelId, Guid TemplateLabelGuid, string TemplateLabelName, string TemplateLabelValue, string Description, DateTime DateCreated)
		{
			DbCommand oDbCommand = DbProviderHelper.CreateCommand("INSERTTemplateLabel",CommandType.StoredProcedure);
			oDbCommand.Parameters.Add(DbProviderHelper.CreateParameter("@TemplateLabelGuid",DbType.Guid,TemplateLabelGuid));
			oDbCommand.Parameters.Add(DbProviderHelper.CreateParameter("@TemplateLabelName",DbType.String,TemplateLabelName));
			oDbCommand.Parameters.Add(DbProviderHelper.CreateParameter("@TemplateLabelValue",DbType.String,TemplateLabelValue));
			if (Description!=null)
				oDbCommand.Parameters.Add(DbProviderHelper.CreateParameter("@Description",DbType.String,Description));
			else
				oDbCommand.Parameters.Add(DbProviderHelper.CreateParameter("@Description",DbType.String,DBNull.Value));
			oDbCommand.Parameters.Add(DbProviderHelper.CreateParameter("@DateCreated",DbType.DateTime,DateCreated));

			return DbProviderHelper.ExecuteNonQuery(oDbCommand);
		}
	}
}
