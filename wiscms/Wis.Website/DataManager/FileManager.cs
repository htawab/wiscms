using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;

namespace Wis.Website.DataManager
{
	public class FileManager
	{
		public FileManager()
		{
			DbProviderHelper.GetConnection();
		}

		public List<File> GetFiles()
		{
			List<File> files = new List<File>();
			DbCommand command = DbProviderHelper.CreateCommand("SELECTFiles",CommandType.StoredProcedure);
			DbDataReader dataReader = DbProviderHelper.ExecuteReader(command);
			while (dataReader.Read())
			{
				File file = new File();
				file.FileId = Convert.ToInt32(dataReader["FileId"]);
				file.FileGuid = (Guid) dataReader["FileGuid"];
				file.SubmissionGuid = (Guid) dataReader["SubmissionGuid"];
				file.OriginalFileName = Convert.ToString(dataReader["OriginalFileName"]);
				file.SaveAsFileName = Convert.ToString(dataReader["SaveAsFileName"]);
                file.Size = Convert.ToInt64(dataReader["Size"]);
				file.Rank = Convert.ToInt32(dataReader["Rank"]);

				if(dataReader["CreatedBy"] != DBNull.Value)
					file.CreatedBy = Convert.ToString(dataReader["CreatedBy"]);
				file.CreationDate = Convert.ToDateTime(dataReader["CreationDate"]);

				if(dataReader["Description"] != DBNull.Value)
					file.Description = Convert.ToString(dataReader["Description"]);
				file.Hits = Convert.ToInt32(dataReader["Hits"]);
				files.Add(file);
			}
			dataReader.Close();
			return files;
		}


        public static List<File> GetFilesBySubmissionGuid(Guid submissionGuid)
        {
            DbProviderHelper.GetConnection();
            List<File> files = new List<File>();
            DbCommand command = DbProviderHelper.CreateCommand("SelectFilesBySubmissionGuid", CommandType.StoredProcedure);
            command.Parameters.Add(DbProviderHelper.CreateParameter("@SubmissionGuid", DbType.Guid, submissionGuid));
            DbDataReader dataReader = DbProviderHelper.ExecuteReader(command);
            while (dataReader.Read())
            {
                File file = new File();
                file.FileId = Convert.ToInt32(dataReader["FileId"]);
                file.FileGuid = (Guid)dataReader["FileGuid"];
                file.SubmissionGuid = (Guid)dataReader["SubmissionGuid"];
                file.OriginalFileName = Convert.ToString(dataReader["OriginalFileName"]);
                file.SaveAsFileName = Convert.ToString(dataReader["SaveAsFileName"]);
                file.Size = Convert.ToInt64(dataReader["Size"]);
                file.Rank = Convert.ToInt32(dataReader["Rank"]);

                if (dataReader["CreatedBy"] != DBNull.Value)
                    file.CreatedBy = Convert.ToString(dataReader["CreatedBy"]);
                file.CreationDate = Convert.ToDateTime(dataReader["CreationDate"]);

                if (dataReader["Description"] != DBNull.Value)
                    file.Description = Convert.ToString(dataReader["Description"]);
                file.Hits = Convert.ToInt32(dataReader["Hits"]);
                files.Add(file);
            }
            dataReader.Close();
            return files;
        }

		public File GetFile(int FileId)
		{
			File oFile = new File();
			DbCommand oDbCommand = DbProviderHelper.CreateCommand("SELECTFile",CommandType.StoredProcedure);
			oDbCommand.Parameters.Add(DbProviderHelper.CreateParameter("@FileId",DbType.Int32,FileId));
			DbDataReader oDbDataReader = DbProviderHelper.ExecuteReader(oDbCommand);
			while (oDbDataReader.Read())
			{
				oFile.FileId = Convert.ToInt32(oDbDataReader["FileId"]);
				oFile.FileGuid = (Guid) oDbDataReader["FileGuid"];
				oFile.SubmissionGuid = (Guid) oDbDataReader["SubmissionGuid"];
				oFile.OriginalFileName = Convert.ToString(oDbDataReader["OriginalFileName"]);
				oFile.SaveAsFileName = Convert.ToString(oDbDataReader["SaveAsFileName"]);
                oFile.Size = Convert.ToInt64(oDbDataReader["Size"]);
				oFile.Rank = Convert.ToInt32(oDbDataReader["Rank"]);

				if(oDbDataReader["CreatedBy"] != DBNull.Value)
					oFile.CreatedBy = Convert.ToString(oDbDataReader["CreatedBy"]);
				oFile.CreationDate = Convert.ToDateTime(oDbDataReader["CreationDate"]);

				if(oDbDataReader["Description"] != DBNull.Value)
					oFile.Description = Convert.ToString(oDbDataReader["Description"]);
				oFile.Hits = Convert.ToInt32(oDbDataReader["Hits"]);
			}
			oDbDataReader.Close();
			return oFile;
		}


