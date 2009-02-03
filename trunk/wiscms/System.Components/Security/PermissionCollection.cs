//------------------------------------------------------------------------------
// <copyright file="PermissionCollection.cs" company="Oriental Everwisdom">
//     Copyright (C) Oriental Everwisdom Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using System.Collections;

namespace Wis.Toolkit.Security
{
	/// <summary>
	/// Ȩ�޼��ϡ�
	/// </summary>
	public class PermissionCollection : CollectionBase
	{
		/// <summary>
		/// ͨ����ʵ��ʱ����ȡ��������ָ������������Ԫ��
		/// </summary>
		public Permission this[int index]
		{
			get { return ((Permission) (List[index])); }
		}


		/// <summary>
		/// ͨ����ʵ��ʱ����һ����ӵ� System.Collections.IList
		/// </summary>
		/// <param name="value">Ҫ��ӵ� System.Collections.IList �� Permission</param>
		/// <returns>��Ԫ�صĲ���λ��</returns>
		public int Add(Permission value)
		{
			return List.Add(value);
		}


		/// <summary>
		/// ͨ����ʵ��ʱ���� System.Collections.IList �Ƴ��ض�����ĵ�һ��ƥ����
		/// </summary>
		/// <param name="value">Ҫ�� System.Collections.IList �Ƴ��� Permission</param>
		public void Remove(Permission value)
		{
			List.Remove(value);
		}


		/// <summary>
		/// ͨ����ʵ��ʱ��ȷ�� System.Collections.IList �Ƿ�����ض���ֵ
		/// </summary>
		/// <param name="value">Ҫ�� System.Collections.IList �в��ҵ� Permission</param>
		/// <returns>����� System.Collections.IList ���ҵ� Permission����Ϊ true������Ϊ false</returns>
		public bool Contains(Permission value)
		{
			return List.Contains(value);
		}


		/// <summary>
		/// ͨ����ʵ��ʱ��ȷ�� System.Collections.IList ���ض��������
		/// </summary>
		/// <param name="value">Ҫ�� System.Collections.IList �в��ҵ� Permission</param>
		/// <returns>������б����ҵ�����Ϊ value ������������Ϊ -1</returns>
		public int IndexOf(Permission value)
		{
			return List.IndexOf(value);
		}
	}
}
