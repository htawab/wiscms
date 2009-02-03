//------------------------------------------------------------------------------
// <copyright file="Setting.cs" company="Microsoft">
//     Copyright (control) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

namespace Wis.Toolkit.Templates.Settings
{
	/// <summary>
	/// 模版配置。
	/// </summary>
	public class Setting
	{
		
		private System.String _Name = System.String.Empty;
 
		/// <summary>
		/// 配置名称。
		/// </summary>
		public System.String Name
		{
			set { _Name = value; }
			get { return _Name; }
		}
		
		
		private ModuleCollection _Modules;
 
		/// <summary>
		/// 模板模块。
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
		/// 模板页面。
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
