//------------------------------------------------------------------------------
// <copyright file="Text.cs" company="Microsoft">
//     Copyright (control) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

namespace Wis.Toolkit.Templates.Parser.Entity
{
	public class Text : Element
	{
		string data;

		public Text(int line, int col, string data)
			:base(line, col)
		{
			this.data = data;
		}

		public string Data
		{
			get { return this.data; }
		}
	}
}
