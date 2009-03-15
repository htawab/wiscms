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
			List<Page> pages = new List<Page>();
			DbCommand command = DbProviderHelper.CreateCommand("SELECTPages",CommandType.StoredProcedure);
			DbDataReader dataReader = DbProviderHelper.ExecuteReader(command);
			while (dataReader.Read())
			{
				Page page = new Page();
				page.PageId = Convert.ToInt32(dataReader["PageId"]);
				page.PageGuid = (Guid) dataReader["PageGuid"];

				if(dataReader["MetaKeywords"] != DBNull.Value)
					page.MetaKeywords = Convert.ToString(dataReader["MetaKeywords"]);

				if(dataReader["MetaDesc"] != DBNull.Value)
					page.MetaDesc = Convert.ToString(dataReader["MetaDesc"]);
				page.Title = Convert.ToString(dataReader["Title"]);

				if(dataReader["ContentHtml"] != DBNull.Value)
					page.ContentHtml = Convert.ToString(dataReader["ContentHtml"]);
				page.TemplatePath = Convert.ToString(dataReader["TemplatePath"]);
				page.ReleasePath = Convert.ToString(dataReader["ReleasePath"]);
				page.Hits = Convert.ToInt32(dataReader["Hits"]);

				if(dataReader["DateCreated"] != DBNull.Value)
					page.DateCreated = Convert.ToDateTime(dataReader["DateCreated"]);
				pages.Add(page);
			}
			dataReader.Close();
			return pages;
		}

        public List<Page> GetPagesByReleaseGuid(Guid releaseGuid)
        {
            List<Page> pages = new List<Page>();
            DbCommand command = DbProviderHelper.CreateCommand("SelectPagesByReleaseGuid", CommandType.StoredProcedure);
            command.Parameters.Add(DbProviderHelper.CreateParameter("@ReleaseGuid", DbType.Guid, releaseGuid));
            DbDataReader dataReader = DbProviderHelper.ExecuteReader(command);
            while (dataReader.Read())
            {
                Page page = new Page();
                page.PageId = Convert.ToInt32(dataReader["PageId"]);
                page.PageGuid = (Guid)dataReader["PageGuid"];

                if (dataReader["MetaKeywords"] != DBNull.Value)
                    page.MetaKeywords = Convert.ToString(dataReader["MetaKeywords"]);

                if (dataReader["MetaDesc"] != DBNull.Value)
                    page.MetaDesc = Convert.ToString(dataReader["MetaDesc"]);
                page.Title = Convert.ToString(dataReader["Title"]);

                if (dataReader["ContentHtml"] != DBNull.Value)
                    page.ContentHtml = Convert.ToString(dataReader["ContentHtml"]);
                page.TemplatePath = Convert.ToString(dataReader["TemplatePath"]);
                page.ReleasePath = Convert.ToString(dataReader["ReleasePath"]);
                page.Hits = Convert.ToInt32(dataReader["Hits"]);

                if (dataReader["DateCreated"] != DBNull.Value)
                    page.DateCreated = Convert.ToDateTime(dataReader["DateCreated"]);
                pages.Add(page);
            }
            dataReader.Close();
            return pages;
        }


		public Page GetPage(int PageId)
		{
			Page page = new Page();
			DbCommand command = DbProviderHelper.CreateCommand("SELECTPage",CommandType.StoredProcedure);
			command.Parameters.Add(DbProviderHelper.CreateParameter("@PageId",DbType.Int32,PageId));
			DbDataReader dataReader = DbProviderHelper.ExecuteReader(command);
			while (dataReader.Read())
			{
				page.PageId = Convert.ToInt32(dataReader["PageId"]);
				page.PageGuid = (Guid) dataReader["PageGuid"];

				if(dataReader["MetaKeywords"] != DBNull.Value)
					page.MetaKeywords = Convert.ToString(dataReader["MetaKeywords"]);

				if(dataReader["MetaDesc"] != DBNull.Value)
					page.MetaDesc = Convert.ToString(dataReader["MetaDesc"]);
				page.Title = Convert.ToString(dataReader["Title"]);

				if(dataReader["ContentHtml"] != DBNull.Value)
					page.ContentHtml = Convert.ToString(dataReader["ContentHtml"]);
				page.TemplatePath = Convert.ToString(dataReader["TemplatePath"]);
				page.ReleasePath = Convert.ToString(dataReader["ReleasePath"]);
				page.Hits = Convert.ToInt32(dataReader["Hits"]);

				if(dataReader["DateCreated"] != DBNull.Value)
					page.DateCreated = Convert.ToDateTime(dataReader["DateCreated"]);
			}
			dataReader.Close();
			return page;
		}

		public int AddNew(Guid PageGuid, string MetaKeywords, string MetaDesc, string Title, string ContentHtml, string TemplatePath, string ReleasePath, int Hits, Nullable<DateTime> DateCreated)
		{
			DbCommand command = DbProviderHelper.CreateCommand("INSERTPage",CommandType.StoredProcedure);
			command.Parameters.Add(DbProviderHelper.CreateParameter("@PageGuid",DbType.Guid,PageGuid));
			if (MetaKeywords!=null)
				command.Parameters.Add(DbProviderHelper.CreateParameter("@MetaKeywords",DbType.String,MetaKeywords));
			else
				command.Parameters.Add(DbProviderHelper.CreateParameter("@MetaKeywords",DbType.String,DBNull.Value));
			if (MetaDesc!=null)
				command.Parameters.Add(DbProviderHelper.CreateParameter("@MetaDesc",DbType.String,MetaDesc));
			else
				command.Parameters.Add(DbProviderHelper.CreateParameter("@MetaDesc",DbType.String,DBNull.Value));
			command.Parameters.Add(DbProviderHelper.CreateParameter("@Title",DbType.String,Title));
			if (ContentHtml!=null)
				command.Parameters.Add(DbProviderHelper.CreateParameter("@ContentHtml",DbType.String,ContentHtml));
			else
				command.Parameters.Add(DbProviderHelper.CreateParameter("@ContentHtml",DbType.String,DBNull.Value));
			command.Parameters.Add(DbProviderHelper.CreateParameter("@TemplatePath",DbType.String,TemplatePath));
			command.Parameters.Add(DbProviderHelper.CreateParameter("@ReleasePath",DbType.String,ReleasePath));
			command.Parameters.Add(DbProviderHelper.CreateParameter("@Hits",DbType.Int32,Hits));
			if (DateCreated.HasValue)
				command.Parameters.Add(DbProviderHelper.CreateParameter("@DateCreated",DbType.DateTime,DateCreated));
			else
				command.Parameters.Add(DbProviderHelper.CreateParameter("@DateCreated",DbType.DateTime,DBNull.Value));

			return Convert.ToInt32(DbProviderHelper.ExecuteScalar(command));
		}

		public int Update(int PageId, Guid PageGuid, string MetaKeywords, string MetaDesc, string Title, string ContentHtml, string TemplatePath, string ReleasePath, int Hits, Nullable<DateTime> DateCreated)
		{

			DbCommand command = DbProviderHelper.CreateCommand("UPDATEPage",CommandType.StoredProcedure);
			command.Parameters.Add(DbProviderHelper.CreateParameter("@PageGuid",DbType.Guid,PageGuid));
			if (MetaKeywords!=null)
				command.Parameters.Add(DbProviderHelper.CreateParameter("@MetaKeywords",DbType.String,MetaKeywords));
			else
				command.Parameters.Add(DbProviderHelper.CreateParameter("@MetaKeywords",DbType.String,DBNull.Value));
			if (MetaDesc!=null)
				command.Parameters.Add(DbProviderHelper.CreateParameter("@MetaDesc",DbType.String,MetaDesc));
			else
				command.Parameters.Add(DbProviderHelper.CreateParameter("@MetaDesc",DbType.String,DBNull.Value));
			command.Parameters.Add(DbProviderHelper.CreateParameter("@Title",DbType.String,Title));
			if (ContentHtml!=null)
				command.Parameters.Add(DbProviderHelper.CreateParameter("@ContentHtml",DbType.String,ContentHtml));
			else
				command.Parameters.Add(DbProviderHelper.CreateParameter("@ContentHtml",DbType.String,DBNull.Value));
			command.Parameters.Add(DbProviderHelper.CreateParameter("@TemplatePath",DbType.String,TemplatePath));
			command.Parameters.Add(DbProviderHelper.CreateParameter("@ReleasePath",DbType.String,ReleasePath));
			command.Parameters.Add(DbProviderHelper.CreateParameter("@Hits",DbType.Int32,Hits));
			if (DateCreated.HasValue)
				command.Parameters.Add(DbProviderHelper.CreateParameter("@DateCreated",DbType.DateTime,DateCreated));
			else
				command.Parameters.Add(DbProviderHelper.CreateParameter("@DateCreated",DbType.DateTime,DBNull.Value));
			command.Parameters.Add(DbProviderHelper.CreateParameter("@PageId",DbType.Int32,PageId));
			return DbProviderHelper.ExecuteNonQuery(command);
		}

		public int Remove(int PageId)
		{
			DbCommand command = DbProviderHelper.CreateCommand("DELETEPage",CommandType.StoredProcedure);
			command.Parameters.Add(DbProviderHelper.CreateParameter("@PageId",DbType.Int32,PageId));
			return DbProviderHelper.ExecuteNonQuery(command);
		}
	}
}
