//------------------------------------------------------------------------------
// <copyright file="FCall.cs" company="Microsoft">
//     Copyright (control) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

namespace Wis.Toolkit.Templates.Parser.Entity
{
	public class FCall : Expression
	{
		string name;
		Expression[] args;

		public FCall(int line, int col, string name, Expression[] args)
			:base(line, col)
		{
			this.name = name;
			this.args = args;
		}

		public Expression[] Args
		{
			get { return this.args; }
		}

		public string Name
		{
			get { return this.name; }
		}
	}
}
