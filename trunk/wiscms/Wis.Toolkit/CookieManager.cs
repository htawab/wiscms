//------------------------------------------------------------------------------
// <copyright file="CookieManager.cs" company="Everwis">
//     Copyright (C) Everwis Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using System.Web;
using System;

namespace Wis.Toolkit
{
    public sealed class CookieManager
    {
        private CookieManager() { }

        private const string Name = "Bet";

        /// <summary>
        /// 写 Cookie 值。
        /// </summary>
        /// <param cookieName="cookieName">项</param>
        /// <param cookieName="cookieValue">值</param>
        public static void SetCookie(string cookieName, string cookieValue)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies[Name];
            if (cookie == null)
            {
                cookie = new HttpCookie(Name);
                cookie.Values[cookieName] = HttpUtility.UrlEncode(cookieValue);
            }
            else
            {
                cookie.Values[cookieName] = HttpUtility.UrlEncode(cookieValue);
                if (HttpContext.Current.Request.Cookies[Name]["Expires"] != null)
                {
                    int expires = 0;
                    if (int.TryParse(HttpContext.Current.Request.Cookies[Name]["Expires"].ToString(), out expires))
                    {
                        if (expires > 0)
                        {
                            cookie.Expires = DateTime.Now.AddMinutes(expires);
                        }
                    }
                }
            }

            HttpContext.Current.Response.AppendCookie(cookie);
        }

        /// <summary>
        /// 获得论坛cookie值
        /// </summary>
        /// <param cookieName="cookieName">项</param>
        /// <returns>值</returns>
        public static string GetCookie(string cookieName)
        {
            if (HttpContext.Current.Request.Cookies != null && HttpContext.Current.Request.Cookies[Name] != null && HttpContext.Current.Request.Cookies[Name][cookieName] != null)
            {
                return HttpUtility.UrlDecode(HttpContext.Current.Request.Cookies[Name][cookieName].ToString());
            }

            return string.Empty;
        }

        /// <summary>
        /// 清除 Cookie
        /// </summary>
        public static void ClearCookie()
        {
            HttpCookie cookie = new HttpCookie(Name);
            cookie.Values.Clear();
            cookie.Expires = DateTime.Now.AddYears(-1);
            HttpContext.Current.Response.AppendCookie(cookie);
        }
    }
}
