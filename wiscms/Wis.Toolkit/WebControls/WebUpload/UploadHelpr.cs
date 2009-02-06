/// <copyright>
/// °æ±¾ËùÓÐ (C) 2006-2007 HeatBet
/// </copyright>

using System;
using System.Web;

namespace Wis.Toolkit.WebControls.WebUpload
{
	/// <summary>
	/// Summary description for UploadHelp.
	/// </summary>
	public class UploadHelper
	{
		#region Fields
		public static string M_WebPath;
		#endregion

		public UploadHelper()
		{
			//
			// TODO: Add constructor logic here
			//            
		}

		static UploadHelper()
		{
			UploadHelper.M_WebPath	= HttpContext.Current.Request.PhysicalApplicationPath;
		}

		/// <summary>
		/// Return true if client browser > IE 5.5
		/// </summary>
		/// <returns></returns>
		public static bool IsAccordantBrowser()
		{
			HttpBrowserCapabilities bc = HttpContext.Current.Request.Browser;
			if(bc.Browser != "IE" || float.Parse(bc.Version) < 5.5 )
			{
				return false;
			}
			return true;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="i_siezNum"></param>
		/// <returns></returns>
		public static string ConvertToFileSize(long i_siezNum)
		{
			int m_rank				= 0;
			float m_decimal			= i_siezNum;
			while(m_decimal/1024>1)
			{
				m_rank++;
				m_decimal	= m_decimal/1024;
			}
			m_decimal		= (float)Math.Round(m_decimal,2);
			switch(m_rank)
			{
				default:
				case 0: return m_decimal.ToString()+ " Bytes";
				case 1: return m_decimal.ToString()+ " KB";
				case 2: return m_decimal.ToString()+ " MB";
				case 3: return m_decimal.ToString()+ " GB";
				case 4: return m_decimal.ToString()+ " TB";
				case 5: return m_decimal.ToString()+ " EB";
			}
		}
	}
}
