//------------------------------------------------------------------------------
// <copyright file="MailPriority.cs" company="Everwis">
//     Copyright (C) Everwis Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

namespace Wis.Toolkit.Net.Smtp
{

using System;

	/// <summary>
	/// This Type stores different priority values used for a MailMessage
	/// <seealso cref="MailMessage"/>
	/// </summary>
	/// <example>
	/// <code>
	///		from = new EmailAddress("support@OpenSmtp.com", "Support");
	///		to = new EmailAddress("recipient@OpenSmtp.com", "Joe Smith");
	///		msg = new MailMessage(from, to);
	///		msg.Priority = MailPriority.High;
	/// </code>
	/// </example>
	public class MailPriority
	{

		public static readonly string Highest 	= 	"1";
		public static readonly string High		= 	"2";
		public static readonly string Normal	= 	"3";
		public static readonly string Low   	=	"4";
		public static readonly string Lowest	=	"5";
	
		private MailPriority() 
		{}
	
	}
}
	
