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
			List<Special> specials = new List<Special>();
			DbCommand command = DbProviderHelper.CreateCommand("SELECTSpecials",CommandType.StoredProcedure);
			DbDataReader dataReader = DbProviderHelper.ExecuteReader(command);
			while (dataReader.Read())
			{
				Special special = new Special();
				special.SpecialId = Convert.ToInt32(dataReader["SpecialId"]);
				special.SpecialGuid = (Guid) dataReader["SpecialGuid"];
				special.Title = Convert.ToString(dataReader["Title"]);

				if(dataReader["ContentHtml"] != DBNull.Value)
					special.ContentHtml = Convert.ToString(dataReader["ContentHtml"]);
				special.TemplatePath = Convert.ToString(dataReader["TemplatePath"]);
				special.ReleasePath = Convert.ToString(dataReader["ReleasePath"]);

				if(dataReader["ImagePath"] != DBNull.Value)
					special.ImagePath = Convert.ToString(dataReader["ImagePath"]);

				if(dataReader["ImageWidth"] != DBNull.Value)
					special.ImageWidth = Convert.ToInt32(dataReader["ImageWidth"]);

				if(dataReader["ImageHeight"] != DBNull.Value)
					special.ImageHeight = Convert.ToInt32(dataReader["ImageHeight"]);
				special.Hits = Convert.ToInt32(dataReader["Hits"]);
				special.Comments = Convert.ToInt32(dataReader["Comments"]);
				specials.Add(special);
			}
			dataReader.Close();
			return specials;
		}


        public List<Special> GetSpecialsByReleaseGuid(Guid releaseGuid)
        {
            List<Special> specials = new List<Special>();
            DbCommand command = DbProviderHelper.CreateCommand("SelectSpecialsByReleaseGuid", CommandType.StoredProcedure);
            command.Parameters.Add(DbProviderHelper.CreateParameter("@ReleaseGuid", DbType.Guid, releaseGuid));
            DbDataReader dataReader = DbProviderHelper.ExecuteReader(command);
            while (dataReader.Read())
            {
                Special special = new Special();
                special.SpecialId = Convert.ToInt32(dataReader["SpecialId"]);
                special.SpecialGuid = (Guid)dataReader["SpecialGuid"];
                special.Title = Convert.ToString(dataReader["Title"]);

                if (dataReader["ContentHtml"] != DBNull.Value)
                    special.ContentHtml = Convert.ToString(dataReader["ContentHtml"]);
                special.TemplatePath = Convert.ToString(dataReader["TemplatePath"]);
                special.ReleasePath = Convert.ToString(dataReader["ReleasePath"]);

                if (dataReader["ImagePath"] != DBNull.Value)
                    special.ImagePath = Convert.ToString(dataReader["ImagePath"]);

                if (dataReader["ImageWidth"] != DBNull.Value)
                    special.ImageWidth = Convert.ToInt32(dataReader["ImageWidth"]);

                if (dataReader["ImageHeight"] != DBNull.Value)
                    special.ImageHeight = Convert.ToInt32(dataReader["ImageHeight"]);
                special.Hits = Convert.ToInt32(dataReader["Hits"]);
                special.Comments = Convert.ToInt32(dataReader["Comments"]);
                specials.Add(special);
            }
            dataReader.Close();
            return specials;
        }


		public Special GetSpecial(int SpecialId)
		{
			Special special = new Special();
			DbCommand command = DbProviderHelper.CreateCommand("SELECTSpecial",CommandType.StoredProcedure);
			command.Parameters.Add(DbProviderHelper.CreateParameter("@SpecialId",DbType.Int32,SpecialId));
			DbDataReader dataReader = DbProviderHelper.ExecuteReader(command);
			while (dataReader.Read())
			{
				special.SpecialId = Convert.ToInt32(dataReader["SpecialId"]);
				special.SpecialGuid = (Guid) dataReader["SpecialGuid"];
				special.Title = Convert.ToString(dataReader["Title"]);

				if(dataReader["ContentHtml"] != DBNull.Value)
					special.ContentHtml = Convert.ToString(dataReader["ContentHtml"]);
				special.TemplatePath = Convert.ToString(dataReader["TemplatePath"]);
				special.ReleasePath = Convert.ToString(dataReader["ReleasePath"]);

				if(dataReader["ImagePath"] != DBNull.Value)
					special.ImagePath = Convert.ToString(dataReader["ImagePath"]);

				if(dataReader["ImageWidth"] != DBNull.Value)
					special.ImageWidth = Convert.ToInt32(dataReader["ImageWidth"]);

				if(dataReader["ImageHeight"] != DBNull.Value)
					special.ImageHeight = Convert.ToInt32(dataReader["ImageHeight"]);
				special.Hits = Convert.ToInt32(dataReader["Hits"]);
				special.Comments = Convert.ToInt32(dataReader["Comments"]);
			}
			dataReader.Close();
			return special;
		}

		public int AddNew(int SpecialId, Guid SpecialGuid, string Title, string ContentHtml, string TemplatePath, string ReleasePath, string ImagePath, Nullable<int> ImageWidth, Nullable<int> ImageHeight, int Hits, int Comments)
		{
			DbCommand command = DbProviderHelper.CreateCommand("INSERTSpecial",CommandType.StoredProcedure);
			command.Parameters.Add(DbProviderHelper.CreateParameter("@SpecialGuid",DbType.Guid,SpecialGuid));
			command.Parameters.Add(DbProviderHelper.CreateParameter("@Title",DbType.String,Title));
			if (ContentHtml!=null)
				command.Parameters.Add(DbProviderHelper.CreateParameter("@ContentHtml",DbType.String,ContentHtml));
			else
				command.Parameters.Add(DbProviderHelper.CreateParameter("@ContentHtml",DbType.String,DBNull.Value));
			command.Parameters.Add(DbProviderHelper.CreateParameter("@TemplatePath",DbType.String,TemplatePath));
			command.Parameters.Add(DbProviderHelper.CreateParameter("@ReleasePath",DbType.String,ReleasePath));
			if (ImagePath!=null)
				command.Parameters.Add(DbProviderHelper.CreateParameter("@ImagePath",DbType.String,ImagePath));
			else
				command.Parameters.Add(DbProviderHelper.CreateParameter("@ImagePath",DbType.String,DBNull.Value));
			if (ImageWidth.HasValue)
				command.Parameters.Add(DbProviderHelper.CreateParameter("@ImageWidth",DbType.Int32,ImageWidth));
			else
				command.Parameters.Add(DbProviderHelper.CreateParameter("@ImageWidth",DbType.Int32,DBNull.Value));
			if (ImageHeight.HasValue)
				command.Parameters.Add(DbProviderHelper.CreateParameter("@ImageHeight",DbType.Int32,ImageHeight));
			else
				command.Parameters.Add(DbProviderHelper.CreateParameter("@ImageHeight",DbType.Int32,DBNull.Value));
			command.Parameters.Add(DbProviderHelper.CreateParameter("@Hits",DbType.Int32,Hits));
			command.Parameters.Add(DbProviderHelper.CreateParameter("@Comments",DbType.Int32,Comments));

			return DbProviderHelper.ExecuteNonQuery(command);
		}

		public int Update(int SpecialId, Guid SpecialGuid, string Title, string ContentHtml, string TemplatePath, string ReleasePath, string ImagePath, Nullable<int> ImageWidth, Nullable<int> ImageHeight, int Hits, int Comments)
		{

			DbCommand command = DbProviderHelper.CreateCommand("UPDATESpecial",CommandType.StoredProcedure);
			command.Parameters.Add(DbProviderHelper.CreateParameter("@SpecialGuid",DbType.Guid,SpecialGuid));
			command.Parameters.Add(DbProviderHelper.CreateParameter("@Title",DbType.String,Title));
			if (ContentHtml!=null)
				command.Parameters.Add(DbProviderHelper.CreateParameter("@ContentHtml",DbType.String,ContentHtml));
			else
				command.Parameters.Add(DbProviderHelper.CreateParameter("@ContentHtml",DbType.String,DBNull.Value));
			command.Parameters.Add(DbProviderHelper.CreateParameter("@TemplatePath",DbType.String,TemplatePath));
			command.Parameters.Add(DbProviderHelper.CreateParameter("@ReleasePath",DbType.String,ReleasePath));
			if (ImagePath!=null)
				command.Parameters.Add(DbProviderHelper.CreateParameter("@ImagePath",DbType.String,ImagePath));
			else
				command.Parameters.Add(DbProviderHelper.CreateParameter("@ImagePath",DbType.String,DBNull.Value));
			if (ImageWidth.HasValue)
				command.Parameters.Add(DbProviderHelper.CreateParameter("@ImageWidth",DbType.Int32,ImageWidth));
			else
				command.Parameters.Add(DbProviderHelper.CreateParameter("@ImageWidth",DbType.Int32,DBNull.Value));
			if (ImageHeight.HasValue)
				command.Parameters.Add(DbProviderHelper.CreateParameter("@ImageHeight",DbType.Int32,ImageHeight));
			else
				command.Parameters.Add(DbProviderHelper.CreateParameter("@ImageHeight",DbType.Int32,DBNull.Value));
			command.Parameters.Add(DbProviderHelper.CreateParameter("@Hits",DbType.Int32,Hits));
			command.Parameters.Add(DbProviderHelper.CreateParameter("@Comments",DbType.Int32,Comments));
			command.Parameters.Add(DbProviderHelper.CreateParameter("@SpecialId",DbType.Int32,SpecialId));
			return DbProviderHelper.ExecuteNonQuery(command);
		}

		public int Remove(int SpecialId)
		{
			DbCommand command = DbProviderHelper.CreateCommand("DELETESpecial",CommandType.StoredProcedure);
			command.Parameters.Add(DbProviderHelper.CreateParameter("@SpecialId",DbType.Int32,SpecialId));
			return DbProviderHelper.ExecuteNonQuery(command);
		}
	}
}
