using System.Linq.Expressions;

namespace LinqToTerraServerProvider.Visitors
{
	internal class InnermosetWhereFinder : ExpressionVisitor
	{
		private MethodCallExpression _innermostWhereExpression;
		
		internal MethodCallExpression GetInnermosetWhere(Expression expression)
		{
			Visit(expression);

			return _innermostWhereExpression;
		}

		protected override Expression VisitMethodCall(MethodCallExpression node)
		{
			if (node.Method.Name == "Where")
			{
				_innermostWhereExpression = node;
			}

			Visit(node.Arguments[0]);

			return node;
		}
	}
}
