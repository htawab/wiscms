//------------------------------------------------------------------------------
// <copyright file="MalformedAddressException.cs" company="Everwis">
//     Copyright (C) Everwis Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

namespace Wis.Toolkit.Net.Smtp
{

using System;

	/// <summary>
	/// This is an System.Exception class for handling
	/// an invalid RFC 822 EmailAddress.</summary>
	public class MalformedAddressException:ApplicationException 
	{
		public MalformedAddressException (String message) : base (message) {}

		public MalformedAddressException (String message, System.Exception inner) : base(message,inner) {}   
	}
}