        /// <summary>
        /// 新增附件信息。
        /// </summary>
        /// <param name="file">附件对象。</param>
        /// <returns></returns>
        public int AddNew(Wis.Website.DataManager.File file)
		{
			DbCommand command = DbProviderHelper.CreateCommand("INSERTFile",CommandType.StoredProcedure);
            command.Parameters.Add(DbProviderHelper.CreateParameter("@FileGuid", DbType.Guid, file.FileGuid));
            command.Parameters.Add(DbProviderHelper.CreateParameter("@SubmissionGuid", DbType.Guid, file.SubmissionGuid));
            command.Parameters.Add(DbProviderHelper.CreateParameter("@OriginalFileName", DbType.String, file.OriginalFileName));
            command.Parameters.Add(DbProviderHelper.CreateParameter("@SaveAsFileName", DbType.String, file.SaveAsFileName));
            command.Parameters.Add(DbProviderHelper.CreateParameter("@Size", DbType.Int64, file.Size));
            command.Parameters.Add(DbProviderHelper.CreateParameter("@Rank", DbType.Int32, file.Rank));
            if (!string.IsNullOrEmpty(file.CreatedBy))
                command.Parameters.Add(DbProviderHelper.CreateParameter("@CreatedBy", DbType.String, file.CreatedBy));
			else
				command.Parameters.Add(DbProviderHelper.CreateParameter("@CreatedBy",DbType.String,DBNull.Value));
            command.Parameters.Add(DbProviderHelper.CreateParameter("@CreationDate", DbType.DateTime, file.CreationDate));
            if (!string.IsNullOrEmpty(file.Description))
                command.Parameters.Add(DbProviderHelper.CreateParameter("@Description", DbType.String, file.Description));
			else
				command.Parameters.Add(DbProviderHelper.CreateParameter("@Description",DbType.String,DBNull.Value));
            command.Parameters.Add(DbProviderHelper.CreateParameter("@Hits", DbType.Int32, file.Hits));

			return Convert.ToInt32(DbProviderHelper.ExecuteScalar(command));
		}

        public int Update(Wis.Website.DataManager.File file)
		{

			DbCommand command = DbProviderHelper.CreateCommand("UPDATEFile",CommandType.StoredProcedure);
            command.Parameters.Add(DbProviderHelper.CreateParameter("@FileGuid", DbType.Guid, file.FileGuid));
            command.Parameters.Add(DbProviderHelper.CreateParameter("@SubmissionGuid", DbType.Guid, file.SubmissionGuid));
            command.Parameters.Add(DbProviderHelper.CreateParameter("@OriginalFileName", DbType.String, file.OriginalFileName));
            command.Parameters.Add(DbProviderHelper.CreateParameter("@SaveAsFileName", DbType.String, file.SaveAsFileName));
            command.Parameters.Add(DbProviderHelper.CreateParameter("@Size", DbType.Int64, file.Size));
            command.Parameters.Add(DbProviderHelper.CreateParameter("@Rank", DbType.Int32, file.Rank));
            if (!string.IsNullOrEmpty(file.CreatedBy))
                command.Parameters.Add(DbProviderHelper.CreateParameter("@CreatedBy", DbType.String, file.CreatedBy));
			else
				command.Parameters.Add(DbProviderHelper.CreateParameter("@CreatedBy",DbType.String,DBNull.Value));
            command.Parameters.Add(DbProviderHelper.CreateParameter("@CreationDate", DbType.DateTime, file.CreationDate));
            if (!string.IsNullOrEmpty(file.Description))
                command.Parameters.Add(DbProviderHelper.CreateParameter("@Description", DbType.String, file.Description));
			else
				command.Parameters.Add(DbProviderHelper.CreateParameter("@Description",DbType.String,DBNull.Value));
            command.Parameters.Add(DbProviderHelper.CreateParameter("@Hits", DbType.Int32, file.Hits));
            
			return DbProviderHelper.ExecuteNonQuery(command);
		}

		public int Remove(int FileId)
		{
			DbCommand command = DbProviderHelper.CreateCommand("DELETEFile",CommandType.StoredProcedure);
			command.Parameters.Add(DbProviderHelper.CreateParameter("@FileId",DbType.Int32,FileId));
			return DbProviderHelper.ExecuteNonQuery(command);
		}
	}
}
