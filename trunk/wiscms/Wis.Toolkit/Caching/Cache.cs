//------------------------------------------------------------------------------
// <copyright file="Cache.cs" company="Oriental Everwisdom">
//     Copyright (C) Oriental Everwisdom Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

namespace Wis.Toolkit.Caching
{
	/// <summary>
	/// ����������¼�ί�С�
	/// </summary>
	public delegate void CacheItemExpiredEventHandler(object source, CacheItemExpiredEventArgs e);

	/// <summary>
	/// �������ȡ�¼�ί�У��¼��ڻ��������ʱ������
	/// </summary>
	public delegate object CacheItemFetchEventHandler(object source, CacheItemFetchEventArgs e);

	/// <summary>
	/// ���棬������г�Ա���̰߳�ȫ��
	/// </summary>
	public class Cache
	{
		/// <summary>
		/// ����������¼�ί�С�
		/// </summary>
		public static event CacheItemExpiredEventHandler	CacheItemExpiredEvent;
		
		/// <summary>
		/// �������ȡ�¼�ί�У��¼��ڻ��������ʱ������
		/// </summary>
		public static event CacheItemFetchEventHandler		CacheItemFetchEvent;

		/// <summary>
		/// �����Ļ�����ͻ�����ļ��ϡ�
		/// </summary>
		private static Dictionary _Dictionary = new Dictionary(0);

		/// <summary>
		/// ����֧�ֵ���д�̺߳Ͷ�����̵߳�����
		/// </summary>
		private static System.Threading.ReaderWriterLock _ReaderWriterLock = new System.Threading.ReaderWriterLock();


		/// <summary>
		/// Ϊָ���Ļ�����Ļ���������ֵ������������Ļ���������򽫾���ָ������ֵ������ӵ����档
		/// </summary>
		/// <param name="key">Ҫ���õĻ�����Ļ����������������� null��</param>
		/// <param name="item">��ʾҪ���õĻ�������������Ϊ null��</param>
		public static void Set(string key, object item)
		{
			// ����Ƿ���й��ڲ���
			object tempItem;

			_ReaderWriterLock.AcquireReaderLock( -1 );

			try
			{
				// ��ȡ������
				tempItem = _Dictionary[key];
			}
			finally
			{
				_ReaderWriterLock.ReleaseReaderLock();
			}

			// �¼�������Ϊ�գ�������й��ڲ�����������������������������������ھ���Ա�������
			if (tempItem == null) Expired();			

			// ��ӻ�����
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
		/// �ӻ������ָ���Ļ����
		/// </summary>
		/// <param name="key">Ҫ�����Ļ�����ı�ʶ����</param>
		/// <returns>�������Ļ����δ�ҵ��ü�ʱΪ null��</returns>
		public static object Get(string key)
		{
			object tempItem;

			_ReaderWriterLock.AcquireReaderLock( -1 );

			try
			{
				// ��ȡ������
				tempItem = _Dictionary[key];
			}
			finally
			{
				_ReaderWriterLock.ReleaseReaderLock();
			}

			// �����������ڣ��򷵻�
			if ( tempItem != null ) return tempItem;

			// û�л�ȡ�¼��󶨣�����null
			if ( CacheItemFetchEvent == null ) return null;

			// ������ȡ�¼� ? nullԭ��Ϊthis��������source
			tempItem = CacheItemFetchEvent(null, new CacheItemFetchEventArgs(key));

			// �¼�������Ϊ�գ��򷵻�
			if (tempItem == null) return null;

			// ��ȡ�Ϊ�գ�������������������������������ھ���Ա�������
			Expired();

			_ReaderWriterLock.AcquireWriterLock( -1 );
			try
			{
				// ��ӻ�����
				_Dictionary.Set(key, tempItem);
			}
			finally
			{
				_ReaderWriterLock.ReleaseWriterLock();
			}

			return tempItem;
		}


		/// <summary>
		/// ��ȡ�����еļ�ֵ�Ե���Ŀ��
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
		/// �Ƴ������
		/// </summary>
		public static void Clear()
		{
			_ReaderWriterLock.AcquireWriterLock( -1 );
			_Dictionary.Clear();
			_ReaderWriterLock.ReleaseWriterLock();
		}


		/// <summary>
		/// �Ƴ���ʱ�Ļ����
		/// </summary>
		private static void Expired()
		{
			_ReaderWriterLock.AcquireWriterLock( -1 );

			try
			{
				// ��������趨Ϊ 0 ���򻺴��������������� ?��Ҫ����
				if (_Capacity == 0) return;

				// ����������������Ļ�����ͻ�����ļ��ϵļ�ֵ�Ե���Ŀ������ھ���
				while (_Capacity - 1 < _Dictionary.Count)
				{
					string tempKey;
					object tempItem = _Dictionary.Expired(out tempKey);

					// ���������¼� ? nullԭ��Ϊthis��������source
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
		/// �����������
		/// </summary>
		private static int _Capacity;

		/// <summary>
		/// ��û����û����������
		/// </summary>
		public static int Capacity
		{

			get { return _Capacity; }
			set
			{
				_Capacity = value;

				if (_Capacity < 0) _Capacity = 0;
				
				// Capacity ����С���㡣
				if (_Capacity > 0 && _Capacity < int.MaxValue)
				{
					// _Dictionary = new Dictionary(int.MaxValue);
					Dictionary tempDictionary = _Dictionary;
					_Dictionary = new Dictionary(_Capacity);
					
					// ��ü�¼��
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
					
					// �Ƴ����л�����
					tempDictionary.Clear();
				}
			}
		}
	}
}
