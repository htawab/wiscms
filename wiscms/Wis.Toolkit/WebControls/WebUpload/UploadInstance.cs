/// <copyright>
/// °æ±¾ËùÓÐ (C) 2006-2007 HeatBet
/// </copyright>

using System;
using System.Web;

namespace Wis.Toolkit.WebControls.WebUpload
{
	/// <summary>
	/// Summary description for UploadProccess.
	/// </summary>
	public class UploadInstance:IDisposable
	{
		public enum UploadStatus {Initialization,Uploading,Finished}

		#region fields
		public DateTime m_startTime;
		public string m_UploadGuid;
		private HttpContext m_httpContext;
		public long m_totalSize;
		public long m_currentSize;
		public string m_currentFileName;
		public bool m_isActive;
		public string m_userName;
		public string m_tempPath;
		public long _MaxRequestSize;
		public UploadInstance.UploadStatus m_status;
		#endregion

		#region Properties
		public long AvgSpeed
		{
			get
			{
				int m_totlaSecond = Convert.ToInt32(this.UploadTimeSpan.TotalSeconds);
				if(m_totlaSecond>0)
				{
					return this.m_currentSize/m_totlaSecond;
				}
				else
				{
					return 0;
				}
			}
		}

		/// <summary>
		/// Return the time spand from the upload start.
		/// </summary>
		public TimeSpan UploadTimeSpan
		{
			get{return DateTime.Now.Subtract(this.m_startTime);}
		}
		public int UploadTimeSpanSeconds
		{
			get{return this.UploadTimeSpan.Seconds;}
		}
		#endregion

		public UploadInstance()
		{
			this.m_UploadGuid = Guid.NewGuid().ToString();
			this.m_startTime = DateTime.Now;
			this.m_httpContext = HttpContext.Current;
			this.m_isActive = true;
			this.m_status = UploadInstance.UploadStatus.Initialization;
			this.m_totalSize = 100;
			this.m_currentSize = 0;
			this.m_currentFileName	= string.Empty;
			this.m_userName = string.Empty;
			this._MaxRequestSize = UploadModule._MaxRequestSize*1024;
			this.m_tempPath = UploadHelper.M_WebPath;
		}

		/// <summary>
		/// 
		/// </summary>
		public void UpdateApplication()
		{
        //if(!this.m_isActive) return;
			if(this.m_httpContext.Application[this.m_UploadGuid]==null)
			{
				this.m_httpContext.Application.Add(this.m_UploadGuid,this);
			}
//			else
//			{
//                this.m_httpContext.Application.Set(this.m_UploadGuid,this);
//			}
		}

		public void ReloadFromApplication()
		{
			if(this.m_httpContext.Application[this.m_UploadGuid]==null)
			{
				return;
			}
			else
			{
				UploadInstance m_temp	= this.m_httpContext.Application[this.m_UploadGuid] as UploadInstance;
				this.m_currentFileName	= m_temp.m_currentFileName;
				this.m_currentSize		= m_temp.m_currentSize;
				this.m_startTime		= m_temp.m_startTime;
				this.m_totalSize		= m_temp.m_totalSize;
			}
		}

		#region IDisposable Members
		/// <summary>
		/// 
		/// </summary>
		public void Dispose()
		{
			// TODO:  Add UploadStatus.Dispose implementation
			if(this.m_httpContext.Application[this.m_UploadGuid]!=null)
			{
                this.m_httpContext.Application.Remove(this.m_UploadGuid);
			}
		}
		#endregion
	}
}
