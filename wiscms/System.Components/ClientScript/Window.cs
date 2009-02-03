//------------------------------------------------------------------------------
// <copyright file="Window.cs" company="Oriental Everwisdom">
//     Copyright (C) Oriental Everwisdom Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using System.Text;
using System.Web;

namespace Wis.Toolkit.ClientScript
{
	/// <summary>
	/// Window��
	/// </summary>
	public class Window
	{
		/// <summary>
		/// Window ���ṩ�Ķ��Ǿ�̬����������Ҫ����ʵ��
		/// </summary>
		private Window() { }
		
		
		/// <summary>
		/// �ȴ�ָ����ʱ�䡣
		/// </summary>
		/// <param name="timeout">�ȴ��ĺ�������</param>
		public static void Wait(int timeout)
		{
			HttpContext.Current.Response.Flush();
			System.Threading.Thread.Sleep(timeout);
		}

		/// <summary>
		/// ���������ͻ��˵Ļ���
		/// </summary>
		public static void ClearClientPageCache() 
		{ 
			HttpContext.Current.Response.Buffer = true; 
			HttpContext.Current.Response.Expires = 0; 
			HttpContext.Current.Response.ExpiresAbsolute = System.DateTime.Now.AddDays(-1); 
			HttpContext.Current.Response.AddHeader("pragma", "no-cache"); 
			HttpContext.Current.Response.AddHeader("cache-control", "private"); 
			HttpContext.Current.Response.CacheControl = "no-cache"; 
		}

		public class Location 
		{ 

			public static void Replace(string url) 
			{ 
				StringBuilder sb = new StringBuilder(); 
				sb.Append("\n<script language=JavaScript>"); 
				sb.Append("\n<!--"); 
				sb.Append("\n	var theWin = self;"); 
				sb.Append("\n	"); 
				sb.Append("\n	while(top != theWin)"); 
				sb.Append("\n	{"); 
				sb.Append("\n		theWin = top;"); 
				sb.Append("\n	}"); 
				sb.Append("\n	"); 
				sb.Append("\n	theWin.location.replace('" + url + "')"); 
				sb.Append("\n//-->"); 
				sb.Append("\n</SCRIPT>"); 
				HttpContext.Current.Response.Write(sb.ToString()); 
			} 
		}

		/// <summary>
		/// ���Թرմ���
		/// </summary>
		public static void Close() 
		{ 
			StringBuilder sb = new StringBuilder(); 
			sb.Append("<script language=JavaScript>\n"); 
			sb.Append("<!--\n"); 
			sb.Append("	var ua = navigator.userAgent\n"); 
			sb.Append("	var ie = navigator.appName==\"Microsoft Internet Explorer\"?true:false\n"); 
			sb.Append("	var theWin = self;\n"); 
			sb.Append(" \n"); 
			sb.Append("	while(top != theWin)\n"); 
			sb.Append("	{\n"); 
			sb.Append("  theWin = top;\n"); 
			sb.Append("	}\n"); 
			sb.Append(" \n"); 
			sb.Append("if(ie){\n"); 
			sb.Append(" var IEversion=parseFloat(ua.substring(ua.indexOf(\"MSIE \")+5,ua.indexOf(\";\",ua.indexOf(\"MSIE \"))))\n"); 
			sb.Append(" if(IEversion< 5.5){\n"); 
			sb.Append(" var str = '<object id=noTipClose classid=\"clsid:ADB880A6-D8FF-11CF-9377-00AA003B7A11\">'\n"); 
			sb.Append(" str += '<param name=\"Command\" value=\"Close\"></object>';\n"); 
			sb.Append(" theWin.document.body.insertAdjacentHTML(\"beforeEnd\", str);\n"); 
			sb.Append(" theWin.document.all.noTipClose.Click();\n"); 
			sb.Append(" }\n"); 
			sb.Append(" else{\n"); 
			sb.Append(" theWin.opener =null;\n"); 
			sb.Append(" theWin.close();\n"); 
			sb.Append(" }\n"); 
			sb.Append(" }\n"); 
			sb.Append("else{ \n"); 
			sb.Append(" theWin.close()\n"); 
			sb.Append("}\n"); 
			sb.Append("//-->\n"); 
			sb.Append("</SCRIPT>\n"); 
			HttpContext.Current.Response.Write(sb.ToString()); 
		}
        /// <summary>
        /// ����
        /// </summary>
        public static void Back()
        {
            HttpContext.Current.Response.Write(string.Format("<script language=\"javascript\">history.back(1);</script>"));

        }

