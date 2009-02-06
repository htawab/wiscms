/// <copyright>
/// 版本所有 (C) 2006-2007 HeatBet
/// </copyright>

using System.Xml;
using System.Data;

namespace Wis.Toolkit.DataAccess
{
    /// <summary>
    /// 数据访问接口。
    /// </summary>
    public interface IDataAccess
    {
        #region Support Property & Method

        /// <summary>
        /// 数据库连接字符串
        /// </summary>
        string ConnectionString { get; set; }

        /// <summary>
        /// 数据库类型
        /// </summary>
        string ProviderType { get; }

        /// <summary>
        /// 数据库连接
        /// </summary>
        IDbConnection DbConnection { get; }

        /// <summary>
        /// 开始数据库事务
        /// </summary>
        /// <returns></returns>
        IDbTransaction BeginTransaction();

        /// <summary>
        /// 提交数据库事务
        /// </summary>
        void CommitTransaction();

        /// <summary>
        /// 回滚数据库事务
        /// </summary>
        void RollbackTransaction();

        /// <summary>
        /// 是否有活动的事务
        /// </summary>
        bool HasTransaction { get; }

        /// <summary>
        /// 打开连接
        /// </summary>
        void Open();

        /// <summary>
        /// 关闭连接
        /// </summary>
        void Close();

    	
    	/// <summary>
    	/// 执行与释放或重置非托管资源相关的应用程序定义的任务。
    	/// </summary>
    	void Dispose();
    	
    	
        /// <summary>
        /// 指示数据库连接是否关闭了
        /// </summary>
        bool IsClosed { get; }


        /// <summary>
        /// 根据数据库字段类型实例化不同的 Command 参数类。
        /// </summary>
        /// <param name="dbTypeName">数据库字段类型。</param>
        /// <returns>Command 参数类实例。</returns>
        IDataParameter CreateDataParameter(string dbTypeName);

        #endregion 

        #region ExecuteNonQuery

        /// <summary>
        /// 执行 SQL 语句，并返回受影响的行数
        /// </summary>
        /// <param name="commandType">指示或指定如何解释 CommandText 属性。默认值为 Text。</param>
        /// <param name="commandText">准备要执行的 SQL 语句</param>
        /// <returns>受影响的行数</returns>
        int ExecuteNonQuery(CommandType commandType, string commandText);

        /// <summary>
        /// 执行 SQL 语句，并返回受影响的行数
        /// </summary>
        /// <param name="commandText">准备要执行的 SQL 语句</param>
        /// <returns>受影响的行数</returns>
        int ExecuteNonQuery(string commandText);

        /// <summary>
        /// 执行 SQL 语句，并返回受影响的行数
        /// </summary>
        /// <param name="commandText">准备要执行的 SQL 语句</param>
        /// <param name="commandParameters">相关的参数集合</param>
        /// <returns>受影响的行数</returns>
        int ExecuteNonQuery(string commandText, IDataParameter[] commandParameters);

        /// <summary>
        /// 执行 SQL 语句，并返回受影响的行数
        /// </summary>
        /// <param name="commandType">指示或指定如何解释 CommandText 属性。默认值为 Text。</param>
        /// <param name="commandText">准备要执行的 SQL 语句</param>
        /// <param name="commandParameters">相关的参数集合</param>
        /// <returns>受影响的行数</returns>
        int ExecuteNonQuery(CommandType commandType, string commandText, IDataParameter[] commandParameters);

        #endregion ExecuteNonQuery

        #region ExecuteDataSet

        /// <summary>
        /// 执行SQL语句，并且以DataSet的形式返回结果
        /// </summary>
        /// <param name="commandType">指示或指定如何解释 CommandText 属性。默认值为 Text。</param>
        /// <param name="commandText">准备要执行的 SQL 语句</param>
        /// <returns>以DataSet的形式返回的结果</returns>
        DataSet ExecuteDataset(CommandType commandType, string commandText);

        /// <summary>
        /// 执行SQL语句，并且以DataSet的形式返回结果
        /// </summary>
        /// <param name="commandText">准备要执行的 SQL 语句</param>
        /// <returns>以DataSet的形式返回的结果</returns>
        DataSet ExecuteDataset(string commandText);

