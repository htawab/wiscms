/// <copyright>
/// °æ±¾ËùÓÐ (C) 2006-2007 HeatBet
/// </copyright>

using System;
using System.IO;
using System.Text;
using System.Web;
using System.Collections;

namespace Wis.Toolkit.WebControls.WebUpload
{
	/// <summary>
	/// Summary description for UploadProcess.
	/// </summary>
	public class UploadProcess:IDisposable
	{
		enum WriteMode{Content=0,File=1}

		#region Field
		static private readonly byte[] m_flag	= new byte[]{13,10,13,10};
		private DateTime m_startTime;
		private UploadFile m_currentFile;
		private WriteMode m_writeMode;
		private bool m_isPerloadData;
		private byte[] m_boundary;
		private MemoryStream m_tempData;
		private MemoryStream m_content;	
		private string m_tempFilePath;
		private UploadFileCollection m_fileCollection;
		private string m_currentFileName;
		
		public UploadLoger m_loger;

		#endregion

		#region Properties
		public string TempPath
		{
			get{return this.m_tempFilePath;}
			set{this.m_tempFilePath=value;}
		}
		public UploadFileCollection FileCollection
		{
			get{return this.m_fileCollection;}
		}
		public byte[] ContentData
		{
			get{return this.m_content.ToArray();}
		}
		public DateTime UploadTime
		{
			get{return this.m_startTime;}
			//set{this.m_startTime = value;}
		}
		public UploadFile CurrentUploadingFile
		{
			get{return this.m_currentFile;}
		}
		public string CurrentuploadFileName
		{
			get{return this.m_currentFileName;}
		}
		public byte[] BoundaryData
		{
			set{this.m_boundary=value;}
			get{return this.m_boundary;}
		}

		public int TempStreamLength
		{
			get
			{
				if(this.m_tempData!=null)
				{
					return (int)(this.m_tempData.Length);
				}
				else
				{
					return 0;
				}
			}
		}


		#endregion

		public UploadProcess()
		{
			//
			// TODO: Add constructor logic here
			//
			this.m_startTime		= DateTime.Now;
			this.m_content			= new MemoryStream();
			this.m_writeMode		= WriteMode.Content;
			this.m_currentFile		= null;
			this.m_tempData			= null;
			this.m_tempFilePath		= Path.Combine(HttpContext.Current.Request.PhysicalApplicationPath,"UploadFile");
			this.m_isPerloadData	= true;
			this.m_fileCollection	= new UploadFileCollection();
		}

		/// <summary>
		/// Main funcion to analyze the data.
		/// </summary>
		/// <param name="i_data"></param>
		public void AnalyizeData(byte[] i_data)
		{
			if(this.m_tempData!=null&&this.m_tempData.Length!=0){this.AnalyizeTempData(ref i_data);}
			int m_totalLength	= i_data.Length;
			int m_currentPoint	= 0;
			int m_tempPoint		= 0; 
			//
			#region Preprocess data
			if(this.m_isPerloadData)
			{
				m_currentPoint	= this.AnalyizePerloadData(i_data);
				this.m_isPerloadData= false;
			}
			#endregion

			while(m_currentPoint<m_totalLength)
			{
				if(i_data[m_currentPoint]==13)
				{
					#region if current point byte is 13
					int m_analyzeSize	= this.AnalyzeContentHeader(i_data,m_currentPoint);
					if(m_analyzeSize==0)
					{
						this.WriteData(i_data,m_currentPoint,1);
					}
					else if(m_analyzeSize==-1)
					{
						this.WriteToTemp(i_data,m_currentPoint);
						break;
					}
					else
					{
						m_currentPoint	+= m_analyzeSize; 
					}
					#endregion
				}
				else
				{				
					#region write data until find next 13, wirte to content or write to file
					while(m_currentPoint+m_tempPoint<m_totalLength)
					{
						if(i_data[m_currentPoint+m_tempPoint]==13/*&&this.CheckBoundary(i_data,m_currentPoint+m_tempPoint+2)==0*/)
						{
							break;
						}
						m_tempPoint++;
					}
					//
					this.WriteData(i_data,m_currentPoint,m_tempPoint);
					m_currentPoint	+= m_tempPoint;
					m_tempPoint	= 0;
					m_currentPoint--;
					#endregion
				}
                m_currentPoint++;
			}
		//	this.FlushData();
		}

		#region Assistant fucntions

