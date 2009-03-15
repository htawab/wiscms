//------------------------------------------------------------------------------
// <copyright file="DataProvider.cs" company="Everwis">
//     Copyright (C) Everwis Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using System.Data;
using System.Data.SqlClient;
using System.Xml;
using System.Data.Common;

namespace Wis.Toolkit
{
	/// <summary>
	/// 用于Microsoft SQL Server数据库的数据库访问实现类。
	/// </summary>
	public sealed class DataProvider
	{
		#region Constructor

		public DataProvider()
		{
		}

        public DataProvider(string connectionString)
		{
			ConnectionString = connectionString;
			_DbConnection.ConnectionString = connectionString;
		}

		#endregion

		#region Components.DataAccess 成员

		#region Support Property & method

		private string _ConnectionString = null;

		/// <summary>
		/// 数据库连接字符串
		/// </summary>
		public string ConnectionString
		{
			get { return _ConnectionString; }
			set
			{
				_ConnectionString = value;

				if (value != null && !value.Equals(string.Empty))
				{
					if (value.Length > 0 && _DbConnection.State.Equals(ConnectionState.Closed))
						_DbConnection.ConnectionString = _ConnectionString;
				}
			}
		}

		/// <summary>
		/// 数据库类型
		/// </summary>
		public string ProviderType
		{
			get { return "sqlserver"; }
		}

		private SqlConnection _DbConnection = new SqlConnection();
        /// <summary>
        /// 
        /// </summary>
		public IDbConnection DBConnection
		{
			get { return _DbConnection; }
		}

        /// <summary>
        /// 执行与释放或重置非托管资源相关的应用程序定义的任务。
        /// </summary>
        public void Dispose()
        {
            DBConnection.Dispose();
        }

        /// <summary>
        /// 关闭数据库连接。
        /// </summary>
        public void Close()
        {
            DBConnection.Close();
        }

        /// <summary>
        /// 打开数据库连接。
        /// </summary>
        public void Open()
        {
            if (DBConnection.State.Equals(ConnectionState.Closed))
                DBConnection.Open();
        }

        /// <summary>
        /// 数据库连接是否关闭。
        /// </summary>
        public bool IsClosed
        {
            get { return DBConnection.State.Equals(ConnectionState.Closed); }
        }

		private SqlTransaction _Transaction = null;

		/// <summary>
		/// 开始数据库事务。
		/// </summary>
		/// <returns></returns>
		public IDbTransaction BeginTransaction()
		{
            if (_DbConnection.State.Equals(ConnectionState.Closed)) _DbConnection.Open();
			_Transaction = _DbConnection.BeginTransaction();
			_HasTransaction = true;
			return _Transaction;
		}

		/// <summary>
		/// 提交事务。
		/// </summary>
		public void CommitTransaction()
		{
			_Transaction.Commit();
			_HasTransaction = false;
		}

		/// <summary>
		/// 回滚事务。
		/// </summary>
		public void RollbackTransaction()
		{
			_Transaction.Rollback();
			_HasTransaction = false;
		}

		private bool _HasTransaction = false;

		/// <summary>
		/// 是否有活动的事务
		/// </summary>
		public bool HasTransaction
		{
			get { return _HasTransaction; }
		}

