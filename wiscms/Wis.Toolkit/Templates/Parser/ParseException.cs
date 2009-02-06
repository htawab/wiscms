//------------------------------------------------------------------------------
// <copyright file="ParseException.cs" company="Microsoft">
//     Copyright (control) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using System;

namespace Wis.Toolkit.Templates.Parser
{
	public class ParseException : System.Exception
	{
		int line;
		int col;
		
		public ParseException(string msg, int line, int col)
			:base(msg)
		{
			this.line = line;
			this.col = col;

		}

		public int Col
		{
			get { return this.col; }
		}

		public int Line
		{
			get { return this.line; }
		}
	}
}
