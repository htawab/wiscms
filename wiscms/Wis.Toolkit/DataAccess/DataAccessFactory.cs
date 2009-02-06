/// <copyright>
/// �汾���� (C) 2006-2007 HeatBet
/// </copyright>

namespace Wis.Toolkit.DataAccess
{
	/// <summary>
	/// �������ݿ������Ĺ�����
	/// </summary>
	public class DataAccessFactory
	{
		/// <summary>
		/// �������ݿ�����ʵ������ͬ�����ݿ�����ࡣ
		/// </summary>
		/// <param name="providerType">���ݿ�����</param>
		/// <returns>���ݿ������ʵ��</returns>
		public static IDataAccess CreateDataAccess(string providerType)
		{
			IDataAccess dataAccess;

			providerType = providerType.ToLower().Trim();
			if (providerType == "sqlserver")
			{
				dataAccess = new SqlServerDataAccess();
			}
			else if (providerType == "oracle")
			{
				dataAccess = new OracleDataAccess();
			}
			else if (providerType == "odbc")
			{
				dataAccess = new OdbcDataAccess();
			}
			else
			{
				dataAccess = new OleDbDataAccess();
			}

			return dataAccess;
		}

		/// <summary>
		/// �������ݿ����Ӻ����ݿ�����ʵ������ͬ�����ݿ�����ࡣ
		/// </summary>
		/// <param name="connectionString">���ݿ�����</param>
		/// <param name="providerType">���ݿ�����</param>
		/// <returns>���ݿ������ʵ��</returns>
		public static IDataAccess CreateDataAccess(string connectionString, string providerType)
		{
			IDataAccess dataAccess;

			providerType = providerType.ToLower().Trim();
			if (providerType == "sqlserver")
			{
				dataAccess = new SqlServerDataAccess(connectionString);
			}
			else if (providerType == "oracle")
			{
				dataAccess = new OracleDataAccess(connectionString);
			}
			else if (providerType == "odbc")
			{
				dataAccess = new OdbcDataAccess(connectionString);
			}
			else
			{
				dataAccess = new OleDbDataAccess(connectionString);
			}

			return dataAccess;
		}

	}
}