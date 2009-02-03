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
	/// Base64 编解码。
	/// </summary>
	public class Base64
	{		
		/// <summary>
		/// Base64 编码。
		/// </summary>
		/// <param name="text">文本。</param>
		/// <returns>返回指定文本的Base64 编码值。</returns>
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
		/// Base64 解码。
		/// </summary>
		/// <param name="text">文本。</param>
		/// <returns>返回指定文本的Base64 解码值。</returns>
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
		/// 判断文本是否为 Base64 编码。
		/// </summary>
		/// <param name="text">文本。</param>
		/// <returns>文本为 Base64 编码返回True，否则返回False。</returns>
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