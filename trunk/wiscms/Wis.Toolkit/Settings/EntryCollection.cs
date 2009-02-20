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
	/// ͨ�����ü��ϡ�
	/// </summary>
    public class EntryCollection : ICollection<Entry>
    {
        private SortedList list = new SortedList();

        /// <summary>
        /// ���ָ��ͨ�����ü���ͨ�����á�
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
        /// ���ָ��ͨ�����ü���ͨ�����á�
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
        /// �������á�
        /// </summary>
        /// <param name="key">�ض����õļ�����</param>
        /// <param name="value">�ض����õ�ֵ</param>
        /// <returns>�ض����õĲ���λ��</returns>
        public void Add(string key, Entry value)
        {
            list[key] = value;
        }


        /// <summary>
        /// ��ͨ�����ü������Ƴ�ָ��ͨ���������Ƶ�ͨ�����á�
        /// </summary>
        /// <param name="key">ͨ�����ü�</param>
        public void Remove(string key)
        {
            list.Remove(key);
        }


        /// <summary>
        /// ȷ��ͨ�����ü����Ƿ����ָ��ͨ�����ü���ͨ�����á�
        /// </summary>
        /// <param name="key">ͨ�����ü�</param>
        /// <returns>�����ͨ�����ü������ҵ�ָ��ͨ���������Ƶ�ͨ�����ã���Ϊ true������Ϊ false</returns>
        public bool Contains(string key)
        {
            return list[key] == null;
        }


        /// <summary>
        /// ���� System.Collections.SortedList ��ָ�����Ĵ��㿪ʼ��������
        /// </summary>
        /// <param name="key">ͨ�����ü�</param>
        /// <returns>������б����ҵ�����Ϊ key ������������Ϊ -1</returns>
        public int IndexOfKey(string key)
        {
            return list.IndexOfKey(key);
        }


        #region ICollection<Entry> ��Ա

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

        #region IEnumerable<Entry> ��Ա

        public IEnumerator<Entry> GetEnumerator()
        {
            for (int index = 0; index < list.Count; index++)
            {
                yield return (Entry) list.GetByIndex(index);
            }
        }

        #endregion

        #region IEnumerable ��Ա

        IEnumerator IEnumerable.GetEnumerator()
        {
            return (IEnumerator)list.GetEnumerator();
        }

        #endregion
    }
}