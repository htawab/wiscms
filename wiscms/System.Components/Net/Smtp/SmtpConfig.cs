//------------------------------------------------------------------------------
// <copyright file="SmtpConfig.cs" company="Everwis">
//     Copyright (C) Everwis Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

namespace Wis.Toolkit.Net.Smtp
{

using System;
using System.IO;

	/// <summary>
	/// This type stores configuration information for the smtp component.
	/// WARNING: If you turn on logging the caller must have proper permissions
	/// and the log file will grow very quickly if a lot of email messages are 
	/// being sent. PLEASE USE THE LOGGING FEATURE FOR DEBUGGING ONLY.
	/// </summary>
	public class SmtpConfig
	{
		private SmtpConfig()
		{}
		
		///<value>Stores the default SMTP host</value>
		public static string 	SmtpHost 			= "localhost";

		///<value>Stores the default SMTP port</value>
		public static int 		SmtpPort 			= 25;

		///<value>Flag used for turning on and off logging to a text file.
		/// The caller must have proper permissions for this to work</value>
		public static bool		LogToText			= false;

#warning TODO:ApplicationPath有多处用到，应给以封装，以达到复用效果
        private static string ApplicationPath
        {
            get
            {
                string path = System.Reflection.Assembly.GetExecutingAssembly().CodeBase.Substring(8).ToLower();
                int length = path.LastIndexOf("/bin/"); // 截取掉DLL的文件名，得到DLL当前的路径
                return path.Substring(0, length).Replace("/", "\\");
            }
        }

		///<value>Path to use when logging to a text file. 
		/// The caller must have proper permissions for this to work</value>
		public static string LogPath
		{
			get
			{    // = @"../logs/SmtpLog.txt";
				// 得到邮件日志文件保存的目录
#warning TODO: 日志路径写入到Setting文件中
                string path = ApplicationPath + "\\Logs\\Net\\";
				if(!System.IO.Directory.Exists(path)) System.IO.Directory.CreateDirectory(path);
				return System.IO.Path.Combine(path, "SmtpLog.txt");
			}
		}
			

		public static long		LogMaxSize			= 1048576; // one meg
		
		///<value>Path used to store temp files used when sending email messages.
		/// The default value is the temp directory specified in the Environment variables.</value>
		public static string	TempPath 			= Path.GetTempPath();
		
		///<value>Flag used to turn on and off address format verification.
		/// If it is turned on all addresses must meet RFC 822 format.
		/// The default value is false.
		/// WARNING: Turning this on will decrease performance.</value>
		public static bool		VerifyAddresses		= false;
		
		///<value>Version of this OpenSmtp SMTP .Net component</value>
		// public static readonly string Version		= "OpenSmtp.net version 01.11.0";
		
		///<value>Mailer header added to each message sent</value>
		internal static string 	X_MAILER_HEADER		= "X-Mailer: Dante Mail";
	}
}