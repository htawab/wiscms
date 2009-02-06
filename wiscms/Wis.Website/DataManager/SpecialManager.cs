using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;

namespace Wis.Website.DataManager
{
	public class SpecialManager
	{
		public SpecialManager()
		{
			DbProviderHelper.GetConnection();
		}

		public List<Special> GetSpecials()
		{
			List<Special> lstSpecials = new List<Special>();
			DbCommand oDbCommand = DbProviderHelper.CreateCommand("SELECTSpecials",CommandType.StoredProcedure);
			DbDataReader oDbDataReader = DbProviderHelper.ExecuteReader(oDbCommand);
			while (oDbDataReader.Read())
			{
				Special oSpecial = new Special();
				oSpecial.SpecialId = Convert.ToInt32(oDbDataReader["SpecialId"]);
				oSpecial.SpecialGuid = (Guid) oDbDataReader["SpecialGuid"];
				oSpecial.Title = Convert.ToString(oDbDataReader["Title"]);

				if(oDbDataReader["ContentHtml"] != DBNull.Value)
					oSpecial.ContentHtml = Convert.ToString(oDbDataReader["ContentHtml"]);
				oSpecial.TemplatePath = Convert.ToString(oDbDataReader["TemplatePath"]);
				oSpecial.ReleasePath = Convert.ToString(oDbDataReader["ReleasePath"]);

				if(oDbDataReader["ImagePath"] != DBNull.Value)
					oSpecial.ImagePath = Convert.ToString(oDbDataReader["ImagePath"]);

				if(oDbDataReader["ImageWidth"] != DBNull.Value)
					oSpecial.ImageWidth = Convert.ToInt32(oDbDataReader["ImageWidth"]);

				if(oDbDataReader["ImageHeight"] != DBNull.Value)
					oSpecial.ImageHeight = Convert.ToInt32(oDbDataReader["ImageHeight"]);
				oSpecial.Hits = Convert.ToInt32(oDbDataReader["Hits"]);
				oSpecial.Comments = Convert.ToInt32(oDbDataReader["Comments"]);
				lstSpecials.Add(oSpecial);
			}
			oDbDataReader.Close();
			return lstSpecials;
		}

		public Special GetSpecial(int SpecialId)
		{
			Special oSpecial = new Special();
			DbCommand oDbCommand = DbProviderHelper.CreateCommand("SELECTSpecial",CommandType.StoredProcedure);
			oDbCommand.Parameters.Add(DbProviderHelper.CreateParameter("@SpecialId",DbType.Int32,SpecialId));
			DbDataReader oDbDataReader = DbProviderHelper.ExecuteReader(oDbCommand);
			while (oDbDataReader.Read())
			{
				oSpecial.SpecialId = Convert.ToInt32(oDbDataReader["SpecialId"]);
				oSpecial.SpecialGuid = (Guid) oDbDataReader["SpecialGuid"];
				oSpecial.Title = Convert.ToString(oDbDataReader["Title"]);

				if(oDbDataReader["ContentHtml"] != DBNull.Value)
					oSpecial.ContentHtml = Convert.ToString(oDbDataReader["ContentHtml"]);
				oSpecial.TemplatePath = Convert.ToString(oDbDataReader["TemplatePath"]);
				oSpecial.ReleasePath = Convert.ToString(oDbDataReader["ReleasePath"]);

				if(oDbDataReader["ImagePath"] != DBNull.Value)
					oSpecial.ImagePath = Convert.ToString(oDbDataReader["ImagePath"]);

				if(oDbDataReader["ImageWidth"] != DBNull.Value)
					oSpecial.ImageWidth = Convert.ToInt32(oDbDataReader["ImageWidth"]);

				if(oDbDataReader["ImageHeight"] != DBNull.Value)
					oSpecial.ImageHeight = Convert.ToInt32(oDbDataReader["ImageHeight"]);
				oSpecial.Hits = Convert.ToInt32(oDbDataReader["Hits"]);
				oSpecial.Comments = Convert.ToInt32(oDbDataReader["Comments"]);
			}
			oDbDataReader.Close();
			return oSpecial;
		}

