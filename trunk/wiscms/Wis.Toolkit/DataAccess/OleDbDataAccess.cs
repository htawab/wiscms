//------------------------------------------------------------------------------
// <copyright file="OleDbDataAccess.cs" company="Oriental Everwisdom">
//     Copyright (C) Oriental Everwisdom Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.IO;
using System.Xml;

namespace Wis.Toolkit.DataAccess
{
	/// <summary>
	/// 用于支持OleDb的数据库的数据库访问实现类。
	/// </summary>
	public sealed class OleDbDataAccess : AbstractDataAccess
	{
		#region Constructor

		public OleDbDataAccess()
		{
		}

		public OleDbDataAccess(string connectionString)
		{
			ConnectionString = connectionString;
			_DbConnection.ConnectionString = connectionString;
		}

		public OleDbDataAccess(OleDbConnection connection)
		{
			_DbConnection = connection;
		}

		#endregion

		#region Components.DataAccess

		#region Support Property & method

		private string _ConnectionString = null;

		/// <summary>
		/// 数据库连接字符串
		/// </summary>
		public override string ConnectionString
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
		public override string ProviderType
		{
			get { return "sqlOleDb"; }
		}

		private OleDbConnection _DbConnection = new OleDbConnection();

		public override IDbConnection DbConnection
		{
			get { return _DbConnection; }
		}

		private OleDbTransaction _Transaction = null;

		/// <summary>
		/// 开始数据库事务。
		/// </summary>
		/// <returns></returns>
		public override IDbTransaction BeginTransaction()
		{
            if (_DbConnection.State.Equals(ConnectionState.Closed)) _DbConnection.Open();
			_Transaction = _DbConnection.BeginTransaction();
			_HasTransaction = true;
			return _Transaction;
		}

		/// <summary>
		/// 提交事务。
		/// </summary>
		public override void CommitTransaction()
		{
			_Transaction.Commit();
			_HasTransaction = false;
		}

		/// <summary>
		/// 回滚事务。
		/// </summary>
		public override void RollbackTransaction()
		{
			_Transaction.Rollback();
			_HasTransaction = false;
		}

		private bool _HasTransaction = false;

		/// <summary>
		/// 是否有活动的事务
		/// </summary>
		public override bool HasTransaction
		{
			get { return _HasTransaction; }
		}

		#endregion Support Property & method

		#region ExecuteNonQuery

		/// <summary>
		/// 
		/// </summary>
		/// <param name="commandType"></param>
		/// <param name="commandText"></param>
		/// <param name="commandParameterCollection"></param>
		/// <returns></returns>
		public override int ExecuteNonQuery(CommandType commandType, string commandText, IDataParameter[] commandParameterCollection)
		{
			OleDbCommand command = new OleDbCommand();
			PrepareCommand(command, commandType, commandText, commandParameterCollection);
			int iExecuteNonQuery = command.ExecuteNonQuery();
			command.Parameters.Clear();

			return iExecuteNonQuery;
		}

		#endregion ExecuteNonQuery

		#region ExecuteDataSet

		/// <summary>
		/// 
		/// </summary>
		/// <param name="commandType"></param>
		/// <param name="commandText"></param>
		/// <param name="commandParameterCollection"></param>
		/// <param name="ds"></param>
		/// <param name="tableName"></param>
		/// <returns></returns>
		public override DataSet ExecuteDataset(CommandType commandType, string commandText, IDataParameter[] commandParameterCollection, DataSet ds, string tableName)
		{
			OleDbCommand command = new OleDbCommand();
			PrepareCommand(command, commandType, commandText, commandParameterCollection);

			OleDbDataAdapter dataAdapter = new OleDbDataAdapter(command);
			if (tableName == null || (tableName.Length < 1))
				dataAdapter.Fill(ds);
			else
				dataAdapter.Fill(ds, tableName);
			command.Parameters.Clear();

			return ds;
		}

		#endregion ExecuteDataSet

		#region ExecuteReader	

		/// <summary>
		/// 
		/// </summary>
		/// <param name="commandType"></param>
		/// <param name="commandText"></param>
		/// <param name="commandParameterCollection"></param>
		/// <returns></returns>
		public override IDataReader ExecuteReader(CommandType commandType, string commandText, IDataParameter[] commandParameterCollection)
		{
			OleDbCommand command = new OleDbCommand();
			PrepareCommand(command, commandType, commandText, commandParameterCollection);
			OleDbDataReader oleDbDataReader = command.ExecuteReader();
			command.Parameters.Clear();

			return oleDbDataReader;
		}

		#endregion ExecuteReader	

		#region ExecuteScalar	

		/// <summary>
		/// 
		/// </summary>
		/// <param name="commandType"></param>
		/// <param name="commandText"></param>
		/// <param name="commandParameterCollection"></param>
		/// <returns></returns>
		public override object ExecuteScalar(CommandType commandType, string commandText, IDataParameter[] commandParameterCollection)
		{
			OleDbCommand command = new OleDbCommand();
			PrepareCommand(command, commandType, commandText, commandParameterCollection);
			object objExecuteScalar = command.ExecuteScalar();
			command.Parameters.Clear();

			return objExecuteScalar;
		}

		#endregion ExecuteScalar	

		#region ExecuteXmlReader	

		/// <summary>
		/// 
		/// </summary>
		/// <param name="commandType"></param>
		/// <param name="commandText"></param>
		/// <param name="commandParameterCollection"></param>
		/// <returns></returns>
		public override XmlReader ExecuteXmlReader(CommandType commandType, string commandText, IDataParameter[] commandParameterCollection)
		{
			DataSet ds = ExecuteDataset(commandType, commandText, commandParameterCollection);
			StringReader stringReader = new StringReader(ds.GetXml());
			XmlReader xmlReader;
			try
			{
				xmlReader = new XmlTextReader(stringReader);
			}
            catch (System.Exception e)
			{
				stringReader.Close();
				throw e;
			}

			return xmlReader;
		}

		#endregion ExecuteXmlReader

		#endregion

		/// <summary>
		/// 
		/// </summary>
		/// <param name="command"></param>
		/// <param name="commandType"></param>
		/// <param name="commandText"></param>
		/// <param name="commandParameterCollection"></param>
		private void PrepareCommand(OleDbCommand command, CommandType commandType, string commandText, IDataParameter[] commandParameterCollection)
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
							//if(oleDbParameters[index].Value == null)
							//{
							//	oleDbParameters[index].Value = DBNull.Value;
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