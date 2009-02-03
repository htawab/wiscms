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
	/// Window。
	/// </summary>
	public class Window
	{
		/// <summary>
		/// Window 类提供的都是静态方法，不需要创建实例
		/// </summary>
		private Window() { }
		
		
		/// <summary>
		/// 等待指定的时间。
		/// </summary>
		/// <param name="timeout">等待的毫秒数。</param>
		public static void Wait(int timeout)
		{
			HttpContext.Current.Response.Flush();
			System.Threading.Thread.Sleep(timeout);
		}

		/// <summary>
		/// 清空浏览器客户端的缓存
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
		/// 绝对关闭窗口
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
        /// 回退
        /// </summary>
        public static void Back()
        {
            HttpContext.Current.Response.Write(string.Format("<script language=\"javascript\">history.back(1);</script>"));

        }

		/// <summary>
		/// 弹出消息
		/// </summary>
		/// <param name="message">消息</param>
		public static void Alert(string message) 
		{
			if(message == null)message = "";

			//清除 回车符；将双引号修改为 \"
			message = message.Replace("\r","").Replace("\n","").Replace("\"", "\\\"");

			HttpContext.Current.Response.Write("<script>\n"); 
			HttpContext.Current.Response.Write(" window.alert(\"" + message + "\");\n"); 
			HttpContext.Current.Response.Write("</script>\n"); 
		}

		
		/// <summary>
		/// 弹出窗口。
		/// </summary>
		/// <param name="url">网址</param>
		public static void Open(string url) 
		{
			HttpContext.Current.Response.Write("<script>\n"); 
			HttpContext.Current.Response.Write(" window.Open(\"" + url + "\");\n"); 
			HttpContext.Current.Response.Write("</script>\n"); 
		}

        /// <summary>
        /// 跳出确认框,并按用户选择跳转页面
        /// </summary>
        /// <param name="Words">提示</param>
        /// <param name="TrueUrl">确认时跳转URL</param>
        /// <param name="FalseUrl">取消时跳转URL</param>
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
		/// 页面跳转
		/// </summary>
		/// <param name="url">跳转URL地址</param>
		public static void Redirect(string url) 
		{
			if(url == null)return;

			url = url.Replace("\n","").Replace("'", "\\'");

////            //没有参数，自动追加随机参数
////            if (url.IndexOf("?") == -1) url += "?Random" + System.Guid.NewGuid().ToString() + "=Random" + System.Guid.NewGuid().ToString();
////
////            //对锚点进行处理
////            //if (url.IndexOf("#") != -1)
////            //{
////            //    anchor = url.url.Substring(0, url.IndexOf("#"));
////            //}
////
////            //?这里会追加多次Guid，要跟踪测试一下
////            string pattern = @"Random^(\{){0,1}[0-9a-fA-F]{8}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{12}(\}){0,1}$";
////            if(!System.Text.RegularExpressions.Regex.IsMatch(url, pattern, System.Text.RegularExpressions.RegexOptions.IgnoreCase))
////                url += "&Random" + System.Guid.NewGuid().ToString() + "=Random" + System.Guid.NewGuid().ToString();
////
////            //更换新的随机数
////            url = System.Text.RegularExpressions.Regex.Replace(url, pattern, "Random" + System.Guid.NewGuid().ToString());

			HttpContext.Current.Response.Write("<script>\n"); 
			HttpContext.Current.Response.Write(" window.location.href = '" + url + "';\n"); 
			HttpContext.Current.Response.Write("</script>\n");
		}

		/// <summary>
		/// 调整窗口大小
		/// </summary>
		/// <param name="resizeWidth">宽度</param>
		/// <param name="resizeHeight">高度</param>
		public static void ResizeTo(int resizeWidth, int resizeHeight) 
		{ 
			HttpContext.Current.Response.Write("<script>\n"); 
			HttpContext.Current.Response.Write(" window.resizeTo(" + resizeWidth.ToString() + "," + resizeHeight.ToString() + ");\n"); 
			HttpContext.Current.Response.Write("</script>\n"); 
		}

        /// <summary>
        /// 刷新父窗口
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
        /// 刷新上一级窗口
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
        /// 刷新上一级窗口 并且返回锚记处
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
        /// 刷新父窗口，并且返回锚记处。
        /// </summary>
        /// <param name="anchor">锚记名称</param>
        public static void ReloadOpener(string anchor)
        {
            HttpContext.Current.Response.Write("<script language=\"javascript\">\n");
            HttpContext.Current.Response.Write("    var _win = window.opener;\n");
            HttpContext.Current.Response.Write("    if(!_win) _win = window.dialogArguments;\n");

            //把旧anchor去除，再附加新的anchor
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
