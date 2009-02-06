//------------------------------------------------------------------------------
// <copyright file="Log.cs" company="Everwis">
//     Copyright (C) Everwis Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

namespace Wis.Toolkit.Net.Smtp
{

using System;
using System.IO;
 
	/// <summary>
	/// This is a Log class. You can log text messages to a 
	/// text file and/or the event log
	/// </summary>
	internal class Log
	{
		
		internal Log() 
		{}
		
		/**
		* Logs string message to TextFile
		*/
		internal void logToTextFile(String path, String msg, String source)
		{
			if (path != null && msg != null && source != null)
			{
				try
				{
					// If the log file exists make sure it is not over the maximum allowable size
					if (File.Exists(path)) {	
						if (new FileInfo(path).Length < SmtpConfig.LogMaxSize) {
							WriteToFile(path, msg, source);
						}
					}
					else
					{
							WriteToFile(path, msg, source);
					}
				}
				catch(Exception e)
				{ Console.WriteLine("An exception occured in logToTextFile: " + e); }
			}
			else
			{
				throw new SmtpException("Null value supplied to Log.logToTextFile().");
			}
		}

		private void WriteToFile(string path, string msg, string source)
		{
			StreamWriter sw = new StreamWriter(path, true);
			sw.Write(source + msg);
			sw.Close();
		}
	}

}