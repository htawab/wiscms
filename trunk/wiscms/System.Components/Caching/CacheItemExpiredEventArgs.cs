//------------------------------------------------------------------------------
// <copyright file="CacheItemExpiredEventArgs.cs" company="Oriental Everwisdom">
//     Copyright (C) Oriental Everwisdom Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

namespace Wis.Toolkit.Caching
{

	/// <summary>
	/// 保存缓存过期事件的缓存键和缓存项。
	/// </summary>
	public class CacheItemExpiredEventArgs
	{
		/// <summary>
		/// 初始化 CacheItemExpiredEventArgs 类的新实例，该实例保存指定的缓存键和缓存项。
		/// </summary>
		/// <param name="key">缓存键</param>
		/// <param name="item">缓存项</param>
		public CacheItemExpiredEventArgs(string key, ref object item)
		{
			_Key = key;
			_Item = item;
		}

		private object _Item;
		/// <summary>
		/// 缓存项。
		/// </summary>
		public object Item
		{
			get { return _Item;}
		}

		private string _Key;
		/// <summary>
		/// 缓存键。
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
