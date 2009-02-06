using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;

namespace Wis.Website.DataManager
{
	public class TagManager
	{
		public TagManager()
		{
			DbProviderHelper.GetConnection();
		}

		public List<Tag> GetTags()
		{
			List<Tag> lstTags = new List<Tag>();
			DbCommand oDbCommand = DbProviderHelper.CreateCommand("SELECTTags",CommandType.StoredProcedure);
			DbDataReader oDbDataReader = DbProviderHelper.ExecuteReader(oDbCommand);
			while (oDbDataReader.Read())
			{
				Tag oTag = new Tag();
				oTag.TagId = Convert.ToInt32(oDbDataReader["TagId"]);
				oTag.TagGuid = (Guid) oDbDataReader["TagGuid"];
				oTag.SubmissionGuid = (Guid) oDbDataReader["SubmissionGuid"];
				oTag.TagName = Convert.ToString(oDbDataReader["TagName"]);
				oTag.Hits = Convert.ToInt32(oDbDataReader["Hits"]);
				lstTags.Add(oTag);
			}
			oDbDataReader.Close();
			return lstTags;
		}

		public Tag GetTag(int TagId)
		{
			Tag oTag = new Tag();
			DbCommand oDbCommand = DbProviderHelper.CreateCommand("SELECTTag",CommandType.StoredProcedure);
			oDbCommand.Parameters.Add(DbProviderHelper.CreateParameter("@TagId",DbType.Int32,TagId));
			DbDataReader oDbDataReader = DbProviderHelper.ExecuteReader(oDbCommand);
			while (oDbDataReader.Read())
			{
				oTag.TagId = Convert.ToInt32(oDbDataReader["TagId"]);
				oTag.TagGuid = (Guid) oDbDataReader["TagGuid"];
				oTag.SubmissionGuid = (Guid) oDbDataReader["SubmissionGuid"];
				oTag.TagName = Convert.ToString(oDbDataReader["TagName"]);
				oTag.Hits = Convert.ToInt32(oDbDataReader["Hits"]);
			}
			oDbDataReader.Close();
			return oTag;
		}

		public int AddNew(Guid TagGuid, Guid SubmissionGuid, string TagName, int Hits)
		{
			DbCommand oDbCommand = DbProviderHelper.CreateCommand("INSERTTag",CommandType.StoredProcedure);
			oDbCommand.Parameters.Add(DbProviderHelper.CreateParameter("@TagGuid",DbType.Guid,TagGuid));
			oDbCommand.Parameters.Add(DbProviderHelper.CreateParameter("@SubmissionGuid",DbType.Guid,SubmissionGuid));
			oDbCommand.Parameters.Add(DbProviderHelper.CreateParameter("@TagName",DbType.String,TagName));
			oDbCommand.Parameters.Add(DbProviderHelper.CreateParameter("@Hits",DbType.Int32,Hits));

			return Convert.ToInt32(DbProviderHelper.ExecuteScalar(oDbCommand));
		}

		public int Update(int TagId, Guid TagGuid, Guid SubmissionGuid, string TagName, int Hits)
		{

			DbCommand oDbCommand = DbProviderHelper.CreateCommand("UPDATETag",CommandType.StoredProcedure);
			oDbCommand.Parameters.Add(DbProviderHelper.CreateParameter("@TagGuid",DbType.Guid,TagGuid));
			oDbCommand.Parameters.Add(DbProviderHelper.CreateParameter("@SubmissionGuid",DbType.Guid,SubmissionGuid));
			oDbCommand.Parameters.Add(DbProviderHelper.CreateParameter("@TagName",DbType.String,TagName));
			oDbCommand.Parameters.Add(DbProviderHelper.CreateParameter("@Hits",DbType.Int32,Hits));
			oDbCommand.Parameters.Add(DbProviderHelper.CreateParameter("@TagId",DbType.Int32,TagId));
			return DbProviderHelper.ExecuteNonQuery(oDbCommand);
		}

		public int Remove(int TagId)
		{
			DbCommand oDbCommand = DbProviderHelper.CreateCommand("DELETETag",CommandType.StoredProcedure);
			oDbCommand.Parameters.Add(DbProviderHelper.CreateParameter("@TagId",DbType.Int32,TagId));
			return DbProviderHelper.ExecuteNonQuery(oDbCommand);
		}
	}
}
