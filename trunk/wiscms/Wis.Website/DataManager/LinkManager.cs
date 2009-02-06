using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;

namespace Wis.Website.DataManager
{
	public class LinkManager
	{
		public LinkManager()
		{
			DbProviderHelper.GetConnection();
		}

		public List<Link> GetLinks()
		{
			List<Link> lstLinks = new List<Link>();
			DbCommand oDbCommand = DbProviderHelper.CreateCommand("SELECTLinks",CommandType.StoredProcedure);
			DbDataReader oDbDataReader = DbProviderHelper.ExecuteReader(oDbCommand);
			while (oDbDataReader.Read())
			{
				Link oLink = new Link();
				oLink.LinkId = Convert.ToInt32(oDbDataReader["LinkId"]);
				oLink.LinkGuid = (Guid) oDbDataReader["LinkGuid"];
				oLink.LinkName = Convert.ToString(oDbDataReader["LinkName"]);

				if(oDbDataReader["LinkUrl"] != DBNull.Value)
					oLink.LinkUrl = Convert.ToString(oDbDataReader["LinkUrl"]);

				if(oDbDataReader["Remark"] != DBNull.Value)
					oLink.Remark = Convert.ToString(oDbDataReader["Remark"]);

				if(oDbDataReader["Logo"] != DBNull.Value)
					oLink.Logo = Convert.ToString(oDbDataReader["Logo"]);
				oLink.Rank = Convert.ToInt32(oDbDataReader["Rank"]);
				oLink.DateCreated = Convert.ToDateTime(oDbDataReader["DateCreated"]);
				lstLinks.Add(oLink);
			}
			oDbDataReader.Close();
			return lstLinks;
		}

		public Link GetLink(int LinkId)
		{
			Link oLink = new Link();
			DbCommand oDbCommand = DbProviderHelper.CreateCommand("SELECTLink",CommandType.StoredProcedure);
			oDbCommand.Parameters.Add(DbProviderHelper.CreateParameter("@LinkId",DbType.Int32,LinkId));
			DbDataReader oDbDataReader = DbProviderHelper.ExecuteReader(oDbCommand);
			while (oDbDataReader.Read())
			{
				oLink.LinkId = Convert.ToInt32(oDbDataReader["LinkId"]);
				oLink.LinkGuid = (Guid) oDbDataReader["LinkGuid"];
				oLink.LinkName = Convert.ToString(oDbDataReader["LinkName"]);

				if(oDbDataReader["LinkUrl"] != DBNull.Value)
					oLink.LinkUrl = Convert.ToString(oDbDataReader["LinkUrl"]);

				if(oDbDataReader["Remark"] != DBNull.Value)
					oLink.Remark = Convert.ToString(oDbDataReader["Remark"]);

				if(oDbDataReader["Logo"] != DBNull.Value)
					oLink.Logo = Convert.ToString(oDbDataReader["Logo"]);
				oLink.Rank = Convert.ToInt32(oDbDataReader["Rank"]);
				oLink.DateCreated = Convert.ToDateTime(oDbDataReader["DateCreated"]);
			}
			oDbDataReader.Close();
			return oLink;
		}

		public int AddNew(Guid LinkGuid, string LinkName, string LinkUrl, string Remark, string Logo, int Rank, DateTime DateCreated)
		{
			DbCommand oDbCommand = DbProviderHelper.CreateCommand("INSERTLink",CommandType.StoredProcedure);
			oDbCommand.Parameters.Add(DbProviderHelper.CreateParameter("@LinkGuid",DbType.Guid,LinkGuid));
			oDbCommand.Parameters.Add(DbProviderHelper.CreateParameter("@LinkName",DbType.String,LinkName));
			if (LinkUrl!=null)
				oDbCommand.Parameters.Add(DbProviderHelper.CreateParameter("@LinkUrl",DbType.String,LinkUrl));
			else
				oDbCommand.Parameters.Add(DbProviderHelper.CreateParameter("@LinkUrl",DbType.String,DBNull.Value));
			if (Remark!=null)
				oDbCommand.Parameters.Add(DbProviderHelper.CreateParameter("@Remark",DbType.String,Remark));
			else
				oDbCommand.Parameters.Add(DbProviderHelper.CreateParameter("@Remark",DbType.String,DBNull.Value));
			if (Logo!=null)
				oDbCommand.Parameters.Add(DbProviderHelper.CreateParameter("@Logo",DbType.String,Logo));
			else
				oDbCommand.Parameters.Add(DbProviderHelper.CreateParameter("@Logo",DbType.String,DBNull.Value));
			oDbCommand.Parameters.Add(DbProviderHelper.CreateParameter("@Rank",DbType.Int32,Rank));
			oDbCommand.Parameters.Add(DbProviderHelper.CreateParameter("@DateCreated",DbType.DateTime,DateCreated));

			return Convert.ToInt32(DbProviderHelper.ExecuteScalar(oDbCommand));
		}

		public int Update(int LinkId, Guid LinkGuid, string LinkName, string LinkUrl, string Remark, string Logo, int Rank, DateTime DateCreated)
		{

			DbCommand oDbCommand = DbProviderHelper.CreateCommand("UPDATELink",CommandType.StoredProcedure);
			oDbCommand.Parameters.Add(DbProviderHelper.CreateParameter("@LinkGuid",DbType.Guid,LinkGuid));
			oDbCommand.Parameters.Add(DbProviderHelper.CreateParameter("@LinkName",DbType.String,LinkName));
			if (LinkUrl!=null)
				oDbCommand.Parameters.Add(DbProviderHelper.CreateParameter("@LinkUrl",DbType.String,LinkUrl));
			else
				oDbCommand.Parameters.Add(DbProviderHelper.CreateParameter("@LinkUrl",DbType.String,DBNull.Value));
			if (Remark!=null)
				oDbCommand.Parameters.Add(DbProviderHelper.CreateParameter("@Remark",DbType.String,Remark));
			else
				oDbCommand.Parameters.Add(DbProviderHelper.CreateParameter("@Remark",DbType.String,DBNull.Value));
			if (Logo!=null)
				oDbCommand.Parameters.Add(DbProviderHelper.CreateParameter("@Logo",DbType.String,Logo));
			else
				oDbCommand.Parameters.Add(DbProviderHelper.CreateParameter("@Logo",DbType.String,DBNull.Value));
			oDbCommand.Parameters.Add(DbProviderHelper.CreateParameter("@Rank",DbType.Int32,Rank));
			oDbCommand.Parameters.Add(DbProviderHelper.CreateParameter("@DateCreated",DbType.DateTime,DateCreated));
			oDbCommand.Parameters.Add(DbProviderHelper.CreateParameter("@LinkId",DbType.Int32,LinkId));
			return DbProviderHelper.ExecuteNonQuery(oDbCommand);
		}

		public int Remove(int LinkId)
		{
			DbCommand oDbCommand = DbProviderHelper.CreateCommand("DELETELink",CommandType.StoredProcedure);
			oDbCommand.Parameters.Add(DbProviderHelper.CreateParameter("@LinkId",DbType.Int32,LinkId));
			return DbProviderHelper.ExecuteNonQuery(oDbCommand);
		}
	}
}
