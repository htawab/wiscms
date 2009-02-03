//------------------------------------------------------------------------------
// <copyright file="CacheItemFetchEventArgs.cs" company="Oriental Everwisdom">
//     Copyright (C) Oriental Everwisdom Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

namespace Wis.Toolkit.Caching
{
	/// <summary>
	/// 保存缓存获取事件的缓存键。
	/// </summary>
	public class CacheItemFetchEventArgs
	{

		/// <summary>
		/// 初始化 CacheItemFetchEventArgs 类的新实例，该实例保存指定的缓存键。
		/// </summary>
		/// <param name="key"></param>
		public CacheItemFetchEventArgs(string key)
		{
			_Key = key;
		}

		private string _Key;
		/// <summary>
		/// 缓存键。
		/// </summary>
		public string Key
		{
			get { return _Key; }
		}
	}
}