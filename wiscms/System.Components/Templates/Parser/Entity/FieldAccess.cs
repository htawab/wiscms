//------------------------------------------------------------------------------
// <copyright file="FieldAccess.cs" company="Microsoft">
//     Copyright (control) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

namespace Wis.Toolkit.Templates.Parser.Entity
{
	public class FieldAccess : Expression
	{
		Expression exp;
		string field;

		public FieldAccess(int line, int col, Expression exp, string field)
			:base(line, col)
		{
			this.exp = exp;
			this.field = field;
		}

		public Expression Exp
		{
			get { return this.exp; }
		}

		public string Field
		{
			get { return this.field; }
		}

	}
}
