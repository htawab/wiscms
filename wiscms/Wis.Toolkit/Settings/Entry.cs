//------------------------------------------------------------------------------
// <copyright file="Entry.cs" company="Oriental Everwisdom">
//     Copyright (C) Oriental Everwisdom Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using System.Xml.Serialization;

namespace Wis.Toolkit.Settings
{
	/// <summary>
	/// 定义可设置或检索的通用字典键值对。
	/// </summary>
	public class Entry
	{
        private string entryKey;
        /// <summary>
        /// 键。
        /// </summary>
        [XmlAttribute(AttributeName = "Key")]
        public string Key
        {
            get { return entryKey; }
            set { entryKey = value; }
        }

        private string entryValue;
        /// <summary>
        /// 值。
        /// </summary>
        [XmlAttribute(AttributeName = "Value")]
        public string Value
        {
            get { return entryValue; }
            set { entryValue = value; }
        }
	}
}
