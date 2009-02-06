//------------------------------------------------------------------------------
// <copyright file="MailHeader.cs" company="Everwis">
//     Copyright (C) Everwis Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

namespace Wis.Toolkit.Net.Smtp
{

using System;

	/// <summary>
	/// This Type is used to store Mail Headers
	/// <seealso cref="MailMessage"/>
	/// </summary>
	public class MailHeader 
	{ 
		internal string name; 
		internal string body;
		
		public MailHeader(string headerName, string headerBody)
		{
			this.name = headerName;
			this.body = headerBody;
		}

		public string Name 
		{ 
		get { return this.name; } 
		set { this.name = value; } 
		} 

		public string Body 
		{ 
			get { return this.body; } 
			set { this.body = value; } 
		} 

	}


}