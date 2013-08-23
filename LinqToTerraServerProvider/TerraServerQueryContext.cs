﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using LinqToTerraServerProvider.Helpers;
using LinqToTerraServerProvider.Visitors;

namespace LinqToTerraServerProvider
{
	/// <summary>
	/// Knows 
	/// </summary>
	internal class TerraServerQueryContext
	{
		internal static object Execute(Expression expression, bool IsEnumerable)
		{
			// The expression must represent a query over the data source. 
			if (IsQueryOverDataSource(expression))
				throw new InvalidProgramException("No query over the data source was specified.");

			// Find the call to Where() and get the lambda expression predicate.
			InnermosetWhereFinder whereFinder = new InnermosetWhereFinder();
			MethodCallExpression whereExpression = whereFinder.GetInnermosetWhere(expression);
			LambdaExpression lambdaExpression = (LambdaExpression) ((UnaryExpression) whereExpression.Arguments[1]).Operand;

			// Send the lambda expression through the partial evaluator.
			lambdaExpression = (LambdaExpression)Evaluator.PartialEval(lambdaExpression);

			return true;
		}

		private static bool IsQueryOverDataSource(Expression expression)
		{
			return expression is MethodCallExpression;
		}
	}
}
