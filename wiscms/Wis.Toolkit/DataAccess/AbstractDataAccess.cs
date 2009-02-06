/// <copyright>
/// �汾���� (C) 2006-2007 HeatBet
/// </copyright>

using System.Data;
using System.Data.SqlClient;
using System.Xml;
using System.Data.OleDb;

namespace Wis.Toolkit.DataAccess
{
    /// <summary>
    /// AbstractDataAccess ��ժҪ˵����
    /// </summary>
    public abstract class AbstractDataAccess : IDataAccess
    {
        #region Components.DataAccess 

        #region Support Property & method

        /// <summary>
        /// ���ݿ������ַ�����
        /// </summary>
        public abstract string ConnectionString { get; set; }

        /// <summary>
        /// ���ݿ����͡�
        /// </summary>
        public abstract string ProviderType { get; }

        public abstract IDbConnection DbConnection { get; }

        /// <summary>
        /// �ر����ݿ����ӡ�
        /// </summary>
        public void Close()
        {
            DbConnection.Close();
        }

    	
		/// <summary>
		/// ִ�����ͷŻ����÷��й���Դ��ص�Ӧ�ó����������
		/// </summary>
		public void Dispose()
		{
			DbConnection.Dispose();
		}
    	
    	
        /// <summary>
        /// �����ݿ����ӡ�
        /// </summary>
        public void Open()
        {
            if (DbConnection.State.Equals(ConnectionState.Closed))
                DbConnection.Open();
        }

        /// <summary>
        /// ���ݿ������Ƿ�رա�
        /// </summary>
        public bool IsClosed
        {
            get { return DbConnection.State.Equals(ConnectionState.Closed); }
        }

        /// <summary>
        /// ��ʼ���ݿ�����
        /// </summary>
        /// <returns></returns>
        public abstract IDbTransaction BeginTransaction();

        /// <summary>
        /// �ύ����
        /// </summary>
        public abstract void CommitTransaction();

        /// <summary>
        /// �ع�����
        /// </summary>
        public abstract void RollbackTransaction();

        /// <summary>
        /// �Ƿ��л������
        /// </summary>
        public abstract bool HasTransaction { get; }


        /// <summary>
        /// �������ݿ��ֶ�����ʵ������ͬ�� Command �����ࡣ
        /// </summary>
        /// <param name="dbTypeName">���ݿ��ֶ����͡�</param>
        /// <returns>Command ������ʵ����</returns>
        public IDataParameter CreateDataParameter(string dbTypeName)
        {
            switch (ProviderType.ToLower().Trim())
            {
                case "access":
                    //System.Data.OleDb.OleDbParameter accessParameter = new OleDbParameter();
                    //accessParameter.OleDbType = GetAccessDbType(dbTypeName);
                    //return accessParameter;
                    throw new System.Exception("Access ���ݿⲻ֧����������:" + dbTypeName);
                case "odbc":
                    //OdbcParameter odbcParameter = new OdbcParameter();
                    //odbcParameter.OdbcType = GetOdbcType(dbTypeName);
                    //return odbcParameter;
                    throw new System.Exception("֧�� Odbc �����ݿⲻ֧����������:" + dbTypeName);
                case "oledb":
                    //OleDbParameter oleDbParameter = new OleDbParameter();
                    //oleDbParameter.OleDbType = GetOleDbType(dbTypeName);
                    //return oleDbParameter;
                    throw new System.Exception("֧�� OleDb �����ݿⲻ֧����������:" + dbTypeName);
                case "oracle":
                    //OracleParameter oracleParameter = new OracleParameter();
                    //oracleParameter.OracleType = GetOracleType(dbTypeName);
                    //return oracleParameter;
                    throw new System.Exception("Oracle ���ݿⲻ֧����������:" + dbTypeName);
                case "sqlserver":
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

                    throw new System.Exception("SqlServer ���ݿⲻ֧����������");

                case "sybase":
                    //OleDbParameter sybaseParameter = new OleDbParameter();
                    //sybaseParameter.OleDbType = GetSybaseDbType(dbTypeName);
                    //return sybaseParameter;
                    throw new System.Exception("Sybase ���ݿⲻ֧����������:" + dbTypeName);
            }

            return new OleDbParameter();
        }

        #endregion Support Property & method

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

        public abstract int ExecuteNonQuery(CommandType commandType, string commandText,
                                            IDataParameter[] commandParameters);

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

        public abstract DataSet ExecuteDataset(CommandType commandType, string commandText,
                                               IDataParameter[] commandParameters, DataSet ds, string tableName);

        #endregion ExecuteDataSet;

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

        public abstract IDataReader ExecuteReader(CommandType commandType, string commandText,
                                                  IDataParameter[] commandParameters);

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

        public abstract object ExecuteScalar(CommandType commandType, string commandText,
                                             IDataParameter[] commandParameters);

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

        public abstract XmlReader ExecuteXmlReader(CommandType commandType, string commandText,
                                                   IDataParameter[] commandParameters);

        #endregion ExecuteXmlReader

        #endregion
    }
}