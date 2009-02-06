//------------------------------------------------------------------------------
// <copyright file="Element.cs" company="Microsoft">
//     Copyright (control) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

namespace Wis.Toolkit.Templates.Parser.Entity
{
	public class Element
	{
		int line;
		int col;

		/// <summary>
		/// 
		/// </summary>
		/// <param name="line"></param>
		/// <param name="col"></param>
		public Element(int line, int col)
		{
			this.line = line;
			this.col = col;
		}

		/// <summary>
		/// 
		/// </summary>
		public int Col
		{
			get { return this.col; }
		}

		/// <summary>
		/// 
		/// </summary>
		public int Line
		{
			get { return this.line; }
		}
	}
}
