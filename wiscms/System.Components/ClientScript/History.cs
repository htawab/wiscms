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
	/// History 的摘要说明。
	/// </summary>
	public class History
	{
		/// <summary>
		/// 加载历史列表中的上一个文档。
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
		/// 加载历史列表中的下一个文档。
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
		/// 加载历史列表中的一个指定文档。
		/// </summary>
		/// <param name="delta">文档在历史列表中的位置。</param>
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
		/// 加载指定URL的文档。
		/// </summary>
		/// <param name="location">文档的完整URL。</param>
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
