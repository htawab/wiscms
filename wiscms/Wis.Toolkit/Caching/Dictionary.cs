//------------------------------------------------------------------------------
// <copyright file="Dictionary.cs" company="Oriental Everwisdom">
//     Copyright (C) Oriental Everwisdom Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using System.Collections.Specialized;

namespace Wis.Toolkit.Caching
{
	/// <summary>
	/// Ϊ�����Ļ�����ͻ�����ļ��ϣ���ͨ����������������������
	/// </summary>
	internal class Dictionary : NameObjectCollectionBase
	{
	
		/// <summary>
		/// ��ʼ�� Dictionary �����ʵ������ʵ��Ϊ�ղ��Ҿ���ָ���ĳ�ʼ������
		/// </summary>
		/// <param name="capacity">ʵ��������԰�������Ĵ����Ŀ��</param>
		public Dictionary(int capacity) : base( capacity )
		{
		}


		/// <summary>
		/// �Ƴ���ʱ�Ļ����
		/// </summary>
		/// <param name="key">�������ù�ʱ������Ļ������</param>
		/// <returns>���ر��Ƴ��Ļ����</returns>
		public object Expired(out string key)
		{
			// ��ʱ��
			object tempItem;

			// û�п��Ƴ�����
			if ( Count < 1 ) // ��ֵ�Ե���Ŀ < 1
			{
                key = null;				
				return null;
			}

			// ��ȡ��ʱ�Ļ�����
			tempItem = BaseGet( 0 ); // ��ȡ System.Collections.Specialized.NameObjectCollectionBase ʵ����ָ������������ֵ��

			// ��ȡ��ʱ�Ļ����
			key = BaseGetKey( 0 );

			// �Ƴ���ʱ�Ļ���
			BaseRemoveAt( 0 );

			return tempItem;
		}


		/// <summary>
		/// ��ȡ�����
		/// </summary>
		public object this[string key]
		{
			get
			{
				return Get(key);
			}
		}


		/// <summary>
		/// ��ӻ����
		/// </summary>
		/// <param name="key">�������û�����Ļ������</param>
		/// <param name="item">Ҫ��ӵ�������</param>
		public void Set(string key, object item)
		{
			BaseSet(key, item);
		}
		
		
		/// <summary>
		/// ��ȡ�����
		/// </summary>
		/// <param name="key">�������</param>
		/// <returns>���ػ����</returns>
		public object Get(string key)
		{
			// ��ʱ��
			object tempItem;

			// ��ȡָ��������Ļ�����
			tempItem = BaseGet(key);

			// �����Ϊ�����ö�
			if ( tempItem != null && Count > 1)
			{
				BaseRemove(key);
				BaseAdd(key, tempItem);
			}

			return tempItem;
		}
		
		
		/// <summary>
		/// �Ƴ������
		/// </summary>
		public void Clear()
		{
			BaseClear();
		}
	}
}
