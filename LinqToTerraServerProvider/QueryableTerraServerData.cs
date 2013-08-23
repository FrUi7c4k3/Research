using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections;
using System.Linq.Expressions;

namespace LinqToTerraServerProvider
{
	public class QueryableTerraServerData<TData> : IOrderedQueryable<TData>
	{
		#region CONSTRUCTORS

		/// <summary> 
		/// This constructor is called by the client to create the data source. 
		/// </summary> 
		public QueryableTerraServerData()
		{
			Provider = new TerraServerQueryProvider();
			Expression = Expression.Constant(this);
		}

		/// <summary> 
		/// This constructor is called by Provider.CreateQuery<TResult>(Expression expression)
		/// </summary> 
		/// <param name="expression"></param>
		public QueryableTerraServerData(TerraServerQueryProvider provider, Expression expression)
		{
			if (provider == null)
				throw new ArgumentNullException("provider");

			if (expression == null)
				throw new ArgumentNullException("expression");

			if (!typeof(IQueryable<TData>).IsAssignableFrom(expression.Type))
				throw new ArgumentOutOfRangeException("expression");

			Provider = provider;
			Expression = expression;
		}

		#endregion //CONSTRUCTORS

		#region IQueryable Members

		public Type ElementType
		{
			get { return typeof(TData); }
		}

		public Expression Expression { get; private set; }

		public IQueryProvider Provider { get; private set; }

		#endregion //IQueryable Members

		#region IENUMERABLE MEMBERS

		public IEnumerator<TData> GetEnumerator()
		{
			return Provider.Execute<IEnumerable<TData>>(Expression).GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return Provider.Execute<IEnumerable>(Expression).GetEnumerator();
		}

		#endregion //IENUMERABLE MEMBERS
	}
}