		public int AddNew(int SpecialId, Guid SpecialGuid, string Title, string ContentHtml, string TemplatePath, string ReleasePath, string ImagePath, Nullable<int> ImageWidth, Nullable<int> ImageHeight, int Hits, int Comments)
		{
			DbCommand oDbCommand = DbProviderHelper.CreateCommand("INSERTSpecial",CommandType.StoredProcedure);
			oDbCommand.Parameters.Add(DbProviderHelper.CreateParameter("@SpecialGuid",DbType.Guid,SpecialGuid));
			oDbCommand.Parameters.Add(DbProviderHelper.CreateParameter("@Title",DbType.String,Title));
			if (ContentHtml!=null)
				oDbCommand.Parameters.Add(DbProviderHelper.CreateParameter("@ContentHtml",DbType.String,ContentHtml));
			else
				oDbCommand.Parameters.Add(DbProviderHelper.CreateParameter("@ContentHtml",DbType.String,DBNull.Value));
			oDbCommand.Parameters.Add(DbProviderHelper.CreateParameter("@TemplatePath",DbType.String,TemplatePath));
			oDbCommand.Parameters.Add(DbProviderHelper.CreateParameter("@ReleasePath",DbType.String,ReleasePath));
			if (ImagePath!=null)
				oDbCommand.Parameters.Add(DbProviderHelper.CreateParameter("@ImagePath",DbType.String,ImagePath));
			else
				oDbCommand.Parameters.Add(DbProviderHelper.CreateParameter("@ImagePath",DbType.String,DBNull.Value));
			if (ImageWidth.HasValue)
				oDbCommand.Parameters.Add(DbProviderHelper.CreateParameter("@ImageWidth",DbType.Int32,ImageWidth));
			else
				oDbCommand.Parameters.Add(DbProviderHelper.CreateParameter("@ImageWidth",DbType.Int32,DBNull.Value));
			if (ImageHeight.HasValue)
				oDbCommand.Parameters.Add(DbProviderHelper.CreateParameter("@ImageHeight",DbType.Int32,ImageHeight));
			else
				oDbCommand.Parameters.Add(DbProviderHelper.CreateParameter("@ImageHeight",DbType.Int32,DBNull.Value));
			oDbCommand.Parameters.Add(DbProviderHelper.CreateParameter("@Hits",DbType.Int32,Hits));
			oDbCommand.Parameters.Add(DbProviderHelper.CreateParameter("@Comments",DbType.Int32,Comments));

			return DbProviderHelper.ExecuteNonQuery(oDbCommand);
		}

		public int Update(int SpecialId, Guid SpecialGuid, string Title, string ContentHtml, string TemplatePath, string ReleasePath, string ImagePath, Nullable<int> ImageWidth, Nullable<int> ImageHeight, int Hits, int Comments)
		{

			DbCommand oDbCommand = DbProviderHelper.CreateCommand("UPDATESpecial",CommandType.StoredProcedure);
			oDbCommand.Parameters.Add(DbProviderHelper.CreateParameter("@SpecialGuid",DbType.Guid,SpecialGuid));
			oDbCommand.Parameters.Add(DbProviderHelper.CreateParameter("@Title",DbType.String,Title));
			if (ContentHtml!=null)
				oDbCommand.Parameters.Add(DbProviderHelper.CreateParameter("@ContentHtml",DbType.String,ContentHtml));
			else
				oDbCommand.Parameters.Add(DbProviderHelper.CreateParameter("@ContentHtml",DbType.String,DBNull.Value));
			oDbCommand.Parameters.Add(DbProviderHelper.CreateParameter("@TemplatePath",DbType.String,TemplatePath));
			oDbCommand.Parameters.Add(DbProviderHelper.CreateParameter("@ReleasePath",DbType.String,ReleasePath));
			if (ImagePath!=null)
				oDbCommand.Parameters.Add(DbProviderHelper.CreateParameter("@ImagePath",DbType.String,ImagePath));
			else
				oDbCommand.Parameters.Add(DbProviderHelper.CreateParameter("@ImagePath",DbType.String,DBNull.Value));
			if (ImageWidth.HasValue)
				oDbCommand.Parameters.Add(DbProviderHelper.CreateParameter("@ImageWidth",DbType.Int32,ImageWidth));
			else
				oDbCommand.Parameters.Add(DbProviderHelper.CreateParameter("@ImageWidth",DbType.Int32,DBNull.Value));
			if (ImageHeight.HasValue)
				oDbCommand.Parameters.Add(DbProviderHelper.CreateParameter("@ImageHeight",DbType.Int32,ImageHeight));
			else
				oDbCommand.Parameters.Add(DbProviderHelper.CreateParameter("@ImageHeight",DbType.Int32,DBNull.Value));
			oDbCommand.Parameters.Add(DbProviderHelper.CreateParameter("@Hits",DbType.Int32,Hits));
			oDbCommand.Parameters.Add(DbProviderHelper.CreateParameter("@Comments",DbType.Int32,Comments));
			oDbCommand.Parameters.Add(DbProviderHelper.CreateParameter("@SpecialId",DbType.Int32,SpecialId));
			return DbProviderHelper.ExecuteNonQuery(oDbCommand);
		}

		public int Remove(int SpecialId)
		{
			DbCommand oDbCommand = DbProviderHelper.CreateCommand("DELETESpecial",CommandType.StoredProcedure);
			oDbCommand.Parameters.Add(DbProviderHelper.CreateParameter("@SpecialId",DbType.Int32,SpecialId));
			return DbProviderHelper.ExecuteNonQuery(oDbCommand);
		}
	}
}
