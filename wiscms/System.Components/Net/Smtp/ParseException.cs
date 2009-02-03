//------------------------------------------------------------------------------
// <copyright file="ParseException.cs" company="Everwis">
//     Copyright (C) Everwis Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

namespace Wis.Toolkit.Net.Smtp
{

using System;

	/// <summary>
	/// This is a System.Exception class for handling exceptions in 
	/// MIME Parsing operations.
	/// </summary>
	public class ParseException : ApplicationException 
	{
		public ParseException (String message) : base (message) {}

		public ParseException (String message, System.Exception inner) : base(message,inner) {}   
	}

}