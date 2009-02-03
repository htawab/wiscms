//------------------------------------------------------------------------------
// <copyright file="Cache.cs" company="Oriental Everwisdom">
//     Copyright (C) Oriental Everwisdom Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

namespace Wis.Toolkit.Caching
{
	/// <summary>
	/// 缓存项过期事件委托。
	/// </summary>
	public delegate void CacheItemExpiredEventHandler(object source, CacheItemExpiredEventArgs e);

	/// <summary>
	/// 缓存项获取事件委托，事件在缓存项不存在时触发。
	/// </summary>
	public delegate object CacheItemFetchEventHandler(object source, CacheItemFetchEventArgs e);

	/// <summary>
	/// 缓存，类的所有成员是线程安全。
	/// </summary>
	public class Cache
	{
		/// <summary>
		/// 缓存项过期事件委托。
		/// </summary>
		public static event CacheItemExpiredEventHandler	CacheItemExpiredEvent;
		
		/// <summary>
		/// 缓存项获取事件委托，事件在缓存项不存在时触发。
		/// </summary>
		public static event CacheItemFetchEventHandler		CacheItemFetchEvent;

		/// <summary>
		/// 关联的缓存键和缓存项的集合。
		/// </summary>
		private static Dictionary _Dictionary = new Dictionary(0);

		/// <summary>
		/// 定义支持单个写线程和多个读线程的锁。
		/// </summary>
		private static System.Threading.ReaderWriterLock _ReaderWriterLock = new System.Threading.ReaderWriterLock();


		/// <summary>
		/// 为指定的缓存键的缓存项设置值（如果有这样的缓存项）；否则将具有指定键和值的项添加到缓存。
		/// </summary>
		/// <param name="key">要设置的缓存项的缓存键。缓存键可以是 null。</param>
		/// <param name="item">表示要设置的缓存项。缓存项可以为 null。</param>
		public static void Set(string key, object item)
		{
			// 检测是否进行过期操作
			object tempItem;

			_ReaderWriterLock.AcquireReaderLock( -1 );

			try
			{
				// 获取缓存项
				tempItem = _Dictionary[key];
			}
			finally
			{
				_ReaderWriterLock.ReleaseReaderLock();
			}

			// 事件返回项为空，则须进行过期操作，如果新增缓存项超出缓存容量限制则过期旧项，以保存新项
			if (tempItem == null) Expired();			

			// 添加缓存项
			_ReaderWriterLock.AcquireWriterLock( -1 );
			try
			{
				_Dictionary.Set(key, item);
			}
			finally
			{
				_ReaderWriterLock.ReleaseWriterLock();
			}
		}

		
		/// <summary>
		/// 从缓存检索指定的缓存项。
		/// </summary>
		/// <param name="key">要检索的缓存项的标识符。</param>
		/// <returns>检索到的缓存项，未找到该键时为 null。</returns>
		public static object Get(string key)
		{
			object tempItem;

			_ReaderWriterLock.AcquireReaderLock( -1 );

			try
			{
				// 获取缓存项
				tempItem = _Dictionary[key];
			}
			finally
			{
				_ReaderWriterLock.ReleaseReaderLock();
			}

			// 如果缓存项存在，则返回
			if ( tempItem != null ) return tempItem;

			// 没有获取事件绑定，返回null
			if ( CacheItemFetchEvent == null ) return null;

			// 触发获取事件 ? null原本为this，如何添加source
			tempItem = CacheItemFetchEvent(null, new CacheItemFetchEventArgs(key));

			// 事件返回项为空，则返回
			if (tempItem == null) return null;

			// 获取项不为空，如果新增缓存项超出缓存容量限制则过期旧项，以保存新项
			Expired();

			_ReaderWriterLock.AcquireWriterLock( -1 );
			try
			{
				// 添加缓存项
				_Dictionary.Set(key, tempItem);
			}
			finally
			{
				_ReaderWriterLock.ReleaseWriterLock();
			}

			return tempItem;
		}


		/// <summary>
		/// 获取缓存中的键值对的数目。
		/// </summary>
		public static int Count
		{
			get
			{
				_ReaderWriterLock.AcquireReaderLock( -1 );

				try
				{
					return _Dictionary.Count;
				}
				finally
				{
					_ReaderWriterLock.ReleaseReaderLock(); 
				}
			}
		}


		/// <summary>
		/// 移除所有项。
		/// </summary>
		public static void Clear()
		{
			_ReaderWriterLock.AcquireWriterLock( -1 );
			_Dictionary.Clear();
			_ReaderWriterLock.ReleaseWriterLock();
		}


		/// <summary>
		/// 移除过时的缓存项。
		/// </summary>
		private static void Expired()
		{
			_ReaderWriterLock.AcquireWriterLock( -1 );

			try
			{
				// 如果容量设定为 0 ，则缓存项容量不受限制 ?需要测试
				if (_Capacity == 0) return;

				// 如果缓存的项超出关联的缓存键和缓存项的集合的键值对的数目，则过期旧项
				while (_Capacity - 1 < _Dictionary.Count)
				{
					string tempKey;
					object tempItem = _Dictionary.Expired(out tempKey);

					// 触发过期事件 ? null原本为this，如何添加source
					if ( CacheItemExpiredEvent != null )
						CacheItemExpiredEvent(null, new CacheItemExpiredEventArgs(tempKey, ref tempItem));
				}
			}
			finally
			{
				_ReaderWriterLock.ReleaseWriterLock();
			}
		}


		/// <summary>
		/// 缓存的容量。
		/// </summary>
		private static int _Capacity;

		/// <summary>
		/// 获得或设置缓存的容量。
		/// </summary>
		public static int Capacity
		{

			get { return _Capacity; }
			set
			{
				_Capacity = value;

				if (_Capacity < 0) _Capacity = 0;
				
				// Capacity 不能小于零。
				if (_Capacity > 0 && _Capacity < int.MaxValue)
				{
					// _Dictionary = new Dictionary(int.MaxValue);
					Dictionary tempDictionary = _Dictionary;
					_Dictionary = new Dictionary(_Capacity);
					
					// 获得记录数
					if(tempDictionary.Count > 0)
					{
						int tempCount = _Capacity;
						if(tempDictionary.Count < _Capacity) tempCount = tempDictionary.Count; 
					
						for(int index = 0; index < tempCount; index++)
						{
							string key = tempDictionary.Keys[index];
							object item = tempDictionary[key];
							_Dictionary.Set(key, item);
						}
					}
					
					// 移除所有缓存项
					tempDictionary.Clear();
				}
			}
		}
	}
}
