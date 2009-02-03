//------------------------------------------------------------------------------
// <copyright file="Permission.cs" company="Oriental Everwisdom">
//     Copyright (C) Oriental Everwisdom Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

namespace Wis.Toolkit.Security
{
	/// <summary>
	/// Ȩ�ޡ�
	/// </summary>
	public class Permission
	{

		private System.Guid _ID = System.Guid.Empty;
		/// <summary>
		/// Ȩ�ޱ�š�
		/// </summary>
		public System.Guid ID 
		{ 
			get 
			{ 
				return _ID; 
			} 
			set 
			{ 
				_ID = value; 
			} 
		} 

		
		private System.String _Name = System.String.Empty;
		/// <summary>
		/// Ȩ�����ơ�
		/// </summary>
		public System.String Name 
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


		private System.String _Description = System.String.Empty; 
		/// <summary>
		/// Ȩ��������
		/// </summary>
		public System.String Description 
		{ 
			get 
			{ 
				return _Description; 
			} 
			set 
			{ 
				_Description = value; 
			} 
		} 

	}
}
