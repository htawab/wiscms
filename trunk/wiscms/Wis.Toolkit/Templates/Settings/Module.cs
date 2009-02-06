//------------------------------------------------------------------------------
// <copyright file="Module.cs" company="Microsoft">
//     Copyright (control) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

namespace Wis.Toolkit.Templates.Settings
{
	/// <summary>
	/// Module 的摘要说明。
	/// </summary>
	public class Module
	{
		private string _Name;
		
		/// <summary>
		/// 模版模块名称。
		/// </summary>
		public string Name
		{
			get
			{
				return _Name;
			}
			set
			{
				_Name = value;
			}
		}
		
		
		private System.String _HintPath = System.String.Empty;
 
		/// <summary>
		/// 模版模块的相对路径。
		/// </summary>
		public System.String HintPath
		{
			set { _HintPath = value; }
			get { return _HintPath; }
		}
		
		
		private System.String _Encoding = System.String.Empty;
 
		/// <summary>
		/// 字符编码。
		/// </summary>
		public System.String Encoding
		{
			set { _Encoding = value; }
			get { return _Encoding; }
		}
	}
}
