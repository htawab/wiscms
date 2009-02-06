//------------------------------------------------------------------------------
// <copyright file="User.cs" company="Oriental Everwisdom">
//     Copyright (C) Oriental Everwisdom Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using System;
using System.Web;
using System.Web.Security;
using System.Collections.Generic;

namespace Wis.Toolkit.Security
{
	/// <summary>
	/// User 的摘要说明。
	/// </summary>
	public class User
	{
        /// <summary>
        /// 构造函数逻辑。
        /// </summary>
		private User() { }

        /// <summary>
        /// 获取一个值，该值指示是否验证了用户。
        /// </summary>
		public static bool IsAuthenticated
		{
			get
			{
                if(System.Web.HttpContext.Current == null) return false;

                System.Security.Principal.IPrincipal user = System.Web.HttpContext.Current.User;
                if (user == null) return false;

                // 如果用户已经过验证，则为 true；否则为 false。
                return user.Identity.IsAuthenticated; 
			}
		}

		/// <summary>
		/// 确定当前用户是否属于指定的角色。
		/// </summary>
		/// <param name="role">要检查其成员资格的角色的名称。</param>
		/// <returns>如果当前用户是指定角色的成员，则为 true；否则为 false。</returns>
		public static bool IsInRole(string role) 
		{
            if (System.Web.HttpContext.Current == null) return false;
            System.Security.Principal.IPrincipal user = System.Web.HttpContext.Current.User;
            if (user == null) return false;

            return user.IsInRole(role); 
		}

        public static void SetAuthenticatedTicket(System.Guid userID, bool createPersistentCookie)
        {
            List<string> roleNames = new List<string>();
            SetAuthenticatedTicket(userID, roleNames, createPersistentCookie);
        }

		public static void SetAuthenticatedTicket(System.Guid userID, List<string> roleNames, bool createPersistentCookie) 
		{ 
			// 为给定的 userName、createPersistentCookie 和 strCookiePath 创建身份验证票，并将其附加到 Cookie 的输出响应集合。它不执行重定向。
			
			string logonID = userID.ToString();
			System.Web.Security.FormsAuthentication.SetAuthCookie(logonID, createPersistentCookie);
			HttpContext.Current.Response.Cookies[FormsAuthentication.FormsCookieName].Expires = DateTime.Now.AddDays(1);

			string userData = "";
			for(int index = 0;index < roleNames.Count;index++)
			{
				userData += roleNames[index];

				if(index < roleNames.Count -1)userData += ",";
			}

			FormsAuthenticationTicket authTicket = new 
				FormsAuthenticationTicket( 
				1, // version 
				logonID, // 用户编号
				DateTime.Now, // creation 
				DateTime.Now.AddMinutes(20),// Expiration 
				false, // Persistent 
				userData); // User data 
			 
			string encryptedTicket = FormsAuthentication.Encrypt(authTicket); //加密 

			//存入Cookie 
			HttpCookie authCookie = 
							   new HttpCookie(FormsAuthentication.FormsCookieName, 
							   encryptedTicket); 

			HttpContext.Current.Response.Cookies.Add(authCookie); 
			
			// 更新当前User
			System.Security.Principal.GenericIdentity genericIdentity = new System.Security.Principal.GenericIdentity(logonID);
			System.Security.Principal.GenericPrincipal genericPrincipal = new System.Security.Principal.GenericPrincipal(genericIdentity, roleNames.ToArray()); 
			HttpContext.Current.User = genericPrincipal;
		}
	}
}
