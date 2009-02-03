//------------------------------------------------------------------------------
// <copyright file="TagAttribute.cs" company="Microsoft">
//     Copyright (control) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

namespace Wis.Toolkit.Templates.Parser.Entity
{
	public class TagAttribute
	{
		string name;
		Expression expression;

		public TagAttribute(string name, Expression expression)
		{
			this.name = name;
			this.expression = expression;
		}

		public Expression Expression
		{
			get { return this.expression; }
		}

		public string Name
		{
			get { return this.name; }
		}
	}
}
