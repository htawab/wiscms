using System;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Wis.Toolkit.WebControls.WebUpload
{
	/// <summary>
	/// Summary description for Class1.
	/// </summary>
	public class UploadView
	{
		private string ProgressBarScript
		{
			get
			{
				string script	= String.Empty;
				if(UploadHelper.IsAccordantBrowser())
				{
					script = @"
					<script language=javascript>
					<!--
					url='${url}$';
					var submited = false;
					var m_progressWin;
					function openProgress()
					{
						if(!submited)
						{
							var ary = document.getElementsByTagName('INPUT');
							var openBar = false;
							for(var i=0;i<ary.length;i++)
							{
								var obj = ary[i];
								if(obj.type  == 'file')
								{
									if(obj.value != '')
									{
										openBar = true;
										break;
									}
								}
							}
							if(openBar)
							{
							m_progressWin =	window.showModelessDialog(url, window, 'status:no;help:no;resizable:no;scroll:no;dialogWidth:398px;dialogHeight:200px');
							//	window.showModalDialog(url, window, 'status:no;help:no;resizable:no;scroll:no;dialogWidth:398px;dialogHeight:200px');
							//	event.srcElement.disabled = true;
								submited = true;
							}
							return true;
						}
						else
						{
							event.srcElement.disabled = true;
							return false;
						}
					}
					//-->
					</script>";
				}
				else
				{
					script = @"
					<script language=javascript>
					<!--
					url='${url}$';
					var submited = false;
					function openProgress()
					{
						if(!submited)
						{
							var ary = document.getElementsByTagName('INPUT');
							var openBar = false;
							for(var i=0;i<ary.length;i++)
							{
								var obj = ary[i];
								if(obj.type  == 'file')
								{
									if(obj.value != '')
									{
										openBar = true;
										break;
									}
								}
							}
							if(openBar)
							{
								var swd = window.screen.availWidth;
								var sht = window.screen.availHeight;
								var wd = 398;
								var ht =170;
								var left = (swd-wd)/2;
								var top = (sht-ht)/2;
								m_progressWin =	window.open(url,'_blank','status=no,toolbar=no,menubar=no,location=no,height='+ht+',width='+wd+',left='+left+',top='+top, true);
							//	event.srcElement.disabled = true;
								submited = true;
							}
							return true;
						}
						else
						{
							event.srcElement.disabled = true;
							return false;
						}
					}
					//-->
					</script>";
				}

				return script;
			}
		}
		
		private UploadInstance _UploadInstance	= new UploadInstance();

		#region Properties
		public int M_MaxUploadSize
		{
			set{this._UploadInstance._MaxRequestSize=value*1024;}
			get{return (int)this._UploadInstance._MaxRequestSize/1024;}
		}
		public string _TempPath
		{
			set{this._UploadInstance.m_tempPath=value;}
		}
		public string M_UserName
		{
			set{this._UploadInstance.m_userName=value;}
		}
		public string UploadGUID
		{
			get{return this._UploadInstance.m_UploadGuid;}
		}
		#endregion


		/// <summary>
		/// 注册进度条脚本。
		/// </summary>
		public void RegisterProgressBarScript()
		{
			string m_processURL	= "UploadStatusBar.ashx?UploadGUID="+this._UploadInstance.m_UploadGuid;
			string script	= ProgressBarScript.Replace("${url}$", m_processURL);
			
			HttpContext.Current.Response.Write(script);
			this._UploadInstance.UpdateApplication();
			if(HttpContext.Current.Response.Cookies["Upload_GUID"]!=null)
			{
				HttpContext.Current.Response.Cookies.Set(new HttpCookie("Upload_GUID",_UploadInstance.m_UploadGuid));
			}
			else
			{
				HttpContext.Current.Response.Cookies.Add(new HttpCookie("Upload_GUID",_UploadInstance.m_UploadGuid));
			}
		}
		
		
		/// <summary>
		/// 获得进度条脚本，并在页面上输出。
		/// </summary>
		public string GetProgressBarScript()
		{
			string processUrl	= "UploadStatusBar.ashx?UploadGUID=" + this._UploadInstance.m_UploadGuid;
			string script	= ProgressBarScript.Replace("${url}$", processUrl);
			
			_UploadInstance.UpdateApplication();
			if(HttpContext.Current.Response.Cookies["Upload_GUID"]!=null)
			{
				HttpContext.Current.Response.Cookies.Set(new HttpCookie("Upload_GUID",_UploadInstance.m_UploadGuid));
			}
			else
			{
				HttpContext.Current.Response.Cookies.Add(new HttpCookie("Upload_GUID",_UploadInstance.m_UploadGuid));
			}
			
			return script;
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="i_fieldName"></param>
		/// <returns></returns>
		public UploadFileCollection GetUploadedFileCollection(string i_fieldName)
		{
			if(HttpContext.Current.Request[i_fieldName]==null)return null;
			UploadFileCollection m_uploadFiles	= new UploadFileCollection();
            string[] m_uploadFilesContent	= HttpContext.Current.Request[i_fieldName].ToString().Split(',');
			foreach(string m_fileContent in m_uploadFilesContent)
			{
				//Content-Type: application/octet-stream; filename=""; filename_server=""
				if(m_fileContent==string.Empty)continue;
				string[] m_fileData		= m_fileContent.Split(';');
				if(m_fileData[1].IndexOf("\"\"")>0||m_fileData[2].IndexOf("\"\"")>0) continue;
				UploadFile	m_file		= new UploadFile();
				m_file.FullPathOnClient	= m_fileData[1].Substring(11,m_fileData[1].Length-12);
				string m_tempPath		= m_fileData[2].Substring(18,m_fileData[2].Length-19);
				m_file.FullPathOnServer	= m_tempPath;
				m_uploadFiles.Add(m_file);
			}
			return m_uploadFiles;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="i_fieldName"></param>
		/// <returns></returns>
		public UploadFile GetUploadFile(string i_fieldName)
		{
			if(HttpContext.Current.Request[i_fieldName]==null)return null;
			UploadFileCollection m_uploadFiles	= new UploadFileCollection();
			string[] m_uploadFilesContent	= HttpContext.Current.Request[i_fieldName].ToString().Split(',');
			if(m_uploadFilesContent.Length!=1)return null;
			string[] m_fileData		= m_uploadFilesContent[0].Split(';');
			if(m_fileData[1].IndexOf("\"\"")>0||m_fileData[2].IndexOf("\"\"")>0) return null;
			UploadFile	m_file		= new UploadFile();
			m_file.FullPathOnClient	= m_fileData[1].Substring(11,m_fileData[1].Length-12);
			m_file.FullPathOnServer	= m_fileData[2].Substring(18,m_fileData[2].Length-19);
			return  m_file;
		}
	
		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public DateTime GetUploadStartTime()
		{
            if(HttpContext.Current.Request["WebbUploadStartTime"]==null)return DateTime.MinValue;
			DateTime m_startTime;
			try
			{
				m_startTime	= Convert.ToDateTime(HttpContext.Current.Request["WebbUploadStartTime"]);
				return m_startTime;
			}
			catch
			{
				return DateTime.MinValue;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="i_Path"></param>
		public void SetUploadPath(string i_Path)
		{
            HttpContext.Current.Response.Cookies.Add(new HttpCookie("Upload_Path",i_Path));
		}
	}
}