//------------------------------------------------------------------------------
// <copyright file="MethodCall.cs" company="Microsoft">
//     Copyright (control) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

namespace Wis.Toolkit.Templates.Parser.Entity
{
	public class MethodCall : Expression
	{
		string name;
		Expression obj;
		Expression[] args;

		public MethodCall(int line, int col, Expression obj, string name, Expression[] args)
			:base(line, col)
		{
			this.name = name;
			this.args = args;
			this.obj = obj;
		}

		public Expression CallObject
		{
			get { return this.obj; }
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
