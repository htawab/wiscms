using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;

namespace Wis.Website.DataManager
{
	public class LogManager
	{
		public LogManager()
		{
			DbProviderHelper.GetConnection();
		}

		public List<Log> GetLogs()
		{
			List<Log> lstLogs = new List<Log>();
			DbCommand oDbCommand = DbProviderHelper.CreateCommand("SELECTLogs",CommandType.StoredProcedure);
			DbDataReader oDbDataReader = DbProviderHelper.ExecuteReader(oDbCommand);
			while (oDbDataReader.Read())
			{
				Log oLog = new Log();
				oLog.LoggerId = Convert.ToInt32(oDbDataReader["LoggerId"]);

				if(oDbDataReader["LoggerGuid"] != DBNull.Value)
					oLog.LoggerGuid = (Guid) oDbDataReader["LoggerGuid"];

				if(oDbDataReader["UserGuid"] != DBNull.Value)
					oLog.UserGuid = (Guid) oDbDataReader["UserGuid"];

				if(oDbDataReader["Title"] != DBNull.Value)
					oLog.Title = Convert.ToString(oDbDataReader["Title"]);
				oLog.SubmissionGuid = (Guid) oDbDataReader["SubmissionGuid"];

				if(oDbDataReader["Message"] != DBNull.Value)
					oLog.Message = Convert.ToString(oDbDataReader["Message"]);
				oLog.DateCreated = Convert.ToDateTime(oDbDataReader["DateCreated"]);
				lstLogs.Add(oLog);
			}
			oDbDataReader.Close();
			return lstLogs;
		}

		public Log GetLog(int LoggerId)
		{
			Log oLog = new Log();
			DbCommand oDbCommand = DbProviderHelper.CreateCommand("SELECTLog",CommandType.StoredProcedure);
			oDbCommand.Parameters.Add(DbProviderHelper.CreateParameter("@LoggerId",DbType.Int32,LoggerId));
			DbDataReader oDbDataReader = DbProviderHelper.ExecuteReader(oDbCommand);
			while (oDbDataReader.Read())
			{
				oLog.LoggerId = Convert.ToInt32(oDbDataReader["LoggerId"]);

				if(oDbDataReader["LoggerGuid"] != DBNull.Value)
					oLog.LoggerGuid = (Guid) oDbDataReader["LoggerGuid"];

				if(oDbDataReader["UserGuid"] != DBNull.Value)
					oLog.UserGuid = (Guid) oDbDataReader["UserGuid"];

				if(oDbDataReader["Title"] != DBNull.Value)
					oLog.Title = Convert.ToString(oDbDataReader["Title"]);
				oLog.SubmissionGuid = (Guid) oDbDataReader["SubmissionGuid"];

				if(oDbDataReader["Message"] != DBNull.Value)
					oLog.Message = Convert.ToString(oDbDataReader["Message"]);
				oLog.DateCreated = Convert.ToDateTime(oDbDataReader["DateCreated"]);
			}
			oDbDataReader.Close();
			return oLog;
		}

		public int AddNew(Nullable<Guid> LoggerGuid, Nullable<Guid> UserGuid, string Title, Guid SubmissionGuid, string Message, DateTime DateCreated)
		{
			DbCommand oDbCommand = DbProviderHelper.CreateCommand("INSERTLog",CommandType.StoredProcedure);
			if (LoggerGuid.HasValue)
				oDbCommand.Parameters.Add(DbProviderHelper.CreateParameter("@LoggerGuid",DbType.Guid,LoggerGuid));
			else
				oDbCommand.Parameters.Add(DbProviderHelper.CreateParameter("@LoggerGuid",DbType.Guid,DBNull.Value));
			if (UserGuid.HasValue)
				oDbCommand.Parameters.Add(DbProviderHelper.CreateParameter("@UserGuid",DbType.Guid,UserGuid));
			else
				oDbCommand.Parameters.Add(DbProviderHelper.CreateParameter("@UserGuid",DbType.Guid,DBNull.Value));
			if (Title!=null)
				oDbCommand.Parameters.Add(DbProviderHelper.CreateParameter("@Title",DbType.String,Title));
			else
				oDbCommand.Parameters.Add(DbProviderHelper.CreateParameter("@Title",DbType.String,DBNull.Value));
			oDbCommand.Parameters.Add(DbProviderHelper.CreateParameter("@SubmissionGuid",DbType.Guid,SubmissionGuid));
			if (Message!=null)
				oDbCommand.Parameters.Add(DbProviderHelper.CreateParameter("@Message",DbType.String,Message));
			else
				oDbCommand.Parameters.Add(DbProviderHelper.CreateParameter("@Message",DbType.String,DBNull.Value));
			oDbCommand.Parameters.Add(DbProviderHelper.CreateParameter("@DateCreated",DbType.DateTime,DateCreated));

			return Convert.ToInt32(DbProviderHelper.ExecuteScalar(oDbCommand));
		}

		public int Update(int LoggerId, Nullable<Guid> LoggerGuid, Nullable<Guid> UserGuid, string Title, Guid SubmissionGuid, string Message, DateTime DateCreated)
		{

			DbCommand oDbCommand = DbProviderHelper.CreateCommand("UPDATELog",CommandType.StoredProcedure);
			if (LoggerGuid.HasValue)
				oDbCommand.Parameters.Add(DbProviderHelper.CreateParameter("@LoggerGuid",DbType.Guid,LoggerGuid));
			else
				oDbCommand.Parameters.Add(DbProviderHelper.CreateParameter("@LoggerGuid",DbType.Guid,DBNull.Value));
			if (UserGuid.HasValue)
				oDbCommand.Parameters.Add(DbProviderHelper.CreateParameter("@UserGuid",DbType.Guid,UserGuid));
			else
				oDbCommand.Parameters.Add(DbProviderHelper.CreateParameter("@UserGuid",DbType.Guid,DBNull.Value));
			if (Title!=null)
				oDbCommand.Parameters.Add(DbProviderHelper.CreateParameter("@Title",DbType.String,Title));
			else
				oDbCommand.Parameters.Add(DbProviderHelper.CreateParameter("@Title",DbType.String,DBNull.Value));
			oDbCommand.Parameters.Add(DbProviderHelper.CreateParameter("@SubmissionGuid",DbType.Guid,SubmissionGuid));
			if (Message!=null)
				oDbCommand.Parameters.Add(DbProviderHelper.CreateParameter("@Message",DbType.String,Message));
			else
				oDbCommand.Parameters.Add(DbProviderHelper.CreateParameter("@Message",DbType.String,DBNull.Value));
			oDbCommand.Parameters.Add(DbProviderHelper.CreateParameter("@DateCreated",DbType.DateTime,DateCreated));
			oDbCommand.Parameters.Add(DbProviderHelper.CreateParameter("@LoggerId",DbType.Int32,LoggerId));
			return DbProviderHelper.ExecuteNonQuery(oDbCommand);
		}

		public int Remove(int LoggerId)
		{
			DbCommand oDbCommand = DbProviderHelper.CreateCommand("DELETELog",CommandType.StoredProcedure);
			oDbCommand.Parameters.Add(DbProviderHelper.CreateParameter("@LoggerId",DbType.Int32,LoggerId));
			return DbProviderHelper.ExecuteNonQuery(oDbCommand);
		}
	}
}
