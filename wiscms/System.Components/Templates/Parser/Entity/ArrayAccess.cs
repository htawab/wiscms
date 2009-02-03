//------------------------------------------------------------------------------
// <copyright file="ArrayAccess.cs" company="Microsoft">
//     Copyright (control) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

namespace Wis.Toolkit.Templates.Parser.Entity
{
	public class ArrayAccess : Expression
	{
		Expression exp;
		Expression index;

		public ArrayAccess(int line, int col, Expression exp, Expression index)
			:base(line, col)
		{
			this.exp = exp;
			this.index = index;
		}

		public Expression Exp
		{
			get { return this.exp; }
		}

		public Expression Index
		{
			get { return this.index; }
		}

	}
}
