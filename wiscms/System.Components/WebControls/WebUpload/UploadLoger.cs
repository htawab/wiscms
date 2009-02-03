/// <copyright>
/// °æ±¾ËùÓÐ (C) 2006-2007 HeatBet
/// </copyright>

using System;
using System.IO;
using System.Diagnostics;

namespace Wis.Toolkit.WebControls.WebUpload
{
	/// <summary>
	/// Summary description for UploadLog.
	/// </summary>
	public class UploadLoger:IDisposable
	{
		#region Fields
		private static DateTime M_LogTime;
		private TextWriter m_fs;
		private string m_uploadGUID;
		//static string LogPath	= Path.Combine(UploadHelper.M_WebPath,"UploadLog");
		static string LogPath	 //= 
		{
			get
			{
				string path = Path.Combine(UploadHelper.M_WebPath,"UploadLog");
				if(!System.IO.Directory.Exists(path))
					System.IO.Directory.CreateDirectory(path);
				
				return path;
			}
		}
		#endregion

		#region properties
		public  TextWriter Writer
		{
			get
			{
				if(this.m_fs!=null)
				{
					return this.m_fs;
				}
				else
				{
					this.OpenLogFile();
					return this.m_fs;
				}
			}
		}
		public string LogGUID
		{
			get{return this.m_uploadGUID;}
			set{this.m_uploadGUID=value;}
		}
		#endregion

		public UploadLoger()
		{
			TimeSpan m_timespan = DateTime.Now.Subtract(UploadLoger.M_LogTime);
			if(m_timespan.Days>=1)
			{
				string m_logName = DateTime.Now.ToString("yyyyMMdd")+".log";
				m_logName = Path.Combine(UploadLoger.LogPath,m_logName);
                UploadLoger.M_LogTime = DateTime.Now;
				Trace.Listeners.Clear();
				Trace.Listeners.Add(new System.Diagnostics.TextWriterTraceListener(m_logName));
			}
		}

		static UploadLoger()
		{
			string m_logName = DateTime.Now.ToString("yyyyMMdd")+".log";
			m_logName = Path.Combine(UploadLoger.LogPath,m_logName);
			UploadLoger.M_LogTime	= DateTime.Now;
			Trace.Listeners.Clear();
			Trace.Listeners.Add(new System.Diagnostics.TextWriterTraceListener(m_logName));
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="i_log"></param>
		public void WriteLine(string i_log)
		{
			Trace.WriteLine(string.Format("{0}\t{1}\t{2}.",DateTime.Now,this.m_uploadGUID,i_log));
		}

		/// <summary>
		/// 
		/// </summary>
		public void OpenLogFile()
		{
			string m_fileName	= Path.Combine(UploadLoger.LogPath,this.m_uploadGUID+".log");
			if(File.Exists(m_fileName))
			{
				this.m_fs	= File.AppendText(m_fileName);
			}
			else
			{
				this.m_fs	= File.CreateText(m_fileName);
			}
		}

		/// <summary>
		/// 
		/// </summary>
		public void CloseLogFile()
		{
			if(this.m_fs!=null)
			{
				this.m_fs.Flush();
				this.m_fs.Close();
				this.m_fs	= null;
			}
		}

		#region IDisposable Members
		public void Dispose()
		{
			// TODO:  Add UploadLog.Dispose implementation
			if(this.m_fs!=null)
			{
				this.m_fs.Flush();
				this.m_fs.Close();
				this.m_fs	= null;
			}
			Trace.Flush();
		}
		#endregion
	}
}
