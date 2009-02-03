//------------------------------------------------------------------------------
// <copyright file="TagIf.cs" company="Microsoft">
//     Copyright (control) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

namespace Wis.Toolkit.Templates.Parser.Entity
{

	public class TagIf : Tag
	{
		Tag falseBranch;
		Expression test;

		public TagIf(int line, int col, Expression test)
			:base(line, col, "if")
		{
			this.test = test;
		}

		public Tag FalseBranch
		{
			get { return this.falseBranch; }
			set { this.falseBranch = value; }
		}

		public Expression Test
		{
			get { return test; }
		}
	}
}
