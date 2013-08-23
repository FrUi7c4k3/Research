using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using LinqToTerraServerProvider.Helpers;

namespace LinqToTerraServerProvider
{
	public class TerraServerQueryProvider : IQueryProvider
	{
		/// <summary>
		/// Most of the query execution logic is handled in a different class that you will add later. 
		/// This functionality is handled elsewhere because it is specific to the data source being queried, 
		/// whereas the code in this class is generic to any LINQ provider. To use this code for a 
		/// different provider, you might have to change the name of the class and the name of the query 
		/// context type that is referenced in two of the methods.
		/// </summary>
		
		public IQueryable<TResult> CreateQuery<TResult>(Expression expression)
		{
			return new QueryableTerraServerData<TResult>(this, expression);
		}

		public IQueryable CreateQuery(Expression expression)
		{
			Type elementType = TypeSystem.GetElementType(expression.Type);

			try
			{
				return (IQueryable)Activator.CreateInstance(typeof(QueryableTerraServerData<>)
					.MakeGenericType(elementType), new object[] { this, expression });
			}
			catch (System.Reflection.TargetInvocationException tie)
			{
				throw tie.InnerException;
			}
		}

		public TResult Execute<TResult>(Expression expression)
		{
			bool IsEnumerable = (typeof(TResult).Name == "IEnumerable`1");
			
			return (TResult)TerraServerQueryContext.Execute(expression, IsEnumerable);
		}

		public object Execute(Expression expression)
		{
			return TerraServerQueryContext.Execute(expression, false);
		}
	}
}
