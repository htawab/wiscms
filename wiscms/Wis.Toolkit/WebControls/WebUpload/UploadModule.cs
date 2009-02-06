/// <copyright>
/// °æ±¾ËùÓÐ (C) 2006-2007 HeatBet
/// </copyright>

using System;
using System.IO;
using System.Reflection;
using System.Web;
using System.Threading;
using System.Configuration;

namespace Wis.Toolkit.WebControls.WebUpload
{
	/// <summary>
	/// Summary description for UploadModule.
	/// </summary>
	public class UploadModule : IHttpModule
	{

		private HttpApplication	m_application;
		private string m_UploadGuid;
		private UploadProcess m_uploadProcess;
		private UploadInstance m_uploadInstance;
		private UploadLoger m_loger;
		private static int _BufferSize	= 128; //KB
		public static int _MaxRequestSize	= 1024000; //KB,2MB default;

		#region IHttpModule Members

		public void Init(HttpApplication i_application)
		{
			// TODO:  Add UploadModule.Init implementation    
			this.m_application			= i_application;
			i_application.Error			+=new EventHandler(m_application_Error);
			i_application.BeginRequest	+=new EventHandler(m_application_BeginRequest);
			i_application.EndRequest	+= new EventHandler(m_application_EndRequest);
		}

		public void Dispose()
		{
			// TODO:  Add UploadModule.Dispose implementation
			this.ReleaseResource();
		}

