//------------------------------------------------------------------------------
// <copyright file="TemplateLexer.cs" company="Microsoft">
//     Copyright (control) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Specialized;
using System.IO;

namespace Wis.Toolkit.Templates.Parser
{
	public class TemplateLexer
	{
        static HybridDictionary keywords;

        static TemplateLexer()
        {
            keywords = new HybridDictionary(false);
            keywords["or"] = TokenKind.OpOr;
            keywords["and"] = TokenKind.OpAnd;
			keywords["is"] = TokenKind.OpIs;
			keywords["isnot"] = TokenKind.OpIsNot;
			keywords["lt"] = TokenKind.OpLt;
			keywords["gt"] = TokenKind.OpGt;
			keywords["lte"] = TokenKind.OpLte;
			keywords["gte"] = TokenKind.OpGte;
        }

		enum LexMode
		{
			Text,
			Tag,
			Expression,
			String
		}

		const char EOF = (char)0;

		LexMode currentMode;
		Stack modes;

		int line;

		int column;

		/// <summary>
		/// 字符位置
		/// </summary>
		int pos;	// position within data

		/// <summary>
		/// 模板数据
		/// </summary>
		string data;

		int saveLine;
		int saveCol;
		int savePos;

		public TemplateLexer(TextReader reader)
		{
			if (reader == null)
				throw new ArgumentNullException("reader");

			data = reader.ReadToEnd();

			Reset();
		}

		public TemplateLexer(string data)
		{
			if (data == null)
				throw new ArgumentNullException("data");

			this.data = data;

			Reset();
		}

		private void EnterMode(LexMode mode)
		{
			modes.Push(currentMode);
			currentMode = mode;
		}

		private void LeaveMode()
		{
			currentMode = (LexMode)modes.Pop();
		}

		private void Reset()
		{
			modes = new Stack();
			currentMode = LexMode.Text;
			modes.Push(currentMode);

			line = 1;
			column = 1;
			pos = 0;
		}


		/// <summary>
		/// 从在模板数据的字符位置pos起第count个字符。

		/// </summary>
		/// <param name="count">字符位置数。</param>
		/// <returns></returns>
		protected char RightChar(int count)
		{
			return SubChar(pos, count);
		}


		/// <summary>
		/// 从模板数据中检索字符。

