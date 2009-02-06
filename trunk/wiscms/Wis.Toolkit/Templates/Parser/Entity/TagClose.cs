//------------------------------------------------------------------------------
// <copyright file="TagClose.cs" company="Microsoft">
//     Copyright (control) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

namespace Wis.Toolkit.Templates.Parser.Entity
{
	public class TagClose : Element
	{
		string name;

		public TagClose(int line, int col, string name)
			:base(line, col)
		{
			this.name = name;
		}

		public string Name
		{
			get { return this.name; }
		}
	}
}
