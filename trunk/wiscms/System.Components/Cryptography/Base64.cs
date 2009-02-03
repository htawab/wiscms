//------------------------------------------------------------------------------
// <copyright file="Base64.cs" company="WisBet">
//     Copyright (C) WisBet Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using System;
using System.Text;

namespace Wis.Toolkit.Cryptography
{
	/// <summary>
	/// Base64 ����롣
	/// </summary>
	public class Base64
	{		
		/// <summary>
		/// Base64 ���롣
		/// </summary>
		/// <param name="text">�ı���</param>
		/// <returns>����ָ���ı���Base64 ����ֵ��</returns>
		public static string Encode(string text) 
		{ 
			try 
			{ 
				byte[] bytes = Encoding.Default.GetBytes(text); 
				return Convert.ToBase64String(bytes); 
			} 
			catch 
			{ 
				return text; 
			} 
		} 

		 
		/// <summary>
		/// Base64 ���롣
		/// </summary>
		/// <param name="text">�ı���</param>
		/// <returns>����ָ���ı���Base64 ����ֵ��</returns>
		public static string Decode(string text) 
		{ 
			try 
			{ 
				byte[] bytes = Convert.FromBase64String(text);
                return Encoding.UTF8.GetString(bytes); 
			} 
			catch 
			{ 
				return text; 
			} 
		}
		
		
		/// <summary>
		/// �ж��ı��Ƿ�Ϊ Base64 ���롣
		/// </summary>
		/// <param name="text">�ı���</param>
		/// <returns>�ı�Ϊ Base64 ���뷵��True�����򷵻�False��</returns>
		public static bool IsBase64(string text) 
		{ 
			try 
			{ 
				byte[] bytes = Convert.FromBase64String(text);
                Encoding.UTF8.GetString(bytes); 
				
				return true; 
			} 
			catch 
			{ 
				return false; 
			} 
		}
	}
}