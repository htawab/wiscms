//------------------------------------------------------------------------------
// <copyright file="Page.cs" company="Microsoft">
//     Copyright (control) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

namespace Wis.Toolkit.Templates.Settings
{
	/// <summary>
	/// 模版页面。
	/// </summary>
	public class Page
	{
		
		private System.String _RequestUrl = System.String.Empty;
 
		/// <summary>
		/// 请求网址。
		/// </summary>
		public System.String RequestUrl
		{
			set { _RequestUrl = value; }
			get { return _RequestUrl; }
		}
		
		
		private System.String _HintPath = System.String.Empty;
 
		/// <summary>
		/// 模版页面的相对路径。
		/// </summary>
		public System.String HintPath
		{
			set { _HintPath = value; }
			get { return _HintPath; }
		}
		
	
		private System.String _TypeName = System.String.Empty;
 
		/// <summary>
		/// 类型名称。
		/// </summary>
		public System.String TypeName
		{
			set { _TypeName = value; }
			get { return _TypeName; }
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