		/// <summary>
		/// 
		/// </summary>
		/// <param name="i_data"></param>
		/// <param name="i_offset"></param>
		/// <returns></returns>
		private int CheckBoundary(byte[] i_data,int i_offset)
		{
			int m_boundaryLength	= this.m_boundary.Length;
			if(i_data.Length<=i_offset+m_boundaryLength+4)
			{
				//Break point in the content head.
				return -1;
			}
			for(int i=0;i<m_boundaryLength;i++)
			{
				if(i_data[i_offset+i]!=this.m_boundary[i]){return 1;/*Not a boundary*/}
			}
			//This is a boundary.
			return 0;
		}

		/// <summary>
		/// Filter the data, and write to file or content.
		/// </summary>
		/// <param name="i_data"></param>
		/// <param name="i_start"></param>
		/// <param name="i_length"></param>
		/// <returns></returns>
		private void WriteData(byte[] i_data,int i_offset,int i_count)
		{
			switch(this.m_writeMode)
			{
				default:
				case WriteMode.Content:
					this.m_content.Write(i_data,i_offset,i_count);
					break;
				case WriteMode.File:
					try{this.m_currentFile.WriteFileStream.Write(i_data,i_offset,i_count);}
					catch(Exception ex)
					{
					//	this.m_loger.Writer.WriteLine("{0}\tWrite file error. MSG:{1}",DateTime.Now,ex.Message);
						this.m_loger.WriteLine("Write file error:"+ex.Message);
						this.m_currentFile.CloseFile();
					}
					break;
			}
		}
		private void WriteToTemp(byte[] i_data,int i_offset)
		{
        //  this.m_tempData	= new MemoryStream();
			if(this.m_tempData==null)
			{
				this.m_tempData	= new MemoryStream();
			}
			this.m_tempData.Write(i_data,i_offset,i_data.Length-i_offset);
		}

		public void FlushTempData()
		{
			if(this.m_tempData!=null&&this.m_tempData.Length!=0)
			{
				byte[] m_temp	= this.m_tempData.ToArray();
				this.WriteData(m_temp,0,(int)this.m_tempData.Length);
			}
		}

