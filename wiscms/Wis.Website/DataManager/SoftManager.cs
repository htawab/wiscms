using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;

namespace Wis.Website.DataManager
{
	public class SoftManager
	{
		public SoftManager()
		{
			DbProviderHelper.GetConnection();
		}

		public List<Soft> GetSofts()
		{
			List<Soft> softs = new List<Soft>();
			DbCommand command = DbProviderHelper.CreateCommand("SELECTSofts",CommandType.StoredProcedure);
			DbDataReader dataReader = DbProviderHelper.ExecuteReader(command);
			while (dataReader.Read())
			{
				Soft soft = new Soft();
				soft.SoftId = Convert.ToInt32(dataReader["SoftId"]);
				soft.SoftGuid = (Guid) dataReader["SoftGuid"];

				if(dataReader["SoftType"] != DBNull.Value)
					soft.SoftType = Convert.ToString(dataReader["SoftType"]);

				if(dataReader["Version"] != DBNull.Value)
					soft.Version = Convert.ToString(dataReader["Version"]);

				if(dataReader["Language"] != DBNull.Value)
					soft.Language = Convert.ToString(dataReader["Language"]);

				if(dataReader["Copyright"] != DBNull.Value)
					soft.Copyright = Convert.ToString(dataReader["Copyright"]);

				if(dataReader["OperatingSystem"] != DBNull.Value)
					soft.OperatingSystem = Convert.ToString(dataReader["OperatingSystem"]);

				if(dataReader["DemoUri"] != DBNull.Value)
					soft.DemoUri = Convert.ToString(dataReader["DemoUri"]);

				if(dataReader["RegUri"] != DBNull.Value)
					soft.RegUri = Convert.ToString(dataReader["RegUri"]);

				if(dataReader["UnzipPassword"] != DBNull.Value)
					soft.UnzipPassword = Convert.ToString(dataReader["UnzipPassword"]);
				softs.Add(soft);
			}
			dataReader.Close();
			return softs;
		}

		public Soft GetSoft(int SoftId)
		{
			Soft soft = new Soft();
			DbCommand command = DbProviderHelper.CreateCommand("SELECTSoft",CommandType.StoredProcedure);
			command.Parameters.Add(DbProviderHelper.CreateParameter("@SoftId",DbType.Int32,SoftId));
			DbDataReader dataReader = DbProviderHelper.ExecuteReader(command);
			while (dataReader.Read())
			{
				soft.SoftId = Convert.ToInt32(dataReader["SoftId"]);
				soft.SoftGuid = (Guid) dataReader["SoftGuid"];

				if(dataReader["SoftType"] != DBNull.Value)
					soft.SoftType = Convert.ToString(dataReader["SoftType"]);

				if(dataReader["Version"] != DBNull.Value)
					soft.Version = Convert.ToString(dataReader["Version"]);

				if(dataReader["Language"] != DBNull.Value)
					soft.Language = Convert.ToString(dataReader["Language"]);

				if(dataReader["Copyright"] != DBNull.Value)
					soft.Copyright = Convert.ToString(dataReader["Copyright"]);

				if(dataReader["OperatingSystem"] != DBNull.Value)
					soft.OperatingSystem = Convert.ToString(dataReader["OperatingSystem"]);

				if(dataReader["DemoUri"] != DBNull.Value)
					soft.DemoUri = Convert.ToString(dataReader["DemoUri"]);

				if(dataReader["RegUri"] != DBNull.Value)
					soft.RegUri = Convert.ToString(dataReader["RegUri"]);

				if(dataReader["UnzipPassword"] != DBNull.Value)
					soft.UnzipPassword = Convert.ToString(dataReader["UnzipPassword"]);
			}
			dataReader.Close();
			return soft;
		}

