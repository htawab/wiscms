//------------------------------------------------------------------------------
// <copyright file="ExceptionAppender.cs" company="Oriental Everwisdom">
//     Copyright (C) Oriental Everwisdom Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

namespace Wis.Toolkit.Kernel
{

	/// <summary>
	/// ��¼�쳣��Ϣ�� XML �ļ���
	/// </summary>
	public class ExceptionAppender
	{
		
		/// <summary>
		/// �쳣�ļ��ĵ�ǰ�洢·����
		/// </summary>
		public static string Path
		{
			get
			{
				// Get the DLL file Path
				string path = System.Reflection.Assembly.GetExecutingAssembly().CodeBase.Substring(8).ToLower();
				int length = path.LastIndexOf("/bin/"); // ��ȡ��DLL���ļ������õ�DLL��ǰ��·��
				path = path.Substring(0, length).Replace("/", "\\") + "\\Logs\\Exception\\";
				//string path = System.Web.HttpContext.Current.Server.MapPath(string.Format("{0}/App_Data/Exception/", Context.ApplicationPath));
				if(System.IO.Directory.Exists(path) == false) System.IO.Directory.CreateDirectory(path);
				
				// �õ��쳣�ļ���·��
				string filename = string.Format("{0}-{1}.xml", System.DateTime.Now.Year.ToString(), System.DateTime.Now.Month.ToString());
				path = System.IO.Path.Combine(path, filename);
			
				return path;
			}
		}

		
		/// <summary>
		/// ��¼�쳣��Ϣ��XML�ļ��С�
		/// </summary>
		/// <param name="ex">�쳣��</param>
		/// <returns>��¼�ɹ�����True����¼ʧ�ܷ���False</returns>
		public static void Append(System.Exception ex) 
		{ 
			if (System.IO.File.Exists(Path))
			{ 
				System.Xml.XmlDocument xd; 
				System.Xml.XmlNode node; 
				System.Xml.XmlElement xe; 
				xd = new System.Xml.XmlDocument(); 
				xd.Load(Path); 
				node = xd.SelectSingleNode("ROOT"); 
				xe = xd.CreateElement("Exception"); 
				node = node.AppendChild(xe); 
				xe = xd.CreateElement("OccurDate"); 
				xe.InnerText = System.DateTime.Now.ToString(); 
				node.AppendChild(xe); 
				xe = xd.CreateElement("Source"); 
				xe.InnerText = ex.Source; 
				node.AppendChild(xe); 
				xe = xd.CreateElement("HelpLink"); 
				xe.InnerText = ex.HelpLink; 
				node.AppendChild(xe); 
				xe = xd.CreateElement("TargetSiteName"); 
				xe.InnerText = ex.TargetSite.Name; 
				node.AppendChild(xe); 
				xe = xd.CreateElement("StackTrace"); 
				xe.InnerText = ex.StackTrace; 
				node.AppendChild(xe); 
				xe = xd.CreateElement("Message"); 
				xe.InnerText = ex.Message; 
				node.AppendChild(xe); 
				xd.Save(Path); 
			} 
			else 
			{ 
				System.Xml.XmlTextWriter xtw;
                xtw = new System.Xml.XmlTextWriter(Path, System.Text.Encoding.UTF8); 
				xtw.Formatting = System.Xml.Formatting.Indented; 
				xtw.WriteStartDocument(); 
				xtw.WriteStartElement("ROOT"); 
				xtw.WriteStartElement("Exception"); 
				xtw.WriteStartElement("OccurDate"); 
				xtw.WriteString(System.DateTime.Now.ToString()); 
				xtw.WriteEndElement(); 
				xtw.WriteStartElement("Source"); 
				xtw.WriteString(ex.Source); 
				xtw.WriteEndElement(); 
				xtw.WriteStartElement("HelpLink"); 
				xtw.WriteString(ex.HelpLink); 
				xtw.WriteEndElement(); 
				xtw.WriteStartElement("TargetSiteName"); 
				xtw.WriteString(ex.TargetSite.Name); 
				xtw.WriteEndElement(); 
				xtw.WriteStartElement("StackTrace"); 
				xtw.WriteString(ex.StackTrace); 
				xtw.WriteEndElement(); 
				xtw.WriteStartElement("Message"); 
				xtw.WriteString(ex.Message); 
				xtw.WriteEndElement(); 
				xtw.WriteEndElement(); 
				xtw.WriteEndElement(); 
				xtw.Flush(); 
				xtw.Close(); 
			}
		} 
	}
}
