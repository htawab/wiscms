//------------------------------------------------------------------------------
// <copyright file="Dictionary.cs" company="Oriental Everwisdom">
//     Copyright (C) Oriental Everwisdom Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using System.Collections.Specialized;

namespace Wis.Toolkit.Caching
{
	/// <summary>
	/// 为关联的缓存键和缓存项的集合（可通过键或索引来访问它）。
	/// </summary>
	internal class Dictionary : NameObjectCollectionBase
	{
	
		/// <summary>
		/// 初始化 Dictionary 类的新实例，该实例为空并且具有指定的初始容量。
		/// </summary>
		/// <param name="capacity">实例最初可以包含的项的大概数目。</param>
		public Dictionary(int capacity) : base( capacity )
		{
		}


		/// <summary>
		/// 移除过时的缓存项。
		/// </summary>
		/// <param name="key">用于引用过时缓存项的缓存键。</param>
		/// <returns>返回被移除的缓存项。</returns>
		public object Expired(out string key)
		{
			// 临时项
			object tempItem;

			// 没有可移除的项
			if ( Count < 1 ) // 键值对的数目 < 1
			{
                key = null;				
				return null;
			}

			// 获取过时的缓存项
			tempItem = BaseGet( 0 ); // 获取 System.Collections.Specialized.NameObjectCollectionBase 实例的指定索引处的项值。

			// 获取过时的缓存键
			key = BaseGetKey( 0 );

			// 移除过时的缓存
			BaseRemoveAt( 0 );

			return tempItem;
		}


		/// <summary>
		/// 获取缓存项。
		/// </summary>
		public object this[string key]
		{
			get
			{
				return Get(key);
			}
		}


		/// <summary>
		/// 添加缓存项。
		/// </summary>
		/// <param name="key">用于引用缓存项的缓存键。</param>
		/// <param name="item">要添加到缓存的项。</param>
		public void Set(string key, object item)
		{
			BaseSet(key, item);
		}
		
		
		/// <summary>
		/// 获取缓存项。
		/// </summary>
		/// <param name="key">缓存键。</param>
		/// <returns>返回缓存项。</returns>
		public object Get(string key)
		{
			// 临时项
			object tempItem;

			// 获取指定缓存键的缓存项
			tempItem = BaseGet(key);

			// 缓存项不为空则置顶
			if ( tempItem != null && Count > 1)
			{
				BaseRemove(key);
				BaseAdd(key, tempItem);
			}

			return tempItem;
		}
		
		
		/// <summary>
		/// 移除所有项。
		/// </summary>
		public void Clear()
		{
			BaseClear();
		}
	}
}
