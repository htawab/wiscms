//------------------------------------------------------------------------------
// <copyright file="StringLiteral.cs" company="Microsoft">
//     Copyright (control) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

namespace Wis.Toolkit.Templates.Parser.Entity
{
	public class StringLiteral : Expression
	{
		string content;

		public StringLiteral(int line, int col, string content)
			:base(line, col)
		{
			this.content = content;
		}

		public string Content
		{
			get { return this.content; }
		}
	}
}