		public int AddNew(Guid SoftGuid, string SoftType, string Version, string Language, string Copyright, string OperatingSystem, string DemoUri, string RegUri, string UnzipPassword)
		{
			DbCommand command = DbProviderHelper.CreateCommand("INSERTSoft",CommandType.StoredProcedure);
			command.Parameters.Add(DbProviderHelper.CreateParameter("@SoftGuid",DbType.Guid,SoftGuid));
			if (SoftType!=null)
				command.Parameters.Add(DbProviderHelper.CreateParameter("@SoftType",DbType.String,SoftType));
			else
				command.Parameters.Add(DbProviderHelper.CreateParameter("@SoftType",DbType.String,DBNull.Value));
			if (Version!=null)
				command.Parameters.Add(DbProviderHelper.CreateParameter("@Version",DbType.String,Version));
			else
				command.Parameters.Add(DbProviderHelper.CreateParameter("@Version",DbType.String,DBNull.Value));
			if (Language!=null)
				command.Parameters.Add(DbProviderHelper.CreateParameter("@Language",DbType.String,Language));
			else
				command.Parameters.Add(DbProviderHelper.CreateParameter("@Language",DbType.String,DBNull.Value));
			if (Copyright!=null)
				command.Parameters.Add(DbProviderHelper.CreateParameter("@Copyright",DbType.String,Copyright));
			else
				command.Parameters.Add(DbProviderHelper.CreateParameter("@Copyright",DbType.String,DBNull.Value));
			if (OperatingSystem!=null)
				command.Parameters.Add(DbProviderHelper.CreateParameter("@OperatingSystem",DbType.String,OperatingSystem));
			else
				command.Parameters.Add(DbProviderHelper.CreateParameter("@OperatingSystem",DbType.String,DBNull.Value));
			if (DemoUri!=null)
				command.Parameters.Add(DbProviderHelper.CreateParameter("@DemoUri",DbType.String,DemoUri));
			else
				command.Parameters.Add(DbProviderHelper.CreateParameter("@DemoUri",DbType.String,DBNull.Value));
			if (RegUri!=null)
				command.Parameters.Add(DbProviderHelper.CreateParameter("@RegUri",DbType.String,RegUri));
			else
				command.Parameters.Add(DbProviderHelper.CreateParameter("@RegUri",DbType.String,DBNull.Value));
			if (UnzipPassword!=null)
				command.Parameters.Add(DbProviderHelper.CreateParameter("@UnzipPassword",DbType.String,UnzipPassword));
			else
				command.Parameters.Add(DbProviderHelper.CreateParameter("@UnzipPassword",DbType.String,DBNull.Value));

			return Convert.ToInt32(DbProviderHelper.ExecuteScalar(command));
		}

		public int Update(int SoftId, Guid SoftGuid, string SoftType, string Version, string Language, string Copyright, string OperatingSystem, string DemoUri, string RegUri, string UnzipPassword)
		{
			DbCommand command = DbProviderHelper.CreateCommand("UPDATESoft",CommandType.StoredProcedure);
			command.Parameters.Add(DbProviderHelper.CreateParameter("@SoftGuid",DbType.Guid,SoftGuid));
			if (SoftType!=null)
				command.Parameters.Add(DbProviderHelper.CreateParameter("@SoftType",DbType.String,SoftType));
			else
				command.Parameters.Add(DbProviderHelper.CreateParameter("@SoftType",DbType.String,DBNull.Value));
			if (Version!=null)
				command.Parameters.Add(DbProviderHelper.CreateParameter("@Version",DbType.String,Version));
			else
				command.Parameters.Add(DbProviderHelper.CreateParameter("@Version",DbType.String,DBNull.Value));
			if (Language!=null)
				command.Parameters.Add(DbProviderHelper.CreateParameter("@Language",DbType.String,Language));
			else
				command.Parameters.Add(DbProviderHelper.CreateParameter("@Language",DbType.String,DBNull.Value));
			if (Copyright!=null)
				command.Parameters.Add(DbProviderHelper.CreateParameter("@Copyright",DbType.String,Copyright));
			else
				command.Parameters.Add(DbProviderHelper.CreateParameter("@Copyright",DbType.String,DBNull.Value));
			if (OperatingSystem!=null)
				command.Parameters.Add(DbProviderHelper.CreateParameter("@OperatingSystem",DbType.String,OperatingSystem));
			else
				command.Parameters.Add(DbProviderHelper.CreateParameter("@OperatingSystem",DbType.String,DBNull.Value));
			if (DemoUri!=null)
				command.Parameters.Add(DbProviderHelper.CreateParameter("@DemoUri",DbType.String,DemoUri));
			else
				command.Parameters.Add(DbProviderHelper.CreateParameter("@DemoUri",DbType.String,DBNull.Value));
			if (RegUri!=null)
				command.Parameters.Add(DbProviderHelper.CreateParameter("@RegUri",DbType.String,RegUri));
			else
				command.Parameters.Add(DbProviderHelper.CreateParameter("@RegUri",DbType.String,DBNull.Value));
			if (UnzipPassword!=null)
				command.Parameters.Add(DbProviderHelper.CreateParameter("@UnzipPassword",DbType.String,UnzipPassword));
			else
				command.Parameters.Add(DbProviderHelper.CreateParameter("@UnzipPassword",DbType.String,DBNull.Value));
			command.Parameters.Add(DbProviderHelper.CreateParameter("@SoftId",DbType.Int32,SoftId));
			return DbProviderHelper.ExecuteNonQuery(command);
		}

		public int Remove(int SoftId)
		{
			DbCommand command = DbProviderHelper.CreateCommand("DELETESoft",CommandType.StoredProcedure);
			command.Parameters.Add(DbProviderHelper.CreateParameter("@SoftId",DbType.Int32,SoftId));
			return DbProviderHelper.ExecuteNonQuery(command);
		}
	}
}
