//------------------------------------------------------------------------------
// <copyright file="CacheItemExpiredEventArgs.cs" company="Oriental Everwisdom">
//     Copyright (C) Oriental Everwisdom Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

namespace Wis.Toolkit.Caching
{

	/// <summary>
	/// ���滺������¼��Ļ�����ͻ����
	/// </summary>
	public class CacheItemExpiredEventArgs
	{
		/// <summary>
		/// ��ʼ�� CacheItemExpiredEventArgs �����ʵ������ʵ������ָ���Ļ�����ͻ����
		/// </summary>
		/// <param name="key">�����</param>
		/// <param name="item">������</param>
		public CacheItemExpiredEventArgs(string key, ref object item)
		{
			_Key = key;
			_Item = item;
		}

		private object _Item;
		/// <summary>
		/// �����
		/// </summary>
		public object Item
		{
			get { return _Item;}
		}

		private string _Key;
		/// <summary>
		/// �������
		/// </summary>
		public string Key
		{
			get
			{
				return _Key;
			}
		}
	}
}
