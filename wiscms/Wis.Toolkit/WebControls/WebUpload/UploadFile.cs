/// <copyright>
/// °æ±¾ËùÓÐ (C) 2006-2007 HeatBet
/// </copyright>

using System;
using System.IO;
using System.Web;

namespace Wis.Toolkit.WebControls.WebUpload
{
	/// <summary>
	/// Summary description for UploadFile.
	/// </summary>
	public class UploadFile
	{
		#region fields
		private string m_nameOnClient;
		private string m_pathOnServer;
		private string m_GUID;
		private long m_size;
		private string m_expand;
		private FileStream m_fileStream;
		#endregion

		#region Property

		public string NameOnServer
		{
			get{return this.m_GUID+this.m_expand;}
		}
		/// <summary>
		/// Get or set the path and file name on the server.
		/// If set the path, the file must exists, and other info will be set too.
		/// </summary>
		public string FullPathOnServer
		{
			get{return Path.Combine(this.m_pathOnServer,this.m_GUID+this.m_expand);}
			set
			{
				if(File.Exists(value))
				{
					FileInfo m_fileInfo	= new FileInfo(value);
					this.m_size			= m_fileInfo.Length;
					this.m_expand		= m_fileInfo.Extension;
					this.m_GUID			= Path.GetFileNameWithoutExtension(value);
					this.m_pathOnServer	= m_fileInfo.DirectoryName;					
				}
			}
		}

		/// <summary>
		/// Get or set the file path for uploading.
		/// </summary>
		public string FilePath
		{
			get{return this.m_pathOnServer;}
			set{this.m_pathOnServer=value;}
		}

		/// <summary>
		/// Get or set file expand name.
		/// If set full path on the server and the file exist, this will be set.
		/// </summary>
		public string FileExpand
		{
			get{return this.m_expand;}
			set{this.m_expand=value;}
		}

		/// <summary>
		/// Get or set file size.
		/// If set full path on the server and the file exist, this will be set.
		/// </summary>
		public long FileSize
		{
			get{return this.m_size;}
			set{this.m_size=value;}
		}

		/// <summary>
		/// Get or set file's client info.
		/// </summary>
		public string FullPathOnClient
		{
			get{return this.m_nameOnClient;}
			set{this.m_nameOnClient=value;}
		}
		/// <summary>
		/// Get the FileStream point to the existed file on the server.
		/// </summary>
		public FileStream WriteFileStream
		{
			get{return this.m_fileStream;}
		}
		#endregion

		public UploadFile()
		{
			this.m_expand		= ".rem";
			this.m_GUID			= Guid.NewGuid().ToString();
			this.m_fileStream	= null;
			this.m_pathOnServer	= HttpContext.Current.Request.MapPath(".");
			this.m_nameOnClient	= null;
			this.m_size			= 0;
		}

		public FileStream OpenFile(FileMode i_mode,FileAccess i_access)
		{
			this.CloseFile();
			if(File.Exists(this.FullPathOnServer))
			{
				this.m_fileStream	= new FileStream(this.FullPathOnServer,i_mode,i_access);
			}
			else
			{
				this.m_fileStream	= File.Create(this.FullPathOnServer);
			}
			return this.m_fileStream;
		}

		public void CloseFile()
		{
			if(this.m_fileStream!=null)
			{
				this.m_fileStream.Flush();
				this.m_size	= this.m_fileStream.Length;
				this.m_fileStream.Close();
				this.m_fileStream	= null;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		public void MoveTo(string i_fullFileName)
		{
			File.Move(this.FullPathOnServer,i_fullFileName);
		}
		/// <summary>
		/// 
		/// </summary>
		public void CopyTo(string i_fullFileName)
		{
			File.Copy(this.FullPathOnServer,i_fullFileName);
		}
		/// <summary>
		/// 
		/// </summary>
		public void DeleteOnServer()
		{
			File.Delete(this.FullPathOnServer);
		}

		/// <summary>
		/// 
		/// </summary>
		public void SaveAs(string i_fullFileName, bool i_overWrite)
		{
//			File.Copy(this.FullPathOnServer,i_fullFileName,i_overWrite);
//			File.Delete(this.FullPathOnServer);
			File.Move(this.FullPathOnServer,i_fullFileName);
		}
	}
}
