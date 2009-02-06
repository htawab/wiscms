using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;

namespace Wis.Website.DataManager
{
	public class PageManager
	{
		public PageManager()
		{
			DbProviderHelper.GetConnection();
		}

		public List<Page> GetPages()
		{
			List<Page> lstPages = new List<Page>();
			DbCommand oDbCommand = DbProviderHelper.CreateCommand("SELECTPages",CommandType.StoredProcedure);
			DbDataReader oDbDataReader = DbProviderHelper.ExecuteReader(oDbCommand);
			while (oDbDataReader.Read())
			{
				Page oPage = new Page();
				oPage.PageId = Convert.ToInt32(oDbDataReader["PageId"]);
				oPage.PageGuid = (Guid) oDbDataReader["PageGuid"];

				if(oDbDataReader["MetaKeywords"] != DBNull.Value)
					oPage.MetaKeywords = Convert.ToString(oDbDataReader["MetaKeywords"]);

				if(oDbDataReader["MetaDesc"] != DBNull.Value)
					oPage.MetaDesc = Convert.ToString(oDbDataReader["MetaDesc"]);
				oPage.Title = Convert.ToString(oDbDataReader["Title"]);

				if(oDbDataReader["ContentHtml"] != DBNull.Value)
					oPage.ContentHtml = Convert.ToString(oDbDataReader["ContentHtml"]);
				oPage.TemplatePath = Convert.ToString(oDbDataReader["TemplatePath"]);
				oPage.ReleasePath = Convert.ToString(oDbDataReader["ReleasePath"]);
				oPage.Hits = Convert.ToInt32(oDbDataReader["Hits"]);

				if(oDbDataReader["DateCreated"] != DBNull.Value)
					oPage.DateCreated = Convert.ToDateTime(oDbDataReader["DateCreated"]);
				lstPages.Add(oPage);
			}
			oDbDataReader.Close();
			return lstPages;
		}

		public Page GetPage(int PageId)
		{
			Page oPage = new Page();
			DbCommand oDbCommand = DbProviderHelper.CreateCommand("SELECTPage",CommandType.StoredProcedure);
			oDbCommand.Parameters.Add(DbProviderHelper.CreateParameter("@PageId",DbType.Int32,PageId));
			DbDataReader oDbDataReader = DbProviderHelper.ExecuteReader(oDbCommand);
			while (oDbDataReader.Read())
			{
				oPage.PageId = Convert.ToInt32(oDbDataReader["PageId"]);
				oPage.PageGuid = (Guid) oDbDataReader["PageGuid"];

				if(oDbDataReader["MetaKeywords"] != DBNull.Value)
					oPage.MetaKeywords = Convert.ToString(oDbDataReader["MetaKeywords"]);

				if(oDbDataReader["MetaDesc"] != DBNull.Value)
					oPage.MetaDesc = Convert.ToString(oDbDataReader["MetaDesc"]);
				oPage.Title = Convert.ToString(oDbDataReader["Title"]);

				if(oDbDataReader["ContentHtml"] != DBNull.Value)
					oPage.ContentHtml = Convert.ToString(oDbDataReader["ContentHtml"]);
				oPage.TemplatePath = Convert.ToString(oDbDataReader["TemplatePath"]);
				oPage.ReleasePath = Convert.ToString(oDbDataReader["ReleasePath"]);
				oPage.Hits = Convert.ToInt32(oDbDataReader["Hits"]);

				if(oDbDataReader["DateCreated"] != DBNull.Value)
					oPage.DateCreated = Convert.ToDateTime(oDbDataReader["DateCreated"]);
			}
			oDbDataReader.Close();
			return oPage;
		}

