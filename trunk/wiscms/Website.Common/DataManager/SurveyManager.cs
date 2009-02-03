using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;

namespace Wis.Website.DataManager
{
	public class SurveyManager
	{
		public SurveyManager()
		{
			DbProviderHelper.GetConnection();
		}

		public List<Survey> GetSurveys()
		{
			List<Survey> lstSurveys = new List<Survey>();
			DbCommand oDbCommand = DbProviderHelper.CreateCommand("SELECTSurveys",CommandType.StoredProcedure);
			DbDataReader oDbDataReader = DbProviderHelper.ExecuteReader(oDbCommand);
			while (oDbDataReader.Read())
			{
				Survey oSurvey = new Survey();
				oSurvey.SurveyId = Convert.ToInt32(oDbDataReader["SurveyId"]);

				if(oDbDataReader["SurveyGuid"] != DBNull.Value)
					oSurvey.SurveyGuid = (Guid) oDbDataReader["SurveyGuid"];
				oSurvey.SubmissionGuid = (Guid) oDbDataReader["SubmissionGuid"];

				if(oDbDataReader["Voter"] != DBNull.Value)
					oSurvey.Voter = Convert.ToString(oDbDataReader["Voter"]);
				oSurvey.Vote = Convert.ToInt32(oDbDataReader["Vote"]);
				oSurvey.IPAddress = Convert.ToString(oDbDataReader["IPAddress"]);

				if(oDbDataReader["DateCreated"] != DBNull.Value)
					oSurvey.DateCreated = Convert.ToDateTime(oDbDataReader["DateCreated"]);
				lstSurveys.Add(oSurvey);
			}
			oDbDataReader.Close();
			return lstSurveys;
		}

		public Survey GetSurvey(int SurveyId)
		{
			Survey oSurvey = new Survey();
			DbCommand oDbCommand = DbProviderHelper.CreateCommand("SELECTSurvey",CommandType.StoredProcedure);
			oDbCommand.Parameters.Add(DbProviderHelper.CreateParameter("@SurveyId",DbType.Int32,SurveyId));
			DbDataReader oDbDataReader = DbProviderHelper.ExecuteReader(oDbCommand);
			while (oDbDataReader.Read())
			{
				oSurvey.SurveyId = Convert.ToInt32(oDbDataReader["SurveyId"]);

				if(oDbDataReader["SurveyGuid"] != DBNull.Value)
					oSurvey.SurveyGuid = (Guid) oDbDataReader["SurveyGuid"];
				oSurvey.SubmissionGuid = (Guid) oDbDataReader["SubmissionGuid"];

				if(oDbDataReader["Voter"] != DBNull.Value)
					oSurvey.Voter = Convert.ToString(oDbDataReader["Voter"]);
				oSurvey.Vote = Convert.ToInt32(oDbDataReader["Vote"]);
				oSurvey.IPAddress = Convert.ToString(oDbDataReader["IPAddress"]);

				if(oDbDataReader["DateCreated"] != DBNull.Value)
					oSurvey.DateCreated = Convert.ToDateTime(oDbDataReader["DateCreated"]);
			}
			oDbDataReader.Close();
			return oSurvey;
		}

		public int AddNew(int SurveyId, Nullable<Guid> SurveyGuid, Guid SubmissionGuid, string Voter, int Vote, string IPAddress, Nullable<DateTime> DateCreated)
		{
			DbCommand oDbCommand = DbProviderHelper.CreateCommand("INSERTSurvey",CommandType.StoredProcedure);
			if (SurveyGuid.HasValue)
				oDbCommand.Parameters.Add(DbProviderHelper.CreateParameter("@SurveyGuid",DbType.Guid,SurveyGuid));
			else
				oDbCommand.Parameters.Add(DbProviderHelper.CreateParameter("@SurveyGuid",DbType.Guid,DBNull.Value));
			oDbCommand.Parameters.Add(DbProviderHelper.CreateParameter("@SubmissionGuid",DbType.Guid,SubmissionGuid));
			if (Voter!=null)
				oDbCommand.Parameters.Add(DbProviderHelper.CreateParameter("@Voter",DbType.String,Voter));
			else
				oDbCommand.Parameters.Add(DbProviderHelper.CreateParameter("@Voter",DbType.String,DBNull.Value));
			oDbCommand.Parameters.Add(DbProviderHelper.CreateParameter("@Vote",DbType.Int32,Vote));
			oDbCommand.Parameters.Add(DbProviderHelper.CreateParameter("@IPAddress",DbType.String,IPAddress));
			if (DateCreated.HasValue)
				oDbCommand.Parameters.Add(DbProviderHelper.CreateParameter("@DateCreated",DbType.DateTime,DateCreated));
			else
				oDbCommand.Parameters.Add(DbProviderHelper.CreateParameter("@DateCreated",DbType.DateTime,DBNull.Value));

			return DbProviderHelper.ExecuteNonQuery(oDbCommand);
		}

		public int Update(int SurveyId, Nullable<Guid> SurveyGuid, Guid SubmissionGuid, string Voter, int Vote, string IPAddress, Nullable<DateTime> DateCreated)
		{

			DbCommand oDbCommand = DbProviderHelper.CreateCommand("UPDATESurvey",CommandType.StoredProcedure);
			if (SurveyGuid.HasValue)
				oDbCommand.Parameters.Add(DbProviderHelper.CreateParameter("@SurveyGuid",DbType.Guid,SurveyGuid));
			else
				oDbCommand.Parameters.Add(DbProviderHelper.CreateParameter("@SurveyGuid",DbType.Guid,DBNull.Value));
			oDbCommand.Parameters.Add(DbProviderHelper.CreateParameter("@SubmissionGuid",DbType.Guid,SubmissionGuid));
			if (Voter!=null)
				oDbCommand.Parameters.Add(DbProviderHelper.CreateParameter("@Voter",DbType.String,Voter));
			else
				oDbCommand.Parameters.Add(DbProviderHelper.CreateParameter("@Voter",DbType.String,DBNull.Value));
			oDbCommand.Parameters.Add(DbProviderHelper.CreateParameter("@Vote",DbType.Int32,Vote));
			oDbCommand.Parameters.Add(DbProviderHelper.CreateParameter("@IPAddress",DbType.String,IPAddress));
			if (DateCreated.HasValue)
				oDbCommand.Parameters.Add(DbProviderHelper.CreateParameter("@DateCreated",DbType.DateTime,DateCreated));
			else
				oDbCommand.Parameters.Add(DbProviderHelper.CreateParameter("@DateCreated",DbType.DateTime,DBNull.Value));
			oDbCommand.Parameters.Add(DbProviderHelper.CreateParameter("@SurveyId",DbType.Int32,SurveyId));
			return DbProviderHelper.ExecuteNonQuery(oDbCommand);
		}

		public int Remove(int SurveyId)
		{
			DbCommand oDbCommand = DbProviderHelper.CreateCommand("DELETESurvey",CommandType.StoredProcedure);
			oDbCommand.Parameters.Add(DbProviderHelper.CreateParameter("@SurveyId",DbType.Int32,SurveyId));
			return DbProviderHelper.ExecuteNonQuery(oDbCommand);
		}
	}
}
