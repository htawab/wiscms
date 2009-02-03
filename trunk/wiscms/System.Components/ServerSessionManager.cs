//------------------------------------------------------------------------------
// <copyright file="Formats.cs" company="Everwis">
//     Copyright (C) Everwis Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using System.Web;

namespace Wis.Toolkit
{
    public sealed class ServerSessionManager
    {
        private static System.Collections.SortedList activeSessions = new System.Collections.SortedList();

        /// <summary>
        /// 写 Session 值。
        /// </summary>
        /// <param sessionName="sessionName">项</param>
        /// <param sessionName="sessionValue">值</param>
        public static void Set(string sessionName, string sessionValue)
        {
            lock (typeof(ServerSessionManager))
            {
                activeSessions[sessionName] = sessionValue;
            }
        }

        /// <summary>
        /// 获得 Session 值
        /// </summary>
        /// <param sessionName="sessionName">项</param>
        /// <returns>值</returns>
        public static string Get(string sessionName)
        {
            lock (typeof(ServerSessionManager))
            {
                return activeSessions[sessionName] == null ? string.Empty : activeSessions[sessionName].ToString();
            }
        }

        public static void Remove(string sessionName)
        {
            lock (typeof(ServerSessionManager))
            {
                activeSessions.Remove(sessionName);
            }
        }
    }
}
