//------------------------------------------------------------------------------
// <copyright file="Name.cs" company="Microsoft">
//     Copyright (control) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

namespace Wis.Toolkit.Templates.Parser.Entity
{
	public class Name : Expression
	{
		string id;

		public Name(int line, int col, string id)
			:base(line, col)
		{
			this.id = id;
		}

		public string Id
		{
			get { return this.id; }
		}
	}
}
