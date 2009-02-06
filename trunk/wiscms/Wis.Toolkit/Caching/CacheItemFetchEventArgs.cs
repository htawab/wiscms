//------------------------------------------------------------------------------
// <copyright file="CacheItemFetchEventArgs.cs" company="Oriental Everwisdom">
//     Copyright (C) Oriental Everwisdom Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

namespace Wis.Toolkit.Caching
{
	/// <summary>
	/// ���滺���ȡ�¼��Ļ������
	/// </summary>
	public class CacheItemFetchEventArgs
	{

		/// <summary>
		/// ��ʼ�� CacheItemFetchEventArgs �����ʵ������ʵ������ָ���Ļ������
		/// </summary>
		/// <param name="key"></param>
		public CacheItemFetchEventArgs(string key)
		{
			_Key = key;
		}

		private string _Key;
		/// <summary>
		/// �������
		/// </summary>
		public string Key
		{
			get { return _Key; }
		}
	}
}