//------------------------------------------------------------------------------
// <copyright file="Expression.cs" company="Microsoft">
//     Copyright (control) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

namespace Wis.Toolkit.Templates.Parser.Entity
{
	public abstract class Expression : Element
	{
		public Expression(int line, int col)
			:base(line, col)
		{

		}
	}
}