		#endregion

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void m_application_Error(object sender, EventArgs e)
		{
			this.ReleaseResource();
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void m_application_BeginRequest(object sender, EventArgs e)
		{
			string m_contentType = this.m_application.Request.ContentType.ToLower();
			if(!m_contentType.StartsWith("multipart/form-data")) return;
			IServiceProvider m_provider	= HttpContext.Current;
			HttpWorkerRequest m_workRequest = ((HttpWorkerRequest) m_provider.GetService(typeof (HttpWorkerRequest)));
			if(!m_workRequest.HasEntityBody()) return;
			//Set request time out to 3 hours.
			this.m_application.Context.Server.ScriptTimeout = 10800; //3 hours
			#region //Get Upload GUID
			try
			{
				this.m_UploadGuid = HttpContext.Current.Request.Cookies["Upload_GUID"].Value;
				this.m_uploadInstance = this.m_application.Application[this.m_UploadGuid] as UploadInstance;
				if(this.m_uploadInstance==null)
				{
					this.m_uploadInstance= new UploadInstance();
				}
			}
			catch{
				this.m_uploadInstance = new UploadInstance();
				this.m_uploadInstance.m_isActive = false;
			}
			this.m_uploadInstance.m_startTime = DateTime.Now;
			//this.m_uploadInstance.UpdateApplication();
			#endregion
			this.m_loger = new UploadLoger();
			this.m_loger.LogGUID = this.m_uploadInstance.m_UploadGuid;
			//
			try
			{
				this.m_uploadProcess = new UploadProcess();
				this.m_uploadProcess.TempPath = this.m_uploadInstance.m_tempPath;
				this.m_uploadProcess.m_loger = this.m_loger;
				//this.m_loger.OpenLogFile();
				long m_totalRequsetSize = Convert.ToInt64(m_workRequest.GetKnownRequestHeader(11));
				if(m_totalRequsetSize>this.m_uploadInstance._MaxRequestSize)
				{
					throw new Exception("Out of upload file size.");
				}
				string m_boundaryStr = "--"+m_contentType.Substring(m_contentType.IndexOf("boundary=")+9);
				//this.m_loger.Writer.WriteLine("{0}:\tStart upload. GUID:{1}.",DateTime.Now,this.m_loger.LogGUID);
				this.m_loger.WriteLine(this.m_uploadInstance.m_userName+" start upload files.");
				//
				long m_uploadedSize = 0;
				byte[] m_perLoadData = m_workRequest.GetPreloadedEntityBody();
				this.m_uploadProcess.BoundaryData = HttpContext.Current.Request.ContentEncoding.GetBytes(m_boundaryStr);
				this.m_uploadProcess.AnalyizeData(m_perLoadData);
				m_uploadedSize = m_perLoadData.Length;
				//Update upload instance
				m_uploadInstance.m_currentSize = m_uploadedSize;
				m_uploadInstance.m_currentFileName = m_uploadProcess.CurrentuploadFileName;
				m_uploadInstance.m_status = UploadInstance.UploadStatus.Uploading;
				m_uploadInstance.m_totalSize = m_totalRequsetSize;
//				m_uploadInstance.UpdateApplication();
				//
				if (!m_workRequest.IsEntireEntityBodyIsPreloaded())
				{
					long m_tempReadSize	= 0;
					int m_buffSize = UploadModule._BufferSize*1024; //KB
					byte[] m_buffer = new byte[m_buffSize];
					MemoryStream m_tempMemoryStream = new MemoryStream();
					while(m_totalRequsetSize-m_uploadedSize>m_buffSize)
					{
						if(!this.m_application.Context.Response.IsClientConnected)
						{
							//	this.m_loger.Writer.WriteLine("{0}:\tUser cancel the upload or connect error.",DateTime.Now);
							this.m_loger.WriteLine(this.m_uploadInstance.m_userName+" cancel the upload. Or net work error.");
							this.ReleaseResource();
							return;
							//	throw new Exception("User cancel the upload or connect error.");
						}
						m_tempReadSize	= m_workRequest.ReadEntityBody(m_buffer,m_buffSize);
						//this.m_uploadProcess.AnalyizeData(m_buffer);
						//if the buffer is too larger, and the buffer contains the old data.
						if(m_tempReadSize<m_buffer.Length)
						{
							//	byte[] m_temp = new byte[m_tempReadSize];
							m_tempMemoryStream.Write(m_buffer,0,(int)m_tempReadSize);
							byte[] m_temp = m_tempMemoryStream.ToArray();
							m_tempMemoryStream.SetLength(0);
							m_tempMemoryStream.Position = 0;
							this.m_uploadProcess.AnalyizeData(m_temp);
							m_uploadedSize	+= m_tempReadSize;
							this.m_uploadInstance.m_currentSize	= m_uploadedSize;
							this.m_uploadInstance.m_currentFileName	= m_uploadProcess.CurrentuploadFileName;
							continue;
							//							m_buffer = m_temp;
							//							m_buffSize = (int)m_tempReadSize;
						}
						this.m_uploadProcess.AnalyizeData(m_buffer);
						//Update status and log
						m_uploadedSize	+= m_tempReadSize;
						this.m_uploadInstance.m_currentSize	= m_uploadedSize;
						this.m_uploadInstance.m_currentFileName	= m_uploadProcess.CurrentuploadFileName;
						//						this.m_uploadInstance.UpdateApplication();
						//						if((m_uploadedSize-m_tempReadSize)*100/m_totalRequsetSize!=m_uploadedSize*100/m_totalRequsetSize)
						//						{
						//							this.m_loger.Writer.WriteLine("{0}:\tRead data. At {1}/{2}. \t{3}%.",DateTime.Now,m_uploadedSize,m_totalRequsetSize,m_uploadedSize*100/m_totalRequsetSize);
						//						}
					}
					if (!m_application.Context.Response.IsClientConnected)
					{
						//this.m_loger.Writer.WriteLine("{0}:\tUser cancel the upload or connect error.",DateTime.Now);
						this.m_loger.WriteLine(this.m_uploadInstance.m_userName+" cancel the upload. Or net work error.");
						this.ReleaseResource();
						return;
						//	throw new Exception("User cancel the upload or connect error.");
					}
					int m_leftData	= (int)(m_totalRequsetSize-m_uploadedSize);
					m_buffer = new byte[m_leftData];
					m_tempReadSize = m_workRequest.ReadEntityBody(m_buffer,m_leftData);
					this.m_uploadProcess.AnalyizeData(m_buffer);
					this.m_uploadProcess.FlushTempData();
					m_uploadedSize += m_tempReadSize;
					//
					this.m_uploadInstance.m_currentSize	= m_uploadedSize;
					this.m_uploadInstance.m_status = UploadInstance.UploadStatus.Finished;
					//this.m_uploadInstance.UpdateApplication();
					//this.m_loger.Writer.WriteLine("{0}:\tRead data. At {1}/{2}. \t{3}%.",DateTime.Now,m_uploadedSize,m_totalRequsetSize,m_uploadedSize*100/m_totalRequsetSize);					
				}
				else
				{
					this.m_uploadProcess.FlushTempData();
				}
				this.AddTextPartToRequest(m_workRequest,m_uploadProcess.ContentData);
			//	this.m_loger.Writer.Write(Encoding.Default.GetChars(m_uploadProcess.ContentData));
			}
			catch(Exception ex)
			{
			//	this.m_loger.Writer.WriteLine("{0}:\tUploading error. Msg:{1}",DateTime.Now,ex.Message);				
				this.m_loger.WriteLine("Uploading error. Msg:"+ex.Message+"|"+ex.TargetSite+"|"+ex.Source);
				this.ReleaseResource();
			//	return;
				throw ex;
			}
			this.m_uploadProcess.Dispose();
			this.m_uploadProcess = null;
			//this.m_loger.Writer.WriteLine("{0}:\tUpload module finished. Upload success.",DateTime.Now);
			this.m_loger.WriteLine(this.m_uploadInstance.m_userName+" finished the upload.");
			this.m_loger.Dispose();
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void m_application_EndRequest(object sender, EventArgs e)
		{
			this.ReleaseResource();
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="m_request"></param>
		/// <param name="m_textData"></param>
		/// <returns></returns>
		private byte[] AddTextPartToRequest(HttpWorkerRequest m_request, byte[] m_textData)
		{
			Type m_type;
			BindingFlags m_flags =(BindingFlags.NonPublic | BindingFlags.Instance);
			//Is there application host IIS6.0?
			if (HttpContext.Current.Request.ServerVariables["SERVER_SOFTWARE"].Equals("Microsoft-IIS/6.0"))
			{
				m_type = m_request.GetType().BaseType.BaseType;
			}
			else
			{
				m_type = m_request.GetType().BaseType;
			}
			//Set values of working request
			//m_type.GetField("_contentLengthSent", m_flags).SetValue(m_request, false);
			try
			{
				m_type.GetField("_contentAvailLength", m_flags).SetValue(m_request, m_textData.Length);
				m_type.GetField("_contentTotalLength", m_flags).SetValue(m_request, m_textData.Length);
				m_type.GetField("_preloadedContent", m_flags).SetValue(m_request, m_textData);	
				m_type.GetField("_preloadedContentRead", m_flags).SetValue(m_request, true);
			}
			catch(Exception ex)
			{
				this.m_loger.Writer.WriteLine("{0}:\tSet context error. Msg:{1}",DateTime.Now,ex.Message);
			}
			return m_textData;
		}

		/// <summary>
		/// Get value from preloaded entity body. Identified by name. You can get any value in the form. 
		/// But you can not get the file data by this function.
		/// But this function too waste time.
		/// </summary>
		/// <param name="preloadedEntityBody"></param>
		/// <param name="name"></param>
		/// <returns></returns>
		private string AnalysePreLoadedContent(string i_perLoadedData, string m_fiedName)
		{
		//	string preloadedContent = HttpContext.Current.Request.ContentEncoding.GetString(m_preLoadedData);
			if (i_perLoadedData.Length > 0)
			{
				string	m_temp	= "name=\""+m_fiedName+"\"\r\n\r\n";
				if(i_perLoadedData.IndexOf(m_temp)<=0) return string.Empty;
				int startIndex	= i_perLoadedData.IndexOf(m_temp)+m_temp.Length;
				int endIndex	= i_perLoadedData.IndexOf("\r\n--",startIndex);
				return i_perLoadedData.Substring(startIndex,endIndex-startIndex);
			}
			else
			{
				return string.Empty;
			}
		}

		#region assitance functions
		/// <summary>
		/// 
		/// </summary>
		/// <param name="i_process"></param>
		private void ReleaseResource()
		{
			if(this.m_uploadProcess!=null)
			{
				if(this.m_uploadProcess.CurrentUploadingFile!=null)
				{
					this.m_uploadProcess.CurrentUploadingFile.CloseFile();
				}				
				if(this.m_uploadProcess.FileCollection!=null)
				{
//					foreach(UploadFile m_file in this.m_uploadProcess.FileCollection)
//					{
//						try
//						{
//							m_file.DeleteOnServer();
//						}
//						catch(Exception)
//						{
//						//	this.m_loger.Writer.WriteLine();
//						}
//					}
				}
				this.m_uploadProcess.Dispose();
			}
			if(this.m_uploadInstance!=null)
			{
				this.m_uploadInstance.Dispose();
			}
			if(this.m_loger!=null)
			{
				this.m_loger.Dispose();
			}
		}

		/// <summary>
		/// 
		/// </summary>
		static public void CollecteGC()
		{
			while(true)
			{
				System.GC.Collect();
				try
				{
					Thread.Sleep(3600*1000);
				}
				catch(ThreadAbortException)
				{
					return;
				}
			}
		}
		#endregion
	}
}
