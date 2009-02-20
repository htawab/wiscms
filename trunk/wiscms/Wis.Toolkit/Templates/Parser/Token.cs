//------------------------------------------------------------------------------
// <copyright file="TokenKind.cs" company="Oriental Everwisdom">
//     Copyright (C) Oriental Everwisdom Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

namespace Wis.Toolkit.Templates.Parser
{
	public enum TokenKind
	{
		EOF,
		Comment,
		// common tokens
		ID,				// (alpha)+

		// text specific tokens
		TextData,

		// tag tokens
		TagStart,		// <tpl: 
		TagEnd,			// > 
		TagEndClose,	// />
		TagClose,		// </tpl:
		TagEquals,		// =


		// expression
		ExpStart,		// $ at the beginning
		ExpEnd,			// $ at the end
		LParen,			// (
		RParen,			// )
		Dot,			// .
		Comma,			// ,
		Integer,		// integer number
		Double,			// double number
		LBracket,		// [
		RBracket,		// ]

		// operators
        OpOr,           // "or" keyword
        OpAnd,          // "and" keyword
		OpIs,			// "is" keyword
        OpIsNot,		// "IsNot" keyword
		OpLt,			// "lt" keyword
		OpGt,			// "gt" keyword
		OpLte,			// "lte" keyword
		OpGte,			// "gte" keyword

		// string tokens
		StringStart,	// "
		StringEnd,		// "
		StringText		// text within the string
	}

	public class Token
	{
		int line;
		int col;
		string data;
		TokenKind tokenKind;

		public Token(TokenKind kind, string data, int line, int col)
		{
			this.tokenKind = kind;
			this.line = line;
			this.col = col;
			this.data = data;
		}

		public int Col
		{
			get { return this.col; }
		}

		public string Data
		{
			get { return this.data; }
			set { this.data = value; }
		}

		public int Line
		{
			get { return this.line; }
		}

		public TokenKind TokenKind
		{
			get { return this.tokenKind; }
			set { this.tokenKind = value; }
		}
	}
}
