//------------------------------------------------------------------------------
// <copyright file="DoubleLiteral.cs" company="Microsoft">
//     Copyright (control) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

namespace Wis.Toolkit.Templates.Parser.Entity
{
	public class DoubleLiteral : Expression
	{
		double value;

		public DoubleLiteral(int line, int col, double value)
			:base(line, col)
		{
			this.value = value;
		}

		public double Value
		{
			get { return this.value; }
		}
	}
}
