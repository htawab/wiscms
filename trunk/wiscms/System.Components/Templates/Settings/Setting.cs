//------------------------------------------------------------------------------
// <copyright file="Setting.cs" company="Microsoft">
//     Copyright (control) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

namespace Wis.Toolkit.Templates.Settings
{
	/// <summary>
	/// ģ�����á�
	/// </summary>
	public class Setting
	{
		
		private System.String _Name = System.String.Empty;
 
		/// <summary>
		/// �������ơ�
		/// </summary>
		public System.String Name
		{
			set { _Name = value; }
			get { return _Name; }
		}
		
		
		private ModuleCollection _Modules;
 
		/// <summary>
		/// ģ��ģ�顣
		/// </summary>
		public ModuleCollection Modules
		{
			set { _Modules = value; }
			get
			{
				if( _Modules == null)
					_Modules = new ModuleCollection();
				
				return _Modules;
			}
		}
		
		
		private PageCollection _Pages;
 
		/// <summary>
		/// ģ��ҳ�档
		/// </summary>
		public PageCollection Pages
		{
			set { _Pages = value; }
			get
			{
				if( _Pages == null)
					_Pages = new PageCollection();
				
				return _Pages;
			}
		}
	}
}