		/// <summary>
		/// ������Ϣ
		/// </summary>
		/// <param name="message">��Ϣ</param>
		public static void Alert(string message) 
		{
			if(message == null)message = "";

			//��� �س�������˫�����޸�Ϊ \"
			message = message.Replace("\r","").Replace("\n","").Replace("\"", "\\\"");

			HttpContext.Current.Response.Write("<script>\n"); 
			HttpContext.Current.Response.Write(" window.alert(\"" + message + "\");\n"); 
			HttpContext.Current.Response.Write("</script>\n"); 
		}

		
		/// <summary>
		/// �������ڡ�
		/// </summary>
		/// <param name="url">��ַ</param>
		public static void Open(string url) 
		{
			HttpContext.Current.Response.Write("<script>\n"); 
			HttpContext.Current.Response.Write(" window.Open(\"" + url + "\");\n"); 
			HttpContext.Current.Response.Write("</script>\n"); 
		}

        /// <summary>
        /// ����ȷ�Ͽ�,�����û�ѡ����תҳ��
        /// </summary>
        /// <param name="Words">��ʾ</param>
        /// <param name="TrueUrl">ȷ��ʱ��תURL</param>
        /// <param name="FalseUrl">ȡ��ʱ��תURL</param>
        public static void ConfirmRedirect(string Words,string TrueUrl,string FalseUrl)
        {
            string Script = @"<script>
                                if(window.confirm('{0}'))
                                    window.location.href='{1}';
                                  else
                                    window.location.href='{2}';
                               </script>";
            HttpContext.Current.Response.Write(string.Format(Script, Words, TrueUrl, FalseUrl));
        }
		/// <summary>
		/// ҳ����ת
		/// </summary>
		/// <param name="url">��תURL��ַ</param>
		public static void Redirect(string url) 
		{
			if(url == null)return;

			url = url.Replace("\n","").Replace("'", "\\'");

////            //û�в������Զ�׷���������
////            if (url.IndexOf("?") == -1) url += "?Random" + System.Guid.NewGuid().ToString() + "=Random" + System.Guid.NewGuid().ToString();
////
////            //��ê����д���
////            //if (url.IndexOf("#") != -1)
////            //{
////            //    anchor = url.url.Substring(0, url.IndexOf("#"));
////            //}
////
////            //?�����׷�Ӷ��Guid��Ҫ���ٲ���һ��
////            string pattern = @"Random^(\{){0,1}[0-9a-fA-F]{8}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{12}(\}){0,1}$";
////            if(!System.Text.RegularExpressions.Regex.IsMatch(url, pattern, System.Text.RegularExpressions.RegexOptions.IgnoreCase))
////                url += "&Random" + System.Guid.NewGuid().ToString() + "=Random" + System.Guid.NewGuid().ToString();
////
////            //�����µ������
////            url = System.Text.RegularExpressions.Regex.Replace(url, pattern, "Random" + System.Guid.NewGuid().ToString());

			HttpContext.Current.Response.Write("<script>\n"); 
			HttpContext.Current.Response.Write(" window.location.href = '" + url + "';\n"); 
			HttpContext.Current.Response.Write("</script>\n");
		}

		/// <summary>
		/// �������ڴ�С
		/// </summary>
		/// <param name="resizeWidth">���</param>
		/// <param name="resizeHeight">�߶�</param>
		public static void ResizeTo(int resizeWidth, int resizeHeight) 
		{ 
			HttpContext.Current.Response.Write("<script>\n"); 
			HttpContext.Current.Response.Write(" window.resizeTo(" + resizeWidth.ToString() + "," + resizeHeight.ToString() + ");\n"); 
			HttpContext.Current.Response.Write("</script>\n"); 
		}