        /// <summary>
        /// 根据数据库字段类型实例化不同的 Command 参数类。
        /// </summary>
        /// <param name="dbTypeName">数据库字段类型。</param>
        /// <returns>Command 参数类实例。</returns>
        public IDataParameter CreateDataParameter(string dbTypeName)
        {
            SqlParameter sqlParameter = new SqlParameter();
            switch (dbTypeName.ToLower())
            {
                case "bigint":
                    sqlParameter.SqlDbType = SqlDbType.BigInt;
                    return sqlParameter;
                case "binary":
                    sqlParameter.SqlDbType = SqlDbType.Binary;
                    return sqlParameter;
                case "bit":
                    sqlParameter.SqlDbType = SqlDbType.Bit;
                    return sqlParameter;
                case "char":
                    sqlParameter.SqlDbType = SqlDbType.Char;
                    return sqlParameter;
                case "datetime":
                    sqlParameter.SqlDbType = SqlDbType.DateTime;
                    return sqlParameter;
                case "numeric":
                case "decimal":
                    sqlParameter.SqlDbType = SqlDbType.Decimal;
                    return sqlParameter;
                case "float":
                    sqlParameter.SqlDbType = SqlDbType.Float;
                    return sqlParameter;
                case "image":
                    sqlParameter.SqlDbType = SqlDbType.Image;
                    return sqlParameter;
                case "int":
                case "int identity":
                    sqlParameter.SqlDbType = SqlDbType.Int;
                    return sqlParameter;
                case "money":
                    sqlParameter.SqlDbType = SqlDbType.Money;
                    return sqlParameter;
                case "nchar":
                    sqlParameter.SqlDbType = SqlDbType.NChar;
                    return sqlParameter;
                case "ntext":
                    sqlParameter.SqlDbType = SqlDbType.NText;
                    return sqlParameter;
                case "nvarchar":
                    sqlParameter.SqlDbType = SqlDbType.NVarChar;
                    return sqlParameter;
                case "real":
                    sqlParameter.SqlDbType = SqlDbType.Real;
                    return sqlParameter;
                case "smalldatetime":
                    sqlParameter.SqlDbType = SqlDbType.SmallDateTime;
                    return sqlParameter;
                case "smallint":
                    sqlParameter.SqlDbType = SqlDbType.SmallInt;
                    return sqlParameter;
                case "smallmoney":
                    sqlParameter.SqlDbType = SqlDbType.SmallMoney;
                    return sqlParameter;
                case "text":
                    sqlParameter.SqlDbType = SqlDbType.Text;
                    return sqlParameter;
                case "timestamp":
                    sqlParameter.SqlDbType = SqlDbType.Timestamp;
                    return sqlParameter;
                case "tinyint":
                    sqlParameter.SqlDbType = SqlDbType.TinyInt;
                    return sqlParameter;
                case "uniqueidentifier":
                    sqlParameter.SqlDbType = SqlDbType.UniqueIdentifier;
                    return sqlParameter;
                case "varbinary":
                    sqlParameter.SqlDbType = SqlDbType.VarBinary;
                    return sqlParameter;
                case "varchar":
                    sqlParameter.SqlDbType = SqlDbType.VarChar;
                    return sqlParameter;
                case "sql_variant":
                    sqlParameter.SqlDbType = SqlDbType.Variant;
                    return sqlParameter;
            }

            return null;
        }

		#endregion Support Property & method

        #region 生成参数

        public IDataParameter CreateInParameter(string parameterName, DbType dbType, int size, object value)
        {
            return CreateParameter(parameterName, dbType, size, ParameterDirection.Input, value);
        }

        public IDataParameter CreateOutParameter(string parameterName, DbType dbType, int size)
        {
            return CreateParameter(parameterName, dbType, size, ParameterDirection.Output, null);
        }

        public IDataParameter CreateParameter(string parameterName, DbType dbType, System.Int32 size, ParameterDirection parameterDirection, object value)
        {
            IDataParameter parameter;

            if (size > 0)
                parameter = new SqlParameter(parameterName, (SqlDbType)dbType, size);
            else
                parameter = new SqlParameter(parameterName, (SqlDbType)dbType);

            parameter.Direction = parameterDirection;
            if (!(parameterDirection == ParameterDirection.Output && value == null))
                parameter.Value = value;

            return parameter;
        }

        #endregion 生成参数结束

        #region ExecuteNonQuery


        public int ExecuteNonQuery(CommandType commandType, string commandText)
        {
            return ExecuteNonQuery(commandType, commandText, null);
        }

        public int ExecuteNonQuery(string commandText)
        {
            return ExecuteNonQuery(CommandType.Text, commandText, null);
        }

        public int ExecuteNonQuery(string commandText, IDataParameter[] commandParameters)
        {
            return ExecuteNonQuery(CommandType.Text, commandText, commandParameters);
        }

		/// <summary>
		/// 
		/// </summary>
		/// <param name="commandType"></param>
		/// <param name="commandText"></param>
		/// <param name="commandParameterCollection"></param>
		/// <returns>受影响的行数</returns>
		public int ExecuteNonQuery(CommandType commandType, string commandText, IDataParameter[] commandParameterCollection)
		{
			SqlCommand command = new SqlCommand();
			PrepareCommand(command, commandType, commandText, commandParameterCollection);
			int iExecuteNonQuery = command.ExecuteNonQuery();
			command.Parameters.Clear();

			return iExecuteNonQuery;
		}

		#endregion ExecuteNonQuery

		#region ExecuteDataSet

        public DataSet ExecuteDataset(CommandType commandType, string commandText)
        {
            return ExecuteDataset(commandType, commandText, null, new DataSet(), null);
        }

        public DataSet ExecuteDataset(string commandText)
        {
            return ExecuteDataset(CommandType.Text, commandText, null, new DataSet(), null);
        }

        public DataSet ExecuteDataset(CommandType commandType, string commandText,
                                      IDataParameter[] commandParameters)
        {
            return ExecuteDataset(commandType, commandText, commandParameters, new DataSet(), null);
        }

        public DataSet ExecuteDataset(string commandText, IDataParameter[] commandParameters)
        {
            return ExecuteDataset(CommandType.Text, commandText, commandParameters, new DataSet(), null);
        }

        public DataSet ExecuteDataset(CommandType commandType, string commandText, DataSet ds)
        {
            return ExecuteDataset(commandType, commandText, null, ds, null);
        }

