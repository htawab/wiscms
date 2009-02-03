//------------------------------------------------------------------------------
// <copyright file="SmtpException.cs" company="Everwis">
//     Copyright (C) Everwis Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using System;

namespace Wis.Toolkit.Net.Smtp
{
	/// <summary>
	/// This is a System.Exception class for handling exceptions in 
	/// SMTP operations.
	/// </summary>
	public class SmtpException : ApplicationException
	{
		public SmtpException (String message) : base (message) {}

		public SmtpException (String message, System.Exception inner) : base(message,inner) {}   
	}
}