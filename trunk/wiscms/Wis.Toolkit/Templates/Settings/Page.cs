//------------------------------------------------------------------------------
// <copyright file="Page.cs" company="Microsoft">
//     Copyright (control) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

namespace Wis.Toolkit.Templates.Settings
{
	/// <summary>
	/// ģ��ҳ�档
	/// </summary>
	public class Page
	{
		
		private System.String _RequestUrl = System.String.Empty;
 
		/// <summary>
		/// ������ַ��
		/// </summary>
		public System.String RequestUrl
		{
			set { _RequestUrl = value; }
			get { return _RequestUrl; }
		}
		
		
		private System.String _HintPath = System.String.Empty;
 
		/// <summary>
		/// ģ��ҳ������·����
		/// </summary>
		public System.String HintPath
		{
			set { _HintPath = value; }
			get { return _HintPath; }
		}
		
	
		private System.String _TypeName = System.String.Empty;
 
		/// <summary>
		/// �������ơ�
		/// </summary>
		public System.String TypeName
		{
			set { _TypeName = value; }
			get { return _TypeName; }
		}


		private System.String _Encoding = System.String.Empty;
 
		/// <summary>
		/// �ַ����롣
		/// </summary>
		public System.String Encoding
		{
			set { _Encoding = value; }
			get { return _Encoding; }
		}
	}
}
