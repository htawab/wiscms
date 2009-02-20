//------------------------------------------------------------------------------
// <copyright file="EntryCollection.cs" company="Oriental Everwisdom">
//     Copyright (C) Oriental Everwisdom Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Wis.Toolkit.Settings
{
	/// <summary>
	/// 通用配置集合。
	/// </summary>
    public class EntryCollection : ICollection<Entry>
    {
        private SortedList list = new SortedList();

        /// <summary>
        /// 获得指定通用配置键的通用配置。
        /// </summary>
        public Entry this[int index]
        {
            get
            {
                return list.GetByIndex(index) as Entry;
            }
            set
            {
                list.SetByIndex(index, value);
            }
        }


        /// <summary>
        /// 获得指定通用配置键的通用配置。
        /// </summary>
        public Entry this[string key]
        {
            get
            {
                return list[key] as Entry;
            }
            set
            {
                list[key] = value;
            }
        }


        /// <summary>
        /// 新增配置。
        /// </summary>
        /// <param name="key">特定配置的键名称</param>
        /// <param name="value">特定配置的值</param>
        /// <returns>特定配置的插入位置</returns>
        public void Add(string key, Entry value)
        {
            list[key] = value;
        }


        /// <summary>
        /// 从通用配置集合中移除指定通用配置名称的通用配置。
        /// </summary>
        /// <param name="key">通用配置键</param>
        public void Remove(string key)
        {
            list.Remove(key);
        }


        /// <summary>
        /// 确定通用配置集合是否包含指定通用配置键的通用配置。
        /// </summary>
        /// <param name="key">通用配置键</param>
        /// <returns>如果在通用配置集合中找到指定通用配置名称的通用配置，则为 true；否则为 false</returns>
        public bool Contains(string key)
        {
            return list[key] == null;
        }


        /// <summary>
        /// 返回 System.Collections.SortedList 中指定键的从零开始的索引。
        /// </summary>
        /// <param name="key">通用配置键</param>
        /// <returns>如果在列表中找到，则为 key 的索引；否则为 -1</returns>
        public int IndexOfKey(string key)
        {
            return list.IndexOfKey(key);
        }


        #region ICollection<Entry> 成员

        public void Add(Entry item)
        {
            if (item == null) throw new System.ArgumentNullException("item");
            this.Add(item.Key, item);
        }

        public void Clear()
        {
            list.Clear();
        }

        public bool Contains(Entry item)
        {
            if (item == null) throw new System.ArgumentNullException("item");
            if (list[item.Key] == null) return false;
            return list[item.Key].ToString() == item.Value;
        }

        public void CopyTo(Entry[] array, int arrayIndex)
        {
            list.CopyTo(array, arrayIndex);
        }

        public int Count
        {
            get { return list.Count; }
        }

        public bool IsReadOnly
        {
            get { return list.IsReadOnly; }
        }

        public bool Remove(Entry item)
        {
            if (item == null) throw new System.ArgumentNullException("item");
            list.Remove(item.Key);
            return list[item.Key] == null;
        }

        #endregion

        #region IEnumerable<Entry> 成员

        public IEnumerator<Entry> GetEnumerator()
        {
            for (int index = 0; index < list.Count; index++)
            {
                yield return (Entry) list.GetByIndex(index);
            }
        }

        #endregion

        #region IEnumerable 成员

        IEnumerator IEnumerable.GetEnumerator()
        {
            return (IEnumerator)list.GetEnumerator();
        }

        #endregion
    }
}