        public DataSet ExecuteDataset(string commandText, DataSet ds)
        {
            return ExecuteDataset(CommandType.Text, commandText, null, ds, null);
        }

        public DataSet ExecuteDataset(string commandText, IDataParameter[] commandParameters, DataSet ds)
        {
            return ExecuteDataset(CommandType.Text, commandText, commandParameters, ds, null);
        }

        public DataSet ExecuteDataset(CommandType commandType, string commandText, string tableName)
        {
            return ExecuteDataset(commandType, commandText, null, new DataSet(), tableName);
        }

        public DataSet ExecuteDataset(string commandText, string tableName)
        {
            return ExecuteDataset(CommandType.Text, commandText, null, new DataSet(), tableName);
        }

        public DataSet ExecuteDataset(CommandType commandType, string commandText,
                                      IDataParameter[] commandParameters, string tableName)
        {
            return ExecuteDataset(commandType, commandText, commandParameters, new DataSet(), tableName);
        }

        public DataSet ExecuteDataset(string commandText, IDataParameter[] commandParameters, string tableName)
        {
            return ExecuteDataset(CommandType.Text, commandText, commandParameters, new DataSet(), tableName);
        }

        public DataSet ExecuteDataset(CommandType commandType, string commandText, DataSet ds, string tableName)
        {
            return ExecuteDataset(commandType, commandText, null, ds, tableName);
        }

        public DataSet ExecuteDataset(string commandText, DataSet ds, string tableName)
        {
            return ExecuteDataset(CommandType.Text, commandText, null, ds, tableName);
        }

        public DataSet ExecuteDataset(string commandText, IDataParameter[] commandParameters, DataSet ds,
                                      string tableName)
        {
            return ExecuteDataset(CommandType.Text, commandText, commandParameters, ds, tableName);
        }

        public DataSet ExecuteDataset(CommandType commandType, string commandText,
                                      IDataParameter[] commandParameters, DataSet ds)
        {
            return ExecuteDataset(CommandType.Text, commandText, commandParameters, ds, null);
        }


		/// <summary>
		/// 
		/// </summary>
		/// <param name="commandType"></param>
		/// <param name="commandText"></param>
		/// <param name="commandParameterCollection"></param>
		/// <param name="ds"></param>
		/// <param name="tableName"></param>
		/// <returns></returns>
		public DataSet ExecuteDataset(CommandType commandType, string commandText, IDataParameter[] commandParameterCollection, DataSet ds, string tableName)
		{
			SqlCommand command = new SqlCommand();
			PrepareCommand(command, commandType, commandText, commandParameterCollection);

			SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
			if (tableName == null || tableName.Length < 1)
				dataAdapter.Fill(ds);
			else
				dataAdapter.Fill(ds, tableName);
			command.Parameters.Clear();

			return ds;
		}

		#endregion ExecuteDataSet

		#region ExecuteReader	
        
        public IDataReader ExecuteReader(CommandType commandType, string commandText)
        {
            return ExecuteReader(commandType, commandText, null);
        }

        public IDataReader ExecuteReader(string commandText)
        {
            return ExecuteReader(CommandType.Text, commandText, null);
        }

        public IDataReader ExecuteReader(string commandText, IDataParameter[] commandParameters)
        {
            return ExecuteReader(CommandType.Text, commandText, commandParameters);
        }


		/// <summary>
		/// 
		/// </summary>
		/// <param name="commandType"></param>
		/// <param name="commandText"></param>
		/// <param name="commandParameterCollection"></param>
		/// <returns></returns>
		public IDataReader ExecuteReader(CommandType commandType, string commandText, IDataParameter[] commandParameterCollection)
		{
			SqlCommand command = new SqlCommand();
			PrepareCommand(command, commandType, commandText, commandParameterCollection);
			SqlDataReader sqlDataReader = command.ExecuteReader();
			command.Parameters.Clear();

			return sqlDataReader;
		}

		#endregion ExecuteReader	

		#region ExecuteScalar	
        
        public object ExecuteScalar(CommandType commandType, string commandText)
        {
            return ExecuteScalar(commandType, commandText, null);
        }

        public object ExecuteScalar(string commandText)
        {
            return ExecuteScalar(CommandType.Text, commandText, null);
        }

        public object ExecuteScalar(string commandText, IDataParameter[] commandParameters)
        {
            return ExecuteScalar(CommandType.Text, commandText, commandParameters);
        }


		/// <summary>
		/// 
		/// </summary>
		/// <param name="commandType"></param>
		/// <param name="commandText"></param>
		/// <param name="commandParameterCollection"></param>
		/// <returns></returns>
		public object ExecuteScalar(CommandType commandType, string commandText, IDataParameter[] commandParameterCollection)
		{
			SqlCommand command = new SqlCommand();
			PrepareCommand(command, commandType, commandText, commandParameterCollection);
			object objExecuteScalar = command.ExecuteScalar();
			command.Parameters.Clear();

