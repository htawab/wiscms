/// <copyright>
/// 版本所有 (C) 2006-2007 HeatBet
/// </copyright>

namespace Wis.Toolkit.DataAccess
{
	/// <summary>
	/// 生产数据库访问类的工厂。
	/// </summary>
	public class DataAccessFactory
	{
		/// <summary>
		/// 根据数据库类型实例化不同的数据库访问类。
		/// </summary>
		/// <param name="providerType">数据库类型</param>
		/// <returns>数据库访问类实例</returns>
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
		/// 根据数据库连接和数据库类型实例化不同的数据库访问类。
		/// </summary>
		/// <param name="connectionString">数据库连接</param>
		/// <param name="providerType">数据库类型</param>
		/// <returns>数据库访问类实例</returns>
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