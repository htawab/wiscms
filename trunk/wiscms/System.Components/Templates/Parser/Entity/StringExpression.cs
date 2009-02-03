//------------------------------------------------------------------------------
// <copyright file="StringExpression.cs" company="Microsoft">
//     Copyright (control) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using System.Collections;

namespace Wis.Toolkit.Templates.Parser.Entity
{
	public class StringExpression : Expression
	{
		ExpressionCollection exps;

		public StringExpression(int line, int col)
			:base(line, col)
		{
			exps = new ExpressionCollection();
		}

		/// <summary>
		/// 
		/// </summary>
		public int ExpCount
		{
			get { return exps.Count; }
		}

		public void Add(Expression exp)
		{
			exps.Add(exp);
		}

		public Expression this[int index]
		{
			get { return exps[index]; }
		}

		public IEnumerable Expressions
		{
			get
			{
				return exps;
			}
		}
	}
}