		public int AddNew(Guid PageGuid, string MetaKeywords, string MetaDesc, string Title, string ContentHtml, string TemplatePath, string ReleasePath, int Hits, Nullable<DateTime> DateCreated)
		{
			DbCommand oDbCommand = DbProviderHelper.CreateCommand("INSERTPage",CommandType.StoredProcedure);
			oDbCommand.Parameters.Add(DbProviderHelper.CreateParameter("@PageGuid",DbType.Guid,PageGuid));
			if (MetaKeywords!=null)
				oDbCommand.Parameters.Add(DbProviderHelper.CreateParameter("@MetaKeywords",DbType.String,MetaKeywords));
			else
				oDbCommand.Parameters.Add(DbProviderHelper.CreateParameter("@MetaKeywords",DbType.String,DBNull.Value));
			if (MetaDesc!=null)
				oDbCommand.Parameters.Add(DbProviderHelper.CreateParameter("@MetaDesc",DbType.String,MetaDesc));
			else
				oDbCommand.Parameters.Add(DbProviderHelper.CreateParameter("@MetaDesc",DbType.String,DBNull.Value));
			oDbCommand.Parameters.Add(DbProviderHelper.CreateParameter("@Title",DbType.String,Title));
			if (ContentHtml!=null)
				oDbCommand.Parameters.Add(DbProviderHelper.CreateParameter("@ContentHtml",DbType.String,ContentHtml));
			else
				oDbCommand.Parameters.Add(DbProviderHelper.CreateParameter("@ContentHtml",DbType.String,DBNull.Value));
			oDbCommand.Parameters.Add(DbProviderHelper.CreateParameter("@TemplatePath",DbType.String,TemplatePath));
			oDbCommand.Parameters.Add(DbProviderHelper.CreateParameter("@ReleasePath",DbType.String,ReleasePath));
			oDbCommand.Parameters.Add(DbProviderHelper.CreateParameter("@Hits",DbType.Int32,Hits));
			if (DateCreated.HasValue)
				oDbCommand.Parameters.Add(DbProviderHelper.CreateParameter("@DateCreated",DbType.DateTime,DateCreated));
			else
				oDbCommand.Parameters.Add(DbProviderHelper.CreateParameter("@DateCreated",DbType.DateTime,DBNull.Value));

			return Convert.ToInt32(DbProviderHelper.ExecuteScalar(oDbCommand));
		}

		public int Update(int PageId, Guid PageGuid, string MetaKeywords, string MetaDesc, string Title, string ContentHtml, string TemplatePath, string ReleasePath, int Hits, Nullable<DateTime> DateCreated)
		{

			DbCommand oDbCommand = DbProviderHelper.CreateCommand("UPDATEPage",CommandType.StoredProcedure);
			oDbCommand.Parameters.Add(DbProviderHelper.CreateParameter("@PageGuid",DbType.Guid,PageGuid));
			if (MetaKeywords!=null)
				oDbCommand.Parameters.Add(DbProviderHelper.CreateParameter("@MetaKeywords",DbType.String,MetaKeywords));
			else
				oDbCommand.Parameters.Add(DbProviderHelper.CreateParameter("@MetaKeywords",DbType.String,DBNull.Value));
			if (MetaDesc!=null)
				oDbCommand.Parameters.Add(DbProviderHelper.CreateParameter("@MetaDesc",DbType.String,MetaDesc));
			else
				oDbCommand.Parameters.Add(DbProviderHelper.CreateParameter("@MetaDesc",DbType.String,DBNull.Value));
			oDbCommand.Parameters.Add(DbProviderHelper.CreateParameter("@Title",DbType.String,Title));
			if (ContentHtml!=null)
				oDbCommand.Parameters.Add(DbProviderHelper.CreateParameter("@ContentHtml",DbType.String,ContentHtml));
			else
				oDbCommand.Parameters.Add(DbProviderHelper.CreateParameter("@ContentHtml",DbType.String,DBNull.Value));
			oDbCommand.Parameters.Add(DbProviderHelper.CreateParameter("@TemplatePath",DbType.String,TemplatePath));
			oDbCommand.Parameters.Add(DbProviderHelper.CreateParameter("@ReleasePath",DbType.String,ReleasePath));
			oDbCommand.Parameters.Add(DbProviderHelper.CreateParameter("@Hits",DbType.Int32,Hits));
			if (DateCreated.HasValue)
				oDbCommand.Parameters.Add(DbProviderHelper.CreateParameter("@DateCreated",DbType.DateTime,DateCreated));
			else
				oDbCommand.Parameters.Add(DbProviderHelper.CreateParameter("@DateCreated",DbType.DateTime,DBNull.Value));
			oDbCommand.Parameters.Add(DbProviderHelper.CreateParameter("@PageId",DbType.Int32,PageId));
			return DbProviderHelper.ExecuteNonQuery(oDbCommand);
		}

		public int Remove(int PageId)
		{
			DbCommand oDbCommand = DbProviderHelper.CreateCommand("DELETEPage",CommandType.StoredProcedure);
			oDbCommand.Parameters.Add(DbProviderHelper.CreateParameter("@PageId",DbType.Int32,PageId));
			return DbProviderHelper.ExecuteNonQuery(oDbCommand);
		}
	}
}
