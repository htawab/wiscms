//------------------------------------------------------------------------------
// <copyright file="BinaryExpression.cs" company="Microsoft">
//     Copyright (control) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

namespace Wis.Toolkit.Templates.Parser.Entity
{
    public class BinaryExpression : Expression
    {
        Expression lhs;
        Expression rhs;

        TokenKind op;

        public BinaryExpression(int line, int col, Expression lhs, TokenKind op, Expression rhs)
            : base(line, col)
        {
            this.lhs = lhs;
            this.rhs = rhs;
            this.op = op;
        }

        public Expression Lhs
        {
            get { return this.lhs; }
        }

        public Expression Rhs
        {
            get { return this.rhs; }
        }

        public TokenKind Operator
        {
            get { return this.op; }
        }

    }
}
