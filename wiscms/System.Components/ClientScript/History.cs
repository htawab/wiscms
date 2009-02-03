//------------------------------------------------------------------------------
// <copyright file="History.cs" company="Oriental Everwisdom">
//     Copyright (C) Oriental Everwisdom Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using System.Text;
using System.Web;

namespace Wis.Toolkit.ClientScript
{
	/// <summary>
	/// History ��ժҪ˵����
	/// </summary>
	public class History
	{
		/// <summary>
		/// ������ʷ�б��е���һ���ĵ���
		/// </summary>
		public static void Back() 
		{
			StringBuilder sb = new StringBuilder(); 
			sb.Append("\n<script language=JavaScript>"); 
			sb.Append("\n<!--"); 
			sb.Append("\n	history.back();"); 
			sb.Append("\n//-->"); 
			sb.Append("\n</SCRIPT>"); 
			HttpContext.Current.Response.Write(sb.ToString()); 
		}
		
		
		/// <summary>
		/// ������ʷ�б��е���һ���ĵ���
		/// </summary>
		public static void Forward() 
		{
			StringBuilder sb = new StringBuilder(); 
			sb.Append("\n<script language=JavaScript>"); 
			sb.Append("\n<!--"); 
			sb.Append("\n	history.forward();"); 
			sb.Append("\n//-->"); 
			sb.Append("\n</SCRIPT>"); 
			HttpContext.Current.Response.Write(sb.ToString()); 
		}

		
		/// <summary>
		/// ������ʷ�б��е�һ��ָ���ĵ���
		/// </summary>
		/// <param name="delta">�ĵ�����ʷ�б��е�λ�á�</param>
		public static void Go(int delta) 
		{
			StringBuilder sb = new StringBuilder();
			sb.Append("\n<script language=JavaScript>");
			sb.Append("\n<!--");
			sb.Append("\n	history.go('" + delta.ToString() + "');");
			sb.Append("\n//-->");
			sb.Append("\n</SCRIPT>");
			HttpContext.Current.Response.Write(sb.ToString());
		}

		
		/// <summary>
		/// ����ָ��URL���ĵ���
		/// </summary>
		/// <param name="location">�ĵ�������URL��</param>
		public static void Go(string location) 
		{
			StringBuilder sb = new StringBuilder(); 
			sb.Append("\n<script language=JavaScript>"); 
			sb.Append("\n<!--"); 
			sb.Append("\n	history.go('" + location + "');"); 
			sb.Append("\n//-->");
			sb.Append("\n</SCRIPT>");
			HttpContext.Current.Response.Write(sb.ToString()); 
		}
	}
}
