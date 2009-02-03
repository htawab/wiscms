using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;

namespace Wis.Website.DataManager
{
	public class OriginalManager
	{
		public OriginalManager()
		{
			DbProviderHelper.GetConnection();
		}

		public List<Original> GetOriginals()
		{
			List<Original> lstOriginals = new List<Original>();
			DbCommand oDbCommand = DbProviderHelper.CreateCommand("SELECTOriginals",CommandType.StoredProcedure);
			DbDataReader oDbDataReader = DbProviderHelper.ExecuteReader(oDbCommand);
			while (oDbDataReader.Read())
			{
				Original oOriginal = new Original();
				oOriginal.OriginalId = Convert.ToInt32(oDbDataReader["OriginalId"]);
				oOriginal.OriginalGuid = (Guid) oDbDataReader["OriginalGuid"];
				oOriginal.OriginalText = Convert.ToString(oDbDataReader["OriginalText"]);

				if(oDbDataReader["HyperLink"] != DBNull.Value)
					oOriginal.HyperLink = Convert.ToString(oDbDataReader["HyperLink"]);

				if(oDbDataReader["ToolTip"] != DBNull.Value)
					oOriginal.ToolTip = Convert.ToString(oDbDataReader["ToolTip"]);
				lstOriginals.Add(oOriginal);
			}
			oDbDataReader.Close();
			return lstOriginals;
		}

		public Original GetOriginal(int OriginalId)
		{
			Original oOriginal = new Original();
			DbCommand oDbCommand = DbProviderHelper.CreateCommand("SELECTOriginal",CommandType.StoredProcedure);
			oDbCommand.Parameters.Add(DbProviderHelper.CreateParameter("@OriginalId",DbType.Int32,OriginalId));
			DbDataReader oDbDataReader = DbProviderHelper.ExecuteReader(oDbCommand);
			while (oDbDataReader.Read())
			{
				oOriginal.OriginalId = Convert.ToInt32(oDbDataReader["OriginalId"]);
				oOriginal.OriginalGuid = (Guid) oDbDataReader["OriginalGuid"];
				oOriginal.OriginalText = Convert.ToString(oDbDataReader["OriginalText"]);

				if(oDbDataReader["HyperLink"] != DBNull.Value)
					oOriginal.HyperLink = Convert.ToString(oDbDataReader["HyperLink"]);

				if(oDbDataReader["ToolTip"] != DBNull.Value)
					oOriginal.ToolTip = Convert.ToString(oDbDataReader["ToolTip"]);
			}
			oDbDataReader.Close();
			return oOriginal;
		}

		public int AddNew(Guid OriginalGuid, string OriginalText, string HyperLink, string ToolTip)
		{
			DbCommand oDbCommand = DbProviderHelper.CreateCommand("INSERTOriginal",CommandType.StoredProcedure);
			oDbCommand.Parameters.Add(DbProviderHelper.CreateParameter("@OriginalGuid",DbType.Guid,OriginalGuid));
			oDbCommand.Parameters.Add(DbProviderHelper.CreateParameter("@OriginalText",DbType.String,OriginalText));
			if (HyperLink!=null)
				oDbCommand.Parameters.Add(DbProviderHelper.CreateParameter("@HyperLink",DbType.String,HyperLink));
			else
				oDbCommand.Parameters.Add(DbProviderHelper.CreateParameter("@HyperLink",DbType.String,DBNull.Value));
			if (ToolTip!=null)
				oDbCommand.Parameters.Add(DbProviderHelper.CreateParameter("@ToolTip",DbType.String,ToolTip));
			else
				oDbCommand.Parameters.Add(DbProviderHelper.CreateParameter("@ToolTip",DbType.String,DBNull.Value));

			return Convert.ToInt32(DbProviderHelper.ExecuteScalar(oDbCommand));
		}

		public int Update(int OriginalId, Guid OriginalGuid, string OriginalText, string HyperLink, string ToolTip)
		{

			DbCommand oDbCommand = DbProviderHelper.CreateCommand("UPDATEOriginal",CommandType.StoredProcedure);
			oDbCommand.Parameters.Add(DbProviderHelper.CreateParameter("@OriginalGuid",DbType.Guid,OriginalGuid));
			oDbCommand.Parameters.Add(DbProviderHelper.CreateParameter("@OriginalText",DbType.String,OriginalText));
			if (HyperLink!=null)
				oDbCommand.Parameters.Add(DbProviderHelper.CreateParameter("@HyperLink",DbType.String,HyperLink));
			else
				oDbCommand.Parameters.Add(DbProviderHelper.CreateParameter("@HyperLink",DbType.String,DBNull.Value));
			if (ToolTip!=null)
				oDbCommand.Parameters.Add(DbProviderHelper.CreateParameter("@ToolTip",DbType.String,ToolTip));
			else
				oDbCommand.Parameters.Add(DbProviderHelper.CreateParameter("@ToolTip",DbType.String,DBNull.Value));
			oDbCommand.Parameters.Add(DbProviderHelper.CreateParameter("@OriginalId",DbType.Int32,OriginalId));
			return DbProviderHelper.ExecuteNonQuery(oDbCommand);
		}

		public int Remove(int OriginalId)
		{
			DbCommand oDbCommand = DbProviderHelper.CreateCommand("DELETEOriginal",CommandType.StoredProcedure);
			oDbCommand.Parameters.Add(DbProviderHelper.CreateParameter("@OriginalId",DbType.Int32,OriginalId));
			return DbProviderHelper.ExecuteNonQuery(oDbCommand);
		}
	}
}
