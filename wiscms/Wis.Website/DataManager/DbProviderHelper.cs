using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using System.Data;
using System.Data.Common;

namespace Wis.Website.DataManager
{
    /// <summary>
    /// 
    /// </summary>
    public class DbProviderHelper
    {
        private static DbProviderFactory dbProviderFactory;

        private static DbConnection dbConnection;

        #region dbConnexion

        public static DbConnection GetConnection()
        {
            if (dbConnection == null)
            {
                ConnectionStringsSection connectionStringsSection = GetConnectionStringsSection();
                dbProviderFactory = DbProviderFactories.GetFactory(connectionStringsSection.ConnectionStrings[1].ProviderName);
                dbConnection = dbProviderFactory.CreateConnection();
                dbConnection.ConnectionString = connectionStringsSection.ConnectionStrings[1].ConnectionString;
            }
            return dbConnection;
        }
        public static DbConnection GetConnection(string providerName, string connectionString)
        {
            if (dbConnection == null)
            {
                dbProviderFactory = DbProviderFactories.GetFactory(providerName);
                dbConnection = dbProviderFactory.CreateConnection();
                dbConnection.ConnectionString = connectionString;
            }
            return dbConnection;
        }
        public static ConnectionStringsSection GetConnectionStringsSection()
        {
            return ConfigurationManager.GetSection("connectionStrings") as ConnectionStringsSection;
        }

        #endregion dbConnexion

        #region  dbCommand

        public static DbCommand CreateCommand(String commandText, CommandType commandType)
        {
            DbCommand dbCommand = dbProviderFactory.CreateCommand();
            dbCommand.Connection = dbConnection;
            dbCommand.CommandType = commandType;
            dbCommand.CommandText = commandText;


            return dbCommand;
        }
        public static DbCommand CreateCommand(String commandText, CommandType commandType, DbTransaction dbTransaction)
        {
            DbCommand dbCommand = dbProviderFactory.CreateCommand();
            dbCommand.Connection = dbConnection;
            dbCommand.CommandType = commandType;
            dbCommand.CommandText = commandText;
            dbCommand.Transaction = dbTransaction;

            return dbCommand;
        }

        #endregion  dbCommand

        #region mapping

        public static DataColumnMapping CreateDataColumnMapping(string sourceColumn, string dataSetColumn)
        {
            return new DataColumnMapping(sourceColumn, dataSetColumn);
        }
        public static DataTableMapping CreateDataTableMapping(string sourceTable, string dataSetTable, DataColumnMappingCollection dataColumnMappings)
        {
            DataTableMapping dataTableMapping = new DataTableMapping(sourceTable, dataSetTable);
            foreach (DataColumnMapping dataColumnMapping in dataColumnMappings)
                dataTableMapping.ColumnMappings.Add(dataColumnMapping);
            return dataTableMapping;
        }

        #endregion mapping

        #region dbDataAdapter

        public static DbDataAdapter CreateDataAdapter(DbCommand selectCommand)
        {
            DbDataAdapter dbDataAdapter = dbProviderFactory.CreateDataAdapter();
            dbDataAdapter.SelectCommand = selectCommand;
            return dbDataAdapter;
        }
        public static DbDataAdapter CreateDataAdapter(DbCommand selectCommand, DbCommand insertCommand, DbCommand updateCommand, DbCommand deleteCommand)
        {
            return CreateDataAdapter(selectCommand, insertCommand, updateCommand, deleteCommand, false);
        }
        public static DbDataAdapter CreateDataAdapter(DbCommand selectCommand, DbCommand insertCommand, DbCommand updateCommand, DbCommand deleteCommand, bool continueUpdateOnError)
        {
            DbDataAdapter dbDataAdapter = dbProviderFactory.CreateDataAdapter();
            dbDataAdapter.ContinueUpdateOnError = continueUpdateOnError;
            dbDataAdapter.SelectCommand = selectCommand;
            dbDataAdapter.InsertCommand = insertCommand;
            dbDataAdapter.UpdateCommand = updateCommand;
            dbDataAdapter.DeleteCommand = deleteCommand;

            return dbDataAdapter;
        }
        #endregion dbDataAdapter

