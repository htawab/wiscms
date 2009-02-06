/// <copyright>
/// �汾���� (C) 2006-2007 HeatBet
/// </copyright>

using System.Xml;
using System.Data;

namespace Wis.Toolkit.DataAccess
{
    /// <summary>
    /// ���ݷ��ʽӿڡ�
    /// </summary>
    public interface IDataAccess
    {
        #region Support Property & Method

        /// <summary>
        /// ���ݿ������ַ���
        /// </summary>
        string ConnectionString { get; set; }

        /// <summary>
        /// ���ݿ�����
        /// </summary>
        string ProviderType { get; }

        /// <summary>
        /// ���ݿ�����
        /// </summary>
        IDbConnection DbConnection { get; }

        /// <summary>
        /// ��ʼ���ݿ�����
        /// </summary>
        /// <returns></returns>
        IDbTransaction BeginTransaction();

        /// <summary>
        /// �ύ���ݿ�����
        /// </summary>
        void CommitTransaction();

        /// <summary>
        /// �ع����ݿ�����
        /// </summary>
        void RollbackTransaction();

        /// <summary>
        /// �Ƿ��л������
        /// </summary>
        bool HasTransaction { get; }

        /// <summary>
        /// ������
        /// </summary>
        void Open();

        /// <summary>
        /// �ر�����
        /// </summary>
        void Close();

    	
    	/// <summary>
    	/// ִ�����ͷŻ����÷��й���Դ��ص�Ӧ�ó����������
    	/// </summary>
    	void Dispose();
    	
    	
        /// <summary>
        /// ָʾ���ݿ������Ƿ�ر���
        /// </summary>
        bool IsClosed { get; }


        /// <summary>
        /// �������ݿ��ֶ�����ʵ������ͬ�� Command �����ࡣ
        /// </summary>
        /// <param name="dbTypeName">���ݿ��ֶ����͡�</param>
        /// <returns>Command ������ʵ����</returns>
        IDataParameter CreateDataParameter(string dbTypeName);

        #endregion 

        #region ExecuteNonQuery

        /// <summary>
        /// ִ�� SQL ��䣬��������Ӱ�������
        /// </summary>
        /// <param name="commandType">ָʾ��ָ����ν��� CommandText ���ԡ�Ĭ��ֵΪ Text��</param>
        /// <param name="commandText">׼��Ҫִ�е� SQL ���</param>
        /// <returns>��Ӱ�������</returns>
        int ExecuteNonQuery(CommandType commandType, string commandText);

        /// <summary>
        /// ִ�� SQL ��䣬��������Ӱ�������
        /// </summary>
        /// <param name="commandText">׼��Ҫִ�е� SQL ���</param>
        /// <returns>��Ӱ�������</returns>
        int ExecuteNonQuery(string commandText);

        /// <summary>
        /// ִ�� SQL ��䣬��������Ӱ�������
        /// </summary>
        /// <param name="commandText">׼��Ҫִ�е� SQL ���</param>
        /// <param name="commandParameters">��صĲ�������</param>
        /// <returns>��Ӱ�������</returns>
        int ExecuteNonQuery(string commandText, IDataParameter[] commandParameters);

        /// <summary>
        /// ִ�� SQL ��䣬��������Ӱ�������
        /// </summary>
        /// <param name="commandType">ָʾ��ָ����ν��� CommandText ���ԡ�Ĭ��ֵΪ Text��</param>
        /// <param name="commandText">׼��Ҫִ�е� SQL ���</param>
        /// <param name="commandParameters">��صĲ�������</param>
        /// <returns>��Ӱ�������</returns>
        int ExecuteNonQuery(CommandType commandType, string commandText, IDataParameter[] commandParameters);

        #endregion ExecuteNonQuery

        #region ExecuteDataSet

        /// <summary>
        /// ִ��SQL��䣬������DataSet����ʽ���ؽ��
        /// </summary>
        /// <param name="commandType">ָʾ��ָ����ν��� CommandText ���ԡ�Ĭ��ֵΪ Text��</param>
        /// <param name="commandText">׼��Ҫִ�е� SQL ���</param>
        /// <returns>��DataSet����ʽ���صĽ��</returns>
        DataSet ExecuteDataset(CommandType commandType, string commandText);

        /// <summary>
        /// ִ��SQL��䣬������DataSet����ʽ���ؽ��
        /// </summary>
        /// <param name="commandText">׼��Ҫִ�е� SQL ���</param>
        /// <returns>��DataSet����ʽ���صĽ��</returns>
        DataSet ExecuteDataset(string commandText);

        /// <summary>
        /// ִ��SQL��䣬������DataSet����ʽ���ؽ��
        /// </summary>
        /// <param name="commandType">ָʾ��ָ����ν��� CommandText ���ԡ�Ĭ��ֵΪ Text��</param>
        /// <param name="commandText">׼��Ҫִ�е� SQL ���</param>
        /// <param name="commandParameters">��صĲ�������</param>
        /// <returns>��DataSet����ʽ���صĽ��</returns>
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
        /// ִ��SQL��䣬������DataReader����ʽ���ؽ��
        /// </summary>
        /// <param name="commandType">ָʾ��ָ����ν��� CommandText ���ԡ�Ĭ��ֵΪ Text��</param>
        /// <param name="commandText">׼��Ҫִ�е� SQL ���</param>
        /// <returns>��DataReader����ʽ���صĽ��</returns>
        IDataReader ExecuteReader(CommandType commandType, string commandText);

        IDataReader ExecuteReader(string commandText);
        IDataReader ExecuteReader(CommandType commandType, string commandText, IDataParameter[] commandParameters);
        IDataReader ExecuteReader(string commandText, IDataParameter[] commandParameters);

        #endregion ExecuteReader

        #region ExecuteScalar

        /// <summary>
        /// ִ�в�ѯ�������ز�ѯ�����صĽ�����е�һ�еĵ�һ�С����Զ�����л��С�
        /// </summary>
        /// <param name="commandType">ָʾ��ָ����ν��� CommandText ���ԡ�Ĭ��ֵΪ Text��</param>
        /// <param name="commandText">׼��Ҫִ�е� SQL ���</param>
        /// <returns>��ѯ�����صĽ�����е�һ�еĵ�һ��</returns>
        object ExecuteScalar(CommandType commandType, string commandText);

        object ExecuteScalar(string commandText);
        object ExecuteScalar(CommandType commandType, string commandText, IDataParameter[] commandParameters);
        object ExecuteScalar(string commandText, IDataParameter[] commandParameters);

        #endregion ExecuteScalar	

        #region ExecuteXmlReader

        /// <summary>
        /// ִ��SQL��䣬������XmlReader����ʽ���ؽ��
        /// </summary>
        /// <param name="commandType">ָʾ��ָ����ν��� CommandText ���ԡ�Ĭ��ֵΪ Text��</param>
        /// <param name="commandText">׼��Ҫִ�е� SQL ���</param>
        /// <returns>��XmlReader����ʽ���صĽ��</returns>
        XmlReader ExecuteXmlReader(CommandType commandType, string commandText);

        XmlReader ExecuteXmlReader(string commandText);

        XmlReader ExecuteXmlReader(CommandType commandType, string commandText,
                                   IDataParameter[] commandParameters);

        XmlReader ExecuteXmlReader(string commandText, IDataParameter[] commandParameters);

        #endregion ExecuteXmlReader
    }
}