		/// </summary>
		/// <param name="startIndex">搜索起始位置。</param>
		/// <param name="count">要检查的字符位置数。</param>
		/// <returns>一个 char，等效于模板数据中从 startIndex 开始的第 count 个字符。   - 或 -   如果 startIndex 与 count 之和大于模板数据的长度，则为 EOF。</returns>
		public char SubChar( int startIndex , int count)
		{
			if (startIndex + count >= data.Length)
				return EOF;
			else
				return data[pos + count];
		}

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		protected char Continue()
		{
			char ret = data[pos];
			pos++;
			column++;

			return ret;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="count"></param>
		/// <returns></returns>
		protected char Continue(int count)
		{
			if (count <= 0)
				throw new ArgumentOutOfRangeException("count", "count必须大于0");

			char ret = ' ';
			while (count > 0)
			{
				ret = Continue();
				count--;
			}
			return ret;
		}

		void NewLine()
		{
			line++;
			column = 1;
		}

		protected Token CreateToken(TokenKind kind, string value)
		{
			return new Token(kind, value, line, column);
		}

		protected Token CreateToken(TokenKind kind)
		{
			string tokenData = data.Substring(savePos, pos - savePos);
			if (kind == TokenKind.StringText)
				tokenData = tokenData.Replace("\"\"", "\""); // replace double "" with single "
			if (kind == TokenKind.StringText || kind == TokenKind.TextData)
				tokenData = tokenData.Replace("$$", "$");	// replace $$ with $

			return new Token(kind, tokenData, saveLine, saveCol);
		}

		/// <summary>
		/// reads all whitespace characters (does not include newline)
		/// </summary>
		/// <returns></returns>
		protected void ReadWhitespace()
		{
			while (true)
			{
				char ch = RightChar(0);
				switch (ch)
				{
					case ' ':
					case '\t':
						Continue();
						break;
					case '\n':
						Continue();
						NewLine();
						break;

					case '\r':
						Continue();
						if (RightChar(0) == '\n')
							Continue();
						NewLine();
						break;
					default:
						return;
				}
			}
		}

		/// <summary>
		/// save read point positions so that CreateToken can use those
		/// </summary>
		private void StartRead()
		{
			saveLine = line;
			saveCol = column;
			savePos = pos;
		}

		public Token Next()
		{
			switch (currentMode)
			{
				case LexMode.Text: return NextText();
				case LexMode.Expression: return NextExpression();
				case LexMode.Tag: return NextTag();
				case LexMode.String: return NextString();
				default: throw new ParseException("Encountered invalid lexer mode: " + currentMode.ToString(), line, column);
			}
		}

		private Token NextExpression()
		{
			StartRead();
			char ch = RightChar(0);
			switch (ch)
			{
				case EOF:
					return CreateToken(TokenKind.EOF);
				case ',':
					Continue();
					return CreateToken(TokenKind.Comma);
				case '.':
					Continue();
					return CreateToken(TokenKind.Dot);
				case '(':
					Continue();
					return CreateToken(TokenKind.LParen);
				case ')':
					Continue();
					return CreateToken(TokenKind.RParen);
				case '$':
					Continue();
					LeaveMode();
					return CreateToken(TokenKind.ExpEnd);
				case '[':
					Continue();
					return CreateToken(TokenKind.LBracket);
				case ']':
					Continue();
					return CreateToken(TokenKind.RBracket);
				case ' ':
				case '\t':
				case '\r':
				case '\n':
					ReadWhitespace();
					return NextExpression();

				case '"':
					Continue();
					EnterMode(LexMode.String);
					return CreateToken(TokenKind.StringStart);

				case '0': case '1': case '2':
				case '3': case '4': case '5':
				case '6': case '7': case '8':
				case '9':
					return ReadNumber();

				case '-':
				{
					if (Char.IsDigit(RightChar(1)))
						return ReadNumber();

					goto default;
				}

				default:
					if (Char.IsLetter(ch) || ch == '_')
						return ReadId();
					else
						throw new ParseException("表达式中发现不正确的字符：" + ch + "。错误字符位于" + line.ToString() + "行, " + column.ToString() + "列", line, column);
			}
		}

		private Token NextTag()
		{
			StartRead();
			StartTagRead:
			char ch = RightChar(0);
			switch (ch)
			{
				case EOF:
					return CreateToken(TokenKind.EOF);
				case '=':
					Continue();
					return CreateToken(TokenKind.TagEquals);
				case '"':
					Continue();
					EnterMode(LexMode.String);
					return CreateToken(TokenKind.StringStart);
				case ' ':
				case '\t':
				case '\r':
				case '\n':
					ReadWhitespace();	// ignore whitespace
					StartRead();		// remark current position
					goto StartTagRead;	// start again
				case '>':
					Continue();
					LeaveMode();
					return CreateToken(TokenKind.TagEnd);
				case '/':
					if (RightChar(1) == '>')
					{
						Continue(2); // consume />
						LeaveMode();
						return CreateToken(TokenKind.TagEndClose);
					}
					break;
				default:
					if (Char.IsLetter(ch) || ch == '_')
						return ReadId();
					break;

			}
			
			throw new ParseException("标签中有不合法的字符：" + ch + "错误发生于行" + line.ToString() + "列" + column.ToString(), line, column);
		}

		private Token NextString()
		{
			StartRead();
			
			StartStringRead:
			char ch = RightChar(0);
			switch (ch)
			{
				case EOF:
					return CreateToken(TokenKind.EOF);

				case '$':
					if (RightChar(1) == '$') // just escape
					{
						Continue(2);
						goto StartStringRead;
					}
					else if (savePos == pos)
					{
						Continue();
						EnterMode(LexMode.Expression);
						return CreateToken(TokenKind.ExpStart);
					}
					else
						break; // just break and we will return the text token

				case '\r':
				case '\n':
					ReadWhitespace();
					goto StartStringRead;
				case '"':
					if (RightChar(1) == '"')
					{
						// just escape
						Continue(2);
						goto StartStringRead;
					}
					else if (pos == savePos)
					{
						Continue();
						LeaveMode();
						return CreateToken(TokenKind.StringEnd);
					}
					else
						break; // just break so that text is returned
				default:
					Continue();
					goto StartStringRead;

			}

			return CreateToken(TokenKind.StringText);
		}

		private Token NextText()
		{
			StartRead();

			StartTextRead:
			switch (RightChar(0))
			{
				case EOF:
					if (savePos == pos)
						return CreateToken(TokenKind.EOF);
					else
						break;

				case '$':
					if (RightChar(1) == '$') // $ was just escape
					{
						Continue(2); // consume both $
						goto StartTextRead;
					}
					else if (savePos == pos)
					{
						Continue();
						EnterMode(LexMode.Expression);
						return CreateToken(TokenKind.ExpStart);
					}
					else
						break; // even if we have exp, we break because we read some characters that need to be returned as text

				case '<':
					if (RightChar(1) == 't' && RightChar(2) == 'p' && RightChar(3) == 'l' && RightChar(4) == ':')
					{
						if (savePos == pos)
						{
							Continue(5); // Continue <tpl:
							EnterMode(LexMode.Tag);
							return CreateToken(TokenKind.TagStart);
						}
						else
							break;
					}
					else if (RightChar(1) == '/' && RightChar(2) == 't' && RightChar(3) == 'p' && RightChar(4) == 'l' && RightChar(5) == ':')
					{
						if (savePos == pos)
						{
							Continue(6); // Continue </tpl:
							EnterMode(LexMode.Tag);
							return CreateToken(TokenKind.TagClose);
						}
						else
							break;
					}
					Continue();
					goto StartTextRead;
				case '\n': 
				case '\r':
					ReadWhitespace();	// handle newlines specially so that line number count is kept
					goto StartTextRead;	

				default:
					Continue();
					goto StartTextRead;
			}

			return CreateToken(TokenKind.TextData);
		}

		/// <summary>
		/// reads word. Word contains any alpha character or _
		/// </summary>
		protected Token ReadId()
		{
			StartRead();

			Continue(); // consume first character of the word

			while (true)
			{
				char ch = RightChar(0);
				if (Char.IsLetterOrDigit(ch) || ch == '_')
					Continue();
				else
					break;
			}

            string tokenData = data.Substring(savePos, pos - savePos);

            if (keywords.Contains(tokenData))
                return CreateToken((TokenKind)keywords[tokenData]);
            else
			    return CreateToken(TokenKind.ID, tokenData);
		}

		/// <summary>
		/// returns either Integer or Double Token
		/// </summary>
		/// <returns></returns>
		protected Token ReadNumber()
		{
			StartRead();
			Continue(); // consume first digit or -

			bool hasDot = false;

			while (true)
			{
				char ch = RightChar(0);
				if (Char.IsNumber(ch))
					Continue();

				// if "." and didn't see "." yet, and next char
				// is number, than starting to read decimal number
				else if (ch == '.' && !hasDot && Char.IsNumber(RightChar(1)))
				{
					Continue();
					hasDot = true;
				}
				else
					break;
			}

			return CreateToken(hasDot ? TokenKind.Double : TokenKind.Integer);
		}

	}
}