			return objExecuteScalar;
		}

		#endregion ExecuteScalar	

		#region ExecuteXmlReader	

        public XmlReader ExecuteXmlReader(CommandType commandType, string commandText)
        {
            return ExecuteXmlReader(commandType, commandText, null);
        }

        public XmlReader ExecuteXmlReader(string commandText)
        {
            return ExecuteXmlReader(CommandType.Text, commandText, null);
        }

        public XmlReader ExecuteXmlReader(string commandText, IDataParameter[] commandParameters)
        {
            return ExecuteXmlReader(CommandType.Text, commandText, commandParameters);
        }


		/// <summary>
		/// 
		/// </summary>
		/// <param name="commandType"></param>
		/// <param name="commandText"></param>
		/// <param name="commandParameterCollection"></param>
		/// <returns></returns>
		public XmlReader ExecuteXmlReader(CommandType commandType, string commandText, IDataParameter[] commandParameterCollection)
		{
			SqlCommand command = new SqlCommand();
			PrepareCommand(command, commandType, commandText, commandParameterCollection);
			XmlReader reader = command.ExecuteXmlReader();
			command.Parameters.Clear();

			return reader;
		}

		#endregion ExecuteXmlReader

		#endregion

		private void PrepareCommand(SqlCommand command, CommandType commandType, string commandText, IDataParameter[] commandParameterCollection)
		{
			command.CommandType = commandType;
			command.CommandText = commandText;
			command.Connection = _DbConnection;
			if (_Transaction != null) command.Transaction = _Transaction;

			if ((commandParameterCollection != null) && (commandParameterCollection.Length > 0))
			{
				for (int index = 0; index < commandParameterCollection.Length; index++)
				{
					SqlParameter parameter = (SqlParameter) commandParameterCollection[index];
					if ((parameter.Direction == ParameterDirection.InputOutput || parameter.Direction == ParameterDirection.Input) && parameter.Value == null)
					{
						parameter.Value = System.DBNull.Value;
					}

					//DBNull处理
					switch (parameter.Value.GetType().ToString())
					{
						case "System.Int64":
							if (parameter.Value.Equals(System.Int64.MinValue) || parameter.Value.Equals(System.Int64.MaxValue))
							{
								parameter.Value = System.DBNull.Value;
							}
							break;
						case "System.Int32":
							if (parameter.Value.Equals(System.Int32.MinValue) || parameter.Value.Equals(System.Int32.MaxValue))
							{
								parameter.Value = System.DBNull.Value;
							}
							break;
						case "System.Int16":
							if (parameter.Value.Equals(System.Int16.MinValue) || parameter.Value.Equals(System.Int16.MaxValue))
							{
								parameter.Value = System.DBNull.Value;
							}
							break;
						case "System.Byte()":
							if (parameter.Value == null)
							{
								parameter.Value = System.DBNull.Value;
							}
							break;
						case "System.Boolean": //bit 类型的字段如何处理？
							//if(sqlParameters[index].Value == null)
							//{
							//	sqlParameters[index].Value = DBNull.Value;
							//}
							break;
						case "System.String":
							if (parameter.Value.Equals(System.String.Empty))
							{
								parameter.Value = System.DBNull.Value;
							}
							break;
						case "System.DateTime":
							if (parameter.Value.Equals(System.DateTime.MinValue) || parameter.Value.Equals(System.DateTime.MaxValue))
							{
								parameter.Value = System.DBNull.Value;
							}
							break;
						case "System.Decimal":
							if (parameter.Value.Equals(System.Decimal.MinValue) || parameter.Value.Equals(System.Decimal.MaxValue))
							{
								parameter.Value = System.DBNull.Value;
							}
							break;
						case "System.Double":
							if (parameter.Value.Equals(System.Double.MinValue) || parameter.Value.Equals(System.Double.MaxValue))
							{
								parameter.Value = System.DBNull.Value;
							}
							break;
						case "System.Single":
							if (parameter.Value.Equals(System.Single.MinValue) || parameter.Value.Equals(System.Single.MaxValue))
							{
								parameter.Value = System.DBNull.Value;
							}
							break;
						case "System.Byte":
							if (parameter.Value.Equals(System.Byte.MinValue) || parameter.Value.Equals(System.Byte.MaxValue))
							{
								parameter.Value = System.DBNull.Value;
							}
							break;
						case "System.Guid":
							if (parameter.Value.Equals(System.Guid.Empty))
							{
								parameter.Value = System.DBNull.Value;
							}
							break;
					}

					command.Parameters.Add(parameter);
				}
			}
		}

	}
}