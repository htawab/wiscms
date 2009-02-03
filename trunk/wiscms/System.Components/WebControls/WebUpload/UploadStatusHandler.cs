/// <copyright>
/// 版本所有 (C) 2006-2007 HeatBet
/// </copyright>

using System.Web;

namespace Wis.Toolkit.WebControls.WebUpload
{
	/// <summary>
	/// Summary description for UploadProcessHandle.
	/// </summary>
	public class UploadStatusHandler:IHttpHandler
	{
		#region Script and static text.
		private static string  m_htmlHand	= @"
<!DOCTYPE HTML PUBLIC '-//W3C//DTD HTML 4.0 Transitional//EN'>
<html><head>
";
		private static string  m_htmlBody	= @"
<script language='JavaScript'>
<!--
function UpdateUploadStatus(){
try
{
	var m_percent = m_currentSize*100/m_totalSize;
	m_percent = Math.floor(m_percent);
	if(m_percent<=0)m_percent=1;
	document.all.o_currentFile.innerHTML= ""文件名: <font color=#CC3333>"" + GetFileName() + ""</font>"";		
	document.all.o_percent_1.width	= m_percent+""%"";
	document.all.o_percent_2.width	= (100-m_percent)+""%"";
	document.all.o_avgSpeed.innerHTML= ""速率: <font color=#CC3333>""+ ConvertToFileSize(m_avgSpeed) +""/秒</font>"";
	document.all.o_status.innerHTML	= ""状态: <font color=#CC3333>上传中……</font>"";
	document.all.o_timeLeft.innerHTML= ""剩余时间: <font color=#CC3333>""+GetTimeReamin()+""</font>"";		
}
catch(ex)
{
	document.all.o_currentFile.innerHTML= ""文件名:"";
	document.all.o_percent_1.width	= ""1"";
	document.all.o_percent_2.width	= ""100%"";
	document.all.o_avgSpeed.innerHTML= ""速率: <font color=#CC3333></font>"";
	document.all.o_status.innerHTML	= ""状态: <font color=#CC3333></font>"";
	document.all.o_timeLeft.innerHTML= ""剩余时间: <font color=#CC3333>未知</font>"";
}
if(m_uploadFinished)
{
	document.all.o_currentFile.innerHTML= ""文件名:<font color=#CC3333>复制中……</font>"";
	document.all.o_percent_1.width	= ""100%"";
	document.all.o_percent_2.width	= ""1"";
	document.all.o_avgSpeed.innerHTML	= ""速率:<font color=#CC3333></font>"";
	document.all.o_status.innerHTML	= ""状态: <font color=#CC3333>完成。<br>请继续等待，或者单击确认按钮关闭进度条。</font>"";
	document.all.o_timeLeft.innerHTML= ""剩余时间:<font color=#CC3333>少于一分钟。</font>"";
}
SetButtons();
}

function ConvertToFileSize(i_size)
{
	if(i_size<=0) return '0 Byte';
	var m_rank	= 0;
	var m_decimal = i_size;
	while(m_decimal/1024>1)
	{
		m_rank++;
		m_decimal= m_decimal/1024;
	}
	m_decimal= m_decimal.toString();
	m_decimal= m_decimal.substring(0,m_decimal.indexOf(""."")+3);
	var m_value;
	switch(m_rank)
	{
		default:
		case 0: m_value	= m_decimal+ "" Bytes"";break;
		case 1: m_value	= m_decimal+ "" KB"";break;
		case 2: m_value	= m_decimal+ "" MB"";break;
		case 3: m_value	= m_decimal+ "" GB"";break;
		case 4: m_value	= m_decimal+ "" TB"";break;
		case 5: m_value	= m_decimal+ "" EB"";break;
	}
	return m_value;
}

function GetFileName()
{
	if(m_currentFileName.length>0)
	{
		var i_start	= m_currentFileName.lastIndexOf(""\\"")+1;
		var i_end	= m_currentFileName.length-1;
		var i_fileName = m_currentFileName.substring(i_start,i_end);
		if(i_fileName.length>30){
			return i_fileName.substring(0,30)+'...';
		}else{
			return 	i_fileName;
		}
	}else{
		return '';
	}
}

function GetTimeReamin()
{
	if(m_currentSize==0) return '';
	if(m_avgSpeed<=0) return '';
	if(m_totalSize-m_currentSize<=0) return '少于一分钟。';
	var m_reaminSecond	= (m_totalSize-m_currentSize)/m_avgSpeed;
	var m_hour	= Math.floor(m_reaminSecond/3600);
	var m_minute= Math.floor((m_reaminSecond%3600)/60);
	var m_second= Math.floor(m_reaminSecond%60);
	if(m_hour<10) m_hour = ""0""+m_hour.toString();
	if(m_minute<10) m_minute = ""0""+m_minute.toString();
	if(m_second<10) m_second = ""0""+m_second.toString();
	return m_hour+"":""+m_minute+"":""+m_second;
}

function SetButtons()
{
	if(m_currentSize<m_totalSize)
	{
		document.all.o_but_ok.disabled	= true;
		document.all.o_but_cancel.disabled	= false;
	}
	else
	{
		document.all.o_but_ok.disabled	= false;
		document.all.o_but_cancel.disabled	= true;
	}
}

function OK_Click()
{
	window.opener=self;window.close();
	return false;
}

function Cancel_Click()
{
	if(CheckBrowser())	
	{
		try{
			dialogArguments.location.href=dialogArguments.location.href;
			window.close();
		}catch(ex){}
	}
	else
	{
		window.opener.opener=null;
		window.opener.location.href=window.opener.location.href;
		window.close();
		this.disabled=true;
	}
	return true;
}

function CheckBrowser()
{
	if (navigator.appName == ""Microsoft Internet Explorer"") 
	{ 
		start = navigator.userAgent.indexOf(""MSIE "") + ""MSIE "".length 
		if(navigator.userAgent.indexOf("";"", start) > 0) 
		{
			end = navigator.userAgent.indexOf("";"", start) 
		} 
		else 
		{ 
			end = navigator.userAgent.indexOf("")"", start) 
		} 
		version = parseFloat(navigator.userAgent.substring(start, end)) 
		if(version>5.5)	return true;
	}
	return false;
}
-->
</script>
<meta http-equiv='Content-Type' content='text/html; charset=utf-8'>
<style>TD { FONT-SIZE: 10pt; FONT-FAMILY: 宋体;}</style>
</head>
<body bgcolor='#CCCCCC' rightMargin='20' leftMargin='20' bottomMargin='10' topMargin='10'  oncontextmenu='self.event.returnValue=false' style='CURSOR:default;BORDER-TOP-STYLE:none;BORDER-RIGHT-STYLE:none;BORDER-LEFT-STYLE:none;BORDER-BOTTOM-STYLE:none' onLoad='UpdateUploadStatus();'>
<table width='100%' border='0' cellspacing='0' cellpadding='0'>
	<tr><td height='8'></td></tr>
	<tr><td>
		<table width='100%' height='10' border='0' cellspacing='0' cellpadding='0' align='center'>
			<TR><TD height=22 id='o_status'>状态:</TD></TR>
			<TR><TD height=22 id='o_currentFile'>文件名:</TD></TR>
			<TR><TD height=15>
				<TABLE style='BORDER-RIGHT: #ffffff 1px solid; BORDER-TOP: #999999 1px outset; BORDER-LEFT: #999999 1px outset; BORDER-BOTTOM: #ffffff 1px solid' height='100%' cellSpacing=0 cellPadding=1 width='100%' align=center border=0>
					<TR><TD bgColor='#006699' id='o_percent_1'></TD><TD id='o_percent_2'></TD></TR>
				</TABLE></TD></TR>
			<TR><TD height=22 id='o_avgSpeed'>速率:</TD></TR>
			<TR><TD height=22 id='o_timeLeft'>剩余时间:</TD></TR>
		</table></td></tr>
	<tr><td height='40' align='right'>
	<INPUT type='button' value=' 确认 ' ID='o_but_ok' onclick='OK_Click()'>&nbsp; 
	<INPUT type='button' value=' 取消 ' ID='o_but_cancel' onclick='Cancel_Click()'>
	</td></tr>
</table>
</body></html>";
		#endregion

		#region IHttpHandler Members
	

		public void ProcessRequest(HttpContext i_context)
		{
			// TODO:  Add UploadProcessHandle.ProcessRequest implementation
			object o_Guid	= i_context.Request["UploadGUID"];
			if(o_Guid==null) return;
			string m_Guid	= o_Guid.ToString();
			UploadInstance m_uploadStatus	= i_context.Application[m_Guid] as UploadInstance;
			bool m_finished	= false;
			if(m_uploadStatus==null)
			{
				m_uploadStatus= new UploadInstance();
				m_uploadStatus.m_currentSize	= 1;
				m_uploadStatus.m_totalSize	= 100;
				m_finished		= true;
			}
			i_context.Response.Write(UploadStatusHandler.m_htmlHand);
			int m_percent	= 0;
			if(m_uploadStatus.m_currentSize!=0&&m_uploadStatus.m_totalSize!=0)
			{
				m_percent	= (int)(m_uploadStatus.m_currentSize*100/m_uploadStatus.m_totalSize);
			}
			if(m_uploadStatus.m_status==UploadInstance.UploadStatus.Finished)
			{
				m_finished	= true;
				m_percent	= 100;
			}
			i_context.Response.Write("<title>" + m_percent.ToString() + "% - 文件上传进度条</title>");
			if(!m_finished)i_context.Response.Write("<meta http-equiv='Refresh' content='1'>\r\n");
			i_context.Response.Write("<meta http-equiv='Refresh' content='1'>\r\n");
			i_context.Response.Write("<script language='javascript'>\r\n");
			i_context.Response.Write("<!--\r\n");
			i_context.Response.Write("var m_currentSize = "+m_uploadStatus.m_currentSize.ToString()+";\r\n");
			i_context.Response.Write("var m_totalSize = "+m_uploadStatus.m_totalSize.ToString()+";\r\n");
			if(m_uploadStatus.m_currentFileName!=null)
			{
				i_context.Response.Write("var m_currentFileName = '" + m_uploadStatus.m_currentFileName.Replace("\\","\\\\") + "';\r\n");
			}
			else
			{
                i_context.Response.Write("var m_currentFileName = '';\r\n");
			}
			i_context.Response.Write("var m_timeSpan = " + m_uploadStatus.UploadTimeSpan.Seconds.ToString()+";\r\n");
			i_context.Response.Write("var m_avgSpeed = '" + m_uploadStatus.AvgSpeed + "';\r\n");
			if(m_finished)
			{
				i_context.Response.Write("var m_uploadFinished = true;\r\n");
			}
			else
			{
				i_context.Response.Write("var m_uploadFinished = false;\r\n");
			}
			i_context.Response.Write("-->\r\n");
			i_context.Response.Write("</script>\r\n");
			i_context.Response.Write(UploadStatusHandler.m_htmlBody);
		}

		public bool IsReusable
		{
			get
			{
				// TODO:  Add UploadProcessHandle.IsReusable getter implementation
				return false;
			}
		}

		private bool IsScriptRequset(HttpContext i_context)
		{
			object o_requestType	= i_context.Request["RequestType"];
			if(o_requestType==null)return false;

			return true;
		}
		#endregion

	}
}
