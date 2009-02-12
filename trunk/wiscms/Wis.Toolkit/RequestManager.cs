//------------------------------------------------------------------------------
// <copyright file="RequestManager.cs" company="Everwis">
//     Copyright (C) Everwis Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using System.Web;

namespace Wis.Toolkit
{
    public class RequestManager
    {
        /// <summary>
        /// 获取客户端的IP地址
        /// </summary>
        /// <returns></returns>
        public static string GetClientIP()
        {
            if(HttpContext.Current == null)
                return "0.0.0.0";

            string result = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (string.IsNullOrEmpty(result))
            {
                result = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
            }
            if (string.IsNullOrEmpty(result))
            {
                result = HttpContext.Current.Request.UserHostAddress;
            }
            if (string.IsNullOrEmpty(result))
                return "0.0.0.0";

            return result;
        }


        /// <summary>
        /// 判断当前访问是否来自浏览器软件

        /// </summary>
        /// <returns>当前访问是否来自浏览器软件</returns>
        public static bool IsBrowser()
        {
            if (HttpContext.Current == null)
                return false;

            string[] BrowserName = { "ie", "opera", "netscape", "mozilla", "konqueror", "firefox" };
            string curBrowser = HttpContext.Current.Request.Browser.Type.ToLower();
            for (int i = 0; i < BrowserName.Length; i++)
            {
                if (curBrowser.IndexOf(BrowserName[i]) >= 0)
                {
                    return true;
                }
            }
            return false;
        }


        /// <summary>
        /// 判断是否来自搜索引擎链接
        /// </summary>
        /// <returns>是否来自搜索引擎链接</returns>
        public static bool IsSearchEngines()
        {
            if (HttpContext.Current == null)
                return false;

            if (HttpContext.Current.Request.UrlReferrer == null)
            {
                return false;
            }
            string[] SearchEngine = { "google", "yahoo", "msn", "baidu", "sogou", "sohu", "sina", "163", "lycos", "tom", "yisou", "iask", "soso", "gougou", "zhongsou" };
            string tmpReferrer = HttpContext.Current.Request.UrlReferrer.ToString().ToLower();
            for (int i = 0; i < SearchEngine.Length; i++)
            {
                if (tmpReferrer.IndexOf(SearchEngine[i]) >= 0)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 获得Url或表单参数的值, 先判断Url参数是否为空字符串, 如为True则返回表单参数的值
        /// </summary>
        /// <param name="name">参数</param>
        /// <returns>Url或表单参数的值</returns>
        public static string Request(string name)
        {
            return Request(name, true);
        }

        /// <summary>
        /// 获得Url或表单参数的值, 先判断Url参数是否为空字符串, 如为True则返回表单参数的值
        /// </summary>
        /// <param name="name">参数</param>
        /// <param name="sqlSafeCheck">是否进行SQL安全检查</param>
        /// <returns>Url或表单参数的值</returns>
        public static string Request(string name, bool sqlSafeCheck)
        {
            if ("".Equals(RequestQueryString(name)))
                return RequestFormString(name, sqlSafeCheck);
            else
                return RequestQueryString(name, sqlSafeCheck);
        }

        /// <summary>
        /// 获得指定Url参数的值
        /// </summary>
        /// <param name="name">Url参数</param>
        /// <returns>Url参数的值</returns>
        public static string RequestQueryString(string name)
        {
            return RequestQueryString(name, false);
        }

        /// <summary>
        /// 获得指定Url参数的值
        /// </summary> 
        /// <param name="name">Url参数</param>
        /// <param name="sqlSafeCheck">是否进行SQL安全检查</param>
        /// <returns>Url参数的值</returns>
        public static string RequestQueryString(string name, bool sqlSafeCheck)
        {
            if (string.IsNullOrEmpty(HttpContext.Current.Request.QueryString[name]))
                return string.Empty;

            if (sqlSafeCheck && !IsSafeSqlString(HttpContext.Current.Request.QueryString[name]))
                return "含有对数据库不安全的文本";

            return HttpContext.Current.Request.QueryString[name];
        }

        /// <summary>
        /// 获得指定表单参数的值
        /// </summary>
        /// <param name="name">表单参数</param>
        /// <returns>表单参数的值</returns>
        public static string RequestFormString(string name)
        {
            return RequestFormString(name, false);
        }

        /// <summary>
        /// 获得指定表单参数的值
        /// </summary>
        /// <param name="name">表单参数</param>
        /// <param name="sqlSafeCheck">是否进行SQL安全检查</param>
        /// <returns>表单参数的值</returns>
        public static string RequestFormString(string name, bool sqlSafeCheck)
        {
            if (string.IsNullOrEmpty(HttpContext.Current.Request.Form[name]))
                return string.Empty;

            if (sqlSafeCheck && !IsSafeSqlString(HttpContext.Current.Request.Form[name]))
                return "含有对数据库不安全的文本";

            return HttpContext.Current.Request.Form[name];
        }

        /// <summary>
        /// 检测是否有Sql危险字符
        /// </summary>
        /// <param name="str">要判断字符串</param>
        /// <returns>判断结果</returns>
        public static bool IsSafeSqlString(string str)
        {
            return !System.Text.RegularExpressions.Regex.IsMatch(str, @"[-|;|,|\/|\(|\)|\[|\]|\}|\{|%|@|\*|!|\']");
        }
    }
}
