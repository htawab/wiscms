//------------------------------------------------------------------------------
// <copyright file="PermissionCollection.cs" company="Oriental Everwisdom">
//     Copyright (C) Oriental Everwisdom Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using System.Collections;

namespace Wis.Toolkit.Security
{
	/// <summary>
	/// 权限集合。
	/// </summary>
	public class PermissionCollection : CollectionBase
	{
		/// <summary>
		/// 通过类实现时，获取或设置在指定的索引处的元素
		/// </summary>
		public Permission this[int index]
		{
			get { return ((Permission) (List[index])); }
		}


		/// <summary>
		/// 通过类实现时，将一项添加到 System.Collections.IList
		/// </summary>
		/// <param name="value">要添加到 System.Collections.IList 的 Permission</param>
		/// <returns>新元素的插入位置</returns>
		public int Add(Permission value)
		{
			return List.Add(value);
		}


		/// <summary>
		/// 通过类实现时，从 System.Collections.IList 移除特定对象的第一个匹配项
		/// </summary>
		/// <param name="value">要从 System.Collections.IList 移除的 Permission</param>
		public void Remove(Permission value)
		{
			List.Remove(value);
		}


		/// <summary>
		/// 通过类实现时，确定 System.Collections.IList 是否包含特定的值
		/// </summary>
		/// <param name="value">要在 System.Collections.IList 中查找的 Permission</param>
		/// <returns>如果在 System.Collections.IList 中找到 Permission，则为 true；否则为 false</returns>
		public bool Contains(Permission value)
		{
			return List.Contains(value);
		}


		/// <summary>
		/// 通过类实现时，确定 System.Collections.IList 中特定项的索引
		/// </summary>
		/// <param name="value">要在 System.Collections.IList 中查找的 Permission</param>
		/// <returns>如果在列表中找到，则为 value 的索引；否则为 -1</returns>
		public int IndexOf(Permission value)
		{
			return List.IndexOf(value);
		}
	}
}
