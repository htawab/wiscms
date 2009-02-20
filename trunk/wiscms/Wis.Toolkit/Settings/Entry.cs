//------------------------------------------------------------------------------
// <copyright file="Entry.cs" company="Oriental Everwisdom">
//     Copyright (C) Oriental Everwisdom Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using System.Xml.Serialization;

namespace Wis.Toolkit.Settings
{
	/// <summary>
	/// ��������û������ͨ���ֵ��ֵ�ԡ�
	/// </summary>
	public class Entry
	{
        private string entryKey;
        /// <summary>
        /// ����
        /// </summary>
        [XmlAttribute(AttributeName = "Key")]
        public string Key
        {
            get { return entryKey; }
            set { entryKey = value; }
        }

        private string entryValue;
        /// <summary>
        /// ֵ��
        /// </summary>
        [XmlAttribute(AttributeName = "Value")]
        public string Value
        {
            get { return entryValue; }
            set { entryValue = value; }
        }
	}
}
