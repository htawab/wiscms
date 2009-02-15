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


        /// <summary>
        /// 添加标记
        /// </summary>
        /// <param name="submissionGuid">标记对应的对象编号</param>
        /// <param name="requestTags">客户端传递的标记</param>
        public void AddNew(Guid submissionGuid, string requestTags)
		{
            if (!string.IsNullOrEmpty(requestTags))
            {
                // TODO:区隔标记的字符作为配置项
                string[] tags = requestTags.Split(new char[] { ' ' });
                foreach (string tagName in tags)
                {
                    using (DbCommand command = DbProviderHelper.CreateCommand("INSERTTag", CommandType.StoredProcedure))
                    {
                        command.Parameters.Add(DbProviderHelper.CreateParameter("@TagGuid", DbType.Guid, Guid.NewGuid()));
                        command.Parameters.Add(DbProviderHelper.CreateParameter("@SubmissionGuid", DbType.Guid, submissionGuid));
                        command.Parameters.Add(DbProviderHelper.CreateParameter("@TagName", DbType.String, tagName));
                        command.Parameters.Add(DbProviderHelper.CreateParameter("@Hits", DbType.Int32, 0));

                        DbProviderHelper.ExecuteNonQuery(command);
                    }
                }
            }
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