        #region dbParameter

        public static DbParameter CreateParameter(string parameterName, DbType dbType, object value)
        {
            DbParameter oDbParameter = dbProviderFactory.CreateParameter();
            oDbParameter.ParameterName = parameterName;
            oDbParameter.DbType = dbType;
            oDbParameter.Value = value;

            return oDbParameter;
        }
        public static DbParameter CreateParameter(string parameterName, DbType dbType, ParameterDirection parameterDirection, string sourceColumn, DataRowVersion dataRowVersion, bool sourceColumnNullMapping, object value)
        {
            DbParameter oDbParameter = dbProviderFactory.CreateParameter();
            oDbParameter.ParameterName = parameterName;
            oDbParameter.DbType = dbType;
            oDbParameter.Direction = parameterDirection;
            oDbParameter.SourceColumn = sourceColumn;
            oDbParameter.SourceVersion = dataRowVersion;
            oDbParameter.SourceColumnNullMapping = sourceColumnNullMapping;
            oDbParameter.Value = value;

            return oDbParameter;
        }

        #endregion dbParameter

        #region Operations

        public static DbDataReader ExecuteReader(DbCommand dbCommand)
        {
            if (dbConnection.State != ConnectionState.Open)
                dbConnection.Open();
            return dbCommand.ExecuteReader(CommandBehavior.CloseConnection);
        }
        public static DbDataReader ExecuteReader(DbCommand dbCommand, CommandBehavior commandBehavior)
        {
            if (dbConnection.State != ConnectionState.Open)
                dbConnection.Open();
            return dbCommand.ExecuteReader(commandBehavior);
        }
        public static int ExecuteNonQuery(DbCommand dbCommand)
        {
            if (dbConnection.State != ConnectionState.Open)
                dbConnection.Open();
            int iExecuteNonQuery = dbCommand.ExecuteNonQuery();
            dbConnection.Close();
            return iExecuteNonQuery;
        }
        public static object ExecuteScalar(DbCommand dbCommand)
        {
            if (dbConnection.State != ConnectionState.Open)
                dbConnection.Open();
            object o = dbCommand.ExecuteScalar();
            dbConnection.Close();
            return o;
        }
        public static DataSet FillDataSet(DbDataAdapter dbDataAdapter)
        {
            DataSet dataSet = new DataSet();
            if (dbConnection.State != ConnectionState.Open)
                dbConnection.Open();
            dbDataAdapter.Fill(dataSet);
            dbConnection.Close();
            return dataSet;
        }
        public static DataTable FillDataTable(DbDataAdapter dbDataAdapter)
        {
            DataTable dataTable = new DataTable();
            if (dbConnection.State != ConnectionState.Open)
                dbConnection.Open();
            dbDataAdapter.Fill(dataTable);
            dbConnection.Close();
            return dataTable;
        }
        public static int UpdateDataSet(DbDataAdapter dbDataAdapter, DataSet dataSet)
        {
            if (dbConnection.State != ConnectionState.Open)
                dbConnection.Open();
            int iUpdate = dbDataAdapter.Update(dataSet);
            dbConnection.Close();
            return iUpdate;
        }
        public static int UpdateDataTable(DbDataAdapter dbDataAdapter, DataTable dataTable)
        {
            if (dbConnection.State != ConnectionState.Open)
                dbConnection.Open();
            int iUpdate = dbDataAdapter.Update(dataTable);
            dbConnection.Close();
            return iUpdate;
        }

        #endregion Operations

        #region dbTransaction

        public static DbTransaction BeginTransaction()
        {
            return dbConnection.BeginTransaction();
        }
        public static void CommitTransaction(DbTransaction dbTransaction)
        {
            dbTransaction.Commit();
        }
        public static void RollbackTransaction(DbTransaction dbTransaction)
        {
            dbTransaction.Rollback();
        }

        #endregion dbTransaction
    }
}