		/// <summary>
		/// 
		/// </summary>
		private void FlushData()
		{
			if(this.m_currentFile!=null&&this.m_currentFile.WriteFileStream!=null){
				try
				{
					this.m_currentFile.WriteFileStream.Flush();
				}
				catch
				{
					this.m_currentFile.CloseFile();
				}
			}
//			if(this.m_tempData!=null){this.m_tempData.Flush();}
//			if(this.m_content!=null){this.m_content.Flush();}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="i_data"></param>
		private void AnalyizeTempData(ref byte[] i_data)
		{
			this.m_tempData.Write(i_data,0,i_data.Length);
			i_data = this.m_tempData.ToArray();
			this.m_tempData.SetLength(0);
			this.m_tempData.Position = 0;
//			ArrayList m_tempList	= new ArrayList();
//			i_data = null;			
//			this.m_tempData.Close();
//			this.m_tempData	= null;
//			this.m_tempData.Position = 0;			
//			for(int i=0;i<this.m_tempData.Length;i++)
//			{
//				this.m_tempData.WriteByte((byte)0);	
//			}
//			this.m_tempData.Position = 0;
//			UploadModule.CollecteGC();
		}

		private int AnalyizePerloadData(byte[] i_data)
		{
			string m_insertStartTime	= Encoding.Default.GetString(this.m_boundary);
			m_insertStartTime			+= "\r\nContent-Disposition: form-data; name=\"WebbUploadStartTime\"\r\n\r\n";
			m_insertStartTime			+= this.m_startTime.ToString()+"\r\n";
			byte[] m_insertData			= Encoding.Default.GetBytes(m_insertStartTime);
			this.WriteData(m_insertData,0,m_insertData.Length);
			int m_index	=0;
			while(m_index+3<i_data.Length)
			{				
				if(i_data[m_index]==13&&i_data[m_index+1]==10&&i_data[m_index+2]==13&&i_data[m_index+3]==10)
				{
					break;
				}
				m_index++;
			}
			this.WriteData(i_data,0,m_index+4);
			return m_index+4;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="i_data"></param>
		/// <param name="i_offSet"></param>
		/// <returns></returns>
		private int AnalyzeContentHeader(byte[] i_data,int i_offSet)
		{
			int m_breakType	= this.CheckBoundary(i_data,i_offSet+2);
			if(m_breakType==1)	return 0; //Break point in the context head.
			if(m_breakType==-1) return -1;
			//break point==0, this is a boundary
			int m_index	= i_offSet+this.m_boundary.Length+4;
			while(m_index+3<i_data.Length)
			{				
				if(i_data[m_index]==13&&i_data[m_index+1]==10&&i_data[m_index+2]==13&&i_data[m_index+3]==10)
				{
					break;
				}
				m_index++;
			}
			if(m_index+3>=i_data.Length)return -1;
			//Analyze contenheader data
			int m_boundarySize	= this.m_boundary.Length+4;
			//include \r\n befor and \r\n behind.
			if(this.m_currentFile!=null&&this.m_writeMode==WriteMode.File)
			{
				this.m_currentFile.CloseFile();
				this.m_currentFile	= null;
				this.m_writeMode	= WriteMode.Content;
			}
			this.WriteData(i_data,i_offSet,this.m_boundary.Length+4);
			//start at this.m_boundary.Length+4, length: m_index-this.m_boundary.Length+4;
			string m_contentHeader	= HttpContext.Current.Request.ContentEncoding.GetString(i_data,i_offSet+m_boundarySize,m_index-m_boundarySize-i_offSet);
			if(m_contentHeader.IndexOf("\"; filename=\"")<0)
			{
				//This is other data.
				this.WriteData(i_data,i_offSet+m_boundarySize,m_index-m_boundarySize-i_offSet+4);
				return m_index-i_offSet+3;
			}
			else
			{
				this.m_currentFile			= new UploadFile();
				this.m_currentFile.FilePath	= this.m_tempFilePath;
				//This is a upload file data.
				string[] m_fileContent	= this.GetFileContent(m_contentHeader);	
				string[] sbArray	= new string[11];
				sbArray[0]	= m_fileContent[0];
				sbArray[1]	= ";";
				sbArray[2]	= m_fileContent[1];
				sbArray[3]	= "\r\n\r\n";
				sbArray[4]	= m_fileContent[3];
				sbArray[5]	= ";";
				sbArray[6]	= m_fileContent[2];
				sbArray[7]	= "; ";
				sbArray[8]	= "filename_server=\"";
				if(m_fileContent[2].IndexOf("\"\"")>=0)
				{
					sbArray[9]	= string.Empty;
				}
				else
				{	
					sbArray[9]	= this.m_currentFile.FullPathOnServer;
					this.m_currentFileName	= m_fileContent[2];
					//this.m_loger.Writer.WriteLine("{0}:\tStart upload file:{1}. Server path:\"{2}\"",DateTime.Now,m_fileContent[2],this.m_currentFile.FullPathOnServer);					
					this.m_loger.WriteLine(string.Format("Upload file. ServerPath=\"{0}\",{1}",this.m_currentFile.FullPathOnServer,m_fileContent[2]));
				}
				sbArray[10]	= "\"";
				//StringBuilder m_sb	= new StringBuilder();
				//m_sb.Append(string.Concat(sbArray));
				//byte[] m_tempContent	= Encoding.UTF8.GetBytes(sb.ToString().ToCharArray());
				byte[] m_tempContent	= HttpContext.Current.Request.ContentEncoding.GetBytes(string.Concat(sbArray));
                this.WriteData(m_tempContent,0,m_tempContent.Length);
				if(m_fileContent[2].IndexOf("\"\"")<=0)
				{
					this.m_currentFile.OpenFile(FileMode.OpenOrCreate,FileAccess.Write);
					this.m_writeMode = WriteMode.File;
					this.m_fileCollection.Add(this.m_currentFile);
				}
			}
			return m_index-i_offSet+3;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="m_contentHeader"></param>
		/// <returns></returns>
		private string[] GetFileContent(string m_contentHeader)
		{
			m_contentHeader = m_contentHeader.Replace("\r\n",";");
			return m_contentHeader.Split(new char[1]{';'});
		}
		#endregion

		#region IDisposable Members

		public void Dispose()
		{
			// TODO:  Add UploadProcess.Dispose implementation
			if(this.m_currentFile!=null&&this.m_currentFile.WriteFileStream!=null)
			{
				this.m_currentFile.CloseFile();
				this.m_currentFile	= null;
			}
			if(this.m_tempData!=null)
			{
				this.m_tempData.Close();
				this.m_tempData	= null;
			}
			if(this.m_content!=null)
			{
				this.m_content.Close();
				this.m_content	= null;
			}
			if(this.m_fileCollection!=null)
			{
				this.m_fileCollection.Clear();
				this.m_fileCollection	= null;
			}
		}
		#endregion
	}
}