        /// <summary>
        /// 执行SQL语句，并且以DataSet的形式返回结果
        /// </summary>
        /// <param name="commandType">指示或指定如何解释 CommandText 属性。默认值为 Text。</param>
        /// <param name="commandText">准备要执行的 SQL 语句</param>
        /// <param name="commandParameters">相关的参数集合</param>
        /// <returns>以DataSet的形式返回的结果</returns>
        DataSet ExecuteDataset(CommandType commandType, string commandText, IDataParameter[] commandParameters);

        DataSet ExecuteDataset(string commandText, IDataParameter[] commandParameters);

        DataSet ExecuteDataset(CommandType commandType, string commandText, string tableName);
        DataSet ExecuteDataset(string commandText, string tableName);

        DataSet ExecuteDataset(CommandType commandType, string commandText, IDataParameter[] commandParameters,
                               string tableName);

        DataSet ExecuteDataset(string commandText, IDataParameter[] commandParameters, string tableName);

        DataSet ExecuteDataset(CommandType commandType, string commandText, DataSet ds);
        DataSet ExecuteDataset(string commandText, DataSet ds);

        DataSet ExecuteDataset(CommandType commandType, string commandText, IDataParameter[] commandParameters,
                               DataSet ds);

        DataSet ExecuteDataset(string commandText, IDataParameter[] commandParameters, DataSet ds);

        DataSet ExecuteDataset(CommandType commandType, string commandText, DataSet ds, string tableName);
        DataSet ExecuteDataset(string commandText, DataSet ds, string tableName);

        DataSet ExecuteDataset(CommandType commandType, string commandText, IDataParameter[] commandParameters,
                               DataSet ds, string tableName);

        DataSet ExecuteDataset(string commandText, IDataParameter[] commandParameters, DataSet ds,
                               string tableName);

        #endregion ExecuteDataSet

        #region ExecuteReader		

        /// <summary>
        /// 执行SQL语句，并且以DataReader的形式返回结果
        /// </summary>
        /// <param name="commandType">指示或指定如何解释 CommandText 属性。默认值为 Text。</param>
        /// <param name="commandText">准备要执行的 SQL 语句</param>
        /// <returns>以DataReader的形式返回的结果</returns>
        IDataReader ExecuteReader(CommandType commandType, string commandText);

        IDataReader ExecuteReader(string commandText);
        IDataReader ExecuteReader(CommandType commandType, string commandText, IDataParameter[] commandParameters);
        IDataReader ExecuteReader(string commandText, IDataParameter[] commandParameters);

        #endregion ExecuteReader

        #region ExecuteScalar

        /// <summary>
        /// 执行查询，并返回查询所返回的结果集中第一行的第一列。忽略额外的列或行。
        /// </summary>
        /// <param name="commandType">指示或指定如何解释 CommandText 属性。默认值为 Text。</param>
        /// <param name="commandText">准备要执行的 SQL 语句</param>
        /// <returns>查询所返回的结果集中第一行的第一列</returns>
        object ExecuteScalar(CommandType commandType, string commandText);

        object ExecuteScalar(string commandText);
        object ExecuteScalar(CommandType commandType, string commandText, IDataParameter[] commandParameters);
        object ExecuteScalar(string commandText, IDataParameter[] commandParameters);

        #endregion ExecuteScalar	

        #region ExecuteXmlReader

        /// <summary>
        /// 执行SQL语句，并且以XmlReader的形式返回结果
        /// </summary>
        /// <param name="commandType">指示或指定如何解释 CommandText 属性。默认值为 Text。</param>
        /// <param name="commandText">准备要执行的 SQL 语句</param>
        /// <returns>以XmlReader的形式返回的结果</returns>
        XmlReader ExecuteXmlReader(CommandType commandType, string commandText);

        XmlReader ExecuteXmlReader(string commandText);

        XmlReader ExecuteXmlReader(CommandType commandType, string commandText,
                                   IDataParameter[] commandParameters);

        XmlReader ExecuteXmlReader(string commandText, IDataParameter[] commandParameters);

        #endregion ExecuteXmlReader
    }
}