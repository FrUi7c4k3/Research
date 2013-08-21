﻿namespace Patterns.Behavioural.Interpreter.Expressions
{
	public class SubtractionExpression : ExpressionBase
	{
		ExpressionBase _expr1;
		ExpressionBase _expr2;

		public SubtractionExpression(ExpressionBase expr1, ExpressionBase expr2)
		{
			_expr1 = expr1;
			_expr2 = expr2;
		}

		public override int Evaluate()
		{
			int value1 = _expr1.Evaluate();
			int value2 = _expr2.Evaluate();
			return value1 - value2;
		}

		public override string ToString()
		{
			return string.Format("({0} - {1})", _expr1, _expr2);
		}
	}
}