        /// <summary>
        /// ˢ�¸�����
        /// </summary>
        public static void ReloadOpener()
        {
            HttpContext.Current.Response.Write("<script language=\"javascript\">\n");
            HttpContext.Current.Response.Write("    var _win = window.opener;\n");
            HttpContext.Current.Response.Write("    if(!_win) _win = window.dialogArguments;\n");
            HttpContext.Current.Response.Write("    if(_win){\n");
            HttpContext.Current.Response.Write("        var url = _win.location.href;\n");
            HttpContext.Current.Response.Write("    if(url.indexOf('#') > -1) url = url.substring(0, url.indexOf('#'));\n");  
            HttpContext.Current.Response.Write("        _win.location.href = url;\n");
            HttpContext.Current.Response.Write("    }\n");

            //HttpContext.Current.Response.Write(" if(window.opener)window.opener.reload();\n");
            HttpContext.Current.Response.Write("</script>\n");
        }
        /// <summary>
        /// 
        /// </summary>
        public static void RedirectOpener(string redirectPath)
        {
            HttpContext.Current.Response.Write("<script language=\"javascript\">\n");
            HttpContext.Current.Response.Write("    var _win = window.opener;\n");
            HttpContext.Current.Response.Write("    if(!_win) _win = window.dialogArguments;\n");
            HttpContext.Current.Response.Write("    if(_win){\n");
            ////HttpContext.Current.Response.Write("        _win.location.href = _win.location.href;\n");
            HttpContext.Current.Response.Write(string.Format("        _win.location.href = '{0}';\n", redirectPath));
            HttpContext.Current.Response.Write("    }\n");

            //HttpContext.Current.Response.Write(" if(window.opener)window.opener.reload();\n");
            HttpContext.Current.Response.Write("</script>\n");
        }

        /// <summary>
        /// ˢ����һ������
        /// </summary>
        public static void ReloadParent()
        {
            HttpContext.Current.Response.Write("<script language=\"javascript\">\n");
            HttpContext.Current.Response.Write("    if(window.parent){\n");
            HttpContext.Current.Response.Write("        window.parent.location.href = window.parent.location.href;\n");
            HttpContext.Current.Response.Write("    }\n");
            HttpContext.Current.Response.Write("</script>\n");
        }

        /// <summary>
        /// ˢ����һ������ ���ҷ���ê�Ǵ�
        /// </summary>
        public static void ReloadParent(string anchor)
        {
            HttpContext.Current.Response.Write("<script language=\"javascript\">\n");
            HttpContext.Current.Response.Write("    var _win = window.parent;\n");
             HttpContext.Current.Response.Write("    _win.location.reload();\n");
            HttpContext.Current.Response.Write("    if(_win){\n");
            HttpContext.Current.Response.Write("    var url = _win.location.href;\n");       
            HttpContext.Current.Response.Write("    if(url.indexOf('#') > -1) url = url.substring(0, url.indexOf('#'));\n");  
            HttpContext.Current.Response.Write("    url += '#" + anchor + "';\n");
            HttpContext.Current.Response.Write("        _win.location.href = url;\n");
            HttpContext.Current.Response.Write("    }\n");
            HttpContext.Current.Response.Write("</script>\n");
        }

        /// <summary>
        /// ˢ�¸����ڣ����ҷ���ê�Ǵ���
        /// </summary>
        /// <param name="anchor">ê������</param>
        public static void ReloadOpener(string anchor)
        {
            HttpContext.Current.Response.Write("<script language=\"javascript\">\n");
            HttpContext.Current.Response.Write("    var _win = window.opener;\n");
            HttpContext.Current.Response.Write("    if(!_win) _win = window.dialogArguments;\n");

            //�Ѿ�anchorȥ�����ٸ����µ�anchor
            HttpContext.Current.Response.Write("    if(_win){\n");
            HttpContext.Current.Response.Write("    var url = _win.location.href;\n");
            HttpContext.Current.Response.Write("    _win.location.reload();\n");
            HttpContext.Current.Response.Write("    if(url.indexOf('#') > -1) url = url.substring(0, url.indexOf('#'));\n");
            HttpContext.Current.Response.Write("    url += '#" + anchor + "';\n");
            HttpContext.Current.Response.Write("    _win.location.href = url;\n");
            //HttpContext.Current.Response.Write("    _win.location.reload();//.href = url;\n");
            HttpContext.Current.Response.Write("    }\n");
            HttpContext.Current.Response.Write("</script>\n");
        }

        public static void CloseOpener()
        {
            HttpContext.Current.Response.Write("<script language=\"javascript\">\n");
            HttpContext.Current.Response.Write(" if(window.opener)window.opener.close();\n");
            HttpContext.Current.Response.Write("</script>\n");
        }

        public static void Reload()
        {
            HttpContext.Current.Response.Write("<script language=\"javascript\">\n");
            HttpContext.Current.Response.Write(" window.location.href=window.location.href");
            HttpContext.Current.Response.Write("</script>\n");
        }

        public static void Reloadparent()
        {
            HttpContext.Current.Response.Write("<script language=\"javascript\">\n");
            HttpContext.Current.Response.Write("    if(window.parent)window.parent.location.reload();\n");
            HttpContext.Current.Response.Write("    var url = window.parent.location.href;\n");
            HttpContext.Current.Response.Write("    window.parent.location.href = url;\n");
            HttpContext.Current.Response.Write("</script>\n");
        }

	}
}
