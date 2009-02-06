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
	/// User ��ժҪ˵����
	/// </summary>
	public class User
	{
        /// <summary>
        /// ���캯���߼���
        /// </summary>
		private User() { }

        /// <summary>
        /// ��ȡһ��ֵ����ֵָʾ�Ƿ���֤���û���
        /// </summary>
		public static bool IsAuthenticated
		{
			get
			{
                if(System.Web.HttpContext.Current == null) return false;

                System.Security.Principal.IPrincipal user = System.Web.HttpContext.Current.User;
                if (user == null) return false;

                // ����û��Ѿ�����֤����Ϊ true������Ϊ false��
                return user.Identity.IsAuthenticated; 
			}
		}

		/// <summary>
		/// ȷ����ǰ�û��Ƿ�����ָ���Ľ�ɫ��
		/// </summary>
		/// <param name="role">Ҫ������Ա�ʸ�Ľ�ɫ�����ơ�</param>
		/// <returns>�����ǰ�û���ָ����ɫ�ĳ�Ա����Ϊ true������Ϊ false��</returns>
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
			// Ϊ������ userName��createPersistentCookie �� strCookiePath ���������֤Ʊ�������丽�ӵ� Cookie �������Ӧ���ϡ�����ִ���ض���
			
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
				logonID, // �û����
				DateTime.Now, // creation 
				DateTime.Now.AddMinutes(20),// Expiration 
				false, // Persistent 
				userData); // User data 
			 
			string encryptedTicket = FormsAuthentication.Encrypt(authTicket); //���� 

			//����Cookie 
			HttpCookie authCookie = 
							   new HttpCookie(FormsAuthentication.FormsCookieName, 
							   encryptedTicket); 

			HttpContext.Current.Response.Cookies.Add(authCookie); 
			
			// ���µ�ǰUser
			System.Security.Principal.GenericIdentity genericIdentity = new System.Security.Principal.GenericIdentity(logonID);
			System.Security.Principal.GenericPrincipal genericPrincipal = new System.Security.Principal.GenericPrincipal(genericIdentity, roleNames.ToArray()); 
			HttpContext.Current.User = genericPrincipal;
		}
	}
}
