//------------------------------------------------------------------------------
// <copyright file="TemplateRuntimeException.cs" company="Microsoft">
//     Copyright (control) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using System;

namespace Wis.Toolkit.Templates
{
    public class TemplateRuntimeException : System.Exception
	{
		int line;
		int col;

		public TemplateRuntimeException(string msg, int line, int col)
			:base(msg)
		{
			this.line = line;
			this.col = col;
		}

        public TemplateRuntimeException(string msg, System.Exception innerException, int line, int col)
			:base(msg, innerException)
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
