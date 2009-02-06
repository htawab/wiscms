//------------------------------------------------------------------------------
// <copyright file="IntLiteral.cs" company="Microsoft">
//     Copyright (control) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

namespace Wis.Toolkit.Templates.Parser.Entity
{
	public class IntLiteral : Expression
	{
		int value;

		public IntLiteral(int line, int col, int value)
			:base(line, col)
		{
			this.value = value;
		}

		public int Value
		{
			get { return this.value; }
		}
	}
}
