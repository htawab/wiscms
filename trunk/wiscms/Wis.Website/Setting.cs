//------------------------------------------------------------------------------
// <copyright file="FrontendSetting.cs" company="Everwis">
//     Copyright (C) Everwis Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

namespace Wis.Website
{
	/// <summary>
	/// 读取和设置通用配置文件。
	/// </summary>
    public sealed class Setting
	{
        private Setting() { }

        /// <summary>
		/// 数据库链接字符串。
		/// </summary>
		public static string ConnectionString
		{
            get { return Wis.Toolkit.Settings.Setting.Current.Entries["ConnectionString"].Value; }
		}
        /// <summary>
        /// 会议数据库链接字符串。
        /// </summary>
        public static string MeetingConnectionString
        {
            get { return Wis.Toolkit.Settings.Setting.Current.Entries["MeetingConnectionString"].Value; }
        }		
	}
}
