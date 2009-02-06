//------------------------------------------------------------------------------
// <copyright file="FrontendSetting.cs" company="Everwis">
//     Copyright (C) Everwis Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

namespace Wis.Website
{
	/// <summary>
	/// ��ȡ������ͨ�������ļ���
	/// </summary>
    public sealed class Setting
	{
        private Setting() { }

        /// <summary>
		/// ���ݿ������ַ�����
		/// </summary>
		public static string ConnectionString
		{
            get { return Wis.Toolkit.Settings.Setting.Current.Entries["ConnectionString"].Value; }
		}
        /// <summary>
        /// �������ݿ������ַ�����
        /// </summary>
        public static string MeetingConnectionString
        {
            get { return Wis.Toolkit.Settings.Setting.Current.Entries["MeetingConnectionString"].Value; }
        }		
	}
}
