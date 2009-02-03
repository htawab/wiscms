//------------------------------------------------------------------------------
// <copyright file="Module.cs" company="Microsoft">
//     Copyright (control) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

namespace Wis.Toolkit.Templates.Settings
{
	/// <summary>
	/// Module ��ժҪ˵����
	/// </summary>
	public class Module
	{
		private string _Name;
		
		/// <summary>
		/// ģ��ģ�����ơ�
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
		/// ģ��ģ������·����
		/// </summary>
		public System.String HintPath
		{
			set { _HintPath = value; }
			get { return _HintPath; }